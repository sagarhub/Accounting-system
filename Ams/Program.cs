
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ams.Data;
using Ams.Manager.Interfaces;
using Ams.Manager;
using Ams.Provider.Interfaces;
using Ams.Provider;
using Ams.Repository.Interfaces;
using Ams.Repository;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(ConnectionString));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => { x.LoginPath = "/Login/Login"; });

builder.Services.AddControllers();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IAuthManager, AuthManager>();


builder.Services.AddScoped<IDbConnectionProvider, DbConnectionProvider>();

builder.Services.AddScoped<IReportsRepo,ReportsRepo>();

var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetService<DbContext>().Database.Migrate();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
    
app.Run();
