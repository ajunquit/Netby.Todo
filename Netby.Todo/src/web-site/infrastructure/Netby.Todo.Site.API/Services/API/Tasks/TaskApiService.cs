using Netby.Todo.Site.API.Services.API.Tasks.Models;
using System.Net.Http.Json;

namespace Netby.Todo.Site.API.Services.API.Tasks
{
    public class TaskApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public TaskApiService(
            IHttpClientFactory httpClientFactory
            )
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<TaskResponse>> GetTasksAsync(string token)
        {
            var client = _httpClientFactory.CreateClient("TodoApi");
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var result = await client.GetFromJsonAsync<List<TaskResponse>>("Tasks");
            return result ?? new List<TaskResponse>();
        }

        public async Task<TaskResponse?> CreateTaskAsync(CreateTaskRequest request)
        {
            var client = _httpClientFactory.CreateClient("TodoApi");
            var response = await client.PostAsJsonAsync("Tasks", request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TaskResponse>();
            return null;
        }
    }

}
