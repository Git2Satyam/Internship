using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.DAL.Interface;
using YummyFood.Models;

namespace YummyFood.DAL.Implementation
{
    public class LoginDAL: DataAccessLayer<User>, ILoginDAL
    {
        YummyFoodContext _context
        {
            get
            {
                return db as YummyFoodContext;
            }
        }

        public LoginDAL(YummyFoodContext _DB): base(_DB)
        {
                
        }

        public LoginModel VerifyUser(string username, string password, Tuple<string, string> Data)
        {
            try
            {
                var user = _context.Users.Where(x => x.Email == username && x.Password == password).Select(t => new LoginModel
                {
                    UserName = t.Email, Password = t.Password, Id = t.Id
                }).FirstOrDefault();
                if (user != null)
                {
                    var userExist = _context.SignInLogs.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (userExist != null)
                    {
                        userExist.UserId = user.Id;
                        userExist.LoginTime = DateTime.Now;
                        userExist.BrowserInfo = Data.Item2;
                        userExist.Ipaddress = Data.Item1;
                    }
                    else
                    {
                        var addUser = new SignInLog
                        {
                            UserId = user.Id,
                            LoginTime = DateTime.Now,
                            BrowserInfo = Data.Item2,
                            Ipaddress = Data.Item1
                        };
                        _context.SignInLogs.Add(addUser);
                    }
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
