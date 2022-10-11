using CitationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

builder.Services.AddSwaggerGen(options =>
    options.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("2022-10-05")
    })
);

builder.Services.AddSwaggerGen(options =>
    options.MapType<TimeOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "time",
        Example = new OpenApiString("23:15:00")
    })
);


builder.Services.AddDbContext<DataContext>(options =>
{
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
    {
        var m = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
        options.UseNpgsql($"Server={m.Groups[3]};Port={m.Groups[4]};User Id={m.Groups[1]};Password={m.Groups[2]};Database={m.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
    }
    else // In Development Environment
    {
        // So, use a local Connection
        options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection"));
    }
});

// Use same origin as angular app (can add more later)
builder.Services.AddCors(opt => opt.AddPolicy(
    name: "CitationOrigins", policy => policy
    .WithOrigins("http://localhost:4200", "https://traffic-citation-frontend.herokuapp.com")
    .AllowAnyMethod()
    .AllowAnyHeader())); 
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CitationOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
