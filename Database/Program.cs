
using Database.Db;
using Database.Models;
using DataBase.Extension;
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
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private const string fileConfig = "appsettings.json";

    public static async Task Main(string[] args)
    {
        var runner = ServiceProviderFactory.ServiceProvider.GetRequiredService<IApplicationRunner>();
        await runner.Run();

    }
}