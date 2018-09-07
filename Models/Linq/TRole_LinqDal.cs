using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class TRole_LinqDal
    {
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_Role(T_ROLE en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count = conn.T_ROLE.Select(c => c).Where(c => c.ROLE_NAME == en.ROLE_NAME).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_ROLE.Add(en);
                    conn.SaveChanges();
                    return en.ROLE_ID;
                }
            }
            catch { return -1; }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_Role(int id , T_ROLE en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count = conn.T_ROLE.Select(c => c).Where(c => c.ROLE_NAME == en.ROLE_NAME && c.ROLE_ID != id).Count();
                if (count > 0)
                    return 0;
                else
                {
                    var query = conn.T_ROLE.Select(c => c).Where(c => c.ROLE_ID == id);
                    foreach(var n in query)
                    {
                        n.ROLE_NAME = en.ROLE_NAME;// = name;
                        n.ROLE_REMARK = en.ROLE_REMARK;// = remark;
                        n.ROLE_VIEWPIC = en.ROLE_VIEWPIC;// = viewpic;
                        n.ROLE_MODIFY = en.ROLE_MODIFY;// = modify;
                        n.ROLE_STATUS = en.ROLE_STATUS;// = status;
                        n.ROLE_V04 = en.ROLE_V04;// = otherView;
                        n.ROLE_V05 = en.ROLE_V05;// = otherModify;
                        n.ROLE_RID = en.ROLE_RID;
                        n.ROLE_V03 = en.ROLE_V03;
                    }
                    conn.SaveChanges();
                    return 1;
                }
            }
            catch { return -1; }
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T_ROLE> Query_Role(string name, int start, int pagesize, out int count)
        {
            count = 0;
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_ROLE.Select(c => c);

                if (name != null && name != "")
                    query = query.Where(c => c.ROLE_NAME.Contains(name));
                count = query.Count();
                return query.OrderBy(c=>c.ROLE_ID).Skip(start).Take(pagesize);
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T_ROLE> Query_Role(string name, int start, int pagesize, out int count,int userid)
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
                     Role_id =n.ROLE_ID.ToString();
                }
                var query = conn.T_ROLE.Select(c => c);
                if (Role_rid == "-1")
                {
                    if (name != null && name != "")
                    query = query.Where(c => c.ROLE_NAME.Contains(name));
                }
                else 
                {
                    int roleid = int.Parse(Role_id);
                    if (name != null && name != "")
                        query = query.Where(c => c.ROLE_RID == roleid && c.ROLE_NAME.Contains(name));
                    else
                        query = query.Where(c => c.ROLE_RID == roleid);
                }

               
                count = query.Count();
                return query.OrderBy(c => c.ROLE_ID).Skip(start).Take(pagesize);
            }
            catch { return null; }
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_ROLE Query_Role(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var en = conn.T_ROLE.Select(c => c).Where(c => c.ROLE_ID == id).Single();
                return en;
            }
            catch { return null; }
        }
    }
}
