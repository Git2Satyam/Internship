using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Models
{
    public class ProductModel
    {
        public int ProdcutId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? Enabled { get; set; }
        public bool? Deleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Title { get; set; }
        public int? DiscountPercentage { get; set; }

        public virtual ICollection<ProductImageModel> ProductImages { get; set; }
    }

    public class ProductImageModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImagePath { get; set; }
         
    }
}
