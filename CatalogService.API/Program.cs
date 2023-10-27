using CatalogService.Application.Implementations;
using CatalogService.Application.Interfaces;
<<<<<<< HEAD
=======
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb
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
