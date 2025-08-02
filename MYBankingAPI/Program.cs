using BankingWebAPI.DAL;
using BankingWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using BankingWebAPI.BLL.Interface;
using BankingWebAPI.BLL.Service;
using BankingWebAPI.BLL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BankingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BMBSQLConnectionString")));

//Add Dependency Injection for BLL and DAL
//Add Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();

//Add Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

// Set the port from environment variable or default to 5000
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");





// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Other services
builder.Services.AddControllers();
builder.Services.AddDbContext<BankingDbContext>(/* options */);

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
