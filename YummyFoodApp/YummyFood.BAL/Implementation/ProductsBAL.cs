using Microsoft.AspNetCore.Http;
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
    public class ProductsBAL : IProductsBAL
    {
        IProductsDAL _productDal;
        public ProductsBAL(IProductsDAL productsDAL)
        {
           _productDal = productsDAL;
        }

        public bool DeletePoduct(int id)
        {
            try
            {
                return _productDal.DeleteProduct(id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            try
            {
                return _productDal.GetAllProducts();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetImages(int id)
        {
            throw new NotImplementedException();
        }

        public int InsertOrUpdateProduct(ProductModel productML)
        {
            try
            {
                return _productDal.InsertOrUpdateProduct(productML);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UploadImages(IFormFile file)
        {
            try
            {
               return _productDal.UploadImages(file);
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}
