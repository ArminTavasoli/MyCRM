using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Services;
using MyCRM.Data.DbContexts;
using MyCRM.Data.Repository;
using MyCRM.Domain.Interfaces;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

//Dependensi Injection
#region Dependensi Injection
//User Repository
builder.Services.AddTransient<IUserRepository, UserRepository>();

//User Service
builder.Services.AddTransient<IUserService, UserServices>();

//Order Repository
builder.Services.AddTransient<IOrderRepository , OrderRepository>();

//Order Service
builder.Services.TryAddTransient<IOrderService , OrderService>();
#endregion

//Encoder
#region Encoder
builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
#endregion

//DbContext
#region DbContext
builder.Services.AddDbContext<CrmContext>(option =>
        {
            option.UseSqlServer(builder.Configuration["ConnctionString:MyCrmConnectionString"]);
        });
#endregion DbContext


//Athentication
#region Athentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(143200);
});
#endregion



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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{Controller=Home}/{action=Index}/{Id?}"
        );
});



app.Run();


