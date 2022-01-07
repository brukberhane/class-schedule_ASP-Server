using System;
using HiLCoECS.Models;
using HiLCoECS.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ? Fetch the strings from appsettings.json
builder.Services.Configure<ScheduleDatabaseSettings>(
     builder.Configuration.GetSection(nameof(ScheduleDatabaseSettings))
);
// ? Adding runtime compilation and the Update page.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// ? Add the mongodb service and initialize it.
builder.Services.AddSingleton<IScheduleDatabaseSettings>(sp => 
    sp.GetRequiredService<IOptions<ScheduleDatabaseSettings>>().Value);
builder.Services.AddSingleton<ScheduleService>();
builder.Services.AddControllersWithViews();

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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Schedule}/{action=Get}/");

app.Run();

