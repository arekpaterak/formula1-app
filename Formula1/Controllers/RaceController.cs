using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formula1.Data;
using Formula1.Models;

namespace Formula1.Controllers
{
    public class RaceController : Controller
    {
        private readonly Formula1Context _context;

        public RaceController(Formula1Context context)
        {
            _context = context;
        }

        // GET: Race
        public async Task<IActionResult> Index()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return _context.RaceModel != null ?
                          View(await _context.RaceModel.ToListAsync()) :
                          Problem("Entity set 'Formula1Context.RaceModel'  is null.");
        }

        // public async Task<IActionResult> RaceResults(){
        //     var races = await _context.RaceModel.ToListAsync();

        //     var names = new List<string>();

        //     foreach (var race in races){
        //         names.Add(race.Name);
        //     }

        // }

        public async Task<IActionResult> RaceResult(int? id)
        {
            var raceName = await _context.RaceModel
                .FirstOrDefaultAsync(m => m.RaceId == id);

            ViewBag.RaceName = raceName.Name;

            var raceResults = await _context.RaceResultsModel
                .Where(m => m.Race.RaceId == id)
                .ToListAsync();

            ViewBag.raceResult = raceResults;

            return View("RaceResult");
        }

        // GET: Race/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.RaceModel == null)
            {
                return NotFound();
            }

            var raceModel = await _context.RaceModel
                .FirstOrDefaultAsync(m => m.RaceId == id);
            if (raceModel == null)
            {
                return NotFound();
            }

            return View(raceModel);
        }

        // GET: Race/Create
        public IActionResult Create()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return View();
        }

        // POST: Race/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RaceId,Year,Round,Name,Date,Url")] RaceModel raceModel)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(raceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raceModel);
        }

        // GET: Race/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.RaceModel == null)
            {
                return NotFound();
            }

            var raceModel = await _context.RaceModel.FindAsync(id);
            if (raceModel == null)
            {
                return NotFound();
            }
            return View(raceModel);
        }

        // POST: Race/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaceId,Year,Round,Name,Date,Url")] RaceModel raceModel)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id != raceModel.RaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceModelExists(raceModel.RaceId))
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
            return View(raceModel);
        }

        // GET: Race/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.RaceModel == null)
            {
                return NotFound();
            }

            var raceModel = await _context.RaceModel
                .FirstOrDefaultAsync(m => m.RaceId == id);
            if (raceModel == null)
            {
                return NotFound();
            }

            return View(raceModel);
        }

        // POST: Race/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (_context.RaceModel == null)
            {
                return Problem("Entity set 'Formula1Context.RaceModel'  is null.");
            }
            var raceModel = await _context.RaceModel.FindAsync(id);
            if (raceModel != null)
            {
                _context.RaceModel.Remove(raceModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceModelExists(int id)
        {
            return (_context.RaceModel?.Any(e => e.RaceId == id)).GetValueOrDefault();
        }
    }
}
