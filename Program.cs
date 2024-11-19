using TaskManagerUI.Data;
using Microsoft.EntityFrameworkCore;
using TaskManagerUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Use SQLite for local development, and Azure SQL Database for production.
var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : builder.Configuration.GetConnectionString("AzureConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // UseSqlite for local or UseSqlServer for Azure.

// Add TaskService for dependency injection.
builder.Services.AddScoped<TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages().WithStaticAssets();
app.Run();
