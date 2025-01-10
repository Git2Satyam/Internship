using Microsoft.EntityFrameworkCore.ChangeTracking;
using StyleSphere.Models;
using StyleSphere.Repository.Interface;
using StyleSphere.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Services.Implementations
{
    public class NavItemService : INavtItemsService
    {
        private readonly INavItemsRepo _repo;
        public NavItemService(INavItemsRepo repo)
        {
            _repo = repo;  
        }

        public IEnumerable<NavItemsModel> GetAdminNavItem()
        {
            try
            {
                return _repo.GetAdminNavItem();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<NavItemsModel> GetNavItems()
        {
            try
            {
                  return _repo.GetNavItems();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
