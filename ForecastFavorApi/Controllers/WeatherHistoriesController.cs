using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForecastFavorApp.Data;
using ForecastFavorLib.Models;

namespace ForecastFavorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeatherHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WeatherHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherHistory>>> GetWeatherHistories()
        {
          if (_context.WeatherHistories == null)
          {
              return NotFound();
          }
            return await _context.WeatherHistories.ToListAsync();
        }

        // GET: api/WeatherHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherHistory>> GetWeatherHistory(int id)
        {
          if (_context.WeatherHistories == null)
          {
              return NotFound();
          }
            var weatherHistory = await _context.WeatherHistories.FindAsync(id);

            if (weatherHistory == null)
            {
                return NotFound();
            }

            return weatherHistory;
        }

        // PUT: api/WeatherHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherHistory(int id, WeatherHistory weatherHistory)
        {
            if (id != weatherHistory.HistoryID)
            {
                return BadRequest();
            }

            _context.Entry(weatherHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherHistoryExists(id))
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

        // POST: api/WeatherHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeatherHistory>> PostWeatherHistory(WeatherHistory weatherHistory)
        {
          if (_context.WeatherHistories == null)
          {
              return Problem("Entity set 'AppDbContext.WeatherHistories'  is null.");
          }
            _context.WeatherHistories.Add(weatherHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherHistory", new { id = weatherHistory.HistoryID }, weatherHistory);
        }

        // DELETE: api/WeatherHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherHistory(int id)
        {
            if (_context.WeatherHistories == null)
            {
                return NotFound();
            }
            var weatherHistory = await _context.WeatherHistories.FindAsync(id);
            if (weatherHistory == null)
            {
                return NotFound();
            }

            _context.WeatherHistories.Remove(weatherHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherHistoryExists(int id)
        {
            return (_context.WeatherHistories?.Any(e => e.HistoryID == id)).GetValueOrDefault();
        }
    }
}
