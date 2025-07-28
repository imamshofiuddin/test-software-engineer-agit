using Microsoft.EntityFrameworkCore;
using ProductionPlanner.DB;
using ProductionPlanner.Services;
using ProductionPlanner.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductionPlanService, ProductionPlanService>();
builder.Services.AddDbContext<ProductionPlannerDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionStrings"))
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
    pattern: "{controller=ProductionPlan}/{action=Index}/{id?}");

app.Run();
