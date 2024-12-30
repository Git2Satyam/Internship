using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Core.DataModels
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImagePath { get; set; }
        public bool? Deleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
       public virtual Product Product { get; set; }
    }
}
