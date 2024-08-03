using Microsoft.EntityFrameworkCore;
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
    public class CartDAL :DataAccessLayer<Cart>, ICartDAL
    {
        YummyFoodContext _context
        {
            get 
            {
                return db as YummyFoodContext;
            }
        }

        public CartDAL(YummyFoodContext _DB): base(_DB)
        {
            
        }

        public ProductModel GetProductDetailForCart(int id)
        {
            try
            {
                var getProduct = _context.Products.Where(p => p.Id == id && p.Enabled == true && p.Deleted == false).Select(x => new ProductModel
                {
                    Name = x.ProductName,
                    //ImagePath = x.ImageUrl,

                }).FirstOrDefault();
                return getProduct;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Cart GetCartByUser(int UserId)
        {
            try
            {
                var cartExist = _context.Carts.Include(c => c.CartItems).Where(c => c.UserId == UserId && c.IsActive == true).FirstOrDefault();
                return cartExist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CartModel GetCartDetails(int userId)
        {
            try
            {
                var getCart = _context.Carts.Where(c => c.UserId == userId && c.IsActive == true)
                    .Select(x => new CartModel
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        CreatedDate = x.CreatedDate,
                        products = (IList<ProductModel>)(from item in _context.CartItems
                                   join prod in _context.Products
                                   on item.ItemId equals prod.Id
                                   where item.CartId == x.Id && item.Enabled != false
                                   select new ProductModel
                                   {
                                       Id = prod.Id,
                                       Name = prod.ProductName,
                                       ImageUrl = prod.ImageUrl,
                                       Quantity = item.Quantity,
                                       UnitPrice = item.UnitPrice,
                                       cartItemId = item.Id,
                                   })
                    }).FirstOrDefault();
                return getCart;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteItemFromCart(int itemId)
        {
            try
            {
                var getItem = _context.CartItems.FirstOrDefault(c => c.ItemId == itemId);
                if(getItem != null)
                {
                    _context.CartItems.Remove(getItem);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CartModel UpdateQuantity(int quantity, int itemId, int userId)
        {
            CartModel cart = new CartModel();
            try
            {
                var getCart = _context.Carts.Include(c => c.CartItems).Where(c => c.UserId == userId).FirstOrDefault();
                if(getCart != null)
                {
                    var getItem = getCart.CartItems.Where(item => item.ItemId == itemId).FirstOrDefault();
                    if(getItem != null)
                    {
                        getItem.Quantity = quantity;
                        _context.Entry(getItem).State = EntityState.Modified;
                        _context.SaveChanges();

                        cart.Quantity = quantity;
                        cart.Unitprice = getItem.UnitPrice;
                        cart.ProductId = getItem.ItemId;
                        cart.TotalPrice = getItem.Quantity * getItem.UnitPrice;
                        return cart;
                    }
                  
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return cart;
        }

        public void RemoveOrderdItemFromCart(int userId)
        {
            try
            {
                var getItem = (from item in  _context.CartItems
                               join ordrItm in _context.OrderItems
                               on item.ItemId equals ordrItm.ItemId
                               where item.Cart.UserId == userId
                               select item).Distinct().ToList();
                if(getItem != null)
                {
                    foreach(var item in getItem)
                    {
                        item.Enabled = false;
                        _context.Entry(item).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
