using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Rate limiting configuration
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "AutoTallerManager API", 
        Version = "v1",
        Description = "API para la gestión de un taller automotriz"
    });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        
        var result = new
        {
            Message = ex.Message,
            StatusCode = HttpStatusCode.InternalServerError
        };

        switch (ex)
        {
            case KeyNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                result = new { Message = "Recurso no encontrado", StatusCode = HttpStatusCode.NotFound };
                break;

            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                result = new { Message = "No autorizado", StatusCode = HttpStatusCode.Unauthorized };
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = new { Message = "Error interno del servidor", StatusCode = HttpStatusCode.InternalServerError };
                break;
        }

        await response.WriteAsJsonAsync(result);
    }
}
