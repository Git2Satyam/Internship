using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.BAL.Interface;
using YummyFood.DAL.Interface;
using YummyFood.Models;

namespace YummyFood.BAL.Implementation
{
    public class OrderBAL : IOrderBAL
    {
        IOrderDAL _orderDAL;
        public OrderBAL(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }

        public int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, DeliveryAddressModel address)
        {
            try
            {
                return _orderDAL.PlaceOrder(userId, orderId, paymentId, cart, address);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
