using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    ///  用户、角色、权限操作
    /// </summary>
    public class TUser_LinqDal
    {
        public static V_USER Get_LoginUser(string name, string pwd)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.V_USER.Select(c => c).Where(c => c.U_NAME == name && c.U_PWD == pwd).Single();

            }
            catch(Exception ex)
            {
                return null; 
            }
        }


        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<V_USER> Query_User(string name, int start, int pagesize, out int count)
        {
            count = 0;
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.V_USER.Select(c => c);
                if (name != null && name != "")
                    query = query.Where(c => c.U_REALNAME.Contains(name));
                count = query.Count();
                return query.OrderBy(c => c.U_ID).Skip(start).Take(pagesize);
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取用户(新)（分角色）
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<V_USER> Query_User(string name, int start, int pagesize, out int count, int userid)
        {
            count = 0;
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var qu = conn.V_USER.Select(c => c).Where(c => c.U_ID == userid);
                string Role_rid = "";// int Role_rid = -1;
                string Role_id = "";// int Role_id = 0;
                foreach (var n in qu)
                {
                    Role_rid = n.ROLE_RID.ToString();
                    Role_id = n.ROLE_ID.ToString();
                }
                var query = conn.V_USER.Select(c => c);

                if (Role_rid == "-1")
                {
                    if (name != null && name != "")
                        query = query.Where(c => c.U_REALNAME.Contains(name));
                }
                else //if (Role_rid == "0")
                {
                    int roleid = int.Parse(Role_id);
                    if (name != null && name != "")
                        query = query.Where(c => c.ROLE_RID == roleid && c.U_REALNAME.Contains(name));
                    else
                        query = query.Where(c => c.ROLE_RID == roleid);
                }

                count = query.Count();
                return query.OrderBy(c => c.U_ID).Skip(start).Take(pagesize);
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static T_USER Query_User(int uid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
               return conn.T_USER.Select(c => c).Where(c => c.U_ID == uid).Single();
            }
            catch { return null; }
        }

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_User(T_USER en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count = conn.T_USER.Select(c => c).Where(c => c.U_NAME == en.U_NAME).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_USER.Add(en);
                    conn.SaveChanges();
                    return en.U_ID;
                }
            }
            catch { return -1; }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_User(int id, T_USER en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count =  conn.T_USER.Select(c => c).Where(c => c.U_NAME == en.U_NAME && c.U_ID != id).Count();
                if (count > 0)
                    return 0;
                else
                {
                    var query = conn.T_USER.Select(c => c).Where(c => c.U_ID == id);
                    foreach(var n in query)
                    {
                       // n.U_D01 = en.U_D01;
                       // n.U_D02 = en.U_D02;
                        n.U_DATE = en.U_DATE;
                        n.U_DEPARMENT = en.U_DEPARMENT;
                        n.U_EMAIL = en.U_EMAIL;
                        n.U_IP = en.U_IP;
                        n.U_ISIP = en.U_ISIP;
                        n.U_NAME = en.U_NAME;
                        n.U_PHONE = en.U_PHONE;
                        n.U_PHOTO = en.U_PHOTO;
                        n.U_PWD = en.U_PWD;
                        n.U_REALNAME = en.U_REALNAME;
                        n.U_REMARK = en.U_REMARK;
                        n.U_ROLEID = en.U_ROLEID;
                        n.U_SEX = en.U_SEX;
                        n.U_STATUS = en.U_STATUS;
                        n.U_V01 = en.U_V01;
                        n.U_ZW = en.U_ZW;
                    }

                    //var type = en.GetType();
                    //foreach(var n in query)
                    //{
                    //    var type2 = n.GetType();
                    //    foreach (var tn in type.GetProperties())
                    //    {
                    //        if(tn.Name != "U_ID")
                    //            type2.GetProperty(tn.Name).SetValue(type2, tn.GetValue(type));
                    //    }
                    //}
                    conn.SaveChanges();
                    return 1;
                }
            }
            catch(Exception ex) { return -1; }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static int Modify_Password(int id, string newPassword)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                //int count = conn.T_USER.Select(c => c).Where(c => c.U_ID == id).Count();
                //if (count > 0)
                //    return 0;
                //else
                {
                    var query = conn.T_USER.Select(c => c).Where(c => c.U_ID == id);
                    foreach (var n in query)
                        n.U_PWD = newPassword;
                    conn.SaveChanges();
                    return 1;
                }
            }
            catch { return -1; }
        }
        /// <summary>
        /// 获取2级部门
        /// </summary>
        /// <param name="status">1. 为启用的， I01. 为是否显示在档案树里</param>
        /// <returns></returns>
        public static IEnumerable<T_ROLE> Query_ROLE(int? status)//, int? i01)
        {
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var query = conn.T_ROLE.Select(c => c);
            if (status != null)
                query = query.Where(c => c.ROLE_STATUS == status);
            //if (i01 != null)
            //    query = query.Where(c => c.AR_I01 == i01);

            return query.OrderBy(c => c.ROLE_RID);
        }
        /// <summary>
        /// 判断权限WSJN
        /// </summary>
        /// <param name="WSJH"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool Query_QX(string WSJH,int userid)
        {
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            int? mid = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_JH == WSJH).Single().WS1_MID;
            string[] view=null;
            bool bView = false;
            string V05 = conn.V_USER.Select(c => c).Where(c => c.U_ID == userid).Single().ROLE_V05;//== null ? null : user.ROLE_V05.Split(',');
            if(V05!=null)
                view=V05.Split(',');
            if (view != null)
                bView = view.Select(c => c).Where(c => c == mid.ToString()).Count() > 0 ? true : false;
            return bView;
        }
        /// <summary>
        /// 判断权限(公文+WSAJ)
        /// </summary>
        /// <param name="WSJH"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool Query_MGQX(string mid, int userid)
        {
            string[] view = null;
            bool bView = false;
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            string V05 = conn.V_USER.Select(c => c).Where(c => c.U_ID == userid).Single().ROLE_V05;//== null ? null : user.ROLE_V05.Split(',');
            if (V05 != null)
                view = V05.Split(',');
            if (view != null)
                bView = view.Select(c => c).Where(c => c == mid).Count() > 0 ? true : false;
            return bView;
        }
    }
}
