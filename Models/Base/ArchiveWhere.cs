using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FineUIMvc;
using FineUIMvc.EmptyProject.Models;

namespace FineUIMvc.EmptyProject.Models
{
    public class ArchiveWhere
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Type = Enum
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 字段名 (数据库里的)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 词典名 V01
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// 是 and 还是 or
        /// </summary>
        public string AndOr { get; set; }
        /// <summary>
        /// 等于、包含、大于等于、大于、小于等于、小于
        /// </summary>
        public string Where { get; set; }
        /// <summary>
        /// 查询值
        /// </summary>
        public string Value { get; set; }

        public ArchiveWhere(string s)
        {
            string[] strs = s.Split(';');
            this.ID = strs[0];
            this.Type = strs[1];
            this.Name = strs[2];
            this.Word = strs[3];
            this.Fields = strs[4];
        }

        /// <summary>
        /// 把SQL语句返回出来 只返回一个where 语句
        /// </summary>
        /// <returns></returns>
        public static string GetWhereSql(List<ArchiveWhere> list)
        {
            if (list == null || list.Count == 0)
                return "";

            string sql = "";
            foreach (var n in list)
            {
                string s = " " + n.Name; //+ " " + n.Where;
                if (n.Type == ((int)EnumShenGoFieldsType.Var).ToString())
                {
                    if (n.Where == "like")
                        s = s + " " + n.Where + " '%" + n.Value + "%' ";
                    else
                        s = s + " " + n.Where + " '" + n.Value + "' ";
                }
                else if (n.Type == ((int)EnumShenGoFieldsType.Int).ToString() || n.Type == ((int)EnumShenGoFieldsType.Word).ToString())
                {
                    s = "(cast(" + s + " as int) " + n.Where + n.Value + " and " + s + " is not null) ";
                }
                else if (n.Type == ((int)EnumShenGoFieldsType.Datetime).ToString())
                {
                    string st = n.Value + " 00:00:01";
                    string se = n.Value + " 23:59:59";
                    s = "(" + n.Name + ">='" + st + "' and " + n.Name + "<='" + se + "') ";
                    //s = s + " " + n.Where + 
                }
                s += n.AndOr;
                sql += s;
            }
            if (sql.EndsWith("and"))
                sql = sql.TrimEnd(new char[] { 'a', 'n', 'd' });
            else if (sql.EndsWith("or"))
                sql = sql.TrimEnd(new char[] { 'o', 'r' });

            /// sql = "select * from " + tableName[0] + " where " + sql;
            /// 
            ////要加上 where ， 好判断
            return " where " + sql;
        }
    }

    /// <summary>
    /// 最终存放在 SESSION 里的 完整分页 SQL语句
    /// </summary>
    public class ArchivePagerWhere
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// where 语句部分
        /// </summary>
        public string Where { get; set; }
        /// <summary>
        /// select 语句部分
        /// </summary>
        public string Select { get; set; }
        /// <summary>
        /// 排序 语句部分
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 起始行
        /// </summary>
        public int iStart { get; set; }
        /// <summary>
        /// 结束行
        /// </summary>
        public int iEnd { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int iCount { get; set; }
    }
}