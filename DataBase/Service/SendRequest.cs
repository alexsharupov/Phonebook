using Database.Models;
using DataBase.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Service
{
    internal class SendRequest
    {
        IUsersWork _httpClient;
        public SendRequest(IUsersWork httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<RandomUser>> Send(int count = 10)
        {
            return await _httpClient.SaveUsers(count);
        }
    }
}
