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
    public class PlacesController : Controller
    {
        private readonly CinemaContext _context;

        public PlacesController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Places
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.Places.Include(p => p.CinemaHall).Include(p => p.ListEvent);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Places == null)
            {
                return NotFound();
            }

            var places = await _context.Places
                .Include(p => p.CinemaHall)
                .Include(p => p.ListEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }

            return View(places);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHalls, "Id", "Id");
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name");
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ListEventId,CinemaHallId,PlaceNumber,TakenSeat")] Places places)
        {
            if (ModelState.IsValid)
            {
                _context.Add(places);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHalls, "Id", "Id", places.CinemaHallId);
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name", places.ListEventId);
            return View(places);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Places == null)
            {
                return NotFound();
            }

            var places = await _context.Places.FindAsync(id);
            if (places == null)
            {
                return NotFound();
            }
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHalls, "Id", "Id", places.CinemaHallId);
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name", places.ListEventId);
            return View(places);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ListEventId,CinemaHallId,PlaceNumber,TakenSeat")] Places places)
        {
            if (id != places.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(places);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacesExists(places.Id))
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
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHalls, "Id", "Id", places.CinemaHallId);
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name", places.ListEventId);
            return View(places);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Places == null)
            {
                return NotFound();
            }

            var places = await _context.Places
                .Include(p => p.CinemaHall)
                .Include(p => p.ListEvent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }

            return View(places);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Places == null)
            {
                return Problem("Entity set 'CinemaContext.Places'  is null.");
            }
            var places = await _context.Places.FindAsync(id);
            if (places != null)
            {
                _context.Places.Remove(places);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacesExists(int id)
        {
          return (_context.Places?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
