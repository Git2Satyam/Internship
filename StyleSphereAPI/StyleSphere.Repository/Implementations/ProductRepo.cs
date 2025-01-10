using Microsoft.Extensions.Logging;
using StyleSphere.Core.Context;
using StyleSphere.Models;
using StyleSphere.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Repository.Implementations
{
    public class ProductRepo : IProductRepo
    {
        private readonly StyleSphereContext _context;
        
        public ProductRepo(StyleSphereContext context)
        {
            _context = context;
        }
        public List<ProductModel> GetAllProducts()
        {
            try
            {
               var list = _context.Products.Where(p => p.Enabled == true && p.Deleted == false).Select(x => new ProductModel
               {
                   ProductName = x.ProductName,
                   ProductDescription = x.ProductDescription,
                   UnitPrice = x.UnitPrice,
                   Quantity = x.Quantity,
                   DiscountPercentage = x.DiscountPercentage,
                   Title = x.Title,
               }).ToList();
                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
