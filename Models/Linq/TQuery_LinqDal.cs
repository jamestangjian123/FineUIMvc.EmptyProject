using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 包括了 SysAr  SysFields  SysWord
    /// </summary>
    public class TQuery_LinqDal
    {
        /// <summary>
        ///  根据表ID查询出列
        /// </summary>
        /// <param name="arid">档案类型</param>
        /// <param name="isQuery">是否可查询</param>
        /// <param name="isShow">是否显示在查询列上</param>
        /// <param name="status">是否启用</param>
        /// <param name="isWriter">是否可著录</param>
        /// <returns></returns>
        public static IEnumerable<T_Query> Query_GridColumns(int Grade)
        {
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var query = conn.T_Query.Select(c => c).Where(c => c.Grade == Grade);
            return query;
        }
        public static string Query_TableName(int Grade)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_Query.Select(c => c).Where(c => c.Grade == Grade);
                string table = "";
                foreach (var n in query)
                {
                    table = n.Table;
                }
                return table;
            }
            catch
            { return null; }
        }
    }
}
