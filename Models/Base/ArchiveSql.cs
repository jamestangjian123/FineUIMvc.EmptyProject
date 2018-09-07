using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FineUIMvc;

namespace FineUIMvc.EmptyProject.Models
{
    public class ArchiveSql
    {
        //public static string CreateInsertSql(int aid, List<string[]> lstparams, string username)
        //{
        //    ///只针对于案卷级
        //    string[] tableName = TSysAr_LinqDal.Query_SysTableName(aid);
        //    string sql = "insert into " + tableName[0] + "(";
        //    foreach(var strs in lstparams)
        //    {
        //        sql += strs[0] + ",";
        //    }
        //    if (sql.EndsWith(","))
        //        sql = sql.TrimEnd(',');
        //    sql += ", CREATEUSER,CREATEDATE,WS_AID,WS_V10) values(";
        //    foreach(var strs in lstparams)
        //    {
        //        sql += "'" + strs[1] + "',";
        //    }
        //    if (sql.EndsWith(","))
        //        sql = sql.TrimEnd(',');

        //    string v10 = "0";
        //    if (tableName.Length == 2 || tableName[1] != "")
        //        v10 = "1";
        //    sql += ",'" + username + "','" + DateTime.Now + "'," + aid.ToString() + ",'" + v10 + "')";
        //    return sql;
        //}

        //public static  string CreateModifySql(int aid, int id, List<string[]> lstparams, string username)
        //{
        //    ///只针对于案卷级
        //    string[] tableName = TSysAr_LinqDal.Query_SysTableName(aid);
        //    string sql = "update  " + tableName[0] + " set ";
        //    foreach (var strs in lstparams)
        //    {
        //        sql += strs[0] + "='" + strs[1] + "',";
        //    }
        //    if (sql.EndsWith(","))
        //        sql = sql.TrimEnd(',');
        //    sql += ", LASTMODIFYUSER='" + username + "',LASTMODIFYDATE='" + DateTime.Now + "' where WS_ID='" + id.ToString() + "'";
           
        //    //if (sql.EndsWith(","))
        //    //    sql = sql.TrimEnd(',');

        //    //string v10 = "0";
        //    //if (tableName.Length == 2 || tableName[1] != "")
        //    //    v10 = "1";
        //    //sql += ",'" + username + "','" + DateTime.Now + "'," + aid.ToString() + ",'" + v10 + "')";
        //    return sql;
        //}

        public static string CreateDelSql()
        {
            return null;
        }
    }
}