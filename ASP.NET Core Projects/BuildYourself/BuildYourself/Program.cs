using BuildYourself.DAL;
using BuildYourself.DAL.Interfaces;
using BuildYourself.DAL.Repositories;
using BuildYourself.Domain.Enities;
using BuildYourself.Service.Implementations;
using BuildYourself.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBaseRepository<FileCategory>, FileCategoryRepository>();
builder.Services.AddScoped<IFileCategoryService, FileCategoryService>();

builder.Services.AddScoped<IBaseRepository<FileItem>, FileRepository>();
builder.Services.AddScoped<IFileService, FileService>();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=File}/{action=Index}/{id?}");

app.Run();