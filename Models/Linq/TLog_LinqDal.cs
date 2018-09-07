using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public class TLog_LinqDal
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_Log(T_Log en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                conn.T_Log.Add(en);
                conn.SaveChanges();
                return en.Log_ID;
            }
            catch { return -1; }
        }

        /// <summary>
        /// 获取LOG记录
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dtstart"></param>
        /// <param name="dtend"></param>
        /// <param name="start"></param>
        /// <param name="row"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T_Log> Query_Log(string type,DateTime? dtstart, DateTime? dtend, int start, int row, out int count)
        {
            count = 0;
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var query = conn.T_Log.Select(c => c);
            if (type != null && type != "")
                query = query.Where(c => c.Log_Type == type);
            if (dtstart != null)
                query = query.Where(c => c.Log_Date >= dtstart.Value);
            if (dtend != null)
                query = query.Where(c => c.Log_Date <= dtend.Value);
            count = query.Count();

            return query.OrderByDescending(c => c.Log_ID).Skip(start).Take(row);
        }
    }
}
