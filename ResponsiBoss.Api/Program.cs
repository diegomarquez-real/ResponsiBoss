using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ResponsiBoss.Api.Services;
using ResponsiBoss.Api.Services.Abstractions;
using ResponsiBoss.Data.DependencyInjectionInfrastructure;
using ResponsiBoss.Data.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();

// Implement Dependency Injection Container
builder.Services.Init(builder.Configuration);

// Register PasswordHasher for UserProfile.
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.IPasswordHasher<UserProfile>, Microsoft.AspNetCore.Identity.PasswordHasher<UserProfile>>();

// Configure settings for IOptions injection.
builder.Services.Configure<ResponsiBoss.Api.Options.JwtBearerOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<ResponsiBoss.Api.Options.TokenOptions>(builder.Configuration.GetSection("TokenSettings"));

// Register AutoMapper.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "ResponsiBoss API",
        Version = "v1.0",
        Description = "RESTful API Built For Interfacing ResponsiBoss!"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Scheme = "Bearer",
        Description = @"JWT Authorization header using the Bearer scheme. Example: ""Authorization: Bearer {token}""",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference()
                {
                    Id = "Bearer",
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenSettings:Key"]))
    };
});

// Configure Serilog.
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();