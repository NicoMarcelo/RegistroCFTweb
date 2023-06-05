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
    public class NotasController : Controller
    {
        private readonly RegistroCftContext _context;

        public NotasController(RegistroCftContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var registroCftContext = _context.Nota.Include(n => n.Asignatura).Include(n => n.Estudiante);
            return View(await registroCftContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notas = await _context.Nota
                .Include(n => n.Asignatura)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AsignaturaId,EstudianteId,Calificacion,Ponderacion")] Notas notas)
        {
            if (notas.Calificacion >=1 && notas.Ponderacion >= 1 && notas.EstudianteId != 0 && notas.AsignaturaId != 0)
            {
                _context.Nota.Add(notas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", notas.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", notas.EstudianteId);
            return View(notas);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notas = await _context.Nota.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", notas.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", notas.EstudianteId);
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AsignaturaId,EstudianteId,Calificacion,Ponderacion")] Notas notas)
        {
            if (id != notas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotasExists(notas.Id))
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
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", notas.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", notas.EstudianteId);
            return View(notas);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notas = await _context.Nota
                .Include(n => n.Asignatura)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nota == null)
            {
                return Problem("Entity set 'RegistroCftContext.Nota'  is null.");
            }
            var notas = await _context.Nota.FindAsync(id);
            if (notas != null)
            {
                _context.Nota.Remove(notas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotasExists(int id)
        {
          return (_context.Nota?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
