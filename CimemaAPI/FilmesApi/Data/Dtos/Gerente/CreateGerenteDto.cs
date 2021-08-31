using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Data.Dtos
{
    public class CreateGerenteDto
    {
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
    }
}
