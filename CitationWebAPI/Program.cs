using CitationWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

//builder.Services.AddAuthAuthentication(builder);

//builder.Services.AddAuthAuthorization();

builder.Services.ConfigureCors();
builder.Services.ConfigureDbContext(builder);
builder.Services.AddSwaggerGenTypes();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CitationOrigins");

//app.UseAuthentication();
//app.UseAuthorization();


app.MapControllers();

app.Run();
