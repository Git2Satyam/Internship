using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Core.DataModels
{
    public class AdminNavItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public decimal? SortOrder { get; set; }
        public int ParentId { get; set; }
        public bool? Enabled { get; set; }
    }
}
