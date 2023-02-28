using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TebanRM.Application.Identity;
using TebanRM.Application.Options;

namespace TebanRM.Application;
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<SymmetricKeyOptions>(builder.Configuration.GetSection("SymmetricKeyOptions"));
        services.AddSingleton<SymmetricKeyService>();
        return services;
    }
}
