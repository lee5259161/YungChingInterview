using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFace
{
    public interface IUserRepository
    {
        List<UserDetail> GetUserList(string query = null);
        UserDetail GetUserByID(string uid);
        ReturnResult CreateUser(UserDetail Data);
        ReturnResult UpdateUser(UserDetail Data);
        ReturnResult UpdateUserPassword(UserDetail Data);
        ReturnResult DisableUser(UserDetail Data);
        ReturnResult EnableUser(UserDetail Data);
    }
}
