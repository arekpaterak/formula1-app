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
    public class StandingsController : Controller
    {
        private readonly Formula1Context _context;

        public StandingsController(Formula1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TeamsStanding(){
            return View();
        }

        public IActionResult DriversStanding(){
            return View();
        }

    }
}