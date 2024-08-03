using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyFood.BAL.Interface;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFoodApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
       // public Guid id { get; set; }
        ICartBAL _cartBAL;
        ILogger _logger;
        public CartController(ICartBAL cartBAL, ILogger<CartController> logger)
        {
            _cartBAL = cartBAL;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetProductDetailByCartItem(int id)
        {
            var response = new ResponseModel();
            try
            {
                var result = _cartBAL.GetProductDetailForCart(id);
                if (result != null)
                {
                    response.Success = true;
                    response.Status = "ok";
                    response.Result = result;
                }
                else
                {
                    response.Success = false;
                    response.Status = "Fail";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddItemToCart(CartModel model)
        {
            var response = new ResponseModel();
            try
            {
                var result = _cartBAL.AddItemToCart(CartId, model);
                if (result != null)
                {
                    response.Success = true;
                    response.Status = "Ok";
                    response.Result = result;
                }
                else
                {
                    response.Success = false;
                    response.Status = "fail";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetCartDetail(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var cartDetail = _cartBAL.GetCartDetails(id);
                if (cartDetail != null)
                {
                    response.Success = true;
                    response.Status = "ok";
                    response.Result = cartDetail;
                }
                else
                {
                    response.Success = false;
                    response.Status = "failed";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        Guid CartId             // cartid we will consider as guid
        {
            get
            {
                Guid Id; // this guid Id will store in cookie bcz if cart is already created then every time it will not create
                Id = Guid.NewGuid();
                return Id;
            }
        }

        [HttpDelete]
        public IActionResult DeleteItemFromCart(int itemId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var result = _cartBAL.DeleteItemFromCart(itemId);
                if(result == true)
                {
                    response.Success = true;
                    response.Status = "ok";
                    response.Result = result;
                }
                else
                {
                    response.Success = false;
                    response.Status = "Failed";
                    response.Result = result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult UpdateQuantity(int quantity, int itemId, int userId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var result = _cartBAL.UpdateQuantity(quantity, itemId, userId);
                if(result == null)
                {
                    response.Success = false;
                    response.Status = "Failed";
                }
                {
                    response.Success = true;
                    response.Status = "OK";
                    response.Result = result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult RemoveOrderdItemFromCart(int userId)
        {
            try
            {
                _cartBAL.RemoveOrderdItemFromCart(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetCartItemCount(int userId)
        {
            try
            {
                var result = _cartBAL.GetCartItemCount(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
               _logger.LogError($"{ex.Message}");
                return BadRequest(ex.Message);  
            }
        }
        //[HttpGet]
        //public IActionResult SendGuidId()
        //{
        //    ResponseModel response = new ResponseModel();
        //    try
        //    {
        //        if (id == Guid.Empty)
        //        {
        //            response.Success = false;
        //        }
        //        else
        //        {
        //            response.Success = true;
        //            response.Result = id.ToString();
        //        }
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

    }
}
