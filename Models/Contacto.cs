using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Videogames_Store.Models
{
    public class Contacto
    {
        public int Id { get; set; }

        public int CódigoArea { get; set; }

        public int NúmeroTeléfono { get; set; }

        public string Email { get; set; }

        public List<Usuario>? Usuarios { get; set; }
    }
}
