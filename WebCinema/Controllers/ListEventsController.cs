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
    public class ListEventsController : Controller
    {
        private readonly CinemaContext _context;

        public ListEventsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: ListEvents
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.ListEvents.Include(l => l.Film);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: ListEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListEvents == null)
            {
                return NotFound();
            }

            var listEvents = await _context.ListEvents
                .Include(l => l.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listEvents == null)
            {
                return NotFound();
            }

            return View(listEvents);
        }

        // GET: ListEvents/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description");
            return View();
        }

        // POST: ListEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,StartTime,EndTime,TicketPrice,FilmId")] ListEvents listEvents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description", listEvents.FilmId);
            return View(listEvents);
        }

        // GET: ListEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListEvents == null)
            {
                return NotFound();
            }

            var listEvents = await _context.ListEvents.FindAsync(id);
            if (listEvents == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description", listEvents.FilmId);
            return View(listEvents);
        }

        // POST: ListEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,StartTime,EndTime,TicketPrice,FilmId")] ListEvents listEvents)
        {
            if (id != listEvents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListEventsExists(listEvents.Id))
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
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description", listEvents.FilmId);
            return View(listEvents);
        }

        // GET: ListEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListEvents == null)
            {
                return NotFound();
            }

            var listEvents = await _context.ListEvents
                .Include(l => l.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listEvents == null)
            {
                return NotFound();
            }

            return View(listEvents);
        }

        // POST: ListEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListEvents == null)
            {
                return Problem("Entity set 'CinemaContext.ListEvents'  is null.");
            }
            var listEvents = await _context.ListEvents.FindAsync(id);
            if (listEvents != null)
            {
                _context.ListEvents.Remove(listEvents);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListEventsExists(int id)
        {
          return (_context.ListEvents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
