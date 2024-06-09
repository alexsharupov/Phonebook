using Database.Db;
using DataBase.Http;
using Database.Models;
using DataBase.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program;
using Castle.Core.Logging;

namespace DataBase.Extension
{
    public class ApplicationRunner : IApplicationRunner
    {
        public async Task Run()
        {
        //    Console.Clear();
          //  Console.WriteLine($"Hello from {nameof(ApplicationRunner)}");
            await GetUser(ServiceProviderFactory.ServiceProvider);
        }

        private async static Task SaveUser(IUsersWork iUsersGet)
        {
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

        private async static Task<RandomUserEntity?> GetUser(IServiceProvider sp)
        {
            var ids = await sp.GetService<IUsersWork>().GetUsers(1);
            if (ids.Count == 0) await SaveUser(sp.GetService<IUsersWork>());
            else Console.WriteLine("Пользователи есть");
            return ids.FirstOrDefault();

        }
    }
}
