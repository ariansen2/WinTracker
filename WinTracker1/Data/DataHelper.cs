using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTracker1.Data
{
    public class DataHelper
    {
        private string _connectionstring = ConfigurationManager.ConnectionStrings["WinTrackerDB"].ToString();
        private SqlConnection _conn;
        public DataHelper()
        {
            _conn = new SqlConnection(_connectionstring);
        }

        public Boolean SaveScreenUsed(string activewindow, DateTime starttime, DateTime stoptime, TimeSpan elapsed)
        {
            int result;
            SqlCommand scmd = new SqlCommand();
            scmd.Connection = _conn;
            scmd.CommandTimeout = 30;
            scmd.CommandType = CommandType.StoredProcedure;
            scmd.CommandText = "spWindowUsed";
            scmd.Parameters.Add("@Windowname", SqlDbType.VarChar,1024).Value = activewindow;


            scmd.Parameters.Add("@startime", SqlDbType.DateTime2, 7).Value = starttime;
            scmd.Parameters.Add("@stoptime", SqlDbType.DateTime2,7).Value = stoptime;

            scmd.Parameters.Add("@ElapsedTime", SqlDbType.Time,7).Value = elapsed;
            scmd.Connection.Open();
            result= scmd.ExecuteNonQuery();
            return true;
        }
    }
}
