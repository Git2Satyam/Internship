using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StyleSphereWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                   throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
    }
}
