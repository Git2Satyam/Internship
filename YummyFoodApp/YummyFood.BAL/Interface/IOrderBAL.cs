using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Models;

namespace YummyFood.BAL.Interface
{
    public interface IOrderBAL
    {
        int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, DeliveryAddressModel address);
    }
}
