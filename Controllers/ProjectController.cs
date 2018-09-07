using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FineUIMvc;
using Newtonsoft.Json.Linq;
using FineUIMvc.EmptyProject.Models;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Reflection;
using System.Data;
using FineUIMvc.EmptyProject.Models.Linq;
using Aspose.Cells;

namespace FineUIMvc.EmptyProject.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        public ActionResult Index(string mid, string name)
        { 
            var list = FineUIMvc.EmptyProject.Models.GridColumns.InitPROQueryColumns();
            ViewBag.MID = mid;
            ViewBag.NAME = name;
            ViewBag.Grid1Columns = list;

            ///页面加载时先查询第一次
            FineUIMvc.EmptyProject.Models.ArchivePagerWhere pager = new FineUIMvc.EmptyProject.Models.ArchivePagerWhere();
            pager.TableName = "T_ProjectInfo";
            pager.Order = "PR_ID";
            pager.Select = "*";
            pager.iStart = 0;
            pager.iEnd = Commons.iPagerRow;
            pager.Where = "where PR_MID=" + mid;

            Session.Add(FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerProject, pager);

            int count = 0;
            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            ViewBag.Grid1RecordCount = count;
            // 2.获取当前分页数据
            ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);

            ////词典
            ViewBag.WD = Get_WD();
            return View();
        }

        //分页
        public ActionResult Grid1_PageIndexChanged(JArray Grid1_fields, int Grid1_pageIndex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[Models.SessionValue._sArchivePagerProject] as Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[Models.SessionValue._sArchivePagerProject] = pager;
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
        /// 单体
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Single(string aid, string name)
        {
            var list = FineUIMvc.EmptyProject.Models.GridColumns.InitSingleQueryColumns();
            ViewBag.PID = aid;

            ViewBag.NAME = TProject_LinqDal.Query_PRName(int.Parse(aid)).PR_Name+"的单体信息";
            ViewBag.Grid1Columns = list;


            ///页面加载时先查询第一次
            FineUIMvc.EmptyProject.Models.ArchivePagerWhere pager = new FineUIMvc.EmptyProject.Models.ArchivePagerWhere();
            pager.TableName = "T_SINGLEINFO";
            pager.Order = "SG_ID";
            pager.Select = "*";
            pager.iStart = 0;
            pager.iEnd = Commons.iPagerRow;
            pager.Where = "where SG_PID=" + aid;

            Session.Add(FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerST, pager);

            int count = 0;
            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            ViewBag.Grid1RecordCount = count;
            // 2.获取当前分页数据
            ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);
            ViewBag.WD = Get_WD();
            return View();
        }

        /// <summary>
        /// 获取词典
        /// </summary>
        private List<T_SYSWD> Get_WD()
        {
            try
            {
                return TSysAr_LinqDal.Query_Word(null, null).ToList();
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取BGXQ的数字
        /// </summary>
        private string Get_NUM(string a)
        {

            if (a == "永久")
                return "1";
            else if (a == "长期")
                return "2";
            else if (a == "25年")
                return "3";
            else if (a == "15年")
                return "4";
            else
                return "5";
        }
        /// <summary>
        /// 获取BGXQ的文本
        /// </summary>
        private string Get_TEXT(string a)
        {
            try
            {
                if (a == "1")
                    return "永久";
                else if (a == "2")
                    return "长期";
                else if (a == "3")
                    return "25年";
                else if (a == "4")
                    return "15年";
                else
                    return "短期";

            }
            catch { return null; }
        }
        /// <summary>
        /// 项目信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public ActionResult PROInfo(string mid, string prid)
        {
            ViewBag.PRID = "";
            ViewBag.MID = mid;

            var listType = TSysAr_LinqDal.Query_WordType();
            foreach (var n in listType)
            {
                var nn = FineUIMvc.EmptyProject.Models.ComItems.InitComboBoxQueryWord(n);
                ViewData.Add(n, nn);
            }
            if (prid == null || prid == "")
            {
                ////新建卷内ID
                ViewBag.StartDay ="";
                ViewBag.EndDay = "";
                ViewBag.StorageDay = "";
                ViewBag.BZDay = "";
                //ViewBag.BGQX = "";
            }
            else
            {
                ////修改卷内ID
                ViewBag.PRID = prid;
                var en = TProject_LinqDal.Query_Project(int.Parse(prid));
                ViewBag.Name = en.PR_Name.ToString();
                ViewBag.BuildUnit = en.PR_BuildUnit.ToString();
                ViewBag.BH = en.PR_BH.ToString();
                if (en.PR_BGQX == null)
                    ViewBag.BGQX = "";
                else
                    ViewBag.BGQX =Get_NUM(en.PR_BGQX.ToString());
                if (en.PR_BuildUnitAddress == null)
                    ViewBag.BuildUnitAddress = "";
                else
                    ViewBag.BuildUnitAddress = en.PR_BuildUnitAddress.ToString();
                if (en.PR_BuildAdress == null)
                    ViewBag.BuildAdress = "";
                else
                    ViewBag.BuildAdress = en.PR_BuildAdress.ToString();
                if (en.PR_DesignUnit == null)
                    ViewBag.DesignUnit = "";
                else
                    ViewBag.DesignUnit = en.PR_DesignUnit.ToString();
                if (en.PR_BuildUnitPhone == null)
                    ViewBag.BuildUnitPhone = "";
                else
                    ViewBag.BuildUnitPhone = en.PR_BuildUnitPhone.ToString();
                if (en.PR_ConstructionUnit == null)
                    ViewBag.ConstructionUnit = "";
                else
                    ViewBag.ConstructionUnit = en.PR_ConstructionUnit.ToString();
                if (en.PR_SupervisionName == null)
                    ViewBag.SupervisionName = "";
                else
                    ViewBag.SupervisionName = en.PR_SupervisionName.ToString();

                if (en.PR_TotalArea == null)
                    ViewBag.TotalArea = "";
                else
                    ViewBag.TotalArea = en.PR_TotalArea.ToString();
                if (en.PR_StartDay == null)
                    ViewBag.StartDay = "";
                else
                    ViewBag.StartDay =Convert.ToDateTime(en.PR_StartDay.ToString());
                if (en.PR_EndDay == null)
                    ViewBag.EndDay = "";
                else
                    ViewBag.EndDay = Convert.ToDateTime(en.PR_EndDay.ToString());
                if (en.PR_StorageDay == null)
                    ViewBag.StorageDay = "";
                else
                    ViewBag.StorageDay = Convert.ToDateTime(en.PR_StorageDay.ToString());
                if (en.PR_TotalInvest == null)
                    ViewBag.TotalInvest = "";
                else
                    ViewBag.TotalInvest = en.PR_TotalInvest.ToString();
                if (en.PR_ARReceiver == null)
                    ViewBag.ARReceiver = "";
                else
                    ViewBag.ARReceiver = en.PR_ARReceiver.ToString();
                if (en.PR_RKReceiver == null)
                    ViewBag.RKReceiver = "";
                else
                    ViewBag.RKReceiver = en.PR_RKReceiver.ToString();
                if (en.PR_TotalNum == null)
                    ViewBag.TotalNum = "";
                else
                    ViewBag.TotalNum = en.PR_TotalNum.ToString();
                if (en.PR_FileNum == null)
                    ViewBag.FileNum = "";
                else
                    ViewBag.FileNum = en.PR_FileNum.ToString();
                if (en.PR_ImageNum == null)
                    ViewBag.ImageNum = "";
                else
                    ViewBag.ImageNum = en.PR_ImageNum.ToString();
                if (en.PR_PhotoNum == null)
                    ViewBag.PhotoNum = "";
                else
                    ViewBag.PhotoNum = en.PR_PhotoNum.ToString();
                if (en.PR_VideoNum == null)
                    ViewBag.VideoNum = "";
                else
                    ViewBag.VideoNum = en.PR_VideoNum.ToString();
                if (en.PR_VoiceNum == null)
                    ViewBag.VoiceNum = "";
                else
                    ViewBag.VoiceNum = en.PR_VoiceNum.ToString();

                if (en.PR_BZUnit == null)
                    ViewBag.BZUnit = "";
                else
                    ViewBag.BZUnit = en.PR_BZUnit.ToString();
                if (en.PR_BZDay == null)
                    ViewBag.BZDay = "";
                else
                    ViewBag.BZDay =Convert.ToDateTime(en.PR_BZDay.ToString());
                if (en.PR_ProAccounts == null)
                    ViewBag.ProAccounts= "";
                else
                    ViewBag.ProAccounts = en.PR_ProAccounts.ToString();
                if (en.PR_ProAccountsUnit == null)
                    ViewBag.ProAccountsUnit = "";
                else
                    ViewBag.ProAccountsUnit = en.PR_ProAccountsUnit.ToString();
                // ViewBag.RQ = en.JN_DATE.ToString();
            }

            return View();
        }
        /// <summary>
        /// 添加项目信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProject(FormCollection values)
        {
            string mid = values["txt_MID"];
            if (mid == "")
            {
                Alert.ShowInParent("未获取案卷信息!");
                return UIHelper.Result();
            }

            T_ProjectInfo en = new T_ProjectInfo();
            en.PR_BH = values["txt_PR_BH"];
            en.PR_Name=values["txt_Name"];
            en.PR_BuildUnit = values["txt_BuildUnit"];
            en.PR_BuildUnitPhone = values["txt_BuildUnitPhone"];
            en.PR_BuildUnitAddress = values["txt_BuildUnitAddress"];
            en.PR_BuildAdress = values["txt_BuildAdress"];
            en.PR_DesignUnit = values["txt_DesignUnit"];
            en.PR_ConstructionUnit = values["txt_ConstructionUnit"];
            en.PR_SupervisionName = values["txt_SupervisionName"];
            en.PR_TotalArea = values["txt_TotalArea"];
            if (values["txt_StartDay"] == null || values["txt_StartDay"] == "")
                en.PR_StartDay = null;
            else
                en.PR_StartDay = DateTime.Parse(values["txt_StartDay"]);
            if (values["txt_EndDay"] == null || values["txt_EndDay"] == "")
                en.PR_EndDay=null;
            else
                en.PR_EndDay = DateTime.Parse(values["txt_EndDay"]);
            if (values["txt_StorageDay"] == null || values["txt_StorageDay"] == "")
                en.PR_StorageDay = null;
            else
                en.PR_StorageDay = DateTime.Parse(values["txt_StorageDay"]);
            if (values["cmb_BGQX"] == null || values["cmb_BGQX"] == "")
                en.PR_BGQX = null;
            else
                en.PR_BGQX =Get_TEXT(values["cmb_BGQX"]);
            en.PR_TotalInvest = values["txt_TotalInvest"];
            en.PR_ARReceiver = values["txt_ARReceiver"];
            en.PR_RKReceiver = values["txt_RKReceiver"];
            if (values["txt_TotalNum"] == null || values["txt_TotalNum"] == "")
                en.PR_TotalNum = null;
            else
                en.PR_TotalNum = int.Parse(values["txt_TotalNum"]);
            if (values["txt_FileNum"] == null || values["txt_FileNum"] == "")
                en.PR_FileNum = null;
            else
                en.PR_FileNum = int.Parse(values["txt_FileNum"]);
            if (values["txt_ImageNum"] == null || values["txt_ImageNum"] == "")
                en.PR_ImageNum = null;
            else
                en.PR_ImageNum = int.Parse(values["txt_ImageNum"]);
            if (values["txt_PhotoNum"] == null || values["txt_PhotoNum"] == "")
                en.PR_PhotoNum = null;
            else
                en.PR_PhotoNum = int.Parse(values["txt_PhotoNum"]);
            if (values["txt_VideoNum"] == null || values["txt_VideoNum"] == "")
                en.PR_VideoNum = null;
            else
                en.PR_VideoNum = int.Parse(values["txt_VideoNum"]);
            if (values["txt_VoiceNum"] == null || values["txt_VoiceNum"] == "")
                en.PR_VoiceNum = null;
            else
                en.PR_VoiceNum = int.Parse(values["txt_VoiceNum"]);
            en.PR_BZUnit = values["txt_BZUnit"];
            if (values["txt_BZDay"] == null || values["txt_BZDay"] == "")
                en.PR_BZDay = null;
            else
                en.PR_BZDay = DateTime.Parse(values["txt_BZDay"]);
            en.PR_ProAccounts = values["txt_ProAccounts"];
            en.PR_ProAccountsUnit = values["txt_ProAccountsUnit"];
            en.PR_MID =int.Parse(values["txt_MID"]);
            string prid = values["txt_ID"];
            int value = -1;
            if (prid == null || prid == "")
            {
                value = TProject_LinqDal.Add_Project(en);
                if (value == 0)
                    Alert.ShowInParent("重复的序号!");
                else if (value == -1)
                    Alert.ShowInParent("添加失败!");
                else
                    Alert.ShowInParent("添加成功!");
            }
            else
            {
                value = TProject_LinqDal.Modify_Project(int.Parse(prid), en);
                if (value == -1)
                    Alert.ShowInParent("修改失败!");
                else
                    Alert.ShowInParent("修改成功!");
            }
            return UIHelper.Result();          
        }
        /// <summary>
        /// 添加或修改项目刷新 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult windAdd_Close(JArray Grid1_fields, int pageindex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            
            //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePager] as FineUIMvc.EmptyProject.Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * pageindex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePager] = pager;
                //UIHelper.Window("Window1").Hide();
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
        /// 单体信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public ActionResult SInfo(string pid, string sgid)
        {
            ViewBag.SG_ID = "";
            ViewBag.PID = pid;

            var listType = TSysAr_LinqDal.Query_WordType();
            foreach (var n in listType)
            {
                var nn = FineUIMvc.EmptyProject.Models.ComItems.InitComboBoxQueryWord(n);
                ViewData.Add(n, nn);
            }
            if (sgid == null || sgid == "")
            {
                ////新建卷内ID
                //ViewBag.StartDay = "";
                //ViewBag.EndDay = "";
                //ViewBag.StorageDay = "";
                //ViewBag.BZDay = "";
                //ViewBag.BGQX = "";
            }
            else
            {
                ////修改卷内ID
                ViewBag.SG_ID = sgid;
                var en = TProject_LinqDal.Query_SINGLE(int.Parse(sgid));
                ViewBag.Name = en.SG_Name.ToString();
                if (en.SG_Xnum == null)
                    ViewBag.Xnum = "";
                else
                    ViewBag.Xnum = en.SG_Xnum.ToString();
                if (en.SG_Type == null)
                    ViewBag.Type = "";
                else
                    ViewBag.Type = en.SG_Type.ToString();
                if (en.SG_Area == null)
                    ViewBag.Area = "";
                else
                    ViewBag.Area = en.SG_Area.ToString();
                if (en.SG_Form == null)
                    ViewBag.Form = "";
                else
                    ViewBag.Form = en.SG_Form.ToString();
                if (en.SG_Bnum == null)
                    ViewBag.Bnum = "";
                else
                    ViewBag.Bnum = en.SG_Bnum.ToString();
                if (en.SG_High == null)
                    ViewBag.High = "";
                else
                    ViewBag.High = en.SG_High.ToString();
                if (en.SG_Znum == null)
                    ViewBag.Znum = "";
                else
                    ViewBag.Znum = en.SG_Znum.ToString();

                if (en.SG_Deep == null)
                    ViewBag.Deep = "";
                else
                    ViewBag.Deep = en.SG_Deep.ToString();
                if (en.SG_Underfloor == null)
                    ViewBag.Underfloor = "";
                else
                    ViewBag.Underfloor = en.SG_Underfloor.ToString();
                if (en.SG_Overfloor == null)
                    ViewBag.Overfloor = "";
                else
                    ViewBag.Overfloor = en.SG_Overfloor.ToString();
                if (en.SG_CZ == null)
                    ViewBag.CZ = "";
                else
                    ViewBag.CZ = en.SG_CZ.ToString();
                if (en.SG_Length == null)
                    ViewBag.Length = "";
                else
                    ViewBag.Length = en.SG_Length.ToString();
                if (en.SG_Vnum == null)
                    ViewBag.Vnum = "";
                else
                    ViewBag.Vnum = en.SG_Vnum.ToString();
                if (en.SG_PD == null)
                    ViewBag.PD = "";
                else
                    ViewBag.PD = en.SG_PD.ToString();
                if (en.SG_Norms == null)
                    ViewBag.Norms = "";
                else
                    ViewBag.Norms = en.SG_Norms.ToString();
                if (en.SG_Dunit == null)
                    ViewBag.Dunit = "";
                else
                    ViewBag.Dunit = en.SG_Dunit.ToString();
                if (en.SG_Wunit == null)
                    ViewBag.Wunit = "";
                else
                    ViewBag.Wunit = en.SG_Wunit.ToString();
                if (en.SG_Junit == null)
                    ViewBag.Junit = "";
                else
                    ViewBag.Junit = en.SG_Junit.ToString();
            }

            return View();
        }
        /// <summary>
        /// 添加单体信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSingle(FormCollection values)
        {
            string mid = values["txt_MID"];
            if (mid == "")
            {
                Alert.ShowInParent("未获取案卷信息!");
                return UIHelper.Result();
            }

            T_SINGLEINFO en = new T_SINGLEINFO();
            //en.SG_ID = values["txt_Name"];
            en.SG_Name = values["txt_Name"];
            en.SG_Xnum = values["txt_Xnum"];
            en.SG_Type = values["txt_Type"];
            en.SG_Area = values["txt_Area"];
            en.SG_Form = values["txt_Form"];
            en.SG_Bnum = values["txt_Bnum"];
            en.SG_High = values["txt_High"];
            en.SG_Znum = values["txt_Znum"];
            en.SG_Deep = values["txt_Deep"];
            en.SG_Underfloor = values["txt_Underfloor"];
            en.SG_Overfloor = values["txt_Overfloor"];
            en.SG_CZ = values["txt_CZ"];
            en.SG_Length = values["txt_Length"];
            en.SG_Vnum = values["txt_Vnum"];
            en.SG_PD = values["txt_PD"];
            en.SG_Norms = values["txt_Norms"];
            en.SG_Dunit = values["txt_Wunit"];
            en.SG_Junit = values["txt_Junit"];
            en.SG_PID = values["txt_PID"];
            string prid = values["txt_ID"];
            int value = -1;
            if (prid == null || prid == "")
            {
                value = TProject_LinqDal.Add_SINGLE(en);
                if (value == 0)
                    Alert.ShowInParent("重复的序号!");
                else if (value == -1)
                    Alert.ShowInParent("添加失败!");
                else
                    Alert.ShowInParent("添加成功!");
            }
            else
            {
                value = TProject_LinqDal.Modify_SINGLE(int.Parse(prid), en);
                if (value == -1)
                    Alert.ShowInParent("修改失败!");
                else
                    Alert.ShowInParent("修改成功!");
            }
            return UIHelper.Result();
        }
        /// <summary>
        /// 添加或修改单体刷新 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult windAdd_CloseST(JArray Grid1_fields, int pageindex)
        {
            var grid1 = UIHelper.Grid("Grid1");

            //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerST] as FineUIMvc.EmptyProject.Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * pageindex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerST] = pager;
                //UIHelper.Window("Window1").Hide();
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
        /// 删除项目
        /// </summary>
        /// <param name="opID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnWSDel_Click(JArray Grid1_fields, int Grid1_pageIndex, string opID)
        {
            try
            {
                int value = TProject_LinqDal.Delete_PRO(int.Parse(opID));
                if (value == -1)
                    Alert.ShowInParent("删除失败!");
                else
                {
                    Alert.ShowInParent("档案删成功!");
                    //Models.ShenGoLog.Add_ArchiveLog(Session, opID.ToString());

                    var grid1 = UIHelper.Grid("Grid1");

                    //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
                    {
                        int count = 0;
                        var pager = Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePager] as FineUIMvc.EmptyProject.Models.ArchivePagerWhere;
                        pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                        if (pager.iStart > 0)
                            pager.iStart++;
                        pager.iEnd = pager.iStart + Commons.iPagerRow;
                        Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePager] = pager;
                        //UIHelper.Window("Window1").Hide();
                        var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
                        // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
                        //ViewBag.Grid1RecordCount = count;
                        // 2.获取当前分页数据
                        //ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);
                        grid1.RecordCount(count);
                        grid1.DataSource(dt, Grid1_fields);

                    }
                }
            }
            catch { Alert.ShowInParent("删除文件失败!"); }
            return UIHelper.Result();
        }
        /// <summary>
        /// 删除单体
        /// </summary>
        /// <param name="opID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnWSDel_ClickST(JArray Grid1_fields, int Grid1_pageIndex, string opID)
        {
            try
            {
                int value = TProject_LinqDal.Delete_SG(int.Parse(opID));
                if (value == -1)
                    Alert.ShowInParent("删除失败!");
                else
                {
                    Alert.ShowInParent("档案删成功!");
                    //Models.ShenGoLog.Add_ArchiveLog(Session, opID.ToString());

                    var grid1 = UIHelper.Grid("Grid1");

                    //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
                    {
                        int count = 0;
                        var pager = Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerST] as FineUIMvc.EmptyProject.Models.ArchivePagerWhere;
                        pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                        if (pager.iStart > 0)
                            pager.iStart++;
                        pager.iEnd = pager.iStart + Commons.iPagerRow;
                        Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerST] = pager;
                        //UIHelper.Window("Window1").Hide();
                        var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
                        // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
                        //ViewBag.Grid1RecordCount = count;
                        // 2.获取当前分页数据
                        //ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);
                        grid1.RecordCount(count);
                        grid1.DataSource(dt, Grid1_fields);

                    }
                }
            }
            catch { Alert.ShowInParent("删除文件失败!"); }
            return UIHelper.Result();
        }
        ///// <summary>
        ///// 导出数据(项目级)
        ///// </summary>
        ///// <param name="selected"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[STAThread]
        //public ActionResult btnleadingoutXM_Click(FormCollection values)
        //{
        //    int MODEL_ID = int.Parse(values["txt_AID"]);
        //    GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //    var Qu = conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_MID == MODEL_ID).ToList();
        //    if (Qu.Count == 0)
        //    {
        //        Alert.ShowInParent("无数据导出");
        //    }
        //    else
        //    {
        //        int i = ImportDialog(MODEL_ID);
        //        if (i == 1)
        //        {
        //            Alert.ShowInParent("导出成功!");
        //        }
        //        else
        //        {
        //            Alert.ShowInParent("导出失败!");
        //        }
        //    }
        //    return UIHelper.Result();
        //}

        /// <summary>
        /// 导出数据(项目级)
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [STAThread]
        public ActionResult btnleadingoutXM_Click(FormCollection values)
        {
            int MODEL_ID = int.Parse(values["txt_AID"]);
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var Qu = conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_MID == MODEL_ID).ToList();
            if (Qu.Count == 0)
            {
                Alert.ShowInParent("无数据导出");
            }
            else
            {
                string path = ImportDialog(MODEL_ID);
                if (!string.IsNullOrEmpty(path))
                {
                    string url = "/Dialog/ShowDownloadFile?FileName=" + path;
                    UIHelper.Window("win_MLDown").Show(url, "下载文件");
                }
                else
                {
                    Alert.ShowInParent("导出失败!");
                }
            }
            return UIHelper.Result();
        }


        /// <summary>
        /// WSAJ导出的数据的线程（无上级）
        /// </summary>
        public string  ImportDialog(int mid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var PRO = conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_MID == mid).ToList();
                DataTable query = Exce_Word1.ConvertToDataTable(PRO);// ConvertToDataTable();
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                bool exist = System.IO.Directory.Exists("D:\\导出数据");
                if (exist == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\导出数据");
                }
                string path = "D:\\导出数据\\" + date + ".xls";

                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0]; //工作表 
                sheet.Name = "T_Project";
                Cells cells = sheet.Cells;//单元格 
                for (int i = 0; i < query.Columns.Count; i++)
                {
                    cells[0, i].PutValue(query.Columns[i].ColumnName);
                    cells.SetRowHeight(0, 25);
                }
                for (int i = 0; i < query.Rows.Count; i++)
                {
                    for (int k = 0; k < query.Columns.Count; k++)
                    {
                        if (query.Columns[k].ColumnName == query.Columns["PR_StartDay"].ColumnName)
                        {
                            if (query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        } 
                        else if (query.Columns[k].ColumnName == query.Columns["PR_EndDay"].ColumnName)
                        {
                            if ( query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        } 
                        else if (query.Columns[k].ColumnName == query.Columns["PR_StorageDay"].ColumnName)
                        {
                            if ( query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        } 
                        else if (query.Columns[k].ColumnName == query.Columns["PR_BZDay"].ColumnName)
                        {
                            if ( query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        } 
                        else
                            cells[1 + i, k].PutValue(query.Rows[i][k].ToString());
                    }
                    cells.SetRowHeight(1 + i, 24);
                }

                string PRBH = query.Rows[0]["PR_BH"].ToString();
                var Res = conn.T_WSAJ1.Select(c => new { c.WS1_JH, c.WS1_TM, c.WS1_YEAR, c.WS1_BGQX, c.WS1_GDRQ, c.WS1_HJBH, c.WS1_BZ, c.WS1_PID, c.WS1_MID }).Where(c => c.WS1_PID == PRBH).ToList();
                for (int nn = 1; nn < query.Rows.Count; nn++)
                {
                    PRBH = query.Rows[nn]["PR_BH"].ToString();
                    var aa = conn.T_WSAJ1.Select(c => new { c.WS1_JH, c.WS1_TM, c.WS1_YEAR, c.WS1_BGQX, c.WS1_GDRQ, c.WS1_HJBH, c.WS1_BZ, c.WS1_PID, c.WS1_MID }).Where(c => c.WS1_PID == PRBH).ToList();
                    Res.AddRange(aa);
                }
                workbook.Worksheets.Add("T_WSAJ1");
                DataTable query1 = Exce_Word1.ConvertToDataTable(Res);
                Worksheet sheet1 = workbook.Worksheets[1]; //工作表 
                Cells cells1 = sheet1.Cells;//单元格 
                for (int i = 0; i < query1.Columns.Count; i++)
                {
                    cells1[0, i].PutValue(query1.Columns[i].ColumnName);
                    cells1.SetRowHeight(0, 25);
                }
                for (int i = 0; i < query1.Rows.Count; i++)
                {
                    for (int k = 0; k < query1.Columns.Count; k++)
                    {
                        if (query1.Columns[k].ColumnName == query1.Columns["WS1_GDRQ"].ColumnName)
                        {
                            if (query1.Rows[i][k].ToString() != "")
                            { cells1[1 + i, k].PutValue(((DateTime)query1.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells1[1 + i, k].PutValue(query1.Rows[i][k].ToString()); }
                        }
                           // cells1[1 + i, k].PutValue(((DateTime)query1.Rows[i][k]).ToString("yyyy-MM-dd"));
                        else
                            cells1[1 + i, k].PutValue(query1.Rows[i][k].ToString());
                    }
                    cells1.SetRowHeight(1 + i, 24);
                }
                

                string AJID = query1.Rows[0]["WS1_JH"].ToString();
                var Res1 = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
                for (int nn = 1; nn < query1.Rows.Count; nn++)
                {
                    AJID = query1.Rows[nn]["WS1_JH"].ToString();
                    var aa = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
                    Res1.AddRange(aa);
                }
                workbook.Worksheets.Add("T_WSJN1");
                DataTable query2 = Exce_Word1.ConvertToDataTable(Res1);
                Worksheet sheet2 = workbook.Worksheets[2]; //工作表 
                Cells cells2 = sheet2.Cells;//单元格 
                for (int i = 0; i < query2.Columns.Count; i++)
                {
                    cells2[0, i].PutValue(query2.Columns[i].ColumnName);
                    cells2.SetRowHeight(0, 25);
                }
                for (int i = 0; i < query2.Rows.Count; i++)
                {
                    for (int k = 0; k < query2.Columns.Count; k++)
                    {
                        if (query2.Columns[k].ColumnName == query2.Columns["JN1_RQ"].ColumnName)
                        {
                            if (query2.Rows[i][k].ToString() != "")
                            { cells2[1 + i, k].PutValue(((DateTime)query2.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells2[1 + i, k].PutValue(query2.Rows[i][k].ToString()); }
                        }
                           // cells2[1 + i, k].PutValue(((DateTime)query2.Rows[i][k]).ToString("yyyy-MM-dd"));
                        else
                            cells2[1 + i, k].PutValue(query2.Rows[i][k].ToString());
                    }
                    cells2.SetRowHeight(1 + i, 24);
                }

                workbook.Save(path);

                return path;
            }
            catch 
            {
                return string.Empty;
            }
        }

        ///// <summary>
        ///// WSAJ导出的数据的线程（无上级）
        ///// </summary>
        //public int ImportDialog(int mid)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var PRO = conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_MID == mid).ToList();
        //        DataTable query = Exce_Word1.ConvertToDataTable(PRO);// ConvertToDataTable();
        //        string date = DateTime.Now.ToString("yyyyMMddHHmmss");
        //        bool exist = System.IO.Directory.Exists("D:\\导出数据");
        //        if (exist == false)
        //        {
        //            System.IO.Directory.CreateDirectory("D:\\导出数据");
        //        }
        //        string path = "D:\\导出数据\\" + date + ".xls";

        //        Workbook workbook = new Workbook();
        //        Worksheet sheet = workbook.Worksheets[0]; //工作表 
        //        sheet.Name = "T_Project";
        //        Cells cells = sheet.Cells;//单元格 
        //        for (int i = 0; i < query.Columns.Count; i++)
        //        {
        //            cells[0, i].PutValue(query.Columns[i].ColumnName);
        //            cells.SetRowHeight(0, 25);
        //        }
        //        for (int i = 0; i < query.Rows.Count; i++)
        //        {
        //            for (int k = 0; k < query.Columns.Count; k++)
        //            {
        //                if (query.Columns[k].ColumnName == query.Columns["PR_StartDay"].ColumnName)
        //                {
        //                    if (query.Rows[i][k].ToString() != "")
        //                    { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
        //                    else
        //                    { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
        //                }
        //                else if (query.Columns[k].ColumnName == query.Columns["PR_EndDay"].ColumnName)
        //                {
        //                    if (query.Rows[i][k].ToString() != "")
        //                    { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
        //                    else
        //                    { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
        //                }
        //                else if (query.Columns[k].ColumnName == query.Columns["PR_StorageDay"].ColumnName)
        //                {
        //                    if (query.Rows[i][k].ToString() != "")
        //                    { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
        //                    else
        //                    { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
        //                }
        //                else if (query.Columns[k].ColumnName == query.Columns["PR_BZDay"].ColumnName)
        //                {
        //                    if (query.Rows[i][k].ToString() != "")
        //                    { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
        //                    else
        //                    { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
        //                }
        //                else
        //                    cells[1 + i, k].PutValue(query.Rows[i][k].ToString());
        //            }
        //            cells.SetRowHeight(1 + i, 24);
        //        }

        //        string PRBH = query.Rows[0]["PR_BH"].ToString();
        //        var Res = conn.T_WSAJ1.Select(c => new { c.WS1_JH, c.WS1_TM, c.WS1_YEAR, c.WS1_BGQX, c.WS1_GDRQ, c.WS1_HJBH, c.WS1_BZ, c.WS1_PID, c.WS1_MID }).Where(c => c.WS1_PID == PRBH).ToList();
        //        for (int nn = 1; nn < query.Rows.Count; nn++)
        //        {
        //            PRBH = query.Rows[nn]["PR_BH"].ToString();
        //            var aa = conn.T_WSAJ1.Select(c => new { c.WS1_JH, c.WS1_TM, c.WS1_YEAR, c.WS1_BGQX, c.WS1_GDRQ, c.WS1_HJBH, c.WS1_BZ, c.WS1_PID, c.WS1_MID }).Where(c => c.WS1_PID == PRBH).ToList();
        //            Res.AddRange(aa);
        //        }
        //        workbook.Worksheets.Add("T_WSAJ1");
        //        DataTable query1 = Exce_Word1.ConvertToDataTable(Res);
        //        Worksheet sheet1 = workbook.Worksheets[1]; //工作表 
        //        Cells cells1 = sheet1.Cells;//单元格 
        //        for (int i = 0; i < query1.Columns.Count; i++)
        //        {
        //            cells1[0, i].PutValue(query1.Columns[i].ColumnName);
        //            cells1.SetRowHeight(0, 25);
        //        }
        //        for (int i = 0; i < query1.Rows.Count; i++)
        //        {
        //            for (int k = 0; k < query1.Columns.Count; k++)
        //            {
        //                if (query1.Columns[k].ColumnName == query1.Columns["WS1_GDRQ"].ColumnName)
        //                {
        //                    if (query1.Rows[i][k].ToString() != "")
        //                    { cells1[1 + i, k].PutValue(((DateTime)query1.Rows[i][k]).ToString("yyyy-MM-dd")); }
        //                    else
        //                    { cells1[1 + i, k].PutValue(query1.Rows[i][k].ToString()); }
        //                }
        //                // cells1[1 + i, k].PutValue(((DateTime)query1.Rows[i][k]).ToString("yyyy-MM-dd"));
        //                else
        //                    cells1[1 + i, k].PutValue(query1.Rows[i][k].ToString());
        //            }
        //            cells1.SetRowHeight(1 + i, 24);
        //        }


        //        string AJID = query1.Rows[0]["WS1_JH"].ToString();
        //        var Res1 = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
        //        for (int nn = 1; nn < query1.Rows.Count; nn++)
        //        {
        //            AJID = query1.Rows[nn]["WS1_JH"].ToString();
        //            var aa = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
        //            Res1.AddRange(aa);
        //        }
        //        workbook.Worksheets.Add("T_WSJN1");
        //        DataTable query2 = Exce_Word1.ConvertToDataTable(Res1);
        //        Worksheet sheet2 = workbook.Worksheets[2]; //工作表 
        //        Cells cells2 = sheet2.Cells;//单元格 
        //        for (int i = 0; i < query2.Columns.Count; i++)
        //        {
        //            cells2[0, i].PutValue(query2.Columns[i].ColumnName);
        //            cells2.SetRowHeight(0, 25);
        //        }
        //        for (int i = 0; i < query2.Rows.Count; i++)
        //        {
        //            for (int k = 0; k < query2.Columns.Count; k++)
        //            {
        //                if (query2.Columns[k].ColumnName == query2.Columns["JN1_RQ"].ColumnName)
        //                {
        //                    if (query2.Rows[i][k].ToString() != "")
        //                    { cells2[1 + i, k].PutValue(((DateTime)query2.Rows[i][k]).ToString("yyyy-MM-dd")); }
        //                    else
        //                    { cells2[1 + i, k].PutValue(query2.Rows[i][k].ToString()); }
        //                }
        //                // cells2[1 + i, k].PutValue(((DateTime)query2.Rows[i][k]).ToString("yyyy-MM-dd"));
        //                else
        //                    cells2[1 + i, k].PutValue(query2.Rows[i][k].ToString());
        //            }
        //            cells2.SetRowHeight(1 + i, 24);
        //        }

        //        workbook.Save(path);

        //        System.IO.FileInfo file = new System.IO.FileInfo(path);//创建一个文件对象
        //        Response.Clear();//清除所有缓存区的内容
        //        Response.Charset = "GB2312";//定义输出字符集
        //        Response.ContentEncoding = System.Text.Encoding.Default;//输出内容的编码为默认编码
        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);//添加头信息。为“文件下载/另存为”指定默认文件名称
        //        Response.AddHeader("Content-Length", file.Length.ToString());//添加头文件，指定文件的大小，让浏览器显示文件下载的速度
        //        Response.WriteFile(file.FullName);// 把文件流发送到客户端
        //        Response.End();//将当前所有缓冲区的输出内容发送到客户端，并停止页面的执行
        //        return 1;
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}
	}
}