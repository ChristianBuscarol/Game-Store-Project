using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Videogames_Store.Models;

namespace Videogames_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsuarioContacto>().HasKey(x => new { x.UsuarioId, x.DomicilioId });
            modelBuilder.Entity<UsuarioTarjeta>().HasKey(x => new { x.UsuarioId, x.TarjetaId });
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Contacto> Contactos { get; set; }

        public DbSet<Tarjeta> Tarjetas { get; set; }

        public DbSet<UsuarioContacto> UsuariosContactos { get; set; }

        public DbSet<UsuarioTarjeta> UsuariosTarjetas { get; set; }

        public DbSet<Videojuego> Videojuegos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
