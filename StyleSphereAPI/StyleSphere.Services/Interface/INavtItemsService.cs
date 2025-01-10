using StyleSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Services.Interface
{
    public interface INavtItemsService
    {
        IEnumerable<NavItemsModel> GetNavItems();

        IEnumerable<NavItemsModel> GetAdminNavItem();
    }
}
