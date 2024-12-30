using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Models
{
    public class NavItemsModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public decimal? SortOrder { get; set; }
        public bool? Enabled { get; set; }
    }
}
