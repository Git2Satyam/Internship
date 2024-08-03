using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyFood.BAL.Interface;
using YummyFood.Models;

namespace YummyFoodApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBAL _userBAL;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserBAL userBAL, ILogger<UserController> logger)
        {
            _userBAL = userBAL;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                var result = _userBAL.GetAllUser();
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return new JsonResult(new {Success = "false"});
                }
            }
            catch(Exception ex)
            {
               _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult SaveUser([FromBody] UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = _userBAL.InsertOrUpdateUser(model);
                    if(id > 0)
                    {
                        return Ok(id);
                    }
                    else
                    {
                        return new JsonResult(new { Success = "False" });
                    }
                }
                else
                {
                    var msg = "Invalid model input";
                    return new JsonResult(new { Success = "False", Message = msg });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                return BadRequest(ex);
            }
         
        }

        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var getUser = _userBAL.GetUserById(id);
                if (getUser != null)
                {
                    return Ok(getUser);
                }
                else
                {
                    return  StatusCode(404);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userBAL.DeleteUser(id);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

    }
}
