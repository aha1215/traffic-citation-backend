using CitationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Text.RegularExpressions;

namespace CitationWebAPI
{
    /**
     * Services used in Program.cs
     */
    public static class ExtensionServices
    {
        // 'this' modifier indicated to compiler this is an extension method
        public static void ConfigureDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<DataContext>(options =>
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
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy(name: "CitationOrigins", policy => policy
            .WithOrigins("http://localhost:4200", "http://localhost:8080", "https://traffic-citation-frontend.herokuapp.com")
            .AllowAnyMethod()
            .AllowAnyHeader()));
        }

        public static void AddSwaggerGenTypes(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
                options.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                    Example = new OpenApiString("2022-10-05")
                })
            );

            services.AddSwaggerGen(options =>
                options.MapType<TimeOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Format = "time",
                    Example = new OpenApiString("23:15:00")
                })
            );
        }
    }
}
