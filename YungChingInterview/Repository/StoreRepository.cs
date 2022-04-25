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
                         (StoreName,Country,GroupID,OpenDate,CloseDate,Manager,UpdateUser,UpdateDate) 
                         VALUES (@StoreName, @Country, @GroupID, @OpenDate, @CloseDate, @Manager, 
                         @UpdateUser, @UpdateDate) ";
            
            var _result = this.ToExecute(_sql, Data);

            return _result;
        }

    }
}
