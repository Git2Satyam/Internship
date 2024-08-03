using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.Models;

namespace YummyFood.DAL.Interface
{
    public interface ILoginDAL: IDataAccessLayer<User>
    {
        LoginModel VerifyUser(string username, string password, Tuple<string, string> Data);
    }
}
