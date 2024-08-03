using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Models;

namespace YummyFood.BAL.Interface
{
    public interface ILoginBAL
    {
        LoginModel VerifyUser(string username, string password, Tuple<string,string> Data);
    }
}
