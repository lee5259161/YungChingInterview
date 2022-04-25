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
    public class StoreService: IStoreService
    {
        private IStoreRepository _IStoreRepository;

        public StoreService(IStoreRepository StoreRepository)
        {
            _IStoreRepository = StoreRepository;
        }

        public List<StoreInfoDetail> GetStoreInfoList(string query = null)
        {

            List<StoreInfoDetail> _result = _IStoreRepository.GetStoreInfoList(query);

            return _result;

        }
        public ReturnResult CreateStoreInfo(StoreInfoDetail Data)
        {

            ReturnResult _result = _IStoreRepository.CreateStoreInfo(Data);

            return _result;
        }

    }
}
