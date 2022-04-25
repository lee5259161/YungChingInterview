using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;


namespace Service
{
    public class SQLiteService
    {
        private string dbPath = "";
        private string connString = "";
        public SQLiteService()
        {
            dbPath = AppDomain.CurrentDomain.BaseDirectory + "StoreSystem.sqlite";
            connString = "data source=" + dbPath;
        }


        public void CreateSQLite()
        {

            if (!File.Exists(dbPath))
            {
                SQLiteRepository sqliteRepository = new SQLiteRepository();
                sqliteRepository.CreateSQLite();
            }

        }

    }
}
