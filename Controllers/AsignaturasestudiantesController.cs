using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegistroCFTweb.Models;

namespace RegistroCFTweb.Controllers
{
    public class AsignaturasestudiantesController : Controller
    {
        private readonly RegistroCftContext _context;

        public AsignaturasestudiantesController(RegistroCftContext context)
        {
            _context = context;
        }

        // GET: Asignaturasestudiantes
        public async Task<IActionResult> Index()
        {
            var registroCftContext = _context.Asignaturasestudiantes.Include(a => a.Asignatura).Include(a => a.Estudiante);
            return View(await registroCftContext.ToListAsync());
        }

        // GET: Asignaturasestudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asignaturasestudiantes == null)
            {
                return NotFound();
            }

            var asignaturasestudiante = await _context.Asignaturasestudiantes
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturasestudiante == null)
            {
                return NotFound();
            }

            return View(asignaturasestudiante);
        }

        // GET: Asignaturasestudiantes/Create
        public IActionResult Create()
        {
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Asignaturasestudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstudianteId,AsignaturaId,FechaRegistro")] Asignaturasestudiante asignaturasestudiante)
        {
            if (asignaturasestudiante.EstudianteId != 0 && asignaturasestudiante.AsignaturaId != 0)
            {
                _context.Asignaturasestudiantes.Add(asignaturasestudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasestudiante.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasestudiante.EstudianteId);
            return View(asignaturasestudiante);
        }

        // GET: Asignaturasestudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asignaturasestudiantes == null)
            {
                return NotFound();
            }

            var asignaturasestudiante = await _context.Asignaturasestudiantes.FindAsync(id);
            if (asignaturasestudiante == null)
            {
                return NotFound();
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasestudiante.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasestudiante.EstudianteId);
            return View(asignaturasestudiante);
        }

        // POST: Asignaturasestudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstudianteId,AsignaturaId,FechaRegistro")] Asignaturasestudiante asignaturasestudiante)
        {
            if (id != asignaturasestudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturasestudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturasestudianteExists(asignaturasestudiante.Id))
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
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturasestudiante.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturasestudiante.EstudianteId);
            return View(asignaturasestudiante);
        }

        // GET: Asignaturasestudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asignaturasestudiantes == null)
            {
                return NotFound();
            }

            var asignaturasestudiante = await _context.Asignaturasestudiantes
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturasestudiante == null)
            {
                return NotFound();
            }

            return View(asignaturasestudiante);
        }

        // POST: Asignaturasestudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asignaturasestudiantes == null)
            {
                return Problem("Entity set 'RegistroCftContext.Asignaturasestudiantes'  is null.");
            }
            var asignaturasestudiante = await _context.Asignaturasestudiantes.FindAsync(id);
            if (asignaturasestudiante != null)
            {
                _context.Asignaturasestudiantes.Remove(asignaturasestudiante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturasestudianteExists(int id)
        {
          return (_context.Asignaturasestudiantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
