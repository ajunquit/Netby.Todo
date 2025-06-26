using Netby.Todo.Site.API.Services.API.Auth.Models;
using System.Net.Http.Json;

namespace Netby.Todo.Site.API.Services.API.Auth
{
    public class AuthApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public string? ErrorMessage { get; set; }

        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<LoginResponse?> OnPostAsync(LoginRequest loginRequest)
        {
            var client = _httpClientFactory.CreateClient("TodoApi");
            var response = await client.PostAsJsonAsync("Auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            return null;
        }
    }
}
