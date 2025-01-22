using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using StyleSphere.Models;
using StyleSphere.Services.Implementations;
using StyleSphere.Services.Interface;

namespace StyleSphereWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NavItemsController : ControllerBase
    {
        private readonly ILogger<NavItemsController> _logger;
        private readonly INavtItemsService _service;
        public NavItemsController(ILogger<NavItemsController> logger, INavtItemsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllNavItems()
        {
            var response = new ResponseModel();
            try
            {
                var result = _service.GetNavItems();
                if(result != null)
                {
                    response.Success = true;
                    response.Status = "Ok";
                    response.Result = result;
                }
                else
                {
                    response.Success = false;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }



        /*********************************************************************AdminNavItems********************************************************************************/


        [Authorize]
        [HttpGet]
        public IActionResult GetAdminNavItems()
        {
            var response = new ResponseModel();
            try
            {
                var result = _service.GetAdminNavItem();
                if (result != null)
                {
                    response.Success = true;
                    response.Status = "Ok";
                    response.Result = result;
                }
                else
                {
                    response.Success = false;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);  
                return BadRequest();
            }
        }
    }
}
