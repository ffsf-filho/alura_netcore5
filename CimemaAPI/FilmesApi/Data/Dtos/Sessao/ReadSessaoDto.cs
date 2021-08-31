using FilmesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Data.Dtos
{
    public class ReadSessaoDto
    {
        public int Id { get; set; }

        public Cinema Cinema { get; set; }

        public Filme Filme { get; set; }

        public DateTime HorarioDeEncerramento { get; set; }
        
        public DateTime HoracioDeinicio { get; set; }
    }
}
