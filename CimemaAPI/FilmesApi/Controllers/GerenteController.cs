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
    public class GerenteController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaGerente([FromBody] CreateGerenteDto dto)
        {
            Gerente gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaGerentePorId), new { Id = gerente.Id }, gerente);
        }

        [HttpGet]
        public IEnumerable<Gerente> RecuperaGerentes()
        {
            return _context.Gerentes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            
            if (gerente == null)
            {
                return NotFound();
            }

            ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
            return Ok(gerenteDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaGerente(int id, [FromBody] UpdateGerenteDto dto)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, gerente);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente == null)
            {
                return NotFound();
            }

            _context.Remove(gerente);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
