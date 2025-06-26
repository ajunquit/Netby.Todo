using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netby.Todo.Site.API.Services.API.Auth;
using Netby.Todo.Site.API.Services.API.Auth.Models;

namespace Netby.Todo.Site.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthApiService _authApiService;

        [BindProperty]
        public LoginRequest LoginData { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public LoginModel(
            IHttpContextAccessor contextAccessor,
            AuthApiService authApiService)
        {
            _contextAccessor = contextAccessor;
            _authApiService = authApiService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var loginResponse = await _authApiService.OnPostAsync(LoginData);
            if (loginResponse is not null)
            {
                _contextAccessor.HttpContext?.Session.SetString("JwtToken", loginResponse?.Token ?? "");
                return RedirectToPage("/Tasks/Index");
            }

            ErrorMessage = "Invalid credentials";
            return Page();
        }
    }
}
