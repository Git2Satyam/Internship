using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StyleSphere.Core.Context;
using StyleSphere.Repository.Implementations;
using StyleSphere.Repository.Interface;
using StyleSphere.Services.Implementations;
using StyleSphere.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Services
{
    public class ConfigureServices
    {
        public static void RegisterService(IServiceCollection _service, IConfiguration _config)
        {
            _service.AddDbContext<StyleSphereContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DbConnection"));
            });


            //Repository
            _service.AddScoped<IProductRepo, ProductRepo>();
            _service.AddScoped<INavItemsRepo, NavItemsRepo>();


            //Services
            _service.AddScoped<IProductService, ProductService>();
            _service.AddScoped<INavtItemsService, NavItemService>();
        }
    }
}
