using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyFood.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
       // public IFormFile? Image { get; set; }
       // public string? ImagePath { get; set; }
        public int? ProductCode { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? Enabled { get; set; }
        public bool? Deleted { get; set;}
        public int? Quantity { get; set; }
        public int? cartItemId { get; set; }
        public string? Description { get; set; }

        public  decimal? TotalPrice { get; set; }
    }
}
