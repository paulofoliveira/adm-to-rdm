using CSharpFunctionalExtensions;
using FilmeOnline.Logica.Dtos;
using FilmeOnline.Logica.Entidades;
using FilmeOnline.Logica.Repositorios;
using FilmeOnline.Logica.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmeOnline.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : BaseController
    {
        private readonly FilmeRepositorio _filmeRepositorio;
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClientesController(UnitOfWork uow, FilmeRepositorio filmeRepositorio, ClienteRepositorio clienteRepositorio)
            : base(uow)
        {
            _clienteRepositorio = clienteRepositorio;
            _filmeRepositorio = filmeRepositorio;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Recuperar(long id)
        {
            var cliente = _clienteRepositorio.RecuperarPorId(id);

            if (cliente == null)
                return NotFound();

            var dto = new ClienteDto()
            {
                Id = cliente.Id,
                Nome = cliente.Nome.Value,
                Email = cliente.Email.Value,
                ValorGasto = cliente.ValorGasto,
                Status = cliente.Status.Tipo.ToString(),
                DataExpiracaoStatus = cliente.Status.DataExpiracao,
                Alugueis = cliente.Alugueis.Select(p => new AluguelDto()
                {
                    Valor = p.Valor,
                    DataExpiracao = p.DataExpiracao,
                    DataAluguel = p.DataAluguel,
                    Filme = new FilmeDto()
                    {
                        Id = p.Filme.Id,
                        Nome = p.Filme.Nome
                    }
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet]
        public IActionResult GetLista()
        {
            var clientes = _clienteRepositorio.RecuperarLista();

            var dto = clientes.Select(p => new ClienteListaDto()
            {
                Id = p.Id,
                Nome = p.Nome.Value,
                Email = p.Email.Value,
                ValorGasto = p.ValorGasto,
                Status = p.Status.ToString(),
                DataExpiracaoStatus = p.Status.DataExpiracao
            }).ToList();

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CriarClienteDto item)
        {
            var nomeOuErro = ClienteNome.Criar(item.Nome);
            var emailOuErro = Email.Criar(item.Email);

            var result = Result.Combine(nomeOuErro, emailOuErro);

            if (result.IsFailure)
                return Error(result.Error);

            if (_clienteRepositorio.RecuperarPorEmail(emailOuErro.Value) != null)
                return Error("Email já está em uso: " + item.Email);

            var cliente = new Cliente(nomeOuErro.Value, emailOuErro.Value);

            _clienteRepositorio.Adicionar(cliente);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Atualizar(long id, [FromBody] AtualizarClienteDto item)
        {
            var nomeOuErro = ClienteNome.Criar(item.Nome);

            var result = Result.Combine(nomeOuErro);

            if (result.IsFailure)
                return Error(result.Error);

            var cliente = _clienteRepositorio.RecuperarPorId(id);

            if (cliente == null)
                return Error("Id de cliente inválido: " + id);

            cliente.Nome = nomeOuErro.Value;

            return Ok();
        }

        [HttpPost]
        [Route("{id}/filmes")]
        public IActionResult AlugarFilme(long id, [FromBody] long filmeId)
        {
            var filme = _filmeRepositorio.RecuperarPorId(filmeId);

            if (filme == null)
                return Error("Id de filme inválido: " + filmeId);

            var cliente = _clienteRepositorio.RecuperarPorId(id);

            if (cliente == null)
                return Error("Id de cliente inválido: " + id);

            if (cliente.TemFilmeAlugado(filme))
                return Error("O filme já foi comprado: " + filme.Nome);

            cliente.AlugarFilme(filme);

            return Ok();
        }

        [HttpPost]
        [Route("{id}/promover")]
        public IActionResult PromoverCliente(long id)
        {
            var cliente = _clienteRepositorio.RecuperarPorId(id);

            if (cliente == null)
                return Error("Id de cliente inválido: " + id);

            // A ideia do PodePromover segue o princípio do CQS (Command Query Segregation) que:
            // Define que em um método (No caso do Promover), não podemos ter consulta e mudança de estado. Assim temos a segregação de código para dois métodos.
            // A ação da mudança permanece no Promover. A checagem com returno via Result class é feita em um método que faz a validação.

            var podePromoverCheck = cliente.PodePromover();

            if (podePromoverCheck.IsFailure)
                return Error(podePromoverCheck.Error);

            cliente.Promover();

            return Ok();
        }
    }
}
