using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.DTO.Response.Account;
using ModelLayer.DTO.Response.Products;
using ModelLayer.Models;
using Newtonsoft.Json;
using System.Text;

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
        public List<ProductRespone> Products { get; set; }
        [BindProperty]
        public Product Product { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("API");
            var response = await client.GetAsync("/api/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductRespone>>(json);
            }
        }
       
    }
}