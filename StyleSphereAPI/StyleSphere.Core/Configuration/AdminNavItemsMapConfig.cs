using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StyleSphere.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Core.Configuration
{
    public class AdminNavItemsMapConfig : IEntityTypeConfiguration<AdminNavItem>
    {
        public void Configure(EntityTypeBuilder<AdminNavItem> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
