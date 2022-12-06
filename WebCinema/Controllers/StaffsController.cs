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
    public class StaffsController : Controller
    {
        private readonly CinemaContext _context;

        public StaffsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
              return _context.Staffs != null ? 
                          View(await _context.Staffs.ToListAsync()) :
                          Problem("Entity set 'CinemaContext.Staffs'  is null.");
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,MiddleName,Post,WorkExperience")] Staffs staffs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffs);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs == null)
            {
                return NotFound();
            }
            return View(staffs);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,MiddleName,Post,WorkExperience")] Staffs staffs)
        {
            if (id != staffs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffsExists(staffs.Id))
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
            return View(staffs);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'CinemaContext.Staffs'  is null.");
            }
            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs != null)
            {
                _context.Staffs.Remove(staffs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffsExists(int id)
        {
          return (_context.Staffs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
