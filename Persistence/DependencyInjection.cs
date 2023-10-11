using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Persistence.Wrappers;
using MovieTicketer.Services;
using System.Reflection;

namespace MovieTicketer.Persistence;

public static class DependencyInjection
{
  internal const string DefaultConnectionStringName = "Default";

  public static void AddInfrastructureServices(this IServiceCollection services)
  {
    services.ConfigurePersistence();

    services.AddWrappers();

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    services.AddEntityServices();
  }

  private static void ConfigurePersistence(this IServiceCollection services)
  {
    services.AddInterceptors();

    services.AddDbContext<MovieTicketerDbContext>((provider, options) =>
    {
      options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")).UseSnakeCaseNamingConvention();
    });
  }

  private static void AddWrappers(this IServiceCollection services) =>
      services.AddSingleton<IDateTimeOffsetWrapper, DateTimeOffsetWrapper>();

  public static void AddEntityServices(this IServiceCollection services)
  {
    Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(t =>
      t.IsClass &&
      t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMovieTicketerService<>)))
    .ToList()
    .ForEach(s =>
    {
      services.AddScoped(s.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMovieTicketerService<>)), s);
    });
  }

  public static void AddInterceptors(this IServiceCollection services)
  {
    Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(t =>
      t.IsClass &&
      t.GetInterfaces().Any(i => i == typeof(IInterceptor)))
    .ToList()
    .ForEach(s =>
    {
      services.AddScoped(typeof(IInterceptor), s);
    });
  }
}
