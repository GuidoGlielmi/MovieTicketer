using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Persistence.Interceptors;
using MovieTicketer.Persistence.Wrappers;
using System.Reflection;

namespace MovieTicketer.Persistence;

public static class DependencyInjection
{
  internal const string DefaultConnectionStringName = "Default";

  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
      IConfiguration configuration)
  {
    services.ConfigurePersistence(configuration);

    services.AddWrappers();

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    return services;
  }

  private static void ConfigurePersistence(this IServiceCollection services,
      IConfiguration configuration)
  {
    services.AddScoped<ISaveChangesInterceptor, AuditSaveChangesInterceptor>();

    services.AddDbContext<MovieTicketerDbContext>((provider, options) =>
    {
      options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());

      options.UseNpgsql(configuration.GetConnectionString(DefaultConnectionStringName));
    });

    services.AddScoped(provider => provider.GetRequiredService<MovieTicketerDbContext>());
  }

  private static void AddWrappers(this IServiceCollection services) =>
      services.AddSingleton<IDateTimeOffsetWrapper, DateTimeOffsetWrapper>();
}
