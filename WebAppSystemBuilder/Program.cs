using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppSystemBuilder;
using WebAppSystemBuilder.Models;
using WebAppSystemBuilder.Services.hw_items;
using WebAppSystemBuilder.Services.hw_params;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("BuilderDbConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = "_AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
});

// items services
builder.Services.AddScoped<ProcessorService>();
builder.Services.AddScoped<MemoryService>();
builder.Services.AddScoped<MotherboardService>();
builder.Services.AddScoped<GraphicsCardService>();
//

// param services
builder.Services.AddScoped<CPUSocketService>();
builder.Services.AddScoped<ChipsetService>();
builder.Services.AddScoped<RamTypeService>();
builder.Services.AddScoped<MemoryModuleTypeService>();
builder.Services.AddScoped<GraphicsBaseService>();
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "hw_params",
    areaName: "hw_params",
    pattern: "Hardware/Parameters/{controller=Home}/{action=Index}/{id?}"
    );
app.MapAreaControllerRoute(
    name: "hw_items",
    areaName: "hw_items",
    pattern: "Hardware/Items/{controller=Home}/{action=Index}/{id?}"
    );
// area controller routes MUST BE above default map controller route!!!
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
