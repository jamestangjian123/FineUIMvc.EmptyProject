using FineUIMvc.EmptyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineUIMvc.EmptyProject.Models
{
    public class SessionValue
    {
        public static string _sArchiveWhere = "_sSessionShenGoArchiveWhere";
        /// <summary>
        /// 当前编辑的是哪个档案类型
        /// </summary>
        public static string _sArchiveEditAid = "_sSessionShenGoArchiveEditNowAid";
        /// <summary>
        /// 最终的查询分页类
        /// </summary>
        public static string _sArchivePager = "_sSessionShenGoArchivePager";
        /// <summary>
        /// 最终的查询分页类(案卷)
        /// </summary>
        public static string _sArchivePagerAJ = "_sSessionShenGoArchivePagerAJ";

        /// <summary>
        /// 最终的查询分页类(Project)
        /// </summary>
        public static string _sArchivePagerProject = "_sSessionShenGoArchivePagerProject";
        /// <summary>
        /// 最终的查询分页类(单体)
        /// </summary>
        public static string _sArchivePagerST = "_sSessionShenGoArchivePagerST";
        /// <summary>
        /// 最终的查询分页类(卷内)
        /// </summary>
        public static string _sArchivePagerJN = "_sSessionShenGoArchivePagerJN";
        /// <summary>
        /// 电子借阅的SESSION
        /// </summary>
        public static string _sArchiveBrr = "_sSessionShenGoArchiveBrr";

        /// <summary>
        /// 是否点击了查询
        /// </summary>
        public static string _sIsQuery = "0";

        /// <summary>
        /// 当前用户
        /// </summary>
        public static string _sUser = "_sSessionShengGoUser";

        /// <summary>
        /// 当前选择的档案类型
        /// </summary>
        public static string _sSelectArchiveType = "_sSessionSelectArchiveType";

        /// <summary>
        /// 添加查询条件到SESSION中
        /// </summary>
        /// <param name="session"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public static List<ArchiveWhere> AddArchiveWhereSession(HttpSessionStateBase session, ArchiveWhere en)
        {
            List<ArchiveWhere> list = null;
            if (session[_sArchiveWhere] != null)
                list = session[_sArchiveWhere] as List<ArchiveWhere>;
            else
                list = new List<ArchiveWhere>();
            list.Add(en);
            session.Remove(_sArchiveWhere);
            session.Add(_sArchiveWhere, list);
            return list;
        }

        public static List<ArchiveWhere> RemoveArchiveWhereSession(HttpSessionStateBase session, int iDelIndex)
        {
            List<ArchiveWhere> list = null;
            if (session[_sArchiveWhere] != null)
            {
                list = session[_sArchiveWhere] as List<ArchiveWhere>;
                list.RemoveAt(iDelIndex);
            }
            else
                list = new List<ArchiveWhere>();
            session.Remove(_sArchiveWhere);
            session.Add(_sArchiveWhere, list);
            return list;
        }

        public static List<ArchiveWhere> GetArchiveWhereSession(HttpSessionStateBase session)
        {
            return session[_sArchiveWhere] as List<ArchiveWhere>;
        }

        public static void ClearArchiveWhereSession(HttpSessionStateBase session)
        {
            if (session[_sArchiveWhere] != null)
                session.Remove(_sArchiveWhere);
        }


        public static void AddUserSession(HttpSessionStateBase session, V_USER user)
        {
            if (session[_sUser] != null)
                session.Remove(_sUser);
            session.Add(_sUser, user);
        }

        public static V_USER GetUserSession(HttpSessionStateBase session)
        {
            if (session[_sUser] == null)
                return null;
            else
                return session[_sUser] as V_USER;
        }

        /// <summary>
        /// 当前选择的档案类型
        /// </summary>
        /// <param name="session"></param>
        /// <param name=""></param>
        public static void AddSelectArchiveType(HttpSessionStateBase session, string[] strs)
        {
            if (session[_sSelectArchiveType] != null)
                session[_sSelectArchiveType] = strs;
            else
                session.Add(_sSelectArchiveType, strs);
        }

        public static void ClearSelectArchiveType(HttpSessionStateBase session)
        {
            if (session[_sSelectArchiveType] != null)
                session.Remove(_sSelectArchiveType);
        }

        public static string[] GetSelectArchiveType(HttpSessionStateBase session)
        {
            if (session[_sSelectArchiveType] != null)
                return session[_sSelectArchiveType] as string[];
            else
                return null;
        }
    }
}