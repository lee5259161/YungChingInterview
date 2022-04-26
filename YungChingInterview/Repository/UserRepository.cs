using Repository.InterFace;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : baseRepository, IUserRepository
    {

        public List<UserDetail> GetUserList(string query = null)
        {
            List<UserDetail> _result = new List<UserDetail>();

            UserDetail _par = new UserDetail();

            var _sql = @" SELECT * FROM UserDetail ";

            if (query != null)
            {
                _par.UserName = query;
                _sql += @" WHERE UserName LIKE @UserName ";
            }

            _result = this.ToClass<UserDetail>(_sql, _par).ToList();

            return _result;

        }
        public UserDetail GetUserByID(string uid)
        {

            UserDetail _result = new UserDetail();
            UserDetail _par = new UserDetail();
            var _sql = @" SELECT * FROM UserDetail where UserID=@UserID ";
            _par.UserID = uid;
            //--------------------------------------------------------------------------
            _result = this.ToClass<UserDetail>(_sql, _par).ToList().FirstOrDefault();

            return _result;

        }

        public ReturnResult CreateUser(UserDetail Data)
        {
            var _sql = @"INSERT INTO [UserDetail]
                         (UserID,UserName,Email,Password,Access,IsDelete,UpdateUser,UpdateDate) 
                         VALUES (@UserID, @UserName, @Email, @Password, @Access, 
                         @IsDelete, @UpdateUser,@UpdateDate) ";

            var _result = this.ToExecute(_sql, Data);

            return _result;
        }
    }
}
