using FilmeOnline.Logica.Entidades;
using FilmeOnline.Logica.Dtos;
using FilmeOnline.Logica.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CSharpFunctionalExtensions;
using FilmeOnline.Logica.Utils;

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
            {
                return NotFound();
            }

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
            try
            {
                var nomeOuErro = ClienteNome.Criar(item.Nome);
                var emailOuErro = Email.Criar(item.Email);

                var result = Result.Combine(nomeOuErro, emailOuErro);

                if (result.IsFailure)
                {
                    return Error(result.Error);
                }

                if (_clienteRepositorio.RecuperarPorEmail(emailOuErro.Value) != null)
                {
                    return Error("Email já está em uso: " + item.Email);
                }

                var cliente = new Cliente(nomeOuErro.Value, emailOuErro.Value);

                _clienteRepositorio.Adicionar(cliente);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Atualizar(long id, [FromBody] AtualizarClienteDto item)
        {
            try
            {
                var nomeOuErro = ClienteNome.Criar(item.Nome);

                var result = Result.Combine(nomeOuErro);

                if (result.IsFailure)
                {
                    return Error(result.Error);
                }


                var cliente = _clienteRepositorio.RecuperarPorId(id);

                if (cliente == null)
                {
                    return Error("Id de cliente inválido: " + id);
                }

                cliente.Nome = nomeOuErro.Value;

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/filmes")]
        public IActionResult AlugarFilme(long id, [FromBody] long filmeId)
        {
            try
            {
                var filme = _filmeRepositorio.RecuperarPorId(filmeId);

                if (filme == null)
                {
                    return Error("Id de filme inválido: " + filmeId);
                }

                var cliente = _clienteRepositorio.RecuperarPorId(id);

                if (cliente == null)
                {
                    return Error("Id de cliente inválido: " + id);
                }

                if (cliente.Alugueis.Any(x => x.Filme.Id == filme.Id && !x.DataExpiracao.Expirou))
                {
                    return Error("O filme já foi comprado: " + filme.Nome);
                }

                cliente.AlugarFilme(filme);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/promover")]
        public IActionResult PromoverCliente(long id)
        {
            try
            {
                var cliente = _clienteRepositorio.RecuperarPorId(id);

                if (cliente == null)
                {
                    return Error("Id de cliente inválido: " + id);
                }

                if (cliente.Status.Avancado)
                {
                    return Error("Cliente já tem status Avançado");
                }

                bool successo = cliente.Promover();

                if (!successo)
                {
                    return Error("Não pode promover o cliente");
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
