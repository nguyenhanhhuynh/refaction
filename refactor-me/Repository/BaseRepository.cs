using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace refactor_me.Repository
{
    public abstract class BaseRepository<T>
    {

        protected string _connectionString =  string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0}\Database.mdf;Integrated Security=True",
            HttpContext.Current.Server.MapPath("~/App_Data"));      
     
        protected int _commandTimeOut;
        protected ArrayList _parameters = new ArrayList();

        #region Public Methods

        public List<K> ExecuteSqlDataTable<K>(string sql, CommandType cmdType = CommandType.Text) where K : new()
        {
            DataTable dt = new DataTable();
            if (_connectionString != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {                   
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = cmdType;
                        CopyParameters(cmd, _parameters);
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            if (connection.State == ConnectionState.Closed)
                                connection.Open();
                            da.SelectCommand = cmd;
                            da.Fill(dt);
                            var ret = new List<K>();
                            foreach (DataRow row in dt.Rows)
                            {
                                ret.Add((K)Activator.CreateInstance(typeof(K), row));
                            }
                            return ret;
                        }
                    }
                }
            }
            return null;
        }
          
        public void ExecuteNonQuery(string sql, CommandType cmdType= CommandType.Text)
        {
            if (_connectionString != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {                    
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = cmdType;
                        CopyParameters(cmd, _parameters);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
      
        public SqlParameter AddParameter(string name, SqlDbType type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = direction;
            prm.ParameterName = name;
            prm.SqlDbType = type;
            prm.Value = PrepareSqlValue(value);
            _parameters.Add(prm);
            return prm;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, int size, ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter prm = new SqlParameter();
            prm.Direction = direction;
            prm.ParameterName = name;
            prm.SqlDbType = type;
            prm.Size = size;
            prm.Value = PrepareSqlValue(value);
            _parameters.Add(prm);
            return prm;
        }

        public void Reset()
        {
            _parameters.Clear();
        }
        #endregion Public Methods

        #region Protected Methods
        protected void CopyParameters(SqlCommand cmd, ArrayList cmdParms)
        {
            for (int i = 0; i < cmdParms.Count; i++)
            {
                cmd.Parameters.Add(cmdParms[i]);
            }
        }
        protected object PrepareSqlValue(object value)
        {
            if (value == null)
                return DBNull.Value;
            return value;
        }
        #endregion Protected Methods

        protected abstract void AddCommonParameters(T obj);
    }
}