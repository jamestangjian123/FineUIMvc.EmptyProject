using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 统计
    /// </summary>
    public class TTongji_LinqDal
    {
        public static DataTable Tongji_Archive(string table, string fieldsA, string fieldsB, int aid)
        {
            string sql = "select  count(*) as n," + fieldsA + " as F1," + fieldsB + " as F2 from " + table + " where WS_AID=" + aid + " group by " + fieldsA + ", " + fieldsB;
            GGArchiveSystemEntities database = new GGArchiveSystemEntities();
            SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            try
            {
                conn.ConnectionString = database.Database.Connection.ConnectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                ///总数
                cmd.CommandText = sql;
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                conn.Close();//连接需要关闭  
                conn.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                return null;
            }
        }
    }
}
