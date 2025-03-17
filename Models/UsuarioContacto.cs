using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Videogames_Store.Models
{
    public class UsuarioContacto
    {
        public int UsuarioId { get; set; }

        public int DomicilioId { get; set; }

        public Usuario? Usuario { get; set; }

        public Contacto? Contacto { get; set; }
    }
}
