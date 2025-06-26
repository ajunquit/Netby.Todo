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
            var client = GetClient();
            SetupToken(token, client);

            var result = await client.GetFromJsonAsync<List<TaskResponse>>("Tasks");
            return result ?? new List<TaskResponse>();
        }

        public async Task<TaskResponse?> CreateTaskAsync(CreateTaskRequest request, string token)
        {
            var client = GetClient();
            SetupToken(token, client);
            var response = await client.PostAsJsonAsync("Tasks", request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TaskResponse>();
            return null;
        }

        public async Task<TaskResponse?> UpdateTaskAsync(Guid id, EditTaskRequest request, string token)
        {
            var client = GetClient();
            SetupToken(token, client);
            var response = await client.PutAsJsonAsync($"Tasks/{id}", request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TaskResponse>();
            return null;
        }

        public async Task<bool> DeleteTaskAsync(Guid id, string token)
        {
            var client = _httpClientFactory.CreateClient("TodoApi");
            SetupToken(token, client);
            var response = await client.DeleteAsync($"Tasks/{id}");
            return response.IsSuccessStatusCode;
        }


        private HttpClient GetClient()
        {
            return _httpClientFactory.CreateClient("TodoApi");
        }

        private static void SetupToken(string token, HttpClient client)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }

}
