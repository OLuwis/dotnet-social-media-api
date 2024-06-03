using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace APICommons;

public static class Extensions
{
    public static IServiceCollection AddRepository<T>(this IServiceCollection services) where T : class
    {
        return services.AddTransient<T>();
    }

    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string conn) where T : DbContext
    {
        return services.AddDbContext<T>(db => db
            .UseSqlite($"Data Source=./{conn}"));
    }

    public static IServiceCollection AddMassTransitMQ(this IServiceCollection services, bool IsProduction)
    {
        return services.AddMassTransit(c =>
        {
            c.UsingRabbitMq((context, config) =>
            {
                config.Host(IsProduction ? "rabbitmq" : "localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                config.ConfigureEndpoints(context);
            });
        });
    }

    public static IServiceCollection AddMassTransitMQ(this IServiceCollection services, bool IsProduction, Assembly[] consumers)
    {
        return services.AddMassTransit(c =>
        {
            c.AddConsumers(consumers);

            c.UsingRabbitMq((context, config) =>
            {
                config.Host(IsProduction ? "rabbitmq" : "localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                config.ConfigureEndpoints(context);
            });
        });
    }
}