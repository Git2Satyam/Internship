using StyleSphere.Core.Context;
using StyleSphere.Core.DataModels;
using StyleSphere.Models;
using StyleSphere.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Repository.Implementations
{
    public class UserRepo : IUserRepo
    {
        private readonly StyleSphereContext _context;
        public UserRepo(StyleSphereContext context)
        {
            _context = context;
        }

        public int InsertOrUpdateUser(UserModel user)
        {
            int result = 0;
            try
            {
                var userExist = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (userExist != null)
                {
                    userExist.FirstName = user.FirstName;
                    userExist.LastName = user.LastName;
                    userExist.Email = user.Email;
                    userExist.Password = user.Password;
                    userExist.ModifiedBy = user.ModifiedBy;
                    user.ModifiedDate = DateTime.Now;

                    _context.SaveChanges();
                }
                else
                {
                    var adduser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.Password,
                        CreatedDate = DateTime.Now,
                        Enabled = true,
                        Deleted = false,
                        PasswordExpiryDate = DateTime.Now.AddMonths(6)
                    };

                    _context.Users.Add(adduser);
                    _context.SaveChanges();
                    result = 1;
                }
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public UserModel VerifyUser(string username, string password)
        {
            var model = new UserModel();
            try
            {
                var user = _context.Users.FirstOrDefault(c => c.Enabled == true && c.Deleted == false && c.Email == username && c.Password == password);
                if (user != null)
                {
                    model.Id = user.Id;
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.Email = user.Email;
                    model.Password = user.Password;
                }
                else
                {
                    return model;
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
