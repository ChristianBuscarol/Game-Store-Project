using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Videogames_Store.Models
{
    public class Videojuego
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Nombre { get; set; }

        public string Imagen { get; set; }

        public string Descripción { get; set; }

        [Required]
        [Range(0,1000000)]
        public float Precio { get; set; }

        [Display(Name = "Año de lanzamiento")]
        [Range(1980, 2025)]
        public int AñoLanzamiento { get; set; }

        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}
