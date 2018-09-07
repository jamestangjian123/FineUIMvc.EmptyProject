using FineUIMvc.EmptyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FineUIMvc.EmptyProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ///清除所有SESSION
            Session.Clear();
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        public JsonResult Login(string name, string pwd)
        {
            //string name = Request.Form["txtName"];
            //string pwd = Request.Form["txtPwd"];

            var user = TUser_LinqDal.Get_LoginUser(name, pwd);

            if (user == null)
                return Json( "用户名或密码错误!", JsonRequestBehavior.AllowGet);
            else
            {
                if (user.U_STATUS == 0)
                    return Json("此用户已被禁用，请联系管理员!", JsonRequestBehavior.AllowGet);

                Models.SessionValue.AddUserSession(Session, user);
                ///记录LOG日志
                Models.ShenGoLog.Add_LoginLog(Session);

                return Json("OK", JsonRequestBehavior.AllowGet);
            }

            /*if (tbxUserName == "admin" && tbxPassword == "admin")
            {
                ShowNotify("成功登录！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }*/
        }
    }
}