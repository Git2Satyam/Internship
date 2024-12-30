using StyleSphere.Core.Context;
using StyleSphere.Models;
using StyleSphere.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Repository.Implementations
{
    public class NavItemsRepo : INavItemsRepo
    {
        private readonly StyleSphereContext _context;
        public NavItemsRepo(StyleSphereContext context)
        {
            _context = context; 
        }
        public IEnumerable<NavItemsModel> GetNavItems()
        {
            try
            {
                var items = _context.NavItems.Where(c => c.Enabled == true).Select(x => new NavItemsModel
                {
                    Id= x.Id,
                    Name= x.Name,
                    Url= x.Url,
                    Icon = x.icon,
                    SortOrder= x.SortOrder,
                }).OrderBy(x => x.SortOrder).ToList();

                return items;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
