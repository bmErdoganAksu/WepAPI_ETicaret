using Business.Abstract;
using Business.Concrete;
using Core.Helpers;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IComputerDal, ComputerDal>();
builder.Services.AddScoped<IComputerService, ComputerService>();
builder.Services.AddSingleton<N11Scraper>();
builder.Services.AddSingleton<TeknosaScraper>();
builder.Services.AddSingleton<EpeyScraper>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("*").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
