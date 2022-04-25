using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class SQLiteRepository : baseRepository
    {

        //-------------------------------------------------------------------------------------------------------

        public ReturnResult CreateSQLite()
        {
            var _sql = @"
                CREATE TABLE [UserDetail] (
                   [UserID] nvarchar(50) COLLATE NOCASE NOT NULL PRIMARY KEY,
                   [UserName] nvarchar(50) COLLATE NOCASE,
                   [Email] nvarchar(50) COLLATE NOCASE,
                   [Password] nvarchar(50) COLLATE NOCASE,
                   [Access] nvarchar(50) COLLATE NOCASE,
                   [IsDelete] int,
                   [UpdateUser] nvarchar(50) COLLATE NOCASE,
                   [UpdateDate] datetime
                ); ";

            _sql += @"
                CREATE TABLE [StoreInfoDetail] (
                   [StoreID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                   [StoreName] nvarchar(50) COLLATE NOCASE,
                   [Country] nvarchar(50) COLLATE NOCASE,
                   [GroupID] nvarchar(50) COLLATE NOCASE,
                   [OpenDate] datetime,
                   [CloseDate] datetime,
                   [Manager] nvarchar(50) COLLATE NOCASE,
                   [UpdateUser] nvarchar(50) COLLATE NOCASE,
                   [UpdateDate] datetime
                ); ";

                    
            var _result = this.ToExecute(_sql, null);

            return _result;
        }
        

    }
}
