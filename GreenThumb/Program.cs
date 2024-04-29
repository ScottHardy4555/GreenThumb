using GreenThumb.Data.Services;
using GreenThumb.Models;
using GreenThumb.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
	options.LowercaseUrls = true;
	options.AppendTrailingSlash = true;
}); 

builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(60 * 5);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProjectContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectContext")));

//Services
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IDailyLogService, DailyLogService>();
builder.Services.AddScoped<IStatusReportService, StatusReportService>();
builder.Services.AddScoped<ITechnicianService, TechnicianService>();

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ProjectContext>().AddDefaultTokenProviders();

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

app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
	await ConfigureIdentity.CreateAdminUserAsync(scope.ServiceProvider);
}

app.UseSession();

#region Routes

app.MapAreaControllerRoute(
	name: "Admin",
	areaName: "Admin",
	pattern: "Admin/{controller=User}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
	name: "DailyLog",
	areaName: "DailyLog",
	pattern: "DailyLog/{controller=DailyLog}/{action=List}/{id?}/{slug?}");

app.MapAreaControllerRoute(
	name: "Contact",
	areaName: "Contact",
	pattern: "Contact/{controller=Contact}/{action=List}/{id?}/{slug?}");

app.MapAreaControllerRoute(
	name: "Technician",
	areaName: "Technician",
	pattern: "Technician/{controller=Technician}/{action=List}/{id?}/{slug?}");

app.MapAreaControllerRoute(
	name: "Ticket",
	areaName: "Ticket",
	pattern: "Ticket/{controller=Ticket}/{action=List}/{id?}/{slug?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.Run();
