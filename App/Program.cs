using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();

builder.Services.AddDataProtection()
    .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect("127.0.0.1:6379"))
    .SetApplicationName("unique");

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

var app = builder.Build();

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapRazorPages();

app.MapGet("/", () => "Hello world");

app.MapGet("/protected", () => "secret!")
    .RequireAuthorization();

app.Run();
