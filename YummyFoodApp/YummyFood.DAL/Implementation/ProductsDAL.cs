using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.DAL.Interface;
using YummyFood.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace YummyFood.DAL.Implementation
{
    public class ProductsDAL : DataAccessLayer<Product>, IProductsDAL
    {
        private IWebHostEnvironment _enviroment;
        private IHttpContextAccessor _accessor;
        YummyFoodContext _context
        {
            get
            {
                return db as YummyFoodContext;
            }
        }

        public ProductsDAL(YummyFoodContext _DB, IHttpContextAccessor accessor, IWebHostEnvironment environment) : base(_DB)
        {
            _enviroment = environment;
            _accessor = accessor;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            try
            {
                var products = _context.Products.Where(x => x.Enabled == true && x.Deleted == false).Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.ProductName,
                    ImageUrl = p.ImageUrl,
                    Enabled = p.Enabled,
                    Deleted = p.Deleted,
                    ProductCode = p.ProductCode,
                    Description = p.Description,
                    UnitPrice = p.UnitPrice,
                }).ToList();
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertOrUpdateProduct(ProductModel model)
        {
            int id = 0;
            try
            {
                var getProduct = _context.Products.FirstOrDefault(x => x.Id == model.Id);
                if (getProduct != null)
                {
                    getProduct.ProductName = model.Name;
                    getProduct.UnitPrice = model.UnitPrice;
                    getProduct.Enabled = model.Enabled;
                    getProduct.Deleted = model.Deleted;
                    getProduct.ModifiedDate = DateTime.Now;
                    getProduct.Description = model.Description;
                    getProduct.ImageUrl = model.ImageUrl;

                    _context.SaveChanges();
                    id = getProduct.Id;
                }
                else
                {
                    var randomNum = new Random();
                    var Code = randomNum.Next(1000);
                    var addProduct = new Product
                    {
                        ProductName = model.Name,
                        Enabled = true,
                        Deleted = false,
                        CreatedDate = DateTime.Now,
                        UnitPrice = model.UnitPrice,
                        ProductCode = Code,
                        Description = model.Description,
                        ImageUrl = model.ImageUrl
                    };

                    _context.Products.Add(addProduct);
                    _context.SaveChanges();
                    id = addProduct.Id;
                }

                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UploadImages(IFormFile uploadFiles)
        {
            bool Result = false;
            try
            {
                string imagePath;

                var filePath = _enviroment.WebRootPath;
                var fullPath = Path.Combine(filePath + "\\UploadImages\\Product\\");
                string fileName = uploadFiles.FileName;
                //Guid guid = Guid.NewGuid();
                //string newGuid = guid.ToString().Substring(0,4);
                // string filePath = GetFilePath(fullPath);
               // var fullPath = GetFilePath(fileName);

                if (!System.IO.Directory.Exists(fullPath))
                {
                    System.IO.Directory.CreateDirectory(fullPath);
                }

                var extension = Path.GetExtension(fileName);
                var allowedExt = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExt.Contains(extension))
                {
                    var newExtension = fileName.Replace(extension, ".png");
                    fileName = newExtension;
                    imagePath = fullPath + newExtension;
                }
                else
                {
                    imagePath = fullPath + fileName;
                }


                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                using (FileStream stream = System.IO.File.Create(imagePath))
                {
                    uploadFiles.CopyTo(stream);
                    if (stream.Length < 2097152)
                    {
                        BinaryReader br = new BinaryReader(stream);
                        byte[] bytes = br.ReadBytes((Int32)stream.Length);
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                        return Result;
                    }
                    //var imgExist = _context.ImageDetails.Where(x => x.ImagePath == imagePath).FirstOrDefault();
                    //if (imgExist != null)
                    //{
                    //    imgExist.ImagePath = imagePath;
                    //    imgExist.ImageName = fileName;
                    //    //imgExist.ImageUrl = fullPath + "api/Products/GetImages" + fileName;
                    //    imgExist.ModifiedDate = DateTime.Now;
                    //    Result = true;
                    //}
                    //else
                    //{
                    //    var addImage = new ImageDetail
                    //    {
                    //        ImagePath = imagePath,
                    //        ImageName = fileName,
                    //       // ImageUrl = fullPath + "api/Product/GetImages" + fileName,
                    //        CreatedDate = DateTime.Now,
                    //    };

                    //    _context.ImageDetails.Add(addImage);
                    //    Result = true;
                    //}
                    //_context.SaveChanges();

                }
                return Result;
            }
            catch (Exception)
            {
                throw;
            }
            return Result;
        }

        public int GetImages(int id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var env = _enviroment.WebRootPath + "\\UploadImages\\Product\\";
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
        }

        public bool DeleteProduct(int id)
        {
            bool result = false;
            try
            {
                var getData = _context.Products.FirstOrDefault(x => x.Id == id);
                if (getData != null)
                {
                    getData.Deleted = true;
                    _context.Entry(getData).State = EntityState.Modified;
                    _context.SaveChanges();
                    result = true;
                }
                else
                {
                    return result;
                }
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //private string GetFilePath(string ProductCode)
        //{
        //    var randomNum = new Random();
        //    var Code = randomNum.Next(1000);
        //    return _enviroment.WebRootPath + "\\UploadImages\\Product\\" + Code;
        //}
    }
}
