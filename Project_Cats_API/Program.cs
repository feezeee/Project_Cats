using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Project_Cats_API;
using Project_Cats_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IService, EFService>();
builder.Services.AddTransient<ICatService, CatService>();
builder.Services.AddTransient<IServiceManager, ServiceManager>();


//// внедрение зависимостей
//NinjectModule serviceModuleBL = new BLL.Infrastructure.ServiceModule();
//var kernel = new StandardKernel(serviceModulePL, serviceModuleBL);
//System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

//NinjectModule serviceModulePL = new Project_Cats_API.ServiceModule(ser);


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
