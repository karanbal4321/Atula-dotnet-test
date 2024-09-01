using Atula_dotnet_test.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Atula_dotnet_test.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Atula_dotnet_test.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Configure the database context to use MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender, DummyEmailSender>();

// Inside the ConfigureServices method
builder.Services.AddScoped<ProductMapper>();


// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Strict-Transport-Security for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use authentication middleware before authorization
app.UseAuthentication(); // This handles login and authentication
app.UseAuthorization();  // This handles authorization based on roles or policies

app.MapRazorPages();

app.Run();
