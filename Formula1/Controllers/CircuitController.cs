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
    public class CircuitController : Controller
    {
        private readonly Formula1Context _context;

        public CircuitController(Formula1Context context)
        {
            _context = context;
        }

        // GET: Circuit
        public async Task<IActionResult> Index()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
              return _context.CircuitModel != null ? 
                          View(await _context.CircuitModel.ToListAsync()) :
                          Problem("Entity set 'Formula1Context.CircuitModel'  is null.");
        }

        // GET: Circuit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.CircuitModel == null)
            {
                return NotFound();
            }

            var circuitModel = await _context.CircuitModel
                .FirstOrDefaultAsync(m => m.CircuitId == id);
            if (circuitModel == null)
            {
                return NotFound();
            }

            return View(circuitModel);
        }

        // GET: Circuit/Create
        public IActionResult Create()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return View();
        }

        // POST: Circuit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CircuitId,Name,City,Country,Url")] CircuitModel circuitModel)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(circuitModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(circuitModel);
        }

        // GET: Circuit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.CircuitModel == null)
            {
                return NotFound();
            }

            var circuitModel = await _context.CircuitModel.FindAsync(id);
            if (circuitModel == null)
            {
                return NotFound();
            }
            return View(circuitModel);
        }

        // POST: Circuit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CircuitId,Name,City,Country,Url")] CircuitModel circuitModel)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id != circuitModel.CircuitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(circuitModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircuitModelExists(circuitModel.CircuitId))
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
            return View(circuitModel);
        }

        // GET: Circuit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.CircuitModel == null)
            {
                return NotFound();
            }

            var circuitModel = await _context.CircuitModel
                .FirstOrDefaultAsync(m => m.CircuitId == id);
            if (circuitModel == null)
            {
                return NotFound();
            }

            return View(circuitModel);
        }

        // POST: Circuit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (_context.CircuitModel == null)
            {
                return Problem("Entity set 'Formula1Context.CircuitModel'  is null.");
            }
            var circuitModel = await _context.CircuitModel.FindAsync(id);
            if (circuitModel != null)
            {
                _context.CircuitModel.Remove(circuitModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CircuitModelExists(int id)
        {
          return (_context.CircuitModel?.Any(e => e.CircuitId == id)).GetValueOrDefault();
        }
    }
}
