using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StyleSphere.Core.Context;
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
        }
    }
}
