using Model;
using Repository.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StoreRepository: baseRepository, IStoreRepository
    {
        public List<StoreInfoDetail> GetStoreInfoList(string query = null)
        {
            List<StoreInfoDetail> _result = new List<StoreInfoDetail>();

            StoreInfoDetail _par = new StoreInfoDetail();

            var _sql = @" SELECT * FROM StoreInfoDetail ";

            if (query != null)
            {
                _par.StoreName = query;
                _sql += @" WHERE StoreName LIKE @StoreName ";
            }

            _result = this.ToClass<StoreInfoDetail>(_sql, _par).ToList();

            return _result;

        }

        public ReturnResult CreateStoreInfo(StoreInfoDetail Data)
        {
            var _sql = @"INSERT INTO [StoreInfoDetail]
                         (StoreName,Country,OpenDate,CloseDate,Manager,UpdateUser,UpdateDate) 
                         VALUES (@StoreName, @Country, @OpenDate, @CloseDate, @Manager, 
                         @UpdateUser, @UpdateDate) ";
            
            var _result = this.ToExecute(_sql, Data);

            return _result;
        }

        public StoreInfoDetail GetStoreInfoByID(int sid)
        {

            StoreInfoDetail _result = new StoreInfoDetail();
            StoreInfoDetail _par = new StoreInfoDetail();
                var _sql = @" SELECT * FROM StoreInfoDetail where StoreID=@StoreID ";
                _par.StoreID = sid;
                //--------------------------------------------------------------------------
                _result = this.ToClass<StoreInfoDetail>(_sql, _par).ToList().FirstOrDefault();

                return _result;

        }
        public ReturnResult UpdateStoreInfo(StoreInfoDetail Data)
        {
            var _sql = @"UPDATE StoreInfoDetail ";
            string[] _columns = "StoreName,Country,OpenDate,CloseDate,Manager,UpdateUser,UpdateDate".Split(',');
            _sql = this.appendUpdateStr(_sql, _columns);
            _sql += " where StoreID = @StoreID";
            //---------------------------------            
            var _result = this.ToExecute(_sql, Data);

            return _result;
        }
        public ReturnResult DeleteStoreInfo(int sid)
        {
            var _sql = @"DELETE from StoreInfoDetail ";
            _sql += " where StoreID = @StoreID";
            //---------------------------------            
            StoreInfoDetail _data = new StoreInfoDetail();
            _data.StoreID = sid;
            var _result = this.ToExecute(_sql, _data);

            return _result;
        }

    }
}
