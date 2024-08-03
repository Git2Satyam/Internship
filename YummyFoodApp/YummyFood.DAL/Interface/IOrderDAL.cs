using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Models;

namespace YummyFood.DAL.Interface
{
    public interface IOrderDAL
    {
       int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, DeliveryAddressModel address);

    }
}
