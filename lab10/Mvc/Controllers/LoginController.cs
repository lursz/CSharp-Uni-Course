using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Mvc.Models;

namespace Mvc.Controllers;

//[Route("/")]
public class LoginController : Controller
{
    private readonly string data_base_name = "db.db";
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

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))
        {
            String username = form["username"].ToString();
            String password = form["password"].ToString();

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password;";
            command.Parameters.AddWithValue("@Username", form["username"].ToString());
            command.Parameters.AddWithValue("@Password", MD5Hash(form["password"].ToString()));
            System.Console.WriteLine(MD5Hash(form["password"].ToString()));

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

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))

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
            command.Parameters.AddWithValue("@Password", MD5Hash(password));

            command.ExecuteNonQuery();

            ViewData["Message"] = "Registration successful";
        }

        return View();
    }



    private string MD5Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }




    /* -------------------------------- View data ------------------------------- */
    [Route("/data")]
    public IActionResult Data()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");

        if (ViewData["Username"] is null)
        {
            return RedirectToAction("Login");
        }

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))

        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Data WHERE UserId = (SELECT Id FROM Users WHERE Username = @Username);";
            command.Parameters.AddWithValue("@Username", ViewData["Username"].ToString());
            var reader = command.ExecuteReader();
            List<String> dataList = new List<String>();
            while (reader.Read())
            {
                dataList.Add(new String(reader.GetString(1)));
            }
            ViewData["DataList"] = dataList;

            // print datalist
            foreach (var item in dataList)
            {
                System.Console.WriteLine(item);
            }
        }
        return View();
    }

    /* -------------------------------- Add data -------------------------------- */
    [HttpPost]
    [Route("/data")]
    public IActionResult Data(IFormCollection form)
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");

        if (ViewData["Username"] is null)
        {
            return RedirectToAction("Login");
        }

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))

        {

            String username = HttpContext.Session.GetString("Username");
            String content = form["data"].ToString();

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Data (Content, UserId) VALUES (@Content, (SELECT Id FROM Users WHERE Username = @Username));";
            System.Console.WriteLine(username);
            System.Console.WriteLine(content);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Content", content);
            command.ExecuteNonQuery();

            ViewData["Message"] = "Data added successfully";

        }

        return Data();
    }




    /* ---------------------------------- Error --------------------------------- */
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}






