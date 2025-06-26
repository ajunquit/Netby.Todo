using Netby.Todo.Site.API.Services.API.Models;
using System.Net.Http.Json;

namespace Netby.Todo.Site.API.Services.API
{
    public class TaskApiService
    {
        private IHttpClientFactory _httpClientFactory;

        public TaskApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<TaskResponse>> GetTasksAsync()
        {
            var client = _httpClientFactory.CreateClient("TodoApi");
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
