using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StyleSphere.Models;
using StyleSphere.Services.Interface;
using System.Drawing.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StyleSphereWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(ILogger<UserController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult VerifyUser(string username, string password)
        {
            var response = new ResponseModel();
            try
            {
                var result = _userService.VerifyUser(username, password);
                if(result.Id == 0)
                {
                    response.Success = false;
                    response.Status = "Failed";
                }
                else
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                        new Claim(ClaimTypes.Email, result.Email)
                    };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var Sectoken = new JwtSecurityToken(_configuration["JWT:Issuer"],
                      _configuration["JWT:Issuer"],
                      claims: claims,
                      expires: DateTime.Now.AddMinutes(60),
                      signingCredentials: credentials);

                    response.Success = true;
                    response.Status = "Ok";
                    response.Result = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult InsertOrUpdateUser(UserModel model)
        {
            var response = new ResponseModel();
            try
            {
                var result = _userService.InsertOrUpdateUser(model);
                if(result == 0 || result == 1)
                {
                    response.Success = true;
                    response.Status = "Ok";
                }
                else
                {
                    response.Success= false;
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
