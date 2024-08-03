using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Models;

namespace YummyFood.BAL.Interface
{
    public interface IProductsBAL
    {
        IEnumerable<ProductModel> GetAllProducts();
        int InsertOrUpdateProduct(ProductModel productML);

        bool DeletePoduct(int id);
        bool UploadImages(IFormFile file);
        int GetImages(int id);
    }
}
