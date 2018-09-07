using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 档案视图操作 
    /// </summary>
    public class VArchive_LinqDal
    {
        /// <summary>
        /// 获取需要鉴定的档案
        /// </summary>
        /// <returns></returns>
        //public static IEnumerable<V_WS> Query_WSJD(int? aid,int? jd, int start, int pagesize, out int count, int? year)
        //{
        //    count = 0;
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var query = conn.V_WS.Select(c => c);
        //        if (year == null)
        //        {
        //            year = DateTime.Now.Year;
        //            query = query.Where(c => c.ENDYEAR < year);
        //        }
        //        else
        //            query = query.Where(c => c.WS_D01.Value.Year == year);
        //        ///jd =1 的表示已经销毁
        //        if (jd != null)
        //            query = query.Where(c => c.WS_JD == jd);
        //        if (aid != null)
        //            query = query.Where(c => c.WS_AID == aid);
        //        count = query.Count();
        //        return query.OrderBy(c => c.WS_AID).OrderBy(c => c.ENDYEAR).OrderBy(c=>c.WS_D01).Skip(start).Take(pagesize);
        //    }
        //    catch { return null; }
        //}

        /// <summary>
        /// 获取有密级的档案
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="mj"></param>
        /// <param name="start"></param>
        /// <param name="pagesize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        //public static IEnumerable<V_WS> Query_WSMJ(int? aid, string mj, int start, int pagesize, out int count)
        //{
        //    count = 0;
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        int year = DateTime.Now.Year;
        //        var query = conn.V_WS.Select(c => c).Where(c => c.MJ != "无密");
        //        if (aid != null)
        //            query = query.Where(c => c.WS_AID == aid);
        //        count = query.Count();
        //        return query.OrderBy(c => c.WS_AID).OrderBy(c => c.WS_ID).Skip(start).Take(pagesize);
        //    }
        //    catch { return null; }
        //}

        /// <summary>
        /// 查询某个档案
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static V_WS Query_WS(int id)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        return conn.V_WS.Select(c => c).Where(c => c.WS_ID == id).Single();
        //    }
        //    catch { return null; }
        //}

        /// <summary>
        /// 馆藏量前十统计
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataTable Tongji_ArchiveTopTen()
        {
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
                ///根据年度统计
                cmd.CommandText = "SELECT top 10 ARCHIVETYPE AS TYPE, COUNT(WS_ID) AS COUNT  FROM V_WS GROUP BY ARCHIVETYPE";
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

        /// <summary>
        /// 统计档案概况，包括： 档案总数，档案分类，档案起始年度，档案结束年度 
        /// </summary>
        /// <returns></returns>
        //public static List<string> Tongji_ArchiveCount()
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        int count = conn.T_WSAJ.Select(c => c).Count();
        //        int? max = conn.T_WSAJ.Max(c => c.WS_ND);
        //        int? min = conn.T_WSAJ.Min(c => c.WS_ND);

        //        List<string> list = new List<string>();
        //        list.Add(count.ToString());
        //        list.Add(min==null?"":min.Value.ToString());
        //        list.Add(max == null ? "" : max.Value.ToString());
        //        return list;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 获取此文件的电子文件
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        //public static string[] Get_File(int aid)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var n = conn.V_WS.Where(c => c.WS_ID == aid).Select(c => new { c.WS_V10, c.GDBM }).Single();
        //        return new string[] { n.WS_V10, n.GDBM };
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}
