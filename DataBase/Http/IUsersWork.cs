using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Http
{
    internal interface IUsersWork
    {
        public Task<List<RandomUser>> SaveUsers(int countUsers);
        public Task<List<RandomUserEntity>> GetUsers(int countUsers);
    }
}
