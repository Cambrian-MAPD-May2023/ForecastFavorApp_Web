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
    public class WeatherHistoriesController : Controller
    {
        private readonly AppDbContext _context;

        public WeatherHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WeatherHistories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.WeatherHistories.Include(w => w.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: WeatherHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WeatherHistories == null)
            {
                return NotFound();
            }

            var weatherHistory = await _context.WeatherHistories
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.HistoryID == id);
            if (weatherHistory == null)
            {
                return NotFound();
            }

            return View(weatherHistory);
        }

        // GET: WeatherHistories/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email");
            return View();
        }

        // POST: WeatherHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryID,UserID,Location,Date,Temperature,Precipitation")] WeatherHistory weatherHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weatherHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", weatherHistory.UserID);
            return View(weatherHistory);
        }

        // GET: WeatherHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WeatherHistories == null)
            {
                return NotFound();
            }

            var weatherHistory = await _context.WeatherHistories.FindAsync(id);
            if (weatherHistory == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", weatherHistory.UserID);
            return View(weatherHistory);
        }

        // POST: WeatherHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryID,UserID,Location,Date,Temperature,Precipitation")] WeatherHistory weatherHistory)
        {
            if (id != weatherHistory.HistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherHistoryExists(weatherHistory.HistoryID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", weatherHistory.UserID);
            return View(weatherHistory);
        }

        // GET: WeatherHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WeatherHistories == null)
            {
                return NotFound();
            }

            var weatherHistory = await _context.WeatherHistories
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.HistoryID == id);
            if (weatherHistory == null)
            {
                return NotFound();
            }

            return View(weatherHistory);
        }

        // POST: WeatherHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WeatherHistories == null)
            {
                return Problem("Entity set 'AppDbContext.WeatherHistories'  is null.");
            }
            var weatherHistory = await _context.WeatherHistories.FindAsync(id);
            if (weatherHistory != null)
            {
                _context.WeatherHistories.Remove(weatherHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherHistoryExists(int id)
        {
          return (_context.WeatherHistories?.Any(e => e.HistoryID == id)).GetValueOrDefault();
        }
    }
}
