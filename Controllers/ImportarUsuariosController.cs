using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Videogames_Store.Data;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Videogames_Store.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Videogames_Store.Controllers
{
    public class ImportarUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;

        public ImportarUsuariosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult importarUsuariosExcel(IFormFile excelImport)
        {
            try
            {
                var archivoCSV = excelImport;
                var destinyPath = Path.Combine(env.WebRootPath, "ImportacionUsuarios");

                // Se genera un nombre ramdom de números y letras para la imagen...
                var archivoCSVFile = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoCSV.FileName);
                string destinyRout = Path.Combine(destinyPath, archivoCSVFile);

                using (var filestream = new FileStream(destinyRout, FileMode.Create))
                {
                    archivoCSV.CopyTo(filestream);
                };


                var workbook = new XLWorkbook(excelImport.OpenReadStream());
                var hoja = workbook.Worksheet(1);
                var primeraFila = hoja.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
                var ultimaFila = hoja.LastRowUsed().RangeAddress.LastAddress.RowNumber;

                List<Usuario> usuarios = new List<Usuario>();
                for (int i = primeraFila; i <= ultimaFila; i++)
                {
                    var fila = hoja.Row(i);
                    Usuario usuario = new Usuario();
                    usuario.Nombre = fila.Cell(1).GetString();
                    usuario.Apellido = fila.Cell(2).GetValue<string>();
                    usuario.Email = fila.Cell(3).GetValue<string>();
                    usuario.Imagen = fila.Cell(4).GetValue<string>();
                    usuarios.Add(usuario);
                }
                _context.Usuarios.AddRange(usuarios);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Index", "Usuarios");
        }
    }
}
