using FilmeOnline.Entidades;
using FilmeOnline.Repositorios;
using FilmeOnline.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FilmeOnline.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly FilmeRepositorio _filmeRepositorio;
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly ClienteServico _clienteServico;

        public ClientesController(FilmeRepositorio filmeRepositorio, ClienteRepositorio clienteRepositorio, ClienteServico clienteServico)
        {
            _clienteRepositorio = clienteRepositorio;
            _filmeRepositorio = filmeRepositorio;
            _clienteServico = clienteServico;
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

            return Json(cliente);
        }

        [HttpGet]
        public JsonResult GetLista()
        {
            var clientes = _clienteRepositorio.RecuperarLista();
            return Json(clientes);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Cliente item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_clienteRepositorio.RecuperarPorEmail(item.Email) != null)
                {
                    return BadRequest("Email já está em uso: " + item.Email);
                }

                item.Id = 0;
                item.Status = ClienteStatus.Normal;
                _clienteRepositorio.Adicionar(item);
                _clienteRepositorio.Commitar();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Atualizar(long id, [FromBody] Cliente item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cliente = _clienteRepositorio.RecuperarPorId(id);

                if (cliente == null)
                {
                    return BadRequest("Id de cliente inválido: " + id);
                }

                cliente.Nome = item.Nome;
                _clienteRepositorio.Commitar();

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
                    return BadRequest("Id de filme inválido: " + filmeId);
                }

                var cliente = _clienteRepositorio.RecuperarPorId(id);

                if (cliente == null)
                {
                    return BadRequest("Id de cliente inválido: " + id);
                }

                if (cliente.Alugueis.Any(x => x.FilmeId == filme.Id && (x.DataExpiracao == null || x.DataExpiracao.Value >= DateTime.UtcNow)))
                {
                    return BadRequest("O filme já foi comprado: " + filme.Nome);
                }

                _clienteServico.AlugarFilme(cliente, filme);

                _clienteRepositorio.Commitar();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/promocao")]
        public IActionResult PromoverCliente(long id)
        {
            try
            {
                var cliente = _clienteRepositorio.RecuperarPorId(id);

                if (cliente == null)
                {
                    return BadRequest("Id de cliente inválido: " + id);
                }

                if (cliente.Status == ClienteStatus.Avancado && (cliente.DataExpiracaoStatus == null || cliente.DataExpiracaoStatus.Value < DateTime.UtcNow))
                {
                    return BadRequest("Cliente já tem status Avançado");
                }

                bool successo = _clienteServico.PromoverCliente(cliente);

                if (!successo)
                {
                    return BadRequest("Não pode promover o cliente");
                }

                _clienteRepositorio.Commitar();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
