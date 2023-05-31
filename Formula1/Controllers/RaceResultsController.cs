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
    public class RaceResultsController : Controller
    {
        private readonly Formula1Context _context;

        public RaceResultsController(Formula1Context context)
        {
            _context = context;
        }

        // GET: RaceResults
        public async Task<IActionResult> Index()
        {
              return _context.RaceResultsModel != null ? 
                          View(await _context.RaceResultsModel.ToListAsync()) :
                          Problem("Entity set 'Formula1Context.RaceResultsModel'  is null.");
        }

        // GET: RaceResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RaceResultsModel == null)
            {
                return NotFound();
            }

            var raceResultsModel = await _context.RaceResultsModel
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (raceResultsModel == null)
            {
                return NotFound();
            }

            return View(raceResultsModel);
        }

        // GET: RaceResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RaceResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,Number")] RaceResultsModel raceResultsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raceResultsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raceResultsModel);
        }

        // GET: RaceResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RaceResultsModel == null)
            {
                return NotFound();
            }

            var raceResultsModel = await _context.RaceResultsModel.FindAsync(id);
            if (raceResultsModel == null)
            {
                return NotFound();
            }
            return View(raceResultsModel);
        }

        // POST: RaceResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultId,Number")] RaceResultsModel raceResultsModel)
        {
            if (id != raceResultsModel.ResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raceResultsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceResultsModelExists(raceResultsModel.ResultId))
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
            return View(raceResultsModel);
        }

        // GET: RaceResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RaceResultsModel == null)
            {
                return NotFound();
            }

            var raceResultsModel = await _context.RaceResultsModel
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (raceResultsModel == null)
            {
                return NotFound();
            }

            return View(raceResultsModel);
        }

        // POST: RaceResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RaceResultsModel == null)
            {
                return Problem("Entity set 'Formula1Context.RaceResultsModel'  is null.");
            }
            var raceResultsModel = await _context.RaceResultsModel.FindAsync(id);
            if (raceResultsModel != null)
            {
                _context.RaceResultsModel.Remove(raceResultsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceResultsModelExists(int id)
        {
          return (_context.RaceResultsModel?.Any(e => e.ResultId == id)).GetValueOrDefault();
        }
    }
}
