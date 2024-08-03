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
    public class LoginBAL : ILoginBAL
    {
        ILoginDAL _loginDAL;
        public LoginBAL(ILoginDAL loginDAL)
        {
            _loginDAL = loginDAL;
        }
        public LoginModel VerifyUser(string username, string password,Tuple<string,string> Data)
        {
            try
            {
                return _loginDAL.VerifyUser(username, password, Data);
            }
            catch
            {
                throw;
            }
           
        }
    }
}
