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
    public class ActorCastsController : Controller
    {
        private readonly CinemaContext _context;

        public ActorCastsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: ActorCasts
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.ActorCasts.Include(a => a.Actor).Include(a => a.Film);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: ActorCasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActorCasts == null)
            {
                return NotFound();
            }

            var actorCasts = await _context.ActorCasts
                .Include(a => a.Actor)
                .Include(a => a.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorCasts == null)
            {
                return NotFound();
            }

            return View(actorCasts);
        }

        // GET: ActorCasts/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "MiddleName");
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description");
            return View();
        }

        // POST: ActorCasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorId,FilmId")] ActorCasts actorCasts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorCasts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "MiddleName", actorCasts.ActorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description", actorCasts.FilmId);
            return View(actorCasts);
        }

        // GET: ActorCasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActorCasts == null)
            {
                return NotFound();
            }

            var actorCasts = await _context.ActorCasts.FindAsync(id);
            if (actorCasts == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "MiddleName", actorCasts.ActorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description", actorCasts.FilmId);
            return View(actorCasts);
        }

        // POST: ActorCasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActorId,FilmId")] ActorCasts actorCasts)
        {
            if (id != actorCasts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorCasts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorCastsExists(actorCasts.Id))
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
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "MiddleName", actorCasts.ActorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Description", actorCasts.FilmId);
            return View(actorCasts);
        }

        // GET: ActorCasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActorCasts == null)
            {
                return NotFound();
            }

            var actorCasts = await _context.ActorCasts
                .Include(a => a.Actor)
                .Include(a => a.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorCasts == null)
            {
                return NotFound();
            }

            return View(actorCasts);
        }

        // POST: ActorCasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActorCasts == null)
            {
                return Problem("Entity set 'CinemaContext.ActorCasts'  is null.");
            }
            var actorCasts = await _context.ActorCasts.FindAsync(id);
            if (actorCasts != null)
            {
                _context.ActorCasts.Remove(actorCasts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorCastsExists(int id)
        {
          return (_context.ActorCasts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
