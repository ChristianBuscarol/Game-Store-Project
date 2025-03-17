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

        public string Apellido { get; set; }

        public int Dni { get; set; }

        [Display(Name = "Fotografía del usuario")]
        public string Imagen { get; set; }

        public int? ContactoId { get; set; }

        public Contacto? Contacto { get; set; }
    }
}
