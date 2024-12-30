using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Core.DataModels
{
    public class NavItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? icon { get; set; }
        public decimal? SortOrder { get; set; }
        public int? ParentId { get; set; }
        public bool? Enabled { get; set; }
    }
}
