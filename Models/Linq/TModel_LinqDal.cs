using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 对于T_Model 表的操作
    /// </summary>
    public class TModel_LinqDal
    {
        /// <summary>
        /// 查询模块 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IEnumerable<T_MODEL> Query_Model(int? status)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_MODEL.Select(c => c);
                if (status != null)
                    query = query.Where(c => c.MD_STATUS == status);
                return query;
            }
            catch { return null; }
        }

        /// <summary>
        /// 查询主模块 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IEnumerable<T_MODEL> Query_MainModel(int? status)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_MODEL.Select(c => c).Where(c => c.MD_PID == 0);
                if (status != null)
                    query = query.Where(c => c.MD_STATUS == status);
                return query.OrderBy(c => c.MD_ORDER).OrderBy(c => c.MD_PID);
            }
            catch { return null; }
        }
    }
}
