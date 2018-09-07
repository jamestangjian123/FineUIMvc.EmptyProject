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
    public class TSysAr_LinqDal
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
        //public static IEnumerable<T_SYFIELDS> Query_GridColumns(int arid, int? status, int? isShow, int? isQuery, int? isWriter, int? isTongji, int? isSystem)
        //{
        //    GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //    var query = conn.T_SYFIELDS.Select(c => c).Where(c => c.ARID == arid);
        //    if (status != null)
        //        query = query.Where(c => c.STATUS == status);
        //    if (isShow != null)
        //        query = query.Where(c => c.Show == isShow);
        //    if (isQuery != null)
        //        query = query.Where(c => c.Query == isQuery);
        //    if (isWriter != null)
        //        query = query.Where(c => c.I02 == isWriter);
        //    if (isTongji != null)
        //        query = query.Where(c => c.I03 == isTongji);
        //    if (isSystem != null)
        //        query = query.Where(c => c.I04 == isSystem);
        //    return query.OrderBy(c => c.OrderBy);
        //}

        ///// <summary>
        ///// 获取档案类型
        ///// </summary>
        ///// <returns></returns>
        //public static IEnumerable<T_SYSAR> Query_MainArchiveType()
        //{

        //}

        /// <summary>
        /// 根据词典类型获取词典
        /// </summary>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IEnumerable<T_SYSWD> Query_Word(string type, int? status)
        {
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var query = conn.T_SYSWD.Select(c => c);
            if (type != null && type != "")
                query = query.Where(c => c.WD_TYPE == type);
            if (status != null)
                query = query.Where(c => c.WD_STATUS == status);
            return query.OrderBy(c => c.WD_ORDER);
        }

        /// <summary>
        /// 添加词典
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_Word(T_SYSWD en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count = conn.T_SYSWD.Select(c => c).Where(c => c.WD_TYPE == en.WD_TYPE && c.WD_NAME == en.WD_NAME).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_SYSWD.Add(en);
                    conn.SaveChanges();
                    return en.WD_ID;
                }
            }
            catch { return -1; }
        }

        /// <summary>
        /// 修改词典
        /// </summary>
        /// <param name="id"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_Word(T_SYSWD en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count = conn.T_SYSWD.Select(c => c).Where(c => c.WD_TYPE == en.WD_TYPE && c.WD_NAME == en.WD_NAME && c.WD_ID != en.WD_ID).Count();
                if (count > 0)
                    return 0;
                else
                {
                    var query = conn.T_SYSWD.Select(c => c).Where(c => c.WD_ID == en.WD_ID);
                    foreach(var n in query)
                    {
                        n.WD_CODE = en.WD_CODE;
                        n.WD_NAME = en.WD_NAME;
                        n.WD_ORDER = en.WD_ORDER;
                        n.WD_STATUS = en.WD_STATUS;
                        n.WD_TYPE = en.WD_TYPE;
                    }
                    conn.SaveChanges();
                    return en.WD_ID;
                }
            }
            catch { return -1; }
        }

        /// <summary>
        /// 查询词典类型里的分类
        /// </summary>
        /// <returns></returns>
        public static List<string> Query_WordType()
        {
            ///因为此方法没有用到延迟的查询，因此必须 try catch
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_SYSWD.Select(c => new { c.WD_TYPE }).Distinct();
                List<string> list = new List<string>();
                foreach (var n in query)
                    list.Add(n.WD_TYPE);
                return list;
            }
            catch { return null; }
        }

        /// <summary>
        /// 添加词典类型里的分类
        /// </summary>
        /// <returns></returns>
        public static int Add_WordType(string name)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count = conn.T_SYSWD.Select(c => c).Where(c => c.WD_TYPE == name).Count();
                if (count > 0)
                    return 0;
                else
                {
                    T_SYSWD en = new T_SYSWD();
                    en.WD_CODE = 0;
                    en.WD_NAME = "无";
                    en.WD_ORDER = 1;
                    en.WD_STATUS = 1;
                    en.WD_TYPE = name;
                    conn.T_SYSWD.Add(en);
                    conn.SaveChanges();
                    return en.WD_ID;
                }
            }
            catch { return -1; }
        }

        ///// <summary>
        ///// 获取档案树
        ///// </summary>
        ///// <param name="status">1. 为启用的， I01. 为是否显示在档案树里</param>
        ///// <returns></returns>
        //public static IEnumerable<T_SYSAR> Query_SysAR(int? status, int? i01)
        //{
        //    GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //    var query = conn.T_SYSAR.Select(c => c);
        //    if (status != null)
        //        query = query.Where(c => c.AR_STATUS == status);
        //    if (i01 != null)
        //        query = query.Where(c => c.AR_I01 == i01);

        //    return query.OrderBy(c => c.AR_ORDER).OrderBy(c => c.AR_PID);
        //}

        ///// <summary>
        ///// 获取表名
        ///// </summary>
        ///// <returns>string[3] 0.案卷表名, 1.卷内表名, 2.档案中文名</returns>
        //public static string[] Query_SysTableName(int aid)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var list = conn.T_SYSAR.Where(c => c.AR_ID == aid).Select(c => new { c.AR_AJTABLE, c.AR_JNTABLE, c.AR_NAME }).ToList();
        //        string[] strs = new string[3];
        //        strs[0] = list[0].AR_AJTABLE;
        //        strs[1] = list[0].AR_JNTABLE;
        //        strs[2] = list[0].AR_NAME;
        //        return strs;
        //    }
        //    catch { return null; }
        //}

        ///// <summary>
        ///// 获取此类型的所有字段名称
        ///// </summary>
        ///// <param name="aid"></param>
        ///// <returns></returns>
        //public static List<string> Query_SysArchiveFields(int aid)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var ls = conn.T_SYFIELDS.Where(c => c.ARID == aid).Select(c => new { c.Name });
        //        List<string> list = new List<string>();
        //        foreach (var n in ls)
        //            list.Add(n.Name);
        //        return list;
        //    }
        //    catch { return null; }
        //}

        ///// <summary>
        ///// 添加字段
        ///// </summary>
        ///// <param name="en"></param>
        ///// <returns></returns>
        //public static int Add_SysArchiveFields(T_SYFIELDS en)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        int count = conn.T_SYFIELDS.Select(c => c).Where(c => c.ARID == en.ARID && (c.Fields == en.Fields || c.Name == en.Name)).Count();
        //        if (count > 0)
        //            return 0;
        //        else
        //        {
        //            conn.T_SYFIELDS.Add(en);
        //            conn.SaveChanges();
        //            return en.ID;
        //        }
        //    }
        //    catch { return -1; }
        //}

        ///// <summary>
        ///// 添加档案类型
        ///// </summary>
        ///// <returns></returns>
        //public static int Add_SysArchiveType(T_SYSAR en)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        if(en.AR_PID == 0)
        //        {
        //            if (conn.T_SYSAR.Select(c => c).Where(c => c.AR_NAME == en.AR_NAME).Count() > 0)
        //                return 0;
        //        }
        //        else
        //        {
        //            if (conn.T_SYSAR.Select(c => c).Where(c => c.AR_NAME == en.AR_NAME && c.AR_PID == en.AR_PID).Count() > 0)
        //                return 0;
        //        }
        //        conn.T_SYSAR.Add(en);
        //        conn.SaveChanges();
        //        if (en.AR_PID > 0)
        //        {
        //            ///建立系统字段(最终通过Get_SystemFields方法生成，目前简化
        //            //List<T_SYFIELDS> list = new List<T_SYFIELDS>();
        //            ///获取文书档案的系统字段
        //            ///
        //            ////从 T_SYSFIELDS_MD 中获取系统字段

        //            var query = conn.T_SYFILEDS_MD.Select(c => c).Where(c => c.STATUS == 1);   //conn.T_SYFIELDS.Select(c => c).Where(c => c.ARID == 3 && c.I04 == 1);
        //            List<T_SYFIELDS> list = new List<T_SYFIELDS>();
        //            foreach(var n in query)
        //            {
        //                T_SYFIELDS newN = new T_SYFIELDS()
        //                {
        //                    Name = n.Name,
        //                    ARID = n.ARID,
        //                    D01 = n.D01,
        //                    D02 = n.D02,
        //                    STATUS = n.STATUS,
        //                    D03 = n.D03,
        //                    Fields = n.Fields,
        //                    I01 = n.I01,
        //                    I02 = n.I02,
        //                    I03 = n.I03,
        //                    I04 = n.I04,
        //                    I05 = n.I05,
        //                    Info = n.Info,
        //                    OrderBy = n.OrderBy,
        //                    Query = n.Query,
        //                    Show = n.Show,
        //                    Type = n.Type,
        //                    V01 = n.V01,
        //                    V02 = n.V02,
        //                    V03 = n.V03,
        //                    V04 = n.V04,
        //                    V05 = n.V05,
        //                    V06 = n.V06,
        //                    V07 = n.V07,
        //                    V08 = n.V08,
        //                    V09 = n.V09

        //                };
        //                newN.ARID = en.AR_ID;
        //                list.Add(newN);
        //            }
        //            conn.T_SYFIELDS.AddRange(list);
        //            conn.SaveChanges();
        //        }
        //        return 1;
        //    }
        //    catch { return -1; }
        //}
        ///// <summary>
        ///// 修改类名
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="name"></param> 
        //public static int Modify_ArName(int id, string name)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var query = conn.T_SYSAR.Select(c => c).Where(c => c.AR_ID == id);
        //        foreach (var n in query)
        //        {
        //            n.AR_NAME = name;
        //        }
        //        conn.SaveChanges();
        //        return 1;
        //    }
        //    catch 
        //    {
        //        return -1;
        //    }
        //}
        ///// <summary>
        ///// 修改字段
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="en"></param>
        ///// <returns></returns>
        //public static int Modify_SysArchiveFields(int id, T_SYFIELDS en)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        int count = conn.T_SYFIELDS.Select(c => c).Where(c => c.ARID == en.ARID && c.Fields == en.Name && c.ID != id).Count();
        //        if (count > 0)
        //            return 0;
        //        else
        //        {
        //            var query = conn.T_SYFIELDS.Select(c => c).Where(c => c.ID == id);
        //            foreach(var n in query)
        //            {
        //                n.Fields = en.Fields;
        //                n.I02 = en.I02;
        //                n.I03 = en.I03;
        //                n.OrderBy = en.OrderBy;
        //                n.Query = en.Query;
        //                n.Show = en.Show;
        //                ///n.STATUS = en.STATUS;
        //            }
        //            //conn.T_SYFIELDS.Add(en);
        //            conn.SaveChanges();
        //            return 1;
        //        }
        //    }
        //    catch { return -1; }
        //}

        ///// <summary>
        ///// 返回系统字段
        ///// </summary>
        ///// <returns></returns>
        //private List<T_SYFIELDS> Get_SystemFields(int arid)
        //{
        //    List<T_SYFIELDS> list = new List<T_SYFIELDS>();

        //    T_SYFIELDS en = new T_SYFIELDS();
        //    en.ARID = arid;
        //    en.Name = "WS_ID";
        //    en.OrderBy = 0;
        //    en.Type = 0;
        //    en.Show = 0;
        //    en.Query = 0;
        //    en.Fields = "ID号";
        //    en.I02 = 0;
        //    en.I03 = 0;
        //    en.I04 = 1;
        //    en.STATUS = 1;
        //    list.Add(en);

        //    en = new T_SYFIELDS();
        //    en.ARID = arid;
        //    en.Name = "WS_QZ";
        //    en.OrderBy = 0;
        //    en.Type = 1;
        //    en.Show = 1;
        //    en.Query = 1;
        //    en.Fields = "全宗号";
        //    en.I02 = 0;
        //    en.I03 = 0;
        //    en.I04 = 1;
        //    en.STATUS = 1;
        //    list.Add(en);

        //    return list;
        //}

        ///// <summary>
        ///// 根据ar_name查询ar_id
        ///// </summary>
        ///// <returns></returns>
        //public static int selectARID(string name) 
        //{
        //    try
        //    {
        //        int ar_id=-2;
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var query = conn.T_SYSAR.Select(c => c).Where(c => c.AR_NAME == name);
        //        foreach (var n in query)
        //        {
        //            ar_id=n.AR_ID;
        //        }
        //        return ar_id;
        //    }
        //    catch { return -1; }
        //}
    }
}
