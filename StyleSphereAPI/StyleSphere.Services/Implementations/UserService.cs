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
    public class UserService: IUserService
    {
        private readonly IUserRepo _repo;
        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public UserModel VerifyUser(string username, string password)
        {
            try
            {
                return _repo.VerifyUser(username, password);    
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
