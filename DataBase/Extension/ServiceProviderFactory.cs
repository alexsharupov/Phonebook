using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Extension
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; }

        static ServiceProviderFactory()
        {
            var args = new string[0];
            var config = CreateConfigurationBuilder(args).Build();
            var serviceProvider = CreateDIContainer(config).BuildServiceProvider();
            ServiceProvider = serviceProvider;
        }
        private static IConfigurationBuilder CreateConfigurationBuilder(string[] args)
        {
            return new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).AddJsonFile("appSettings.json", false, false);
        }
        private static IServiceCollection CreateDIContainer(IConfigurationRoot config)
        {
            return new ServiceCollection().UseStartup<Startup>(config);
        }
    }

}
