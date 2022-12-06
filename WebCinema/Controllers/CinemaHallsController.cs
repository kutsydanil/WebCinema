using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaCore.Models;
using WebCinema;

namespace WebCinema.Controllers
{
    public class CinemaHallsController : Controller
    {
        private readonly CinemaContext _context;

        public CinemaHallsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: CinemaHalls
        public async Task<IActionResult> Index()
        {
              return _context.CinemaHalls != null ? 
                          View(await _context.CinemaHalls.ToListAsync()) :
                          Problem("Entity set 'CinemaContext.CinemaHalls'  is null.");
        }

        // GET: CinemaHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CinemaHalls == null)
            {
                return NotFound();
            }

            var cinemaHalls = await _context.CinemaHalls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaHalls == null)
            {
                return NotFound();
            }

            return View(cinemaHalls);
        }

        // GET: CinemaHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CinemaHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HallNumber,MaxPlaceNumber")] CinemaHalls cinemaHalls)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinemaHalls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinemaHalls);
        }

        // GET: CinemaHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CinemaHalls == null)
            {
                return NotFound();
            }

            var cinemaHalls = await _context.CinemaHalls.FindAsync(id);
            if (cinemaHalls == null)
            {
                return NotFound();
            }
            return View(cinemaHalls);
        }

        // POST: CinemaHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HallNumber,MaxPlaceNumber")] CinemaHalls cinemaHalls)
        {
            if (id != cinemaHalls.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinemaHalls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaHallsExists(cinemaHalls.Id))
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
            return View(cinemaHalls);
        }

        // GET: CinemaHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CinemaHalls == null)
            {
                return NotFound();
            }

            var cinemaHalls = await _context.CinemaHalls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaHalls == null)
            {
                return NotFound();
            }

            return View(cinemaHalls);
        }

        // POST: CinemaHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CinemaHalls == null)
            {
                return Problem("Entity set 'CinemaContext.CinemaHalls'  is null.");
            }
            var cinemaHalls = await _context.CinemaHalls.FindAsync(id);
            if (cinemaHalls != null)
            {
                _context.CinemaHalls.Remove(cinemaHalls);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaHallsExists(int id)
        {
          return (_context.CinemaHalls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
