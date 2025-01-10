using StyleSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Repository.Interface
{
    public interface IUserRepo
    {
        UserModel VerifyUser(string username, string password);
    }
}
