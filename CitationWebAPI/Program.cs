using CitationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

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

/* Note: "RemoteConnection" to use remote database (on App Harbor)
         "LocalConnection" to use local database 
         Registers context using dependency injection
*/
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

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
