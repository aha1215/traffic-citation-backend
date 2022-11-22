using System.Security.Claims;
using CitationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            .WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:8080", "https://localhost:8080", 
            "https://traffic-citation-frontend.herokuapp.com", "https://localhost:7190", "http://localhost:7190")
            .AllowAnyMethod()
            .AllowAnyHeader()));
        }

        public static void AddAuthAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-3k36-3cg.us.auth0.com/";
                    options.Audience = "https://traffic-citation-backend.herokuapp.com/api";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "Roles",
                        RoleClaimType = "dev-3k36-3cg.us.auth0.com/roles"
                    };
                });
        }

        /*public static void AddAuthAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read", policy => policy.RequireClaim("permissions", "read:citations"));
                options.AddPolicy("write", policy => policy.RequireClaim("permissions", "read:citations", "write:citations"));
                options.AddPolicy("write-delete", policy => policy.RequireClaim("permissions", "read:citations", "write:citations", "delete:citations"));
                options.AddPolicy("read-all", policy => policy.RequireClaim("permissions", "read:all-citations"));
            });
        }*/

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
