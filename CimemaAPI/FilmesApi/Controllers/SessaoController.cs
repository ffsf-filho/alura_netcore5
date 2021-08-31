using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SessaoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaSessao([FromBody] CreateSessaoDto dto)
        {
            Sessao sessao = _mapper.Map<Sessao>(dto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessaoPorId), new { Id = sessao.Id }, sessao);
        }

        [HttpGet]
        public IEnumerable<Sessao> RecuperaSessao()
        {
            return _context.Sessoes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaSessaoPorId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);

            if (sessao == null)
            {
                return NotFound();
            }

            ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
            return Ok(sessaoDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaSessao(int id, [FromBody] UpdateSessaoDto dto)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);

            if (sessao == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, sessao);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSessao(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);

            if (sessao == null)
            {
                return NotFound();
            }

            _context.Remove(sessao);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
