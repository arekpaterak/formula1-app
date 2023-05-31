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
    public class TeamController : Controller
    {
        private readonly Formula1Context _context;

        public TeamController(Formula1Context context)
        {
            _context = context;
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
              return _context.TeamModel != null ? 
                          View(await _context.TeamModel.ToListAsync()) :
                          Problem("Entity set 'Formula1Context.TeamModel'  is null.");
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeamModel == null)
            {
                return NotFound();
            }

            var teamModel = await _context.TeamModel
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (teamModel == null)
            {
                return NotFound();
            }

            return View(teamModel);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name,Nationality,Url")] TeamModel teamModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamModel);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeamModel == null)
            {
                return NotFound();
            }

            var teamModel = await _context.TeamModel.FindAsync(id);
            if (teamModel == null)
            {
                return NotFound();
            }
            return View(teamModel);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name,Nationality,Url")] TeamModel teamModel)
        {
            if (id != teamModel.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamModelExists(teamModel.TeamId))
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
            return View(teamModel);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeamModel == null)
            {
                return NotFound();
            }

            var teamModel = await _context.TeamModel
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (teamModel == null)
            {
                return NotFound();
            }

            return View(teamModel);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeamModel == null)
            {
                return Problem("Entity set 'Formula1Context.TeamModel'  is null.");
            }
            var teamModel = await _context.TeamModel.FindAsync(id);
            if (teamModel != null)
            {
                _context.TeamModel.Remove(teamModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamModelExists(int id)
        {
          return (_context.TeamModel?.Any(e => e.TeamId == id)).GetValueOrDefault();
        }
    }
}
