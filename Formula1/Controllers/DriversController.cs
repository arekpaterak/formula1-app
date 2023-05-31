using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formula1.Data;
using Formula1.Models;


namespace Formula1.Controllers;

public class DriversController : Controller{

    public IActionResult Drivers(){
        Console.WriteLine("Drivers");
        return View();
    }
}