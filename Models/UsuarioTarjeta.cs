using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Videogames_Store.Models
{
    public class UsuarioTarjeta
    {
        public int UsuarioId { get; set; }

        public int TarjetaId { get; set; }

        public Usuario? Usuario { get; set; }

        public Tarjeta? Tarjeta { get; set; }
    }
}
