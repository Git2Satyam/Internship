using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.BAL.Implementation;
using YummyFood.BAL.Interface;
using YummyFood.Core.Entities;
using YummyFood.DAL.Implementation;
using YummyFood.DAL.Interface;

namespace YummyFood.BAL
{
    public static class ConfigureServices
    {
        public static void RegistreSevice(IServiceCollection _service, IConfiguration _config)
        {
            _service.AddDbContext<YummyFoodContext>(option =>
            {
                option.UseSqlServer(_config.GetConnectionString("DbConnection"));
                option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            _service.AddScoped<DbContext, YummyFoodContext>();
            _service.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            //Database
            _service.AddScoped<IDataAccessLayer<User>, DataAccessLayer<User>>();
            _service.AddScoped<IDataAccessLayer<Product>, DataAccessLayer<Product>>();
            _service.AddScoped<IDataAccessLayer<Cart>, DataAccessLayer<Cart>>();
            _service.AddScoped<IDataAccessLayer<CartItem>, DataAccessLayer<CartItem>>();
            _service.AddScoped<IDataAccessLayer<PaymentDetail>, DataAccessLayer<PaymentDetail>>();
            _service.AddScoped<IDataAccessLayer<Order>, DataAccessLayer<Order>>();
            _service.AddScoped<IDataAccessLayer<OrderItem>, DataAccessLayer<OrderItem>>();

            //DAL
            _service.AddScoped<IUserDAL, UserDAL>();
            _service.AddScoped<IProductsDAL, ProductsDAL>();
            _service.AddScoped<ILoginDAL, LoginDAL>();
            _service.AddScoped<ICartDAL, CartDAL>();
            _service.AddScoped<IOrderDAL, OrderDAL>();
            //BAL
            _service.AddScoped<IUserBAL, UserBAL>();
            _service.AddScoped<IProductsBAL, ProductsBAL>();
            _service.AddScoped<ILoginBAL, LoginBAL>();  
            _service.AddScoped<ICartBAL, CartBAL>();
            _service.AddScoped<IPaymentBAL, PaymentBAL>();
            _service.AddScoped<IOrderBAL, OrderBAL>();
        }

    }
}
