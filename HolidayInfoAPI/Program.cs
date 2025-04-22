using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddHttpClient();

// CORS policies for allowing frontend access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Your frontend origin
              .AllowAnyHeader()  // Allow custom headers like Authorization
              .AllowAnyMethod()  // Allow all HTTP methods (GET, POST, etc.)
              .AllowCredentials() // Allow cookies if needed
              .SetIsOriginAllowed(_ => true) // Allow all origins temporarily for debugging
              .WithExposedHeaders("Access-Control-Allow-Origin"); // Expose custom headers
    });
});


builder.Services.AddControllers();

var app = builder.Build();

// Apply CORS policies
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
