using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Controllers
{
    public class ParkingsEditController : Controller
    {
        private readonly DbContext _context;

        public ParkingsEditController(DbContext context)
        {
            _context = context;
        }

        // GET: ParkingsEdit
        public async Task<IActionResult> Index()
        {
              return _context.Parking != null ? 
                          View(await _context.Parking.ToListAsync()) :
                          Problem("Entity set 'DbContext.Parking'  is null.");
        }

        // GET: ParkingsEdit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parking == null)
            {
                return NotFound();
            }

            var parking = await _context.Parking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        // GET: ParkingsEdit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkingsEdit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrandName,Color,ModelName,CarNumber")] Parking parking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parking);
        }

        // GET: ParkingsEdit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parking == null)
            {
                return NotFound();
            }

            var parking = await _context.Parking.FindAsync(id);
            if (parking == null)
            {
                return NotFound();
            }
            return View(parking);
        }

        // POST: ParkingsEdit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrandName,Color,ModelName,CarNumber")] Parking parking)
        {
            if (id != parking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingExists(parking.Id))
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
            return View(parking);
        }

        // GET: ParkingsEdit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parking == null)
            {
                return NotFound();
            }

            var parking = await _context.Parking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        // POST: ParkingsEdit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parking == null)
            {
                return Problem("Entity set 'DbContext.Parking'  is null.");
            }
            var parking = await _context.Parking.FindAsync(id);
            if (parking != null)
            {
                _context.Parking.Remove(parking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingExists(int id)
        {
          return (_context.Parking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
