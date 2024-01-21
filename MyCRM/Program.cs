using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Services;
using MyCRM.Data.DbContexts;
using MyCRM.Data.Repository;
using MyCRM.Domain.Interfaces;
using MyCRM.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();


builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserService, UserServices>();



//DbContext
#region DbContext
builder.Services.AddDbContext<CrmContext>(option =>
        {
            option.UseSqlServer(builder.Configuration["ConnctionString:MyCrmConnectionString"]);
        });
#endregion DbContext

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{Controller=Home}/{action=Index}/{Id?}"
        );
});



app.Run();


