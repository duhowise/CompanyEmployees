﻿using Contracts;
using Entities;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Repository;

namespace CompanyEmployees.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }


    public static void ConfigureIISIntegration(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options => { });
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddScoped<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                sqlServerOptionsAction => { sqlServerOptionsAction.MigrationsAssembly("CompanyEmployees"); }
            );
        });
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder)
    {
        return builder.AddMvcOptions(config =>
            {
                config.OutputFormatters.Add(new CsvOutputFormatter());
                config.FormatterMappings.SetMediaTypeMappingForFormat("csv", MediaTypeHeaderValue.Parse("text/csv"));
            }
        );
    }
}