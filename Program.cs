using Microsoft.EntityFrameworkCore;
using RegistroCFTweb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RegistroCftContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("conexionDb"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.25-mariadb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
