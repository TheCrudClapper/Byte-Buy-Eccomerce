using ByteBuy.API.Extensions;
using ByteBuy.API.Middleware;
using ByteBuy.Core.Extensions;
using ByteBuy.Infrastructure;
using EquipmentService.API.Extensions;

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

// -----------------------------
// Custom Services & Handlers
// -----------------------------
builder.Services.AddJwtTokenAuth(builder.Configuration);

// -----------------------------
// Add Identity
// -----------------------------
builder.Services.AddIdentity();

// -----------------------------
// Swagger & OpenApi
// -----------------------------
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
// Https
// -----------------------------
app.UseHttpsRedirection();

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