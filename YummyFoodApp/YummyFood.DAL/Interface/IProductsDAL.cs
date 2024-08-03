using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFood.DAL.Interface
{
    public interface IProductsDAL: IDataAccessLayer<Product>
    {
        IEnumerable<ProductModel> GetAllProducts();
        int InsertOrUpdateProduct (ProductModel productML);

        bool DeleteProduct(int id);
        bool UploadImages(IFormFile file);

        int GetImages(int id);
    }
}
