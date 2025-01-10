using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleSphere.Models;
using StyleSphere.Services.Interface;

namespace StyleSphereWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;
        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var response = new ResponseModel();
            try
            {
                 var result = _productService.GetAllProducts();
                if(result.Count() > 0)
                {
                    response.Success = true;
                    response.Status = "Ok";
                    response.Result = result;
                }
                else 
                { 
                    response.Success = false;
                    response.Status = "Failed";
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
    }
}
