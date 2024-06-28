using DataAccessLayer.Repositorys;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly WebCoustemClothingContext _context;
        public ProductController(IProductRepository productRepository, WebCoustemClothingContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }
    }
}
