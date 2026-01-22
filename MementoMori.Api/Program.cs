using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MementoMori API",
        Version = "v1",
        Description = "MementoMori Game Helper REST API"
    });
});

// Add SignalR
builder.Services.AddSignalR();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add HttpClient
builder.Services.AddHttpClient();

// Register infrastructure services
builder.Services.AddSingleton<MementoMori.Api.Infrastructure.NetworkManager>();

// Register Ortega proxy services
builder.Services.AddSingleton<MementoMori.Api.Services.OrtegaApiDiscoveryService>();
builder.Services.AddScoped<MementoMori.Api.Infrastructure.OrtegaInvoker>();

// Register account manager (Singleton)
builder.Services.AddSingleton<MementoMori.Api.Services.AccountManager>();

// Register application services
builder.Services.AddScoped<MementoMori.Api.Services.AccountCredentialService>();
builder.Services.AddScoped<MementoMori.Api.Services.AuthService>();
builder.Services.AddScoped<MementoMori.Api.Services.AccountService>();
builder.Services.AddScoped<MementoMori.Api.Services.MissionService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Add User ID authentication middleware
app.UseMiddleware<MementoMori.Api.Middleware.UserIdAuthenticationMiddleware>();

app.UseAuthorization();
app.MapControllers();

app.Run();
