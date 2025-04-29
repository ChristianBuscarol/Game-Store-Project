using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Videogames_Store.Data;
using Videogames_Store.Models;

namespace Videogames_Store.Controllers
{
    public class ResidenciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Residencias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Residencias.ToListAsync());
        }

        // GET: Residencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residencia = await _context.Residencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residencia == null)
            {
                return NotFound();
            }

            return View(residencia);
        }

        // GET: Residencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Residencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreProvincia,NombreCiudad")] Residencia residencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(residencia);
        }

        // GET: Residencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residencia = await _context.Residencias.FindAsync(id);
            if (residencia == null)
            {
                return NotFound();
            }
            return View(residencia);
        }

        // POST: Residencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreProvincia,NombreCiudad")] Residencia residencia)
        {
            if (id != residencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenciaExists(residencia.Id))
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
            return View(residencia);
        }

        // GET: Residencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residencia = await _context.Residencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residencia == null)
            {
                return NotFound();
            }

            return View(residencia);
        }

        // POST: Residencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residencia = await _context.Residencias.FindAsync(id);
            _context.Residencias.Remove(residencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenciaExists(int id)
        {
            return _context.Residencias.Any(e => e.Id == id);
        }
    }
}
