using Microsoft.EntityFrameworkCore;
using Vuelos.Data;
using Vuelos.Repository;
using Vuelos.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VuelosDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("sqlite")));
builder.Services.AddScoped<IVuelosRepository, VuelosRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Vuelos/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vuelos}/{action=Index}/{id?}");

app.Run();
