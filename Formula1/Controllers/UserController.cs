using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formula1.Data;
using Formula1.Models;
using System.Security.Cryptography;
using System.Text;

namespace Formula1.Controllers
{
    public class UserController : Controller
    {
        private readonly Formula1Context _context;

        public UserController(Formula1Context context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return _context.UserModel != null ?
                        View(await _context.UserModel.ToListAsync()) :
                        Problem("Entity set 'Formula1Context.UserModel'  is null.");
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.UserModel == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string? username)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (username == null || _context.UserModel == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.Username == username);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password")] UserModel userModel)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (ModelState.IsValid)
            {
                HashAlgorithm hashAlgorithm = MD5.Create();
                byte[] passwordBytes = Encoding.UTF8.GetBytes(userModel.Password);
                byte[] hashedPasswordBytes = hashAlgorithm.ComputeHash(passwordBytes);
                string hashedPassword = "";
                foreach (byte b in hashedPasswordBytes)
                {
                    hashedPassword += b.ToString("X2");
                }

                userModel.Password = hashedPassword;

                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.UserModel == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password")] UserModel userModel)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HashAlgorithm hashAlgorithm = MD5.Create();
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(userModel.Password);
                    byte[] hashedPasswordBytes = hashAlgorithm.ComputeHash(passwordBytes);
                    string hashedPassword = "";
                    foreach (byte b in hashedPasswordBytes)
                    {
                        hashedPassword += b.ToString("X2");
                    }

                    userModel.Password = hashedPassword;

                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.Id))
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
            return View(userModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (id == null || _context.UserModel == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            if (_context.UserModel == null)
            {
                return Problem("Entity set 'Formula1Context.UserModel'  is null.");
            }
            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel != null)
            {
                _context.UserModel.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
            return (_context.UserModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
