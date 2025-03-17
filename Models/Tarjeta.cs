using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Videogames_Store.Models
{
    public class Tarjeta
    {
        public int Id { get; set; }

        public int Clave { get; set; }

        public string Marca { get; set; }

        public string NombreBanco { get; set; }

        public List<Usuario>? Usuarios { get; set; }
    }
}
