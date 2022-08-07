using Microsoft.EntityFrameworkCore;
using prjChuju.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<dbChujuContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbChujuConnection"));
});

builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Activity}/{action=Index}"
    );

app.MapRazorPages();

app.Run();



//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();