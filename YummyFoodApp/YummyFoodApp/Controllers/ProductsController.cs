using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyFood.BAL.Interface;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFoodApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductsBAL _productBAL;
        ILogger _logger;
        YummyFoodContext _context;
        private IWebHostEnvironment _hostingEnv;
        public ProductsController(IProductsBAL productsBAL, ILogger<ProductsController> logger, YummyFoodContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _productBAL = productsBAL;
            _context = context;
            _hostingEnv = hostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var result = _productBAL.GetAllProducts();
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(404);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult InsertOrUpdateProduct(ProductModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   
                    //UploadImages(model.Url);
                    int Id = _productBAL.InsertOrUpdateProduct(model);
                    if(Id > 0)
                    {
                        return Ok(new
                        {
                            message = "Product is saved successfully!"
                        });
                    }
                    else
                    {
                        return StatusCode(404, new { Message = "Product is not added" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "Invalid Product"});
                }
              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}", ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var remove = _productBAL.DeletePoduct(id);
                if(remove == true)
                {
                    return Ok(new
                    {
                        Message = "Record is removed."
                    });
                }
                else
                {
                    return StatusCode(404, new { Message = "Record not found" });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError (ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult UploadImages(IFormFile file) // this method save images within wwwroot folder
        {
            try
            {
                var uploadImage = _productBAL.UploadImages(file);
                if(uploadImage == true)
                {
                    return Ok(new
                    {
                        Message = "Image is uploaded"
                    }) ;
                }
                else
                {
                    return StatusCode(404, new {Message = "Image is not upladed"});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return BadRequest();
            }
        }

        [NonAction]
        private string GetImagebycode(int Code)
        {
            string hosturl = "https://localhost:44308";
            string mainimagepath = GetActualpath(Code.ToString());
            string Filepath = mainimagepath + "\\1.png";

            if (System.IO.File.Exists(Filepath))
                return hosturl + "/Uploads/Product/" + Code + "/1.png";
            else
                return hosturl + "/Uploads/Common/noimage.png";
        }

        [NonAction]
        public string GetActualpath(string FileName)
        {
            return _hostingEnv.WebRootPath + "\\Uploads\\Product\\" + FileName;
        }
    }

    //[HttpGet]
    //public IActionResult GetImages(int id)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"{ex.Message}", ex);
    //        return BadRequest();
    //    }
    //}

    //[HttpPost]
    //public async Task<IActionResult> insertImages([FromForm] ImagesFileModel imgFileML)
    //{
    //    if(ModelState.IsValid)
    //    {

    //        using(var memoryStream = new MemoryStream())
    //        {
    //            await imgFileML.File.CopyToAsync(memoryStream);
    //            if(memoryStream.Length < 2097152)
    //            {
    //                var file = new AppFile()
    //                {
    //                    Name = imgFileML.Name,
    //                    Content = memoryStream.ToArray()
    //                };
    //                _context.ProductImages.Add(file);
    //                _context.SaveChanges();
    //            }
    //            else
    //            {
    //                ModelState.AddModelError("File", "The file is too large.");
    //            }
    //        }

    //        var returndata = _context.AppFiles
    //            .Where(c => c.Name == fileviewmodel.Name)
    //            .Select(c => new ReturnData()
    //            {
    //                Name = c.Name,
    //                ImageBase64 = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(c.Content))
    //            }).FirstOrDefault();
    //        return Ok(returndata);
    //    }
    //    return Ok("Invalid");
    //}
}
            
        

    

