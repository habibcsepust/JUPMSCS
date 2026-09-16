using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HallManagement.Model.Entities;

namespace HallManagement.Web.Controllers
{
    public class ReligionController : BaseController
    {
        private readonly BangamataHallContext _context;

        public ReligionController(BangamataHallContext context)
        {
            _context = context;
        }

        // GET: Religion
        public async Task<IActionResult> Index()
        {
              return _context.Religions != null ? 
                          View(await _context.Religions.ToListAsync()) :
                          Problem("Entity set 'BangamataHallContext.Religions'  is null.");
        }

        // GET: Religion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Religions == null)
            {
                return NotFound();
            }

            var religion = await _context.Religions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (religion == null)
            {
                return NotFound();
            }

            return View(religion);
        }

        // GET: Religion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Religion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Religion religion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(religion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(religion);
        }

        // GET: Religion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Religions == null)
            {
                return NotFound();
            }

            var religion = await _context.Religions.FindAsync(id);
            if (religion == null)
            {
                return NotFound();
            }
            return View(religion);
        }

        // POST: Religion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Religion religion)
        {
            if (id != religion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(religion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReligionExists(religion.Id))
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
            return View(religion);
        }

        // GET: Religion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Religions == null)
            {
                return NotFound();
            }

            var religion = await _context.Religions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (religion == null)
            {
                return NotFound();
            }

            return View(religion);
        }

        // POST: Religion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Religions == null)
            {
                return Problem("Entity set 'BangamataHallContext.Religions'  is null.");
            }
            var religion = await _context.Religions.FindAsync(id);
            if (religion != null)
            {
                _context.Religions.Remove(religion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReligionExists(int id)
        {
          return (_context.Religions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
