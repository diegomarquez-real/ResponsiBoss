using Serilog;
using MudBlazor.Services;
using ResponsiBoss.Api.Client.Abstractions;
using ResponsiBoss.Api.Client.DependencyInjectionInfrastructure;
using ResponsiBoss.BlazorServerApp.Identity;
using ResponsiBoss.BlazorServerApp.Identity.Abstractions;
using ResponsiBoss.BlazorServerApp.Settings;
using ResponsiBoss.BlazorServerApp.Identity.Providers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
{
    config.Cookie.Name = "ResponsiBossBlazorServerUserLoginCookie";
    config.LoginPath = "/Login";
});

// Add Services To The Container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);

builder.Services.AddScoped<IApplicationSignInManager, ApplicationSignInManager>();
builder.Services.AddScoped<IApplicationSignOutManager, ApplicationSignOutManager>();
builder.Services.AddScoped<IUserClaimService, UserClaimService>();
builder.Services.AddScoped<IAuthTokenProvider, AuthTokenProvider>();

builder.Services.AddTransient<IApiClientSettings, ApiClientSettings>();

builder.Services.AddSingleton<CustomStorage>();

// Implement Dependency Injection Container.
builder.Services.Init(builder.Configuration);

// Configure Serilog.
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();