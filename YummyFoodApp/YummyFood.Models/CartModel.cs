using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyFood.Models
{
    public class CartModel
    {
        public CartModel()
        {
            products = new List<ProductModel>();
        }
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Unitprice { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Tax {  get; set; }
        public decimal? GrandTotal {  get; set; }
        public IList<ProductModel> products { get; set; }
    }
}
