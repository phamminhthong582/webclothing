using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ModelLayer.DTO.Response.Products;
using Newtonsoft.Json;

namespace Presentation.Pages.Product
{
    public class HomePage : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IndexModel> _logger;

        public HomePage(IHttpClientFactory clientFactory, ILogger<IndexModel> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public IList<ProductRespone> Products { get; set; } = new List<ProductRespone>();

        public async Task OnGetAsync()
        {
            var jwtToken = HttpContext.Session.GetString("SerectKey");
            if (string.IsNullOrEmpty(jwtToken))
            {
                _logger.LogWarning("JWT token not found in session.");
                return;
            }

            var client = _clientFactory.CreateClient("API");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await client.GetAsync("/api/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received JSON: " + json);
                Products = JsonConvert.DeserializeObject<List<ProductRespone>>(json);
                _logger.LogInformation($"Deserialized Products: {Products.Count} items");
            }
            else
            {
                _logger.LogError($"Failed to fetch products: {response.ReasonPhrase}");
            }
        }
    }
}