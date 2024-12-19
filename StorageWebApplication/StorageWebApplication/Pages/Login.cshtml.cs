using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StorageWebApplication.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("BackendApi");

            var loginDto = new
            {
                Email = Email,
                Password = Password
            };

            var response = await client.PutAsJsonAsync("User", loginDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/About"); // Átirányítás az About oldalra
            }

            Message = "Helytelen email vagy jelszó.";
            return Page(); // Hibás bejelentkezés esetén marad az aktuális oldalon
        }
    }
}
