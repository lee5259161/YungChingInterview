using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.InterFace
{
    public interface IStoreService
    {
        List<StoreInfoDetail> GetStoreInfoList(string query = null);
        ReturnResult CreateStoreInfo(StoreInfoDetail Data, string uid);
        StoreInfoDetail GetStoreInfoByID(int sid);
        ReturnResult UpdateStoreInfo(StoreInfoDetail Data, string uid);
        ReturnResult DeleteStoreInfo(int sid);

    }
}
