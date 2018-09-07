using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 权限
    /// </summary>
    public class TQX_LinqDal
    {
        public static IEnumerable<T_QX> Query_QX(int roleid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_QX.Select(c => c).Where(c => c.QX_RID == roleid);
            }
            catch { return null; }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Add_QX(int roleid, List<T_QX> list)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_QX.Select(c => c).Where(c => c.QX_RID == roleid);
                conn.T_QX.RemoveRange(query);
                conn.SaveChanges();
                conn.T_QX.AddRange(list);
                conn.SaveChanges();
                return 1;
            }
            catch { return -1; }
        }
    }
}
