using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Classifieds.Data;
using Classifieds.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity.UI.Services;
using Classifieds.Web.Services;
using Classifieds.Web.Services.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var sqlBuilder = new SqlConnectionStringBuilder(connectionString);
var userId = Environment.GetEnvironmentVariable("MY_SQL_USR");
var sqlPwd = Environment.GetEnvironmentVariable("MY_SQL_PWD");
sqlBuilder.UserID = userId;
sqlBuilder.Password = sqlPwd;
sqlBuilder.TrustServerCertificate = true;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(sqlBuilder.ConnectionString));
    
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<IEmailSender>(s => new EmailSender("localhost", 25, "no-reply@classified.com"));

builder.Services.AddDefaultIdentity<User>(options => {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 3;
    })
    .AddRoles<IdentityRole>() // Must be defined before adding entity framework stores
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddPasswordValidator<PasswordValidatorService>()
    .AddClaimsPrincipalFactory<CustomerClaimsService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
