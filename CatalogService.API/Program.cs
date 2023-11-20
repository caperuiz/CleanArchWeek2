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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

// ConfigureServices method
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure method




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Configure Swagger to use JWT Bearer authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = "Cookies";
//    options.DefaultChallengeScheme = "oidc";
//})
//.AddCookie("Cookies")
//.AddOpenIdConnect("oidc", options =>
//{
//    options.Authority = "http://localhost:8180/auth/realms/master";
//    options.ClientId = "EngEx";
//    options.ClientSecret = "UIgpIZ39BxzghY0KFOBMKdnMRjNKLvXJ";
//    options.ResponseType = "code";
//    options.SaveTokens = true;
//    options.GetClaimsFromUserInfoEndpoint = true;
//    options.CallbackPath = "/signin-oidc";
//    options.RequireHttpsMetadata = false;
//    // Add any additional configurations based on your needs
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = "JwtBearer";
//    options.DefaultChallengeScheme = "JwtBearer";
//})
//.AddJwtBearer("JwtBearer", jwtBearerOptions =>
//{
//    jwtBearerOptions.Authority = "http://localhost:8180/auth/realms/master";
//    jwtBearerOptions.Audience = "EngEx";
//    jwtBearerOptions.RequireHttpsMetadata = false; // Change to true in production
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = "Keycloak";
//})
//   .AddCookie()
//   .AddOpenIdConnect("Keycloak", options =>
//   {
//       options.Authority = builder.Configuration["Keycloak:Authority"];
//       options.ClientId = builder.Configuration["Keycloak:ClientId"];
//       options.ClientSecret = builder.Configuration["Keycloak:ClientSecret"];
//       options.ResponseType = builder.Configuration["Keycloak:ResponseType"];
//       options.SaveTokens = true;
//       options.RequireHttpsMetadata = false;
//           options.GetClaimsFromUserInfoEndpoint = true;
//       options.CallbackPath = "/signin-oidc";


//       // Add additional configuration as needed
//       // ...

//       // Example: handle token validation
//       options.TokenValidationParameters = new TokenValidationParameters
//       {
//           ValidateIssuer = true,
//           ValidateAudience = true,
//           ValidateLifetime = true,
//           ValidateIssuerSigningKey = true,
//           ValidIssuer = builder.Configuration["Keycloak:Authority"],
//           ValidAudience = builder.Configuration["Keycloak:ClientId"]
//       };
//   });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Keycloak:Authority"];
        options.Audience = "account";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Keycloak:Authority"]
        };
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

// Configure method


app.MapControllers();

app.Run();
