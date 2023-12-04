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
    public class CalendarEventsController : Controller
    {
        private readonly AppDbContext _context;

        public CalendarEventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CalendarEvents
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CalendarEvents.Include(c => c.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CalendarEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CalendarEvents == null)
            {
                return NotFound();
            }

            var calendarEvent = await _context.CalendarEvents
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            return View(calendarEvent);
        }

        // GET: CalendarEvents/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email");
            return View();
        }

        // POST: CalendarEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,UserID,EventTitle,EventDate,EventTime,EventLocation,Notes")] CalendarEvent calendarEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendarEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", calendarEvent.UserID);
            return View(calendarEvent);
        }

        // GET: CalendarEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CalendarEvents == null)
            {
                return NotFound();
            }

            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", calendarEvent.UserID);
            return View(calendarEvent);
        }

        // POST: CalendarEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,UserID,EventTitle,EventDate,EventTime,EventLocation,Notes")] CalendarEvent calendarEvent)
        {
            if (id != calendarEvent.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendarEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarEventExists(calendarEvent.EventID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", calendarEvent.UserID);
            return View(calendarEvent);
        }

        // GET: CalendarEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CalendarEvents == null)
            {
                return NotFound();
            }

            var calendarEvent = await _context.CalendarEvents
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            return View(calendarEvent);
        }

        // POST: CalendarEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CalendarEvents == null)
            {
                return Problem("Entity set 'AppDbContext.CalendarEvents'  is null.");
            }
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent != null)
            {
                _context.CalendarEvents.Remove(calendarEvent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarEventExists(int id)
        {
          return (_context.CalendarEvents?.Any(e => e.EventID == id)).GetValueOrDefault();
        }
    }
}
