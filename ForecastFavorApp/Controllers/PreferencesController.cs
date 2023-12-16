using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForecastFavorLib.Data;
using ForecastFavorLib.Models;
using ForecastFavorApp.Models;

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
            var sth = await appDbContext.ToListAsync();
            var model = new List<PreferencesViewModel>();
            if (sth != null)
            {
                foreach (var item in sth)
                {
                    model.Add(new PreferencesViewModel {

                        PreferencesId = item.PreferencesID,
                        UserId = item.UserID,
                        NotifyOnClouds = item.NotifyOnClouds, 
                        NotifyOnRain = item.NotifyOnRain,
                        NotifyOnSnow = item.NotifyOnRain,
                        NotifyOnSun = item.NotifyOnSun,
                        PreferredLocations = item.PreferredLocations ,
                        User = item.User
                        
                    });;
                }
               
            }
            return View(model);

           // return View(await appDbContext.ToListAsync());
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
       public async Task<IActionResult> Create([Bind("PreferencesID,UserID,NotifyOnRain,NotifyOnSun,NotifyOnClouds,NotifyOnSnow,PreferredLocations")] Preferences preferences)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(preferences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
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

            var appDbContext = _context.Preferences.Include(p => p.User);
            var sth = await appDbContext.ToListAsync();

            var preferences = sth.Find(p => p.PreferencesID == id.Value);

            //var preferences = await _context.Preferences.FindAsync(id);
            
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
        public async Task<IActionResult> Editss(int id, [Bind("PreferencesID,UserID,NotifyOnRain,NotifyOnSun,NotifyOnClouds,NotifyOnSnow,PreferredLocations")] Preferences preferences)
        {
            if (id != preferences.PreferencesID)
            {
                return NotFound();
            }

            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", preferences.UserID);


            //var appDbContext = _context.Preferences.Include(p => p.User);
            //var sth = await appDbContext.ToListAsync();

            //var preferencess = sth.Find(p => p.PreferencesID == id);

            var appDbContext = await _context.Preferences.Include(p => p.User).ToListAsync();

            var existingPreference = appDbContext.Where(p => p.PreferencesID == id ).FirstOrDefault();
            //var existingPreference = await _context.Preferences.FindAsync(id);
            if (existingPreference == null)
            {
                return NotFound();
            }

            // Update the properties of the fetched entity
            existingPreference.UserID = preferences.UserID;
            existingPreference.NotifyOnRain = preferences.NotifyOnRain;
            existingPreference.NotifyOnSun = preferences.NotifyOnSun;
            existingPreference.NotifyOnClouds = preferences.NotifyOnClouds;
            existingPreference.NotifyOnSnow = preferences.NotifyOnSnow;
            existingPreference.PreferredLocations = preferences.PreferredLocations;



            //if (ModelState.IsValid)
            //{
            try
                {
                    //_context.Update(preferences);
                    await _context.SaveChangesAsync();
                }
            //catch (DbUpdateConcurrencyException)

            catch (Exception ex)
            {
                var sth = ex;
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
            //}
            //ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", preferences.UserID);
            //return View(preferences);
        }




        // POST: Preferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PreferencesViewModel model)
        {
            if (model.PreferencesId == 0)
            {
                return NotFound();
            }


            var existingPreference = _context.Preferences.Where(p => p.PreferencesID == model.PreferencesId).FirstOrDefault();


            if (existingPreference == null)
            {
                return NotFound();
            }


            // Update the properties of the fetched entity
            existingPreference.UserID = model.UserId;
            existingPreference.NotifyOnRain = model.NotifyOnRain;
            existingPreference.NotifyOnSun = model.NotifyOnSun;
            existingPreference.NotifyOnClouds = model.NotifyOnClouds;
            existingPreference.NotifyOnSnow = model.NotifyOnSnow;
            existingPreference.PreferredLocations = model.PreferredLocations;


            try
            {
                _context.Update(existingPreference);
                await _context.SaveChangesAsync();
            }
            //catch (DbUpdateConcurrencyException)

            catch (Exception ex)
            {
                var sth = ex;
                if (!PreferencesExists(model.PreferencesId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", preferences.UserID);
            //return View(preferences);
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
