using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyFood.BAL.Interface;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFoodApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        ILogger _logger;
        IConfiguration _config;
        IPaymentBAL _paymentBAL;
        IOrderBAL _orderBAL;
        public PaymentController(ILogger<PaymentController> logger, IConfiguration config, IPaymentBAL paymentBAL,IOrderBAL orderBAL)
        {
            _logger = logger;
            _config = config;
            _paymentBAL = paymentBAL;
            _orderBAL = orderBAL;
        }

        [HttpPost]
        public IActionResult CreateOrder(CartModel cart)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                PaymentModel payment = new PaymentModel();
                if (cart != null)
                {
                    payment.Cart = cart;
                    payment.GrandTotal = cart.GrandTotal;
                    payment.Currency = "INR";
                    payment.Description = string.Join(",", cart.products.Select(p => p.Name));
                    payment.RazorpayKey = _config["Razorpay:Key"];
                    payment.Receipt = Guid.NewGuid().ToString();
                    payment.OrderId = _paymentBAL.CreateOrder(payment.GrandTotal * 100, payment.Currency, payment.Receipt);
                }
                if(payment.OrderId != null)
                {
                    response.Success = true;
                    response.Status = "ok";
                    response.Result = payment;
                }
                else
                {
                    response.Success = false;
                    response.Status = "failed";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
            return Ok(response);
            

        }

        [HttpPost]
        public IActionResult LogOrderAndPaymentDetailData(OrderDetailModel orderML)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if(orderML != null && orderML.rzp_PaymentId != null)
                {
                    string paymentId = orderML.rzp_PaymentId;
                    string orderId = orderML.rzp_OrderId;
                    string signature = orderML.rzp_Signature;
                    string transcationId = orderML.Receipt;
                    string currency = orderML.Currency;
                    var payment = _paymentBAL.GetPaymentDetail(paymentId); // here we get payment detail at the razorpay level to ensure the safety from hackers
                    var isSignVerified = _paymentBAL.IsSignatureVerified(signature,orderId, paymentId); // also verified the signature
                    if(isSignVerified && payment != null)
                    {
                        PaymentDetail model = new PaymentDetail();
                        model.CartId = orderML.CartModel.Id;
                        model.Total = orderML.CartModel.TotalPrice;
                        model.Tax = orderML.CartModel.Tax;
                        model.GrandTotal = orderML.CartModel.GrandTotal;

                        model.Status = payment.Attributes["status"];
                        model.TransactionId = transcationId;
                        model.Currency = payment.Attributes["currency"];
                        model.Email = payment.Attributes["email"];
                        model.Id = paymentId;
                        model.UserId = orderML.UserId;
                        model.CreatedDate = DateTime.Now;

                        int status = _paymentBAL.SavePaymentDetail(model);
                        if(status > 0)
                        {
                            _orderBAL.PlaceOrder(orderML.UserId, orderId, paymentId, orderML.CartModel, orderML.deliveryAddress);
                            response.Success = true;
                            response.Status = "OK";
                        }
                        else
                        {
                            response.Success = false;
                            response.Status = "Failed";
                            response.Result = "Although, due to some technical issues it's not get updated in our side." +
                                              " We will contact you soon..";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Status = "Failed";
                        response.Result = "Paymet Failed!";
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }
    }
}
