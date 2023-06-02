using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Formula1.Models;

namespace Formula1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if ((HttpContext.Session.GetString("IsLoggedIn") == "true"))
        {
            return RedirectToAction("Account", "LoggedIn");
        }

        return RedirectToAction("Account", "LoggedIn");
    }

    [HttpPost]
    public IActionResult Index(IFormCollection form)
    {
        if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
        {
            return RedirectToAction("Account", "LoggedIn");        
        }

        return RedirectToAction("Account", "LoggedIn");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
