using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FineUIMvc;
using System.Net;
using FineUIMvc.EmptyProject.Models;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// shengo的LOG
    /// </summary>
    public class ShenGoLog
    {
        /// <summary>
        /// 写入登录Log
        /// </summary>
        /// <returns></returns>
        public static void Add_LoginLog(HttpSessionStateBase session)
        {
            var user = SessionValue.GetUserSession(session);
            if (user != null)
            {
                string ip = GetWebClientIp();
                if (ip == "::1")
                    ip = "127.0.0.1";
                //string strHostName = Dns.GetHostName(); //得到本机的主机名
                //IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP128
                //string ip = ipEntry.AddressList[0].ToString();
                TLog_LinqDal.Add_Log(new T_Log()
                {
                    Log_Date = DateTime.Now,
                    Log_Type = Get_LogType(enumLogType.Login),
                    Log_UName = user.U_REALNAME,
                    Log_UID = user.U_ID,
                    Log_Content = user.U_REALNAME + " 通过 IP: " + ip + " 的计算机登录系统"
                });
            }
        }

        /// <summary>
        /// 写入查看附件Log
        /// </summary>
        /// <param name="session"></param>
        public static void Add_ViewLog(HttpSessionStateBase session, T_DOCUMENT doc)
        {
            var user = SessionValue.GetUserSession(session);
            if (user != null)
            {
                TLog_LinqDal.Add_Log(new T_Log()
                {
                    Log_Date = DateTime.Now,
                    Log_Type = Get_LogType(enumLogType.ImgView),
                    Log_UName = user.U_REALNAME,
                    Log_UID = user.U_ID,
                    Log_Content = user.U_REALNAME + " 浏览了 " + doc.DOC_V01 + " 档号(件号)为: " + doc.DOC_V02 + " 题名为: " + doc.DOC_V03 + " 的电子附件"
                });
            }
        }
         //删除档案
        public static void Add_ArchiveLog(HttpSessionStateBase session, string Name)
        {
            var user = SessionValue.GetUserSession(session);
            if (user != null)
            {
                TLog_LinqDal.Add_Log(new T_Log()
                {
                    Log_Date = DateTime.Now,
                    Log_Type = Get_LogType(enumLogType.ArchiveDel),
                    Log_UName = user.U_REALNAME,
                    Log_UID = user.U_ID,
                    Log_Content = user.U_REALNAME + " 删除了 " + Name + " 档案编号的档案"
                });
            }
        }
        //添加或修改用户
        public static void Add_UserLog(HttpSessionStateBase session, string UserId, string UserName)
        {
            var user = SessionValue.GetUserSession(session);
            if (user != null)
            {
                if (UserId == null || UserId == "")
                {
                    TLog_LinqDal.Add_Log(new T_Log()
                    {
                        Log_Date = DateTime.Now,
                        Log_Type = Get_LogType(enumLogType.User),
                        Log_UName = user.U_REALNAME,
                        Log_UID = user.U_ID,
                        Log_Content = user.U_REALNAME + " 添加了名为“" + UserName + "”的用户"
                    });
                }
                else 
                {
                    TLog_LinqDal.Add_Log(new T_Log()
                    {
                        Log_Date = DateTime.Now,
                        Log_Type = Get_LogType(enumLogType.User),
                        Log_UName = user.U_REALNAME,
                        Log_UID = user.U_ID,
                        Log_Content = user.U_REALNAME + " 修改了用户“" + UserName + "”的信息"
                    });
                }
            }
        }
        //添加或修改角色
        public static void Add_RoleLog(HttpSessionStateBase session, string Name, string flag)
        {
            var user = SessionValue.GetUserSession(session);
            if (user != null)
            {
                if (flag == "1") 
                {
                    TLog_LinqDal.Add_Log(new T_Log()
                    {
                        Log_Date = DateTime.Now,
                        Log_Type = Get_LogType(enumLogType.Role),
                        Log_UName = user.U_REALNAME,
                        Log_UID = user.U_ID,
                        Log_Content = user.U_REALNAME + " 添加了名为“" + Name + "”的角色"
                    });
                }
                else 
                { 
                    TLog_LinqDal.Add_Log(new T_Log()
                    {
                        Log_Date = DateTime.Now,
                        Log_Type = Get_LogType(enumLogType.Role),
                        Log_UName = user.U_REALNAME,
                        Log_UID = user.U_ID,
                        Log_Content = user.U_REALNAME + " 修改了角色“" + Name + "”的权限"
                    });
                }
            }
        }

        public static string Get_LogType(enumLogType type)
        {
            string stype = "";
            switch(type)
            {
                case enumLogType.Login:
                    stype = "登录系统";
                    break;
                case enumLogType.ImgView:
                    stype = "浏览电子文件";
                    break;
                case enumLogType.User:
                    stype = "添加或修改用户";
                    break;
                case enumLogType.Role:
                    stype = "添加或修改角色";
                    break;
                case enumLogType.ArchiveDel:
                    stype = "删除档案";
                    break;
                default:
                    stype = "其他类型";
                    break;
            }
            return stype;
        }

        /// <summary>
        /// 获取web客户端ip
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {

            string userIP = "未获取用户IP";

            try
            {
                if (System.Web.HttpContext.Current == null
                 || System.Web.HttpContext.Current.Request == null
                 || System.Web.HttpContext.Current.Request.ServerVariables == null)
                {
                    return "";
                }

                string CustomerIP = "";

                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!String.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (CustomerIP == null)
                    {
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.Compare(CustomerIP, "unknown", true) == 0 || String.IsNullOrEmpty(CustomerIP))
                {
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                return CustomerIP;
            }
            catch { }

            return userIP;

        }
    }

    public enum enumLogType
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login = 0,
        /// <summary>
        /// 浏览电子文件
        /// </summary>
        ImgView = 1,
        /// <summary>
        /// 添加或修改用户
        /// </summary>
        User = 2,
        /// <summary>
        /// 添加或修改角色
        /// </summary>
        Role = 3,
        /// <summary>
        /// 删除档案
        /// </summary>
        ArchiveDel = 4


    }
}
