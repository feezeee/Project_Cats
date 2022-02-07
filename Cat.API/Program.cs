using BLL.Finders;
using BLL.Repository;
using BLL.Services;
using BLL.UnitOfWork;
using Cat.API.AutoMapper;
using Cat.API.Middleware;
using DAL;
using DAL.Finders;
using DAL.Repositories;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();

// Получаю строку подключения
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляю контекст
builder.Services.AddDbContext<CatContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IRepository<BLL.Entities.Cat>, Repository<BLL.Entities.Cat>>();
builder.Services.AddTransient<Finder<BLL.Entities.Cat>>();
builder.Services.AddTransient<ICatFinder, CatFinder>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ICatService, CatService>();
builder.Services.AddTransient<IRepository<BLL.Entities.Account>, Repository<BLL.Entities.Account>>();
builder.Services.AddTransient<Finder<BLL.Entities.Account>>();
builder.Services.AddTransient<IAccountFinder, AccountFinder>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddTransient<IAuthorizationService, AuthorizationService>();



builder.Services.AddAutoMapper(typeof(CatProfile), typeof(UserProfile), typeof(AuthorizationProfile));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuerSigningKey = true
    };
});

//builder.Services.AddAuthorization(builder =>
//{
//    builder.AddPolicy("default scheme", policy =>
//    {
//        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
//        policy.RequireAuthenticatedUser();
//    });
//});
var app = builder.Build();


var us = app.Services.CreateScope().ServiceProvider.GetService<IAccountService>();
app.UseMiddleware<JWTMiddleware>(us);



// Configure the HTTP request pipeline.

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
