using Byte_Buy.API.Middleware;
using Byte_Buy.Core.Extensions;
using Byte_Buy.Infrastructure;

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
app.UseAuthorization();

// --------------------------------
// Map Controllers
// --------------------------------
app.MapControllers();

app.Run();
#endregion