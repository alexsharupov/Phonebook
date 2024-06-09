using DataBase.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;

public static class ServiceWorker {
    private static IServiceCollection SetServiceHttp(this IServiceCollection serviceProvider, Uri baseUrl)
    {

        serviceProvider.AddHttpClient<IUsersWork, UsersGet>(client => {
            client.BaseAddress = baseUrl;
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
        });
        return serviceProvider;
    }

    public static void ConfigureHttpServices(IServiceCollection serviceProvider, Uri baseUrl)
    {
        serviceProvider.SetServiceHttp(baseUrl);
    }
    public static void ConfigureLoggingServices(IServiceCollection serviceProvider, IConfigurationSection logging)
    {
        serviceProvider.AddLogging(c => c.ClearProviders().AddConsole().AddConfiguration(logging));
    }
}