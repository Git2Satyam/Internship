using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFood.DAL.Interface
{
    public interface IUserDAL: IDataAccessLayer<User>
    {
        IEnumerable<UserModel> GetAllUser();
        UserModel GetUserById(int id);
        int InsertOrUpdateUser(UserModel userModel);
        void DeleteUser(int id);

    }
}
