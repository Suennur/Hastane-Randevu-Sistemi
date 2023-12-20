using Microsoft.EntityFrameworkCore;
using Has.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




var connection = "Server=(localdb)\\mssqllocaldb;Database=xHasData;Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<HasDataContext>(options => options.UseSqlServer(connection));



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
