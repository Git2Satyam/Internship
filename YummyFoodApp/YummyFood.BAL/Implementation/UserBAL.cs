using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.BAL.Interface;
using YummyFood.DAL.Interface;
using YummyFood.Models;

namespace YummyFood.BAL.Implementation
{
    public class UserBAL : IUserBAL
    {
        IUserDAL _useDAL;
        public UserBAL(IUserDAL userDAL)
        {
            _useDAL = userDAL;
        }
        public void DeleteUser(int id)
        {
            try
            {
                _useDAL.DeleteUser(id);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            try
            {
               return _useDAL.GetAllUser();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetUserById(int id)
        {
            try
            {
                return _useDAL.GetUserById(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int InsertOrUpdateUser(UserModel userModel)
        {
            try
            {
                return _useDAL.InsertOrUpdateUser(userModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
