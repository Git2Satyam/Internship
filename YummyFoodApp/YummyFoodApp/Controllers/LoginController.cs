using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using UAParser;
using YummyFood.BAL.Interface;
using YummyFood.DAL.Interface;
using YummyFood.Models;

namespace YummyFoodApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginBAL _loginBAL;
        ILogger _logger;
        public LoginController(ILoginBAL loginBAL, ILogger<LoginController> logger)
        {
            _loginBAL = loginBAL;
            _logger = logger;
        }

        //[HttpPost]
        //public IActionResult VerifyUser(string username, string password)
        //{
        //    try
        //    {
        //        var result = _loginBAL.VerifyUser(username, password);
        //        if (result != null)
        //        {
        //            return Ok(new { Message = "Login Successful!" });
        //        }
        //        else
        //        {
        //            return NotFound(new { Message = "User not found!" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message, ex);
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost]
        public IActionResult AuthUser([FromBody] LoginModel model) // same method
        {
            try
            {
                model.Data = GetIPAddress();
                if (model == null)
                   return BadRequest();
                   var user = _loginBAL.VerifyUser(model.UserName, model.Password,model.Data);
                if (user == null)
                    return NotFound(new { Message = "User Not Found!" });
                return Ok(new
                {
                    Message = "Login Success!"
                }); ;
                
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        
        protected Tuple<string,string> GetIPAddress()
        {
            
            string browser;
            string ipAddress;
            var userAgent = HttpContext.Request.Headers.UserAgent;
            var ua_parser = Parser.GetDefault();
            var client = ua_parser.Parse(userAgent);
            browser = client.UA.Family + " " + client.OS + " " + client.Device + " " + client.UA.Major + " " + client.UA.Minor;
            ipAddress = Response.HttpContext.Connection.RemoteIpAddress.ToString();

            if(ipAddress.Trim() == "::1")
            {
                var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList(); // here we get both IPAddress(v4 and v6)
                ipAddress = string.Format("IPv6: {0}, IPv4: {1}", ip[0], ip[1]);
                //foreach(IPAddress address in ip)
                //{
                //    if(address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) // this is used for IPv4
                //    {
                //        var Ip4 = address.ToString();
                //    }
                //    else if(address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)  // this is used for IPv6
                //    {
                //        ipAddress = address.ToString();
                //    }
                //}

            }
            return Tuple.Create(ipAddress, browser);
        }

    }
}
