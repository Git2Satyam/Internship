using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFood.BAL.Interface
{
    public interface ICartBAL
    {
        ProductModel GetProductDetailForCart(int id);
        Cart AddItemToCart(Guid CartId, CartModel cartML);
        CartModel GetCartDetails(int userId);
        bool DeleteItemFromCart(int itemId);
        CartModel UpdateQuantity(int quantity, int itemId, int userId);
        void RemoveOrderdItemFromCart(int userId);
        int GetCartItemCount(int userId);
    }
}
