using Model;
using Repository.InterFace;
using Service.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {

        private IUserRepository _IUserRepository;
        private IUtilService _IUtilService;

        public UserService(IUserRepository UserRepository, IUtilService UtilService)
        {
            _IUserRepository = UserRepository;
            _IUtilService = UtilService;
        }

        public List<UserDetail> GetUserList(string query = null)
        {

            List<UserDetail> _result = _IUserRepository.GetUserList(query);

            return _result;

        }
        public UserDetail GetUserByID(string uid)
        {

            UserDetail _result = _IUserRepository.GetUserByID(uid);

            return _result;

        }
        public ReturnResult CreateUser(UserDetail Data, List<string> selectedItem, string uid)
        {
            Data.UpdateUser = uid;
            Data.UpdateDate = DateTime.Now;
            Data.Password = _IUtilService.encryptPWD(Data.UserID, Data.Password);
            Data.IsDelete = 0;
            if (selectedItem != null)
            {
                Data.Access = string.Join(",", selectedItem);
            }
            else
            {
                Data.Access = "N";
            }
           
            ReturnResult _result = _IUserRepository.CreateUser(Data);

            return _result;
        }

        public ReturnResult UpdateUser(UserDetail Data, List<string> selectedItem, string uid)
        {
            Data.UpdateUser = uid;
            Data.UpdateDate = DateTime.Now;
            if (selectedItem != null)
            {
                Data.Access = string.Join(",", selectedItem);
            }
            else
            {
                Data.Access = "N";
            }

            ReturnResult _result = _IUserRepository.UpdateUser(Data);

            return _result;
        }
        public ReturnResult UpdateUserPassword(UserDetail Data,string uid)
        {
            Data.UpdateUser = uid;
            Data.UpdateDate = DateTime.Now;

            ReturnResult _result = _IUserRepository.UpdateUserPassword(Data);

            return _result;
        }
        public ReturnResult DisableUser(string uid, string currentUserId)
        {
            UserDetail Data = new UserDetail();
            Data.UserID = uid;
            Data.IsDelete = 1;

            Data.UpdateUser = currentUserId;
            Data.UpdateDate = DateTime.Now;

            ReturnResult _result = _IUserRepository.DisableUser(Data);

            return _result;
        }
        
        public ReturnResult EnableUser(string uid, string currentUserId)
        {
            UserDetail Data = new UserDetail();
            Data.UserID = uid;
            Data.IsDelete = 0;

            Data.UpdateUser = currentUserId;
            Data.UpdateDate = DateTime.Now;

            ReturnResult _result = _IUserRepository.EnableUser(Data);

            return _result;
        }
    }
}
