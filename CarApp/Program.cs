using CarApp.Core.Services;
using CarApp.Core.Services.Contracts;
using CarApp.Infrastructure.Data;
using CarApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);
string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;
string adminUsername = builder.Configuration.GetValue<string>("Administrator:Email")!;
string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!;
string adminFName = builder.Configuration.GetValue<string>("Administrator:FirstName")!;
string adminLName = builder.Configuration.GetValue<string>("Administrator:LastName")!;

builder.Services.AddMvc()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.FormInputRenderMode = FormInputRenderMode.AlwaysUseCurrentCulture;
    });

builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

await app.SeedAdministrator(adminEmail, adminUsername, adminPassword, adminFName, adminLName);

app.MapRazorPages();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<IBrandAndModelSeedService>();
    await seedService.SeedBrandsAndModelsFromJson();
}

await app.RunAsync();
