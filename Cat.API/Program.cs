using AutoMapper;
using BLL.Entities;
using BLL.Repository;
using BLL.Services;
using Cat.API.AutoMapper;
using Cat.API.Models;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();

// Получаю строку подключения
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляю контекст
builder.Services.AddDbContext<CatContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IRepository<BLL.Entities.Cat>, CatsRepository>();
builder.Services.AddTransient<ICatService, CatService>();
builder.Services.AddAutoMapper(typeof(CatProfile));

var app = builder.Build();





// Configure the HTTP request pipeline.

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
