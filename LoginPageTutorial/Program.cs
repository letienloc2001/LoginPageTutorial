using LoginPageTutorial.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

bool testApi = false;

if (!testApi)
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<MyDbContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));
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
        pattern: "{controller=Home}/{action=Signin}");

    app.Run();
} 
else 
{
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<MyDbContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));

    var app = builder.Build();

    //Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()) 
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}