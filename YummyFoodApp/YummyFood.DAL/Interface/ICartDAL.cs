using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFood.DAL.Interface
{
    public interface ICartDAL: IDataAccessLayer<Cart>
    {
        Cart GetCartByUser(int UserId);
        ProductModel GetProductDetailForCart(int id);
        CartModel GetCartDetails(int userId);
        bool DeleteItemFromCart(int itemId);
        CartModel UpdateQuantity(int quantity, int itemId, int userId);
        void RemoveOrderdItemFromCart(int userId);
    }
}
