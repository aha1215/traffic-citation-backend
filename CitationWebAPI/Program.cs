using CitationWebAPI.Converters;
using CitationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using DateOnlyJsonConverter = CitationWebAPI.Converters.DateOnlyJsonConverter;
using TimeOnlyJsonConverter = CitationWebAPI.Converters.TimeOnlyJsonConverter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(
    options =>
    {
        options.UseDateOnlyTimeOnlyStringConverters();
    }).AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });


//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Note: "RemoteConnection" to use remote database (on App Harbor)
         "LocalConnection" to use local database 
         Registers context using dependency injection
*/
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

// Use same origin as angular app (can add more later)
builder.Services.AddCors(opt => opt.AddPolicy(name: "CitationOrigins",
    policy => policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()));

/*builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());*/

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
