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
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }
            return View();
        }

        public IActionResult TeamsStanding(){
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }

            var raceResults = _context.RaceResultsModel.Where(r => r.Position != "NC").ToList();

            Dictionary<TeamModel, int> teamPoints = new Dictionary<TeamModel, int>();
            var teams = _context.TeamModel.ToList();

            foreach (var team in teams)
            {
                teamPoints.Add(team, 0);
            }

            foreach (var raceResult in raceResults)
            {
                var teamId = raceResult.Team.TeamId;
                var team = _context.TeamModel.FirstOrDefault(t => t.TeamId == teamId);
                var points = raceResult.Points;
                teamPoints[team] += points;
            }

            var sortedTeamPoints = teamPoints.OrderByDescending(t => t.Value).ToList();

            ViewBag.TeamPoints = sortedTeamPoints;
            
            return View();
        }

        public async Task<IActionResult> DriversStanding(){
            if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
            {
                return RedirectToAction("Account", "Login");
            }            
            
            var raceResults = await _context.RaceResultsModel.Where(r => r.Position != "NC").ToListAsync();

            Dictionary<DriverModel, int> driverPoints = new Dictionary<DriverModel, int>();
            var drivers = await _context.DriverModel.ToListAsync();
            foreach (var driver in drivers)
            {
                driverPoints.Add(driver, 0);
            }

            foreach (var raceResult in raceResults)
            {
                var driverId = raceResult.Driver.DriverId;
                var driver = await _context.DriverModel.FirstOrDefaultAsync(d => d.DriverId == driverId);
                var points = raceResult.Points;
                driverPoints[driver] += points;
                if (raceResult.SetFastestLap == "Yes"){
                    driverPoints[driver] += 1;
                }
            }

            var sprintResults = await _context.RaceResultsModel.Where(r => r.Sprint == true).ToListAsync();

            foreach (var sprintResult in sprintResults)
            {
                var driverId = sprintResult.Driver.DriverId;
                var driver = await _context.DriverModel.FirstOrDefaultAsync(d => d.DriverId == driverId);
                var points = sprintResult.Points;
                driverPoints[driver] += points;
            }

            var sortedDriverPoints = driverPoints.OrderByDescending(d => d.Value).ToList();
            ViewBag.DriverPoints = sortedDriverPoints;
                    
            return View();
        }


    }
}