using CitationWebAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Note: "RemoteConnection" to use remote database (on App Harbor)
         "LocalConnection" to use local database 
         Registers context using dependency injection
*/
builder.Services.AddDbContext<CitationContext>(options =>
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
