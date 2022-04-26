using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFace
{
    public interface IStoreRepository
    {
        List<StoreInfoDetail> GetStoreInfoList(string query = null);
        ReturnResult CreateStoreInfo(StoreInfoDetail Data);
        StoreInfoDetail GetStoreInfoByID(int sid);
        ReturnResult UpdateStoreInfo(StoreInfoDetail Data);
        ReturnResult DeleteStoreInfo(int sid);
    }
}
