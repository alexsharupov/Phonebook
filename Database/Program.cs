
using Database.Db;
using Database.Models;
using DataBase.Http;
using DataBase.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private const string fileConfig = "appsettings.json";


    public static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder().AddJsonFile(Program.fileConfig).Build();
        //Не стал проверять значения
        var serviceProvider = new ServiceCollection();
        ServiceWorker.ConfigureHttpServices(serviceProvider, new Uri(config.GetSection("ApiUrl").Value));
        ServiceWorker.ConfigureLoggingServices(serviceProvider, config?.GetSection("Logging"));
        var sp = serviceProvider.BuildServiceProvider();
        await GetUser(sp);
        Console.WriteLine("Hello, World!");
    }
    private static async Task RunAsync()
    {

        //...

    }

    private async static Task SaveUser(IUsersWork iUsersGet) { 
        var users = await new SendRequest(httpClient: iUsersGet).Send(1000);
        if (users == null) return; 
        var list = users?.ConvertAll(item => new RandomUserEntity(item));
        using var _context = new ApplicationContext();
        //Тут куча операций по вставке вычислению и подобному
        using var dbContextTransaction = _context.Database.BeginTransaction();
        await _context.Users.AddRangeAsync(list);
        await _context.SaveChangesAsync();
        dbContextTransaction.Commit();
    }

    private async static Task<RandomUserEntity?> GetUser(ServiceProvider sp)
    {
        var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        var ids = await sp.GetService<IUsersWork>().GetUsers(1);
        if (ids.Count == 0) await SaveUser(sp.GetService<IUsersWork>());
        else Console.WriteLine("Пользователи есть");
        return ids.FirstOrDefault();

    }

}