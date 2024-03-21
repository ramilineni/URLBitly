using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL;
using UserRegistration.Data;
using UserRegistration.Interface;
using UserRegistration.Repository;
using UserRegistration.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICallToSaveData, CallToSaveDataInAmelia>();
builder.Services.AddScoped<IAmeliaDBDAL, AmeliaDBDAL>();
builder.Services.AddScoped<IPageExpiration, PageExpirationRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
);
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
