using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Data.Models.Auth;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Daten-Kontext zu Services hinzufügen
services.AddDbContext<TodoListDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default"));
});
// Identity (UserManager etc.) zu Services hinzufügen
services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<TodoListDbContext>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
