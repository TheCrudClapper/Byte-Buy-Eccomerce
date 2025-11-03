using Byte_Buy.API.Middleware;

var builder = WebApplication.CreateBuilder(args);


// -----------------------------
// Api Controllers
// -----------------------------
builder.Services.AddControllers();

// -----------------------------
// Open Api
// -----------------------------
builder.Services.AddOpenApi();

var app = builder.Build();

// -----------------------------
// Global Error Handling
// -----------------------------
app.UseGlobalExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
