using EgyptianEscape.Application.ImageUploader;
using EgyptianEscape.Application.Repository;
using EgyptianEscape.Application.Repository.IRepository;
using EgyptianEscape.Domain.Data;
using EgyptianEscape.Domain.Entities;
using EgyptianEscape.Infrastructure.Automapper;
using EgyptianEscape.Infrastructure.Repository;
using EgyptianEscape.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");




// Add Controllers with Views
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
    b.AllowAnyHeader().
    AllowAnyOrigin().
    AllowAnyHeader());
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IImageUploader, ImageUploader>();
builder.Services.AddScoped<IDbInitalizer, DbInitalizer>();
builder.Services.AddAutoMapper(typeof(AutomapperConfiguration));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

/*
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";
    options.AccessDeniedPath = "/Home/AccessDenied";
});
*/
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
}); 

var app = builder.Build();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SeceretKey").Get<string>();
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
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
await SeedDataBase();
async Task SeedDataBase()
{
    using(var scoped=app.Services.CreateScope())
    {
        var dbInitializer = scoped.ServiceProvider.GetRequiredService<IDbInitalizer>();
        await dbInitializer.Initalize();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
