using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

            //Jwt configuration starts here
            var jwtIssuer = _config.GetSection("JWT:Issuer").Get<string>();
            var jwtKey = _config.GetSection("JWT:Key").Get<string>();

            _service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero 
                };
            });

            //Repository
            _service.AddScoped<IProductRepo, ProductRepo>();
            _service.AddScoped<INavItemsRepo, NavItemsRepo>();
            _service.AddScoped<IUserRepo, UserRepo>();


            //Services
            _service.AddScoped<IProductService, ProductService>();
            _service.AddScoped<INavtItemsService, NavItemService>();
            _service.AddScoped<IUserService, UserService>();
        }
    }
}
