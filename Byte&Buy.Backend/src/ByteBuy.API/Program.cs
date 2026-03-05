using ByteBuy.API.Authorization;
using ByteBuy.API.Extensions;
using ByteBuy.API.Middleware;
using ByteBuy.Core.Extensions;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.HangfireJobs;
using EquipmentService.API.Extensions;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

#region Service Registration & Configuration
// -----------------------------
// Api Controllers
// -----------------------------
builder.Services.AddControllers();

// -----------------------------
// Custom Services & Handlers
// -----------------------------
builder.Services
    .AddCoreLayer()
    .AddInfrastructureLayer(builder.Configuration);

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();


// -----------------------------
// Adding Hangfire
// -----------------------------
builder.Services.AddHangfire(config =>
    config
        .UsePostgreSqlStorage(bootstraper => bootstraper
            .UseNpgsqlConnection(builder.Configuration.GetConnectionString("Default"))));

builder.Services.AddHangfireServer();

// -----------------------------
// Add Identity
// -----------------------------
builder.Services.AddIdentity();
// -----------------------------
// Configure Cors Policies
// -----------------------------
builder.Services.ConfigureCors();

// -----------------------------
// Swagger & OpenApi
// -----------------------------
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -----------------------------
// Authorization & Permissions
// -----------------------------
builder.Services.AddJwtTokenAuth(builder.Configuration);
#endregion
#region Middleware Pipeline
var app = builder.Build();

// -----------------------------
// Global Error Handling
// -----------------------------
app.UseGlobalExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // -----------------------------
    // Use Swagger & OpenApi
    // -----------------------------
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
// -----------------------------
// Hangfire
// -----------------------------
app.UseHangfireDashboard("/hangfire");


RecurringJob.AddOrUpdate<RentalStatusJob>(
    "update-rental-statuses",
    job => job.Execute(),
    Cron.Daily(0, 0)
);

// -----------------------------
// Https
// -----------------------------
app.UseHttpsRedirection();

// -----------------------------
// Enable serving static files
// -----------------------------
app.UseStaticFiles();

// --------------------------------
// Cors
// --------------------------------
app.UseCors("AllowAll");

// --------------------------------
// Authentication && Authorization
// --------------------------------
app.UseAuthentication();
app.UseAuthorization();

// --------------------------------
// Map Controllers
// --------------------------------
app.MapControllers();

app.Run();
#endregion