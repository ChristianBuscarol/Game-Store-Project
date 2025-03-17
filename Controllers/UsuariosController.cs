using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Videogames_Store.Data;
using Videogames_Store.Models;
using X.PagedList;

namespace Videogames_Store.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;
        public UsuariosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string userSearch, int userAtributeSelection, int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;
            var usuarios = from Usuario in _context.Usuarios select Usuario;
            //var appDBcontextual = _context.Usuarios.Select(a => a);

            if (!string.IsNullOrEmpty(userSearch))
            {
                if (userAtributeSelection == 1)
                {
                    usuarios = usuarios.Where(s => s.Nombre.Contains(userSearch));
                    //appDBcontextual = appDBcontextual.Where(a => a.Nombre.Contains(userSearch));
                }
                else if (userAtributeSelection == 2)
                {
                    usuarios = usuarios.Where(s => s.Apellido.Contains(userSearch));
                    //appDBcontextual = appDBcontextual.Where(a => a.Apellido.Contains(userSearch));
                }
                else if (userAtributeSelection == 3)
                {
                    usuarios = usuarios.Where(s => s.Dni.ToString().Contains(userSearch));
                }
            }

            var usuariosSeleccionadosPaginado = usuarios.OrderByDescending(p => p.Id).ToPagedList(pageNumber, pageSize);
            //var applicationDbContext = _context.Usuarios.Include(u => u.Contacto);
            //return View(await applicationDbContext.ToListAsync());
            //await usuarios.ToListAsync()
            return View(usuariosSeleccionadosPaginado);
        }

        // public async Task<IActionResult> Importar()
        public IActionResult Importar()
        {
            var archivo = HttpContext.Request.Form.Files;
            if (archivo != null && archivo.Count > 0)
            {
                var archivoCSV = archivo[0];

                if (archivoCSV.Length > 0)
                {
                    var destinyPath = Path.Combine(env.WebRootPath, "Importaciones");

                    // Se genera un nombre ramdom de números y letras para el archivo importado...
                    var archivoCSVFile = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoCSV.FileName);
                    string destinyRout = Path.Combine(destinyPath, archivoCSVFile);

                    using (var filestream = new FileStream(destinyRout, FileMode.Create))
                    {
                        archivoCSV.CopyTo(filestream);
                    };

                    using (var bazinga = new FileStream(destinyRout, FileMode.Open))
                    {
                        List<string> renglones = new List<string>();
                        List<Usuario> usuariosArch = new List<Usuario>();

                        StreamReader fileContent = new StreamReader(bazinga); // , System.Text.Encoding.Default
                        do
                        {
                            renglones.Add(fileContent.ReadLine());
                        } while (!fileContent.EndOfStream);

                        foreach (var renglon in renglones)
                        {
                            string[] data = renglon.Split(';');
                            if (data.Length == 4)
                            {
                                Usuario newUsuario = new Usuario()
                                {
                                    Nombre = data[0].Trim(),
                                    Apellido = data[1].Trim(),
                                    Dni = int.Parse(data[2].Trim()),
                                    Imagen = data[3].Trim()
                                };
                                usuariosArch.Add(newUsuario);
                            }
                        }

                        if (usuariosArch.Count > 0)
                        {
                            //_context.Add(usuariosArch);
                            //await _context.SaveChangesAsync();
                            _context.Usuarios.AddRange(usuariosArch);
                            _context.SaveChanges();
                        }
                    }
                }
            }
            //var applicationDbContext = _context.Usuarios.Include(u => u.Contacto);
            //return View("Index", await applicationDbContext.ToListAsync());

            return View();
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Contacto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "Id", "Id");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Imagen,ContactoId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                if(archivo != null && archivo.Count > 0)
                {
                    var userFoto = archivo[0];
                    
                    if(userFoto.Length > 0)
                    {
                        var destinyPath = Path.Combine(env.WebRootPath, "UserFotos");

                        // Se genera un nombre ramdom de números y letras para la imagen...
                        var fotoFile = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(userFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(destinyPath, fotoFile), FileMode.Create))
                        {
                            userFoto.CopyTo(filestream);
                            usuario.Imagen = fotoFile;
                        }
                    }
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "Id", "Id", usuario.ContactoId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "Id", "Id", usuario.ContactoId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Dni,Imagen,ContactoId")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                if (archivo != null && archivo.Count > 0)
                {
                    var userFoto = archivo[0];

                    if (userFoto.Length > 0)
                    {
                        var destinyPath = Path.Combine(env.WebRootPath, "UserFotos");

                        // Se genera un nombre ramdom de números y letras para la imagen...
                        var fotoFile = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(userFoto.FileName);

                        if(!string.IsNullOrEmpty(usuario.Imagen))
                        {
                            string userPreviousPhoto = Path.Combine(destinyPath, usuario.Imagen);
                            if (System.IO.File.Exists(userPreviousPhoto))
                            {
                                System.IO.File.Delete(userPreviousPhoto);
                            }
                        }
                        
                        using (var filestream = new FileStream(Path.Combine(destinyPath, fotoFile), FileMode.Create))
                        {
                            userFoto.CopyTo(filestream);
                            usuario.Imagen = fotoFile;
                        }
                    }
                }

                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "Id", "Id", usuario.ContactoId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Contacto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
