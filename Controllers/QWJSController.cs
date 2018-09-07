using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// 以下为本系统的需要引用的命名空间
using FineUIMvc;
using Newtonsoft.Json.Linq;
using FineUIMvc.EmptyProject.Models;

namespace FineUIMvc.EmptyProject.Controllers
{
    /// <summary>
    /// 全文检索
    /// </summary>
    public class QWJSController : Controller
    {
        // GET: QWJS
        public ActionResult Index()
        {
            ///每次加载此页面时，先清除档案选择的类型
            Models.SessionValue.ClearSelectArchiveType(Session);
            ViewBag.Grid1Columns = Models.GridColumns.InitQWJSColumns();
            return View();
        }
        public ActionResult XMJS()
        {
            ///每次加载此页面时，先清除档案选择的类型
            Models.SessionValue.ClearSelectArchiveType(Session);
            ViewBag.Grid1Columns = Models.GridColumns.InitXMJSColumns();
            return View();
        }
        public ActionResult AJJS()
        {
            ///每次加载此页面时，先清除档案选择的类型
            Models.SessionValue.ClearSelectArchiveType(Session);
            ViewBag.Grid1Columns = Models.GridColumns.InitAJJSColumns();
            return View();
        }
        public ActionResult JNJS()
        {
            ///每次加载此页面时，先清除档案选择的类型
            var user = Models.SessionValue.GetUserSession(Session) as V_USER;
            bool bview;
            if (user.U_ROLEID == 1 || user.U_ROLEID == 2)
                bview = true;
            else
                bview = false;
            Models.SessionValue.ClearSelectArchiveType(Session);
            ViewBag.Grid1Columns = Models.GridColumns.InitJNJSColumns(bview);
            return View();
        }

        public ActionResult MGJS()
        {
            Models.SessionValue.ClearSelectArchiveType(Session);
            ViewBag.Grid1Columns = Models.GridColumns.InitMGJSColumns();
            return View();
        }
        /// <summary>
        /// 全文检索
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnQuery_QWSJ(JArray Grid1_fields, string content)
        {
            int count = 0;
            if (content == "")
            {
                Alert.ShowInParent("请填写关键字!");
            }

            else 
            {
                Models.ArchivePagerWhere pager = new Models.ArchivePagerWhere();
                pager.TableName = "V_TEXT1";
                pager.Order = "DOC_ATYPE";
                pager.Select = "*";
                pager.iStart = 0;
                pager.iEnd = Commons.iPagerRow;
                pager.Where = "where TEXT_CONTENT like" + "'%" + content + "%'";
                //else 
                //    pager.Where = "where TEXT_CONTENT like '%" + content + "%'and TEXT_CONTENT like '%" + content1 + "%'";
                Session.Add(Models.SessionValue._sArchivePager, pager);
                var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
                ViewBag.Grid1RecordCount = count;
               // ViewBag.Grid1DataSource = dt;

                //var list = FineUIMvc.EmptyProject.Models.Linq.VText_LinqDal.Query_Text(content, content1, 0, Commons.iPagerRow, out count);
                var grid = UIHelper.Grid("Grid1");
                grid.RecordCount(count);
                grid.DataSource(dt, Grid1_fields);
            }

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseWindows()
        {
            string[] strs = Models.SessionValue.GetSelectArchiveType(Session);
            var lbl = UIHelper.Label("lbl_ArchiveType");
            var txt = UIHelper.HiddenField("txt_ArchiveType");
            if (strs == null)
            {
                lbl.Text("");
                txt.Text("");
            }
            else
            {
                lbl.Text(strs[1]);
                txt.Text(strs[0]);
            }
            return UIHelper.Result();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="Grid1_fields"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult win_AdvQueryClose(JArray Grid1_fields)
        {
            //Alert.Show("触发了窗体的关闭事件！");
            ///UIHelper.Window("win_AdvQuery").Hide();

            if (Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[Models.SessionValue._sArchivePager] as Models.ArchivePagerWhere;

                //UIHelper.Window("Window1").Hide();
                var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
                // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
                ViewBag.Grid1RecordCount = count;
                // 2.获取当前分页数据
                ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);
                var grid1 = UIHelper.Grid("Grid1");
                grid1.DataSource(dt, Grid1_fields);
                grid1.RecordCount(count);
            }

            return UIHelper.Result();
        }
        //分页
        public ActionResult Grid1_PageIndexChanged(JArray Grid1_fields, int Grid1_pageIndex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[Models.SessionValue._sArchivePager] as Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[Models.SessionValue._sArchivePager] = pager;
                var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
                // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
                //ViewBag.Grid1RecordCount = count;
                // 2.获取当前分页数据
                //ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);
                grid1.RecordCount(count);
                grid1.DataSource(dt, Grid1_fields);

            }

            return UIHelper.Result();
        }
        /// <summary>
        /// 显示电子文件
        /// </summary>
        /// <param name="type">0. 案卷或一文一件, 1.卷内目录</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewFile(string type = "0", string id = "0", int rid = 0)
        {
            try
            {
                string url = "";
                if (type == "0")
                {
                    var en = TDocument_LinqDal.Get_DocumentWSAJ(int.Parse(id));
                    url = en.DOC_PATH + "/" + en.DOC_FILE;
                    Models.ShenGoLog.Add_ViewLog(Session, en);
                }
                else if (type == "1")
                {
                    var en = TDocument_LinqDal.Get_DocumentWSJN(int.Parse(id));
                    url = en.DOC_PATH + "/" + en.DOC_FILE;
                    Models.ShenGoLog.Add_ViewLog(Session, en);

                }
                else if (type == "2")
                {
                    var en = TDocument_LinqDal.Get_DocumentMG(int.Parse(id));
                    url = en.DOC_PATH + "/" + en.DOC_FILE;
                    Models.ShenGoLog.Add_ViewLog(Session, en);

                }
                url = url.Replace("~", "");
                url = "../../res/generic/web/viewer.html?file=" + url;
                UIHelper.Window("win_MLPic").Show(url, "电子文件");
            }
            catch { Alert.ShowInParent("无电子文件!"); }
            return UIHelper.Result();
        }
        public JsonResult Scan(string mid)
        {
            
            var user = Models.SessionValue.GetUserSession(Session) as V_USER;
            bool bView = TUser_LinqDal.Query_MGQX(mid, user.U_ID);

            if (bView == false)
                return Json("你没有浏览的权利!", JsonRequestBehavior.AllowGet);
            else
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
        }
    }
}