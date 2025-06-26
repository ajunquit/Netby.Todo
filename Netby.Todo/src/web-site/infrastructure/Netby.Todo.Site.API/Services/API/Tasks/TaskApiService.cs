using Microsoft.Extensions.Logging;
using Netby.Todo.Site.API.Services.API.Tasks.Models;
using System.Net.Http.Json;

namespace Netby.Todo.Site.API.Services.API.Tasks
{
    public class TaskApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TaskApiService> _logger;

        public TaskApiService(
            IHttpClientFactory httpClientFactory,
            ILogger<TaskApiService> logger
            )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<TaskResponse>> GetTasksAsync(string token)
        {
            var client = GetClient();
            SetupToken(token, client);

            _logger.LogInformation("Solicitando listado de tareas al API...");

            try
            {
                var result = await client.GetFromJsonAsync<List<TaskResponse>>("Tasks");
                int count = result?.Count ?? 0;
                _logger.LogInformation("Recibidas {Count} tareas desde API.", count);
                return result ?? new List<TaskResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al solicitar las tareas al API.");
                return new List<TaskResponse>();
            }
        }

        public async Task<TaskResponse?> CreateTaskAsync(CreateTaskRequest request, string token)
        {
            var client = GetClient();
            SetupToken(token, client);

            _logger.LogInformation("Enviando solicitud para crear tarea: {@Request}", request);

            try
            {
                var response = await client.PostAsJsonAsync("Tasks", request);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<TaskResponse>();
                    _logger.LogInformation("Tarea creada exitosamente. Id: {Id}, Título: {Title}", created?.Id, created?.Title);
                    return created;
                }
                else
                {
                    _logger.LogWarning("Fallo la creación de tarea. Status: {Status}, Body: {Body}", response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la tarea.");
            }
            return null;
        }

        public async Task<TaskResponse?> UpdateTaskAsync(Guid id, EditTaskRequest request, string token)
        {
            var client = GetClient();
            SetupToken(token, client);

            _logger.LogInformation("Enviando solicitud para actualizar tarea Id: {Id}", id);

            try
            {
                var response = await client.PutAsJsonAsync($"Tasks/{id}", request);
                if (response.IsSuccessStatusCode)
                {
                    var updated = await response.Content.ReadFromJsonAsync<TaskResponse>();
                    _logger.LogInformation("Tarea actualizada exitosamente. Id: {Id}", updated?.Id);
                    return updated;
                }
                else
                {
                    _logger.LogWarning("Fallo la actualización de tarea. Id: {Id}, Status: {Status}, Body: {Body}", id, response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la tarea Id: {Id}.", id);
            }
            return null;
        }

        public async Task<bool> DeleteTaskAsync(Guid id, string token)
        {
            var client = _httpClientFactory.CreateClient("TodoApi");
            SetupToken(token, client);

            _logger.LogInformation("Enviando solicitud para eliminar tarea Id: {Id}", id);

            try
            {
                var response = await client.DeleteAsync($"Tasks/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Tarea eliminada exitosamente. Id: {Id}", id);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Fallo al eliminar tarea. Id: {Id}, Status: {Status}, Body: {Body}", id, response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la tarea Id: {Id}.", id);
            }
            return false;
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
