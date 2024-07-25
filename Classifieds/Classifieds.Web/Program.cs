using Classifieds.Data;
using Classifieds.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

var mySqlUsr = Environment.GetEnvironmentVariable("MY_SQL_USR");
var mySqlPwd = Environment.GetEnvironmentVariable("MY_SQL_PWD");

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(q => q.LoginPath = "/Auth/Login");

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    SqlConnectionStringBuilder sqlBuilder = new(Configuration.GetConnectionString("DatabaseConnection"));
    sqlBuilder.UserID = mySqlUsr;
    sqlBuilder.Password = mySqlPwd;
    Console.WriteLine(sqlBuilder.ConnectionString);
    options.UseSqlServer(sqlBuilder.ConnectionString);
});

builder.Services.AddTransient<IEmailSender>(s => new EmailSender("localhost", 25, "no-reploy@classified.com"));

builder.Services.AddRazorPages()
    .AddMvcOptions(q => q.Filters.Add(new AuthorizeFilter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
