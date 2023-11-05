using Microsoft.EntityFrameworkCore;
using PPPKZadatak03.Models;
using PPPKZadatak03.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<ActorService>();
builder.Services.AddScoped<DirectorService>();
builder.Services.AddScoped<PosterService>();

//var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
//var environmentName = builder.Environment.EnvironmentName;
//
//builder.Configuration
//    .SetBasePath(currentDirectory)
//    .AddJsonFile("appsettings.json", false, true)
//    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
//    .AddEnvironmentVariables();

builder.Services.AddDbContext<PppkmoviesContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PPPKConnString");
    options.UseSqlServer(connectionString);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Actor}/{action=Index}/{id?}");

app.Run();
