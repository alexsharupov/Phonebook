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

        public UsersGet(HttpClient httpClient, ILogger<UsersGet> logger)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            _logger = logger;
        }

        public async Task<List<RandomUserEntity>> GetUsers(int countUsers)
        {
            using var _context = new ApplicationContext();
            return _context.Users.Take(countUsers).ToList<RandomUserEntity>();
        }

        public static ResultRequest ConvertRequest(string? body, JsonSerializerOptions options) => JsonSerializer.Deserialize<ResultRequest>(body, options);

        public async Task<List<RandomUser>> SaveUsers(int countUsers = 10)
        {

            try
            {
                _logger.LogInformation($"Запрос данных");
                //Нам же не нужны лишние данные
                using var response = await _httpClient.GetAsync($"?nat=us&exc=location,email,dob,phone,cell,id,nat&results={countUsers}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Error: {response.StatusCode}");
                }
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return ConvertRequest(responseBody, _jsonSerializerOptions).results;

            }
            catch (HttpRequestException e)
            {
                _logger.LogInformation($"Request error: {e.Message}");
            }
            catch (TaskCanceledException e) {
                if (e.CancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Request was canceled.");
                }
                else
                {
                    _logger.LogInformation("Request timed out.");
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogInformation($"InvalidOperationException: {e.Message}");
            }
            return null;


        }
    }
}
