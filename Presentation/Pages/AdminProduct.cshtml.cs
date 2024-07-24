using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.DTO.Response.Account;
using ModelLayer.DTO.Response.Products;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace Presentation.Pages
{
    public class AdminProductModel : PageModel
    {
        private readonly ILogger<AdminProductModel> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public AdminProductModel(ILogger<AdminProductModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }
        
        public List<ProductRespone> Products { get; set; } = new List<ProductRespone>();
        
        public async Task OnGetAsync()
        {
            var jwtToken = HttpContext.Session.GetString("SerectKey");
            if (string.IsNullOrEmpty(jwtToken))
            {
                // Handle the case where the JWT token is not found in session
                _logger.LogWarning("JWT token not found in session.");
                return;
            }

            var client = _clientFactory.CreateClient("API");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await client.GetAsync("/api/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductRespone>>(json);
            }
            else
            {
                // Handle the case where the API call fails
                _logger.LogError($"Failed to fetch products: {response.ReasonPhrase}");
            }
        }
    }
}