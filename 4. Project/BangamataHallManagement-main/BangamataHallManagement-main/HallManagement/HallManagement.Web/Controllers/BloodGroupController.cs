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
    public class BloodGroupController : BaseController
    {
        private readonly BangamataHallContext _context;

        public BloodGroupController(BangamataHallContext context)
        {
            _context = context;
        }

        // GET: BloodGroup
        public async Task<IActionResult> Index()
        {
              return _context.BloodGroups != null ? 
                          View(await _context.BloodGroups.ToListAsync()) :
                          Problem("Entity set 'BangamataHallContext.BloodGroups'  is null.");
        }

        // GET: BloodGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BloodGroups == null)
            {
                return NotFound();
            }

            var bloodGroup = await _context.BloodGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodGroup == null)
            {
                return NotFound();
            }

            return View(bloodGroup);
        }

        // GET: BloodGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BloodGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BloodGroup bloodGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloodGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodGroup);
        }

        // GET: BloodGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BloodGroups == null)
            {
                return NotFound();
            }

            var bloodGroup = await _context.BloodGroups.FindAsync(id);
            if (bloodGroup == null)
            {
                return NotFound();
            }
            return View(bloodGroup);
        }

        // POST: BloodGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BloodGroup bloodGroup)
        {
            if (id != bloodGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodGroupExists(bloodGroup.Id))
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
            return View(bloodGroup);
        }

        // GET: BloodGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BloodGroups == null)
            {
                return NotFound();
            }

            var bloodGroup = await _context.BloodGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloodGroup == null)
            {
                return NotFound();
            }

            return View(bloodGroup);
        }

        // POST: BloodGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BloodGroups == null)
            {
                return Problem("Entity set 'BangamataHallContext.BloodGroups'  is null.");
            }
            var bloodGroup = await _context.BloodGroups.FindAsync(id);
            if (bloodGroup != null)
            {
                _context.BloodGroups.Remove(bloodGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodGroupExists(int id)
        {
          return (_context.BloodGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
