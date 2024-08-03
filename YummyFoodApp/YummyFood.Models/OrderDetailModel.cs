using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyFood.Models
{
    public class OrderDetailModel
    {
        public string  rzp_OrderId { get; set; }  
        public string rzp_PaymentId { get; set; }
        public string rzp_Signature {  get; set; }
        public string Receipt { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }
        public DeliveryAddressModel deliveryAddress { get; set; }
        public CartModel CartModel { get; set; }
    }
}
