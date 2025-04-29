using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Videogames_Store.Data;
using Videogames_Store.Models;
using X.PagedList;

namespace Videogames_Store.Controllers
{
    public class VideojuegosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;
        public VideojuegosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Videojuegos
        public async Task<IActionResult> Index(string videoGameSearch, int videogamesAtributeSelection, int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;
            var videojuegos = from Videojuego in _context.Videojuegos select Videojuego;
            var categorias = from Categoria in _context.Categorias select Categoria;

            if (!string.IsNullOrEmpty(videoGameSearch))
            {
                if (videogamesAtributeSelection == 1)
                {
                    videojuegos = videojuegos.Where(s => s.Nombre.Contains(videoGameSearch));
                }
                else if (videogamesAtributeSelection == 2)
                {
                    videojuegos = videojuegos.Where(s => s.Precio.ToString().Contains(videoGameSearch));
                }
                else if (videogamesAtributeSelection == 3)
                {
                    videojuegos = videojuegos.Where(s => s.AñoLanzamiento.ToString().Contains(videoGameSearch));
                }
                else if (videogamesAtributeSelection == 4)
                {
                    videojuegos = videojuegos.Where(s => s.Categoria.Nombre.Contains(videoGameSearch));
                }
            }

            var videojuegosSeleccionadosPaginado = videojuegos
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.Id)
                .ToPagedList(pageNumber, pageSize);
            return View(videojuegosSeleccionadosPaginado);
        }

        // GET: Videojuegos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuegos
                .Include(v => v.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videojuego == null)
            {
                return NotFound();
            }

            return View(videojuego);
        }

        [Authorize]
        // GET: Videojuegos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        // POST: Videojuegos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Imagen,Descripción,Precio,AñoLanzamiento,CategoriaId")] Videojuego videojuego)
        {
            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                if(archivo != null && archivo.Count > 0)
                {
                    var gameImage = archivo[0];

                    if(gameImage.Length > 0)
                    {
                        var destinyPath = Path.Combine(env.WebRootPath, "GamesImages");

                        // Se genera un nombre ramdom de números y letras para la imagen...
                        var imageFile = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(gameImage.FileName);

                        using (var fileStream = new FileStream(Path.Combine(destinyPath, imageFile), FileMode.Create))
                        {
                            gameImage.CopyTo(fileStream);
                            videojuego.Imagen = imageFile;
                        }
                    }
                }

                _context.Add(videojuego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Id", videojuego.CategoriaId);
            return View(videojuego);
        }

        // GET: Videojuegos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuegos.FindAsync(id);
            if (videojuego == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", videojuego.CategoriaId);
            return View(videojuego);
        }

        // POST: Videojuegos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Imagen,Descripción,Precio,AñoLanzamiento,CategoriaId")] Videojuego videojuego)
        {
            if (id != videojuego.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                if(archivo != null && archivo.Count > 0)
                {
                    var gameImage = archivo[0];

                    if(gameImage.Length > 0)
                    {
                        var destinyPath = Path.Combine(env.WebRootPath, "GamesImages");

                        // Se genera un nombre ramdom de números y letras para la imagen...
                        var imageFile = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(gameImage.FileName);

                        if(!string.IsNullOrEmpty(videojuego.Imagen))
                        {
                            string gamePreviousImage = Path.Combine(destinyPath, videojuego.Imagen);

                            if(System.IO.File.Exists(gamePreviousImage))
                            {
                                System.IO.File.Delete(gamePreviousImage);
                            }
                        }

                        using (var filestream = new FileStream(Path.Combine(destinyPath, imageFile), FileMode.Create))
                        {
                            gameImage.CopyTo(filestream);
                            videojuego.Imagen = imageFile;
                        }
                    }
                }

                try
                {
                    _context.Update(videojuego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideojuegoExists(videojuego.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Id", videojuego.CategoriaId);
            return View(videojuego);
        }

        // GET: Videojuegos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuegos
                .Include(v => v.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videojuego == null)
            {
                return NotFound();
            }

            return View(videojuego);
        }

        // POST: Videojuegos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videojuego = await _context.Videojuegos.FindAsync(id);
            _context.Videojuegos.Remove(videojuego);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideojuegoExists(int id)
        {
            return _context.Videojuegos.Any(e => e.Id == id);
        }
    }
}
