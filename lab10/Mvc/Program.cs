using Microsoft.Data.Sqlite;
// dotnet add package Microsoft.EntityFrameworkCore.Sqlite && dotnet new mvc -o Mvc && dotnet dev-certs https --trust

internal class Program
{
    private static void Main(string[] args)
    {
        /* -------------------------------------------------------------------------- */
        /*                                  DataBase                                  */
        /* -------------------------------------------------------------------------- */
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "db.db";

        using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();
        var Command = connection.CreateCommand();
        // string createTable = "DROP TABLE IF EXISTS Users; CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL, Password TEXT NOT NULL);";
        string createTable = "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL, Password TEXT NOT NULL);";
        Command.CommandText = createTable;
        Command.ExecuteNonQuery();

        string dataTable = "DROP TABLE IF EXISTS Data; CREATE TABLE IF NOT EXISTS Data (id INTEGER PRIMARY KEY AUTOINCREMENT, Content TEXT NOT NULL, UserId INTEGER NOT NULL REFERENCES Users(id));";
        // string dataTable = "CREATE TABLE IF NOT EXISTS Data (id INTEGER PRIMARY KEY AUTOINCREMENT, Content TEXT NOT NULL, UserId INTEGER NOT NULL REFERENCES Users(id));";
        Command.CommandText = dataTable;
        Command.ExecuteNonQuery();

        connection.Close();



        /* -------------------------------------------------------------------------- */
        /*                                     MVC                                    */
        /* -------------------------------------------------------------------------- */
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //Session handling
        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;//plik cookie jest niedostępny przez skrypt po stronie klienta
            options.Cookie.IsEssential = true;//pliki cookie sesji będą zapisywane dzięki czemu sesje będzie mogła być śledzona podczas nawigacji lub przeładowania strony
        });
        //KONIEC

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        //Session handling
        app.UseSession();
        //KONIEC

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Login}/{action=login}/{id?}");
        app.UseStatusCodePagesWithReExecute("/");

        app.Use(async (ctx, next) =>
        {
            await next();

            if ((ctx.Response.StatusCode == 404 || ctx.Response.StatusCode == 400) && !ctx.Response.HasStarted)
            {
                //Re-execute the request so the user gets the error page
                string originalPath = ctx.Request.Path.Value ?? "";
                ctx.Items["originalPath"] = originalPath;
                ctx.Request.Path = "/login/";
                ctx.Response.Redirect("/login/");
                await next();
            }
        });

        app.Run();
    }
}