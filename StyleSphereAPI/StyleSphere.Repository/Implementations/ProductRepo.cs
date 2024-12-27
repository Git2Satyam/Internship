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
               throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
