using DataBase.Http.Exception;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Database.Db;
using Microsoft.Extensions.Logging;

namespace DataBase.Http
{
    class ResultRequest {
        public List<RandomUser> results { get; set; }
    }

    internal class UsersGet :IUsersWork
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger _logger;

        public UsersGet(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
         //   _logger = logger;
        }

        public async Task<List<RandomUserEntity>> GetUsers(int countUsers)
        {
            using var _context = new ApplicationContext();
            return _context.Users.Take(countUsers).ToList<RandomUserEntity>();
        }

        public async Task<List<RandomUser>> SaveUsers(int countUsers = 10)
        {

            try
            {
                using var response = await _httpClient.GetAsync($"?nat=us&results={countUsers}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<ResultRequest>(responseBody, _jsonSerializerOptions);

                return users.results;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
              //  throw new HttpUserRequestException(e);
            }
            catch (TaskCanceledException e) {
                if (e.CancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Request was canceled.");
                }
                else
                {
                    Console.WriteLine("Request timed out.");
                }
            }
            catch (InvalidOperationException e)
            {

            }
            return null;


        }
    }
}
