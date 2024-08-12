using Classifieds.Data;
using Classifieds.Data.Entities;
using Classifieds.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

var mySqlUsr = Environment.GetEnvironmentVariable("MY_SQL_USR");
var mySqlPwd = Environment.GetEnvironmentVariable("MY_SQL_PWD");
var emailUser = builder.Configuration["EmailCredentials:UserName"];
var emailPwd = builder.Configuration["EmailCredentials:Password"];

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    SqlConnectionStringBuilder sqlBuilder = new(Configuration.GetConnectionString("DatabaseConnection"));
    sqlBuilder.UserID = mySqlUsr;
    sqlBuilder.Password = mySqlPwd;
    Console.WriteLine(sqlBuilder.ConnectionString);
    options.UseSqlServer(sqlBuilder.ConnectionString);
});

//builder.Services.AddTransient<IEmailSender>(s => new EmailSender("localhost", 587, emailUser!, emailPwd));

builder.Services.AddDefaultIdentity<User>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 7;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.SignIn.RequireConfirmedAccount = false;
})
    //.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
    //.AddMvcOptions(q => q.Filters.Add(new AuthorizeFilter()));

//builder.Services.AddAuthentication(o => {
//    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddCookie(q => q.LoginPath = "/Auth/Login");
//    // .AddGoogle(o => { o.ClientId = ""; o.ClientSecret = ""; })
//    // .AddMicrosoftAccount(o => { o.ClientId = ""; o.ClientSecret = ""; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
