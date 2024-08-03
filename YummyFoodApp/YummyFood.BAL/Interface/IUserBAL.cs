using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Models;

namespace YummyFood.BAL.Interface
{
    public interface IUserBAL
    {
        IEnumerable<UserModel> GetAllUser();
        UserModel GetUserById(int id);
        int InsertOrUpdateUser(UserModel userModel);
        void DeleteUser(int id);
    }
}
