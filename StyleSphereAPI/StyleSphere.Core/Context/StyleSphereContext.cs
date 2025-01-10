using Microsoft.EntityFrameworkCore;
using StyleSphere.Core.Configuration;
using StyleSphere.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Core.Context
{
    public class StyleSphereContext: DbContext
    {
        public StyleSphereContext(DbContextOptions<StyleSphereContext> option): base(option)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<NavItem> NavItems { get; set; }
        public DbSet<AdminNavItem> AdminNavItems { get; set; }
        public DbSet<User> Users { get; set; }

        public void OnModelCreating(ModelBuilder builder)
        {
           builder.ApplyConfiguration(new ProductMapConfig());
           builder.ApplyConfiguration(new ProductImageMapConfig());
           builder.ApplyConfiguration(new NavItemsMapConfig());
           builder.ApplyConfiguration(new UserMapConfig());
        }
    }
}
