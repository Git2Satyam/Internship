using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.BAL.Interface;
using YummyFood.Core.Entities;
using YummyFood.DAL.Interface;
using YummyFood.Models;

namespace YummyFood.BAL.Implementation
{
    public class CartBAL : ICartBAL
    {
        ICartDAL _cartDAL;
        IDataAccessLayer<CartItem> _cartItem;
        public CartBAL(ICartDAL cartDAL, IDataAccessLayer<CartItem> cartItems)
        {
            _cartDAL = cartDAL;  
            _cartItem = cartItems;
        }

        public Cart AddItemToCart( Guid CartId, CartModel cartML)
        {
            try
            {
                Cart cart = _cartDAL.GetCartByUser(cartML.UserId);
                if (cart == null)
                {
                    cart = new Cart();
                    cart.Id = CartId;
                    cart.UserId = cartML.UserId;
                    cart.IsActive = true;
                    cart.CreatedDate = DateTime.Now;
                    CartItem item = new CartItem { ItemId = cartML.ProductId, UnitPrice = cartML.Unitprice, Quantity = cartML.Quantity, Enabled = true };
                    cart.CartItems.Add(item);
                    _cartDAL.Add(cart);
                    _cartDAL.saveChanges();
                }
                else
                {
                    CartItem item = cart.CartItems.Where(x => x.ItemId == cartML.ProductId).FirstOrDefault();
                    if (item != null)
                    {
                        item.Quantity = cartML.Quantity;
                        item.Enabled = true;
                        _cartItem.Update(item);
                        _cartItem.saveChanges();
                    }
                    else
                    {
                        item = new CartItem { ItemId = cartML.ProductId, UnitPrice = cartML.Unitprice, Quantity = cartML.Quantity, Enabled = true };
                        item.CartId = cart.Id;
                        cart.CartItems.Add(item);

                        _cartItem.Update(item);
                        _cartItem.saveChanges();

                    }
                }
                return cart;
            }   
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteItemFromCart(int itemId)
        {
            return _cartDAL.DeleteItemFromCart(itemId);
        }

        public CartModel GetCartDetails(int userId)
        {
            try
            {
                var detail = _cartDAL.GetCartDetails(userId);
                if (detail != null && detail.products.Count > 0)
                {
                    decimal? subTotal = 0;
                    foreach (var product in detail.products)
                    {
                        product.TotalPrice = (product.Quantity * product.UnitPrice);
                        subTotal += product.TotalPrice;
                    }
                    detail.TotalPrice = subTotal;
                    detail.Tax = Math.Round((decimal)(detail.TotalPrice * 5 / 100), 2);
                    detail.GrandTotal = detail.TotalPrice + detail.Tax;
                }
                return detail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCartItemCount(int userId)
        {
            try
            {
                Cart cart = _cartDAL.GetCartByUser(userId);
                var count = cart.CartItems.Where(c => c.Enabled == true).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductModel GetProductDetailForCart(int id)
        {
            try
            {
                return _cartDAL.GetProductDetailForCart(id);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void RemoveOrderdItemFromCart(int userId)
        {
            try
            {
                _cartDAL.RemoveOrderdItemFromCart(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CartModel UpdateQuantity(int quantity, int itemId, int userId)
        {
            try
            {
                return _cartDAL.UpdateQuantity(quantity, itemId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
