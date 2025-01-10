using StyleSphere.Models;
using StyleSphere.Repository.Interface;
using StyleSphere.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public List<ProductModel> GetAllProducts()
        {
            try
            {
                return _productRepo.GetAllProducts();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
