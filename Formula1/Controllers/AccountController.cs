using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Text;

namespace Formula1.Controllers;

[Route("[controller]/[action]")]
public class AccountController : Controller
{
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("IsLoggedIn") == "true")
        {
            return RedirectToAction("LoggedIn");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Login(IFormCollection form)
    {
        if (HttpContext.Session.GetString("IsLoggedIn") == "true")
        {
            return RedirectToAction("LoggedIn");
        }

        // Pobranie danych z formularza
        string username = form["username"];
        string password = form["password"];
        
        HttpContext.Session.SetString("IsAdmin", "false");

        if(username == "admin")
        {
            HttpContext.Session.SetString("IsAdmin", "true");
        }

        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "./app.db";
        using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM logins WHERE login = '{username}'";
            SqliteDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                ViewBag.ErrorMessage = "Login nie istnieje w bazie danych.";
                return View();
            }

            HashAlgorithm hashAlgorithm = MD5.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedPasswordBytes = hashAlgorithm.ComputeHash(passwordBytes);
            string hashedPassword = "";
            foreach (byte b in hashedPasswordBytes)
            {
                hashedPassword += b.ToString("X2");
            }

            reader.Read();
            string passwordFromDatabase = reader.GetString(1);
            if (hashedPassword != passwordFromDatabase)
            {
                ViewBag.ErrorMessage = "Niepoprawne hasło.";
                return View();
            }

            reader.Close();
            
           
            
            HttpContext.Session.SetString("IsLoggedIn", "true");

            return RedirectToAction("LoggedIn");
        }
    }

    public IActionResult LoggedIn()
    {
        if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
        {
            return RedirectToAction("Login");
        }

        return View();
    }

    public IActionResult Logout()
    {
        if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
        {
            return RedirectToAction("Login");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Logout(IFormCollection form)
    {
        if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
        {
            return RedirectToAction("Login");
        }

        // Usuń informację o zalogowaniu z sesji
        HttpContext.Session.Remove("IsLoggedIn");

        // Przekieruj na stronę logowania
        return RedirectToAction("Login");
    }

    // public IActionResult Data()
    // {
    //     if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
    //     {
    //         return RedirectToAction("Login");
    //     }

    //     // string header = "";
    //     string data = "";
    //     int count = 0;
    //     try
    //     {
    //         var connectionStringBuilder = new SqliteConnectionStringBuilder();
    //         connectionStringBuilder.DataSource = "./app.db";
    //         using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
    //         {
    //             connection.Open();
    //             SqliteCommand command = connection.CreateCommand();
    //             command.CommandText = $"SELECT * FROM data";

    //             using (SqliteDataReader reader = command.ExecuteReader())
    //             {
    //                 // for (int a = 0; a < reader.FieldCount; a++)
    //                 // {
    //                 //     data += reader.GetName(a) + "\t";
    //                 // }

    //                 // data += "\n";

    //                 while (reader.Read())
    //                 {
    //                     String? val = null;
    //                     try
    //                     {
    //                         val = reader.GetString(1);
    //                     }
    //                     catch { }
    //                     data += (val != null ? val : "NULL");

    //                     data += "\n";

    //                     count++;
    //                 }
    //             }

    //             ViewData["Data"] = data;
    //             ViewData["Count"] = count;
    //         }
    //     }
    //     catch { }

    //     return View();
    // }

    // [HttpPost]
    // public IActionResult Data(IFormCollection form)
    // {
    //     if (!(HttpContext.Session.GetString("IsLoggedIn") == "true"))
    //     {
    //         return RedirectToAction("Login");
    //     }

    //     string data = form["data"];

    //     var connectionStringBuilder = new SqliteConnectionStringBuilder();
    //     connectionStringBuilder.DataSource = "./app.db";
    //     using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
    //     {
    //         connection.Open();
    //         SqliteCommand command = connection.CreateCommand();
    //         command.CommandText = $"INSERT INTO data (data) VALUES ('{data}')";
    //         command.ExecuteNonQuery();
    //     }

    //     return RedirectToAction("Data");
    // }
}
