using TradeApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Swagger Services
builder.Services.AddEndpointsApiExplorer(); // Discovers endpoints
builder.Services.AddSwaggerGen();            // Generates Swagger specification

var app = builder.Build();

// 2. Enable Swagger Middleware (Usually just for development mode)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Generates the underlying JSON (/swagger/v1/swagger.json)
    app.UseSwaggerUI(); // Renders the interactive visual dashboard (/swagger/index.html)
}

app.MapTradesEndpoints();
app.MapGet("/health", () => "App running fine!!");
app.MapGet("/", () => "Welcome!!");

app.Run();
