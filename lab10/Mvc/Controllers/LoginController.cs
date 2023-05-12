﻿using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Mvc.Models;

namespace Mvc.Controllers;

//[Route("/")]
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    [Route("/")]
    public IActionResult Index()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");

        return View();
    }

    /* --------------------------------- Log out -------------------------------- */
    [Route("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    /* --------------------------------- Log In --------------------------------- */
    [Route("/login")]
    public IActionResult Login()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        return View();
    }


    [HttpPost]
    [Route("/login")]
    public IActionResult Login(IFormCollection form)
    {
        if (form is null)
            return View();

        using (var connection = new SqliteConnection("Data Source=db.db"))
        {
            String username = form["username"].ToString();
            String password = form["password"].ToString();

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password;";
            command.Parameters.AddWithValue("@Username", form["username"].ToString());
            command.Parameters.AddWithValue("@Password", form["password"].ToString());
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                ViewData["Username"] = username;
                ViewData["Message"] = "Login successful";
                HttpContext.Session.SetString("Username", username);
            }
            else
            {
                ViewData["Message"] = "Invalid username or password";
            }

        }
        return View();
    }

    /* -------------------------------- Register -------------------------------- */
    [Route("/register")]
    public IActionResult Register()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        return View();
    }

    [HttpPost]
    [Route("/register")]
    public IActionResult Register(IFormCollection form)
    {
        if (form is null)
            return View();

        using (var connection = new SqliteConnection("Data Source=db.db"))
        {

            String username = form["username"].ToString();
            String password = form["password1"].ToString();

            connection.Open();
            if (password != form["password2"].ToString())
            {
                ViewData["Message"] = "Passwords do not match";
                return View();
            }

            // Check if username already exists
            var commandCheckIfRegistered = connection.CreateCommand();
            commandCheckIfRegistered.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username;";
            commandCheckIfRegistered.Parameters.AddWithValue("@Username", username);
            var reader = commandCheckIfRegistered.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetInt32(0) > 0)
                {
                    ViewData["Message"] = "Username already exists";
                    return View();
                }
            }

            // Register user
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password);";
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            // command.Parameters.AddWithValue("@Password", MD5Encryption(password));

            command.ExecuteNonQuery();

            ViewData["Message"] = "Registration successful";
        }

        return View();
    }
    
    

    private string MD5Encryption(string password)
    {
        using (var md5 = MD5.Create())
        {
            var result = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
            return System.Text.Encoding.ASCII.GetString(result);
        }
    }


    /* -------------------------------- Add data -------------------------------- */
    [Route("/data")]
    public IActionResult Data()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        return View();
    }

    [HttpPost]
    [Route("/data")]
    public IActionResult Data(IFormCollection form)
    {
        if (form is null)
            return View();

        using (var connection = new SqliteConnection("Data Source=db.db"))
        {
            String username = form["username"].ToString();
            String content = form["content"].ToString();

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Data (Content, UserId) VALUES (@Content, @Username);";
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Content", content);
            command.ExecuteNonQuery();

            ViewData["Message"] = "Data added successfully";

        }

        return View();
    }




    /* ---------------------------------- Error --------------------------------- */
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
