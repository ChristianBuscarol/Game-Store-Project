using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Videogames_Store.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Fotografía del usuario")]
        public string Imagen { get; set; }

        public int? ResidenciaId { get; set; }

        public Residencia? Residencia { get; set; }
    }
}
