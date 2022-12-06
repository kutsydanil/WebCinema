using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaCore.Models;
using WebCinema.Models.IndexViewModels;

namespace WebCinema.Controllers
{
    public class ActorsController : Controller
    {
        private readonly CinemaContext _context;

        public ActorsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index(string? actorSurName, string? actorName, string? actorMiddleName)
        {
            IQueryable<Actors> actors = _context.Actors;
            if (!string.IsNullOrEmpty(actorName))
            {
                actors = actors.Where(p => p.Name!.Contains(actorName));
            }
            if (!string.IsNullOrEmpty(actorSurName))
            {
                actors = actors.Where(p => p.Surname!.Contains(actorSurName));
            }
            if (!string.IsNullOrEmpty(actorMiddleName))
            {
                actors = actors.Where(p => p.MiddleName!.Contains(actorMiddleName));
            }

            ActorsViewModel viewModel = new ActorsViewModel
            {
                ActorsList = await actors.ToListAsync(),
                ActorName = actorName,
                ActorMiddleName = actorMiddleName,
                ActorSurName = actorSurName
            };
            return View(viewModel);
        }


        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actors == null)
            {
                return NotFound();
            }

            return View(actors);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,MiddleName")] Actors actors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actors);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors.FindAsync(id);
            if (actors == null)
            {
                return NotFound();
            }
            return View(actors);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,MiddleName")] Actors actors)
        {
            if (id != actors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorsExists(actors.Id))
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
            return View(actors);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actors == null)
            {
                return NotFound();
            }

            return View(actors);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'CinemaContext.Actors'  is null.");
            }
            var actors = await _context.Actors.FindAsync(id);
            if (actors != null)
            {
                _context.Actors.Remove(actors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorsExists(int id)
        {
          return (_context.Actors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
