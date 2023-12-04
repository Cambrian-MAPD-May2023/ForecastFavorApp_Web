using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForecastFavorApp.Data;
using ForecastFavorLib.Models;

namespace ForecastFavorApp.Controllers
{
    public class PreferencesController : Controller
    {
        private readonly AppDbContext _context;

        public PreferencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Preferences
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Preferences.Include(p => p.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Preferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Preferences == null)
            {
                return NotFound();
            }

            var preferences = await _context.Preferences
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PreferencesID == id);
            if (preferences == null)
            {
                return NotFound();
            }

            return View(preferences);
        }

        // GET: Preferences/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email");
            return View();
        }

        // POST: Preferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PreferencesID,UserID,NotificationTriggers,PreferredLocations")] Preferences preferences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preferences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", preferences.UserID);
            return View(preferences);
        }

        // GET: Preferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Preferences == null)
            {
                return NotFound();
            }

            var preferences = await _context.Preferences.FindAsync(id);
            if (preferences == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", preferences.UserID);
            return View(preferences);
        }

        // POST: Preferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PreferencesID,UserID,NotificationTriggers,PreferredLocations")] Preferences preferences)
        {
            if (id != preferences.PreferencesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preferences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferencesExists(preferences.PreferencesID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", preferences.UserID);
            return View(preferences);
        }

        // GET: Preferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Preferences == null)
            {
                return NotFound();
            }

            var preferences = await _context.Preferences
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PreferencesID == id);
            if (preferences == null)
            {
                return NotFound();
            }

            return View(preferences);
        }

        // POST: Preferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Preferences == null)
            {
                return Problem("Entity set 'AppDbContext.Preferences'  is null.");
            }
            var preferences = await _context.Preferences.FindAsync(id);
            if (preferences != null)
            {
                _context.Preferences.Remove(preferences);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreferencesExists(int id)
        {
          return (_context.Preferences?.Any(e => e.PreferencesID == id)).GetValueOrDefault();
        }
    }
}
