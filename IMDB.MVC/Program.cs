using IMDB.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using IMDB.Infrastructure.Extensions;
using IMDB.Infrastructure.Seeders;
using IMDB.Domain.Interfaces;
using IMDB.Infrastructure.Repositories;
using IMDB.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IMDBSeeder>();
    await seeder.Seed();
}
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
    pattern: "{controller=IMDB}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
