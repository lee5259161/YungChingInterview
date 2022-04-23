using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Model;
using System.Configuration;
using System.Data.SQLite;
using System.IO;

namespace Repository
{

    public class baseRepository : IDisposable
    {
        private SQLiteConnection objConn;

        public baseRepository()
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + "StoreSystem.sqlite";
            string connString = "data source=" + dbPath;
            objConn = new SQLiteConnection(connString);
        }


        public void Dispose()
        {
            this.ToConnClose();
        }

        public bool ToConnOpen()
        {
            bool _result = true;
            try
            {
                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }
            }
            catch
            {
                _result = false;
            }
            return _result;
        }

        public bool ToConnClose()
        {
            bool _result = true;
            try
            {
                if (objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                }
            }
            catch
            {

                _result = false;
            }
            return _result;
        }



        public IList<T> ToClass<T>(string sql, object Params) where T : new()
        {
            try
            {
                if (string.IsNullOrEmpty(sql))
                {
                    return null;
                }

                if (Params != null)
                {
                    var _result = this.objConn.Query<T>(sql, Params).ToList();
                    return _result;
                }
                else
                {
                    var _result = this.objConn.Query<T>(sql).ToList();
                    return _result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<T> ToClass<T>(string sql, DynamicParameters Params) where T : new()
        {
            if (string.IsNullOrEmpty(sql) == true)
            {
                return null;
            }

            try
            {
                if (Params != null)
                {
                    var _result = this.objConn.Query<T>(sql, Params).ToList();
                    return _result;
                }
                else
                {
                    var _result = this.objConn.Query<T>(sql).ToList();
                    return _result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ReturnResult ToExecute(string sql, object Params)
        {
            var _result = new ReturnResult();
            int _flag = 0;

            try
            {
                if (Params != null)
                {
                    _flag = this.objConn.Execute(sql, Params);
                }
                else
                {
                    _flag = this.objConn.Execute(sql);
                }
                _result.Code = "1";
                _result.Msg = "";
            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.Msg = ex.Message;
                _result.Code = "0";

            }

            _result.Success = (_flag == 0) ? false : true;

            return _result;
        }


        public ReturnResult ToExecute(string sql, DynamicParameters Params)
        {
            var _result = new ReturnResult();
            int _flag = 0;

            try
            {
                if (Params != null)
                {
                    _flag = this.objConn.Execute(sql, Params);
                }
                else
                {
                    _flag = this.objConn.Execute(sql);
                }
                _result.Code = "1";
                _result.Msg = "";
            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.Msg = ex.Message;
                _result.Code = "0";

            }

            _result.Success = (_flag == 0) ? false : true;

            return _result;
        }


        public dynamic ToObj(string sql, object Params)
        {
            if (string.IsNullOrEmpty(sql) == true)
            {
                return null;
            }

            try
            {
                if (Params != null)
                {
                    var _result = this.objConn.Query(sql, Params);
                    return _result;
                }
                else
                {
                    var _result = this.objConn.Query(sql);
                    return _result;
                }
            }
            catch (Exception)
            {
                return null;

            }
        }

        public dynamic ToObj(string sql, DynamicParameters Params)
        {
            if (string.IsNullOrEmpty(sql) == true)
            {
                return null;
            }

            try
            {
                if (Params != null)
                {
                    var _result = this.objConn.Query(sql, Params);
                    return _result;
                }
                else
                {
                    var _result = this.objConn.Query(sql);
                    return _result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string appendInsertStr(string sql, string[] columns)
        {
            var _result = string.Empty;
            List<string> _pars = new List<string>();
            for (int i = 0; i < columns.Length; i++)
            {
                _pars.Add(string.Format("{0}", columns[i]));
            }
            if (_pars.Count > 0)
            {
                _result = string.Format("{0} ({1}) values(@{2})", sql, string.Join(",", _pars), string.Join(",@", _pars));
            }
            return _result;
        }

        public string appendUpdateStr(string sql, string[] columns)
        {
            var _result = string.Empty;
            List<string> _pars = new List<string>();
            for (int i = 0; i < columns.Length; i++)
            {
                _pars.Add(string.Format("{0}=@{0}", columns[i]));
            }
            if (_pars.Count > 0)
            {
                _result = string.Format("{0} set {1}", sql, string.Join(",", _pars));
            }
            return _result;
        }



    }



}
