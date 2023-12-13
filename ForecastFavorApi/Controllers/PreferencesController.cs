using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForecastFavorLib.Data;
using ForecastFavorLib.Models;

namespace ForecastFavorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PreferencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Preferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Preferences>>> GetPreferences()
        {
          if (_context.Preferences == null)
          {
              return NotFound();
          }
            return await _context.Preferences.ToListAsync();
        }

        // GET: api/Preferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Preferences>> GetPreferences(int id)
        {
          if (_context.Preferences == null)
          {
              return NotFound();
          }
            var preferences = await _context.Preferences.FindAsync(id);

            if (preferences == null)
            {
                return NotFound();
            }

            return preferences;
        }

        // PUT: api/Preferences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreferences(int id, Preferences preferences)
        {
            if (id != preferences.PreferencesID)
            {
                return BadRequest();
            }

            _context.Entry(preferences).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreferencesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Preferences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Preferences>> PostPreferences(Preferences preferences)
        {
          if (_context.Preferences == null)
          {
              return Problem("Entity set 'AppDbContext.Preferences'  is null.");
          }
            _context.Preferences.Add(preferences);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreferences", new { id = preferences.PreferencesID }, preferences);
        }

        // DELETE: api/Preferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreferences(int id)
        {
            if (_context.Preferences == null)
            {
                return NotFound();
            }
            var preferences = await _context.Preferences.FindAsync(id);
            if (preferences == null)
            {
                return NotFound();
            }

            _context.Preferences.Remove(preferences);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreferencesExists(int id)
        {
            return (_context.Preferences?.Any(e => e.PreferencesID == id)).GetValueOrDefault();
        }
    }
}
