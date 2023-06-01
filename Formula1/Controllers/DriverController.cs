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
    public class DriverController : Controller
    {
        private readonly Formula1Context _context;

        public DriverController(Formula1Context context)
        {
            _context = context;
        }

        // GET: Driver
        public async Task<IActionResult> Index()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return _context.DriverModel != null ? 
                          View(await _context.DriverModel.ToListAsync()) :
                          Problem("Entity set 'Formula1Context.DriverModel'  is null.");
        }

        // GET: Driver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DriverModel == null)
            {
                return NotFound();
            }

            var driverModel = await _context.DriverModel
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverModel == null)
            {
                return NotFound();
            }

            return View(driverModel);
        }

        // GET: Driver/Create
        public IActionResult Create()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return View();
        }

        // POST: Driver/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverId,Number,Code,FirstName,LastName,DateOfBirth,Nationality,Url")] DriverModel driverModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driverModel);
        }

        // GET: Driver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            
            if (id == null || _context.DriverModel == null)
            {
                return NotFound();
            }

            var driverModel = await _context.DriverModel.FindAsync(id);
            if (driverModel == null)
            {
                return NotFound();
            }
            return View(driverModel);
        }

        // POST: Driver/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DriverId,Number,Code,FirstName,LastName,DateOfBirth,Nationality,Url")] DriverModel driverModel)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id != driverModel.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverModelExists(driverModel.DriverId))
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
            return View(driverModel);
        }

        // GET: Driver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.DriverModel == null)
            {
                return NotFound();
            }

            var driverModel = await _context.DriverModel
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverModel == null)
            {
                return NotFound();
            }

            return View(driverModel);
        }

        // POST: Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DriverModel == null)
            {
                return Problem("Entity set 'Formula1Context.DriverModel'  is null.");
            }
            var driverModel = await _context.DriverModel.FindAsync(id);
            if (driverModel != null)
            {
                _context.DriverModel.Remove(driverModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverModelExists(int id)
        {
          return (_context.DriverModel?.Any(e => e.DriverId == id)).GetValueOrDefault();
        }
    }
}
