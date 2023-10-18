using CatalogService.Application.Implementations;
using CatalogService.Application.Interfaces;
using CatalogService.Application.Services;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories;
using CatalogService.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetSection("ConnectionString").Get<string>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(conn));

// Register application services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();

// Register repository interfaces
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddTransient<CreateItemValidator>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
