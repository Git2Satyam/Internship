using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.DAL.Interface;
using YummyFood.Models;

namespace YummyFood.DAL.Implementation
{
    public class OrderDAL : IOrderDAL
    {
        IDataAccessLayer<Order> _orderDAL;
        public OrderDAL(IDataAccessLayer<Order> orderDAL)
        {
            _orderDAL = orderDAL;
        }
        public int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, DeliveryAddressModel address)
        {
            Order order = new Order()
            {
                UserId = userId,
                Id = orderId,
                PaymentId = paymentId,
                Address = address.Address,
                State = address.State,
                Country = address.Country,
                ZipCode = address.Zipcode,
                PhoneNumber = address.PhoneNumber,
                CreatedDate = DateTime.Now,
            };
            foreach (var item in cart.products)
            {
                OrderItem orderItm = new OrderItem()
                {
                    ItemId = item.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.TotalPrice,
                };
                order.OrderItems.Add(orderItm);
            }
            _orderDAL.Add(order);
           return _orderDAL.saveChanges();
        }
    }
}
