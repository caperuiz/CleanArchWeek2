using CartingService.Messaging;
using CatalogService.Application.Implementations;
using CatalogService.Application.Interfaces;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Messaging;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories;
using CatalogService.Persistence.Repositories.Interfaces;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetSection("ConnectionString").Get<string>();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(conn));

builder.Services.AddScoped<ApplicationDbContext>(_ => new ApplicationDbContext(conn));


// Register application services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();

// Register repository interfaces
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<IValidator<Item>, CreateItemValidator>();


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
