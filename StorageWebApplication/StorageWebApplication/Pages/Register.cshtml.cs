using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StorageWebApplication.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public DateTime BirthDate { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("BackendApi");

            var registrationDto = new
            {
                Name = Name,
                Email = Email,
                Password = Password,
                BirthDate = BirthDate
            };

            var response = await client.PostAsJsonAsync("User", registrationDto);
            if (response.IsSuccessStatusCode)
            {
                Message = "Sikeres regisztráció!";
                return Page();
            }

            Message = "Hiba történt a regisztráció során.";
            return Page();
        }
    }
}
