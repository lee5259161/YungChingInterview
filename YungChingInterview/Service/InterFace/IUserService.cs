using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.InterFace
{
    public interface IUserService
    {
        List<UserDetail> GetUserList(string query = null);
        UserDetail GetUserByID(string uid);
        ReturnResult CreateUser(UserDetail Data, List<string> selectedItem, string uid);
        ReturnResult UpdateUser(UserDetail Data, List<string> selectedItem, string uid);
        ReturnResult UpdateUserPassword(UserDetail Data, string uid);
        ReturnResult DisableUser(string uid, string currentUserId);
        ReturnResult EnableUser(string uid, string currentUserId);
    }
}
