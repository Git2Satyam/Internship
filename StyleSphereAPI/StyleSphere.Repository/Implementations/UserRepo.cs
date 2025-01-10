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
    public class UserRepo : IUserRepo
    {
        private readonly StyleSphereContext _context;
        public UserRepo(StyleSphereContext context)
        {
            _context = context;
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
