using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Formula1.Data;

HashAlgorithm hashAlgorithm = MD5.Create();

string password = "1234";
byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
byte[] hashedPasswordBytes = hashAlgorithm.ComputeHash(passwordBytes);
string hashedPassword = "";
foreach (byte b in hashedPasswordBytes)
{
    hashedPassword += b.ToString("X2");
}

var connectionStringBuilder = new SqliteConnectionStringBuilder();
connectionStringBuilder.DataSource = "./app.db";
using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
{
    connection.Open();

    SqliteCommand command = connection.CreateCommand();

    command.CommandText = "DROP TABLE IF EXISTS logins;";
    command.ExecuteNonQuery();

    // command.CommandText = "DROP TABLE IF EXISTS data;";
    // command.ExecuteNonQuery();

    command.CommandText = "CREATE TABLE logins (\n" +
        "    login TEXT PRIMARY KEY,\n" +
        "    password TEXT NOT NULL\n" +
        ");";
    command.ExecuteNonQuery();

    // command.CommandText = "CREATE TABLE data (\n" +
    //     "    id INTEGER PRIMARY KEY,\n" +
    //     "    data TEXT NOT NULL\n" +
    //     ");";
    // command.ExecuteNonQuery();

    command.CommandText = $"INSERT INTO logins (login, password) VALUES ('admin', '{hashedPassword}'), ('user', '{hashedPassword}');";
    command.ExecuteNonQuery();

    // command.CommandText = "INSERT INTO data (data) VALUES ('Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla euismod, nunc sed ultricies ultricies, diam nisl ultricies nisl, vitae ultricies nisl nisl eget nisl.'), ('Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla euismod, nunc sed ultricies ultricies, diam nisl ultricies nisl, vitae ultricies nisl nisl eget nisl.');";
    // command.ExecuteNonQuery();

    connection.Close();
}

var builder = WebApplication.CreateBuilder(args);

// Dodanie obsługi stron Razor
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Formula1Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Formula1Context") ?? throw new InvalidOperationException("Connection string 'Formula1Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dodanie obsługi sesji
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10 * 60);
    options.Cookie.HttpOnly = true; //plik cookie jest niedostępny przez skrypt po stronie klienta
    options.Cookie.IsEssential = true; //pliki cookie sesji będą zapisywane dzięki czemu sesje będzie mogła być śledzona podczas nawigacji lub przeładowania strony
});
// END

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

app.UseSession(); // Dodanie obsługi sesji

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Dodanie obsługi stron Razor
app.MapRazorPages();

// Przekierowanie na stronę logowania, jeśli zasób nie istnieje
app.UseStatusCodePagesWithReExecute("/Account/Login", "?statusCode={0}"); // Przekierowanie na stronę logowania, jeśli zasób nie istnieje

app.Run();