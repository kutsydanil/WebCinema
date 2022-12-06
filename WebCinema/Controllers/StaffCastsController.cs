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
    public class StaffCastsController : Controller
    {
        private readonly CinemaContext _context;

        public StaffCastsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: StaffCasts
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.StaffCasts.Include(s => s.ListEvent).Include(s => s.Staff);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: StaffCasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StaffCasts == null)
            {
                return NotFound();
            }

            var staffCasts = await _context.StaffCasts
                .Include(s => s.ListEvent)
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffCasts == null)
            {
                return NotFound();
            }

            return View(staffCasts);
        }

        // GET: StaffCasts/Create
        public IActionResult Create()
        {
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "MiddleName");
            return View();
        }

        // POST: StaffCasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffId,ListEventId")] StaffCasts staffCasts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffCasts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name", staffCasts.ListEventId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "MiddleName", staffCasts.StaffId);
            return View(staffCasts);
        }

        // GET: StaffCasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StaffCasts == null)
            {
                return NotFound();
            }

            var staffCasts = await _context.StaffCasts.FindAsync(id);
            if (staffCasts == null)
            {
                return NotFound();
            }
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name", staffCasts.ListEventId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "MiddleName", staffCasts.StaffId);
            return View(staffCasts);
        }

        // POST: StaffCasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StaffId,ListEventId")] StaffCasts staffCasts)
        {
            if (id != staffCasts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffCasts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffCastsExists(staffCasts.Id))
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
            ViewData["ListEventId"] = new SelectList(_context.ListEvents, "Id", "Name", staffCasts.ListEventId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "MiddleName", staffCasts.StaffId);
            return View(staffCasts);
        }

        // GET: StaffCasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StaffCasts == null)
            {
                return NotFound();
            }

            var staffCasts = await _context.StaffCasts
                .Include(s => s.ListEvent)
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffCasts == null)
            {
                return NotFound();
            }

            return View(staffCasts);
        }

        // POST: StaffCasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StaffCasts == null)
            {
                return Problem("Entity set 'CinemaContext.StaffCasts'  is null.");
            }
            var staffCasts = await _context.StaffCasts.FindAsync(id);
            if (staffCasts != null)
            {
                _context.StaffCasts.Remove(staffCasts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffCastsExists(int id)
        {
          return (_context.StaffCasts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
