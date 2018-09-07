using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// 以下为本系统的需要引用的命名空间
using FineUIMvc;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Drawing;
using System.IO;
using FineUIMvc.EmptyProject.Models;

namespace FineUIMvc.EmptyProject.Controllers
{
    public class DialogController : Controller
    {
        // GET: Dialog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowQueryArchiveDialog(string aid)
        {
            Session.Add(Models.SessionValue._sArchiveEditAid, aid);
            Session.Add(Models.SessionValue._sIsQuery, "0");
            ViewBag.AID = aid;

            ///当重新打开查询界面时，删除之前的查询条件列表, 但不删除最终的查询条件
            Models.SessionValue.ClearArchiveWhereSession(Session);

            var items = FineUIMvc.EmptyProject.Models.ComItems.InitComboBoxQueryArchive(int.Parse(aid));
            ViewBag.Fields = items;
            return View();
        }
        #region  查询界面里的方法 
        /// <summary>
        /// 此方法为 前端选择了字段后，根据字段来生成其他下拉框的内容
        /// </summary>
        /// <param name="DropDownList1"></param>
        /// <param name="DropDownList1_text"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CmbFields_SelectedIndexChanged(string cmb_Fields, string cmb_Fields_text)
        {
            //ShowResult(DropDownList1, DropDownList1_text);
            string s = cmb_Fields;
            string[] strs = s.Split(';');
            int itype = int.Parse(strs[1]);
            ///where 类型
            var cmb = UIHelper.DropDownList("cmb_Where");
            cmb.LoadData(Models.ComItems.InitComboBoxQueryWhere(itype));


            var txt = UIHelper.TextBox("txt_Value");
            var cmbWord = UIHelper.DropDownList("cmb_Word");
            var date = UIHelper.DatePicker("date_Date");
            if (itype == (int)EnumShenGoFieldsType.Var || itype == (int)EnumShenGoFieldsType.Int)
            {
                txt.Hidden(false);
                date.Hidden(true);
                cmbWord.Hidden(true);
            }
            else if (itype == (int)EnumShenGoFieldsType.Datetime)
            {
                txt.Hidden(true);
                date.Hidden(false);
                cmbWord.Hidden(true);
            }
            else if (itype == (int)EnumShenGoFieldsType.Word)
            {
                txt.Hidden(true);
                date.Hidden(true);
                cmbWord.Hidden(false);
                ///绑定词典，如果是类型5，则说明是词典，获取 strs[3]个内容
                cmbWord.LoadData(Models.ComItems.InitComboBoxQueryWord(strs[3]));
            }
            return UIHelper.Result();
        }


        /// <summary>
        /// 此方法为 增加where条件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnAdd_Clcik(FormCollection values)
        {
            string s = values["cmb_Fields"];
            string value = "";
            var where = new Models.ArchiveWhere(s);
            if (where.Type == ((int)EnumShenGoFieldsType.Var).ToString() || where.Type == ((int)EnumShenGoFieldsType.Int).ToString())
                value = values["txt_Value"];
            else if (where.Type == ((int)EnumShenGoFieldsType.Datetime).ToString())
                value = values["date_Date"];
            else if (where.Type == ((int)EnumShenGoFieldsType.Word).ToString())
                value = values["cmb_Word"];

            where.Value = value;
            where.Where = values["cmb_Where"];
            where.AndOr = values["cmb_AndOr"];

            var list = Models.SessionValue.AddArchiveWhereSession(Session, where);
            //var list = AddViewBagArchiveWhere(where);

            JArray Grid1_fields = JArray.Parse(values["grid_Where_fields"]);

            var grid = UIHelper.Grid("grid_Where");
            grid.DataSource(list, Grid1_fields);

            //grid.LoadData();
            return UIHelper.Result();
        }

        /// <summary>
        /// 此方法为 增加where条件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnQuery_Click(FormCollection values)
        {
            var list = Models.SessionValue.GetArchiveWhereSession(Session);
            string aid = Session[Models.SessionValue._sArchiveEditAid].ToString();
            ///得到了查询where部分的语句
            string sql = Models.ArchiveWhere.GetWhereSql(list);

            ///只针对于案卷或一文一件
            string tableName = TQuery_LinqDal.Query_TableName(int.Parse(aid));

            Models.ArchivePagerWhere pager = new Models.ArchivePagerWhere();
            pager.TableName = tableName;
            if (tableName == "V_WSAJ")
                pager.Order = "WS1_ID";
            else if (tableName == "V_WSJN")
                pager.Order = "JN1_ID";
            else if (tableName == "V_XM")
                pager.Order = "PR_ID";
            else
                pager.Order = "Message_ID";
            pager.Select = "*";
            pager.iStart = 1;
            pager.Where = sql;
            pager.iEnd = Commons.iPagerRow;

            //if (pager.Where == null || pager.Where == "")
            //    pager.Where += "where WS_AID=" + values["txt_AID"];
            //else
            //    pager.Where += " and WS_AID=" + values["txt_AID"];

            Session.Add(Models.SessionValue._sArchivePager, pager);
            Session.Add(Models.SessionValue._sIsQuery, "1");

            //UIHelper.Window("Window1").Hide();
            //var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Order, pager.Select, pager.iStart, pager.iEnd, ref count);
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            //ViewBag.Grid1RecordCount = count;
            // 2.获取当前分页数据
            //ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);

            //grid.LoadData();
            ///关闭当前窗口
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            return UIHelper.Result();
        }
        /// <summary>
        /// 此方法为 删除where条件
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult grid_DelWhere(int rowIndex, FormCollection values)
        {
            //ShowNotify(String.Format("你点击了第 {0} 行的删除按钮，行ID：{1}，姓名：{2}", rowIndex + 1, rowId, rowText));
            var list = Models.SessionValue.RemoveArchiveWhereSession(Session, rowIndex);

            string json = @"[  
                'Fields',  
                'Where',  
                'Value',
                'AndOr',
            ]";

            JArray Grid1_fields = JArray.Parse(json);
            var grid = UIHelper.Grid("grid_Where");
            grid.DataSource(list, Grid1_fields);
            return UIHelper.Result();
        }
        #endregion 


        ///// <summary>
        ///// 添加档案 [ 同时将 aid 存放在 session 中，让 btnAddNew 方法调用 ]
        ///// </summary>
        ///// <param name="aid"></param>
        ///// <returns></returns>
        //public ActionResult ShowCreateArchiveDialog(string aid, string wsid)
        //{
        //    Session.Add(Models.SessionValue._sArchiveEditAid, aid);
        //    ViewBag.AID = aid;
        //    ViewBag.WSAJ = null;

        //    var list = TSysAr_LinqDal.Query_GridColumns(int.Parse(aid), null, null, null, (int)(EnumShenGoStatus.Effective), null, null);
        //    ///词典赋值
        //    var listType = TSysAr_LinqDal.Query_WordType();
        //    foreach (var n in listType)
        //    {
        //        var nn = Models.ComItems.InitComboBoxQueryWord(n);
        //        ViewData.Add(n, nn);
        //    }
        //    ///有WSID，那么就是编辑
        //    if (wsid != null && wsid != "")
        //    {
        //        var en = TArchive_LinqDal.Query_Archive(int.Parse(wsid));
        //        ViewBag.WSAJ = en;
        //        if (en == null)
        //            Alert.ShowInTop("档案ID获取失败!");
        //    }
        //    return View(list);
        //}
        //#region 添加界面里的方法
        ///// <summary>
        ///// 此方法为 增加档案
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult btnAddNew_Click(FormCollection values)
        //{
        //    List<string[]> listValues = new List<string[]>();
        //    string[] strs = values.AllKeys;
        //    foreach (var n in strs)
        //    {
        //        if (n.StartsWith("txt_") || n.StartsWith("cmb_") || n.StartsWith("date_"))
        //        {
        //            if (n.EndsWith("_text"))  //如果是  cmb 控件，还会有个 xxx_text  的html元素
        //                continue;
        //            string[] ss = new string[2];
        //            ss[0] = n.Substring(n.IndexOf("_") + 1);
        //            ss[1] = values[n];
        //            listValues.Add(ss);
        //        }
        //    }

        //    var user = Models.SessionValue.GetUserSession(Session);

        //    string aid = Session[Models.SessionValue._sArchiveEditAid].ToString();
        //    string sql = Models.ArchiveSql.CreateInsertSql(int.Parse(aid), listValues, user.U_REALNAME);

        //    int value = TArchive_LinqDal.Add_Archive(sql);
        //    if (value == 0)
        //        Alert.ShowInParent("档号编号重复!");
        //    else if (value == -1)
        //        Alert.ShowInParent("数据保存失败!");
        //    else
        //        Alert.ShowInParent("添加档案成功!");
        //    //grid.LoadData();
        //    return UIHelper.Result();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult btnModifyAJ_Click(FormCollection values)
        //{
        //    List<string[]> listValues = new List<string[]>();
        //    string[] strs = values.AllKeys;
        //    foreach (var n in strs)
        //    {
        //        if (n.StartsWith("txt_") || n.StartsWith("cmb_") || n.StartsWith("date_"))
        //        {
        //            if (n.EndsWith("_text"))  //如果是  cmb 控件，还会有个 xxx_text  的html元素
        //                continue;
        //            string[] ss = new string[2];
        //            ss[0] = n.Substring(n.IndexOf("_") + 1);
        //            ss[1] = values[n];
        //            listValues.Add(ss);
        //        }
        //    }

        //    string id = values["hidd_WSID"];

        //    var user = Models.SessionValue.GetUserSession(Session);

        //    string aid = Session[Models.SessionValue._sArchiveEditAid].ToString();
        //    string sql = Models.ArchiveSql.CreateModifySql(int.Parse(aid),int.Parse(id), listValues, user.U_REALNAME);

        //    int value = TArchive_LinqDal.Add_Archive(sql);
        //    if (value == 0)
        //        Alert.ShowInParent("档号编号重复!");
        //    else if (value == -1)
        //        Alert.ShowInParent("数据保存失败!");
        //    else
        //        Alert.ShowInParent("修改档案成功!");
        //    //grid.LoadData();
        //    return UIHelper.Result();
        //}
      //  #endregion

//        /// <summary>
//        /// 电子档案借阅 
//        /// </summary>
//        /// <param name="aid"></param>
//        /// <returns></returns>
//        public ActionResult ShowBrrowDZArchiveDialog(string aid)
//        {
//            ViewBag.LYMD = Models.ComItems.InitComboBoxQueryWord(Commons._sWDLYMD);
//            string[] strs = TSysAr_LinqDal.Query_SysTableName(int.Parse(aid));
//            //string json = @"[  
//            //    'AID',  
//            //    'DAID',
//            //    'DATABLE',
//            //    'DATYPE',
//            //    'KEY'
//            //]";
//            string json = @"[  
//                'DATYPE',
//                'KEY',
//                'TM'
//            ]";

//            JArray Grid1_fields = JArray.Parse(json);
//            var list = Session[Models.SessionValue._sArchiveBrr] as List<Models.ArchiveBrr>;
//            foreach (var n in list)
//            {
//                n.AID = int.Parse(aid);
//                n.DATABLE = strs[0];
//                n.DATYPE = strs[2];
//            }
//            ViewBag.db = list.ToArray();
//            Session[Models.SessionValue._sArchiveBrr] = list;
//            //var grid = UIHelper.Grid("grid_Archive");
//            //grid.DataSource(list, Grid1_fields);
//            return View();
//        }

//        /// <summary>
//        /// 电子档案借阅 (卷内)
//        /// </summary>
//        /// <param name="aid"></param>
//        /// <returns></returns>
//        public ActionResult ShowBrrowDZArchiveDialogJN(string aid)
//        {
//            ViewBag.LYMD = Models.ComItems.InitComboBoxQueryWord(Commons._sWDLYMD);
//            string[] strs = TSysAr_LinqDal.Query_SysTableName(int.Parse(aid));
//            //string json = @"[  
//            //    'AID',  
//            //    'DAID',
//            //    'DATABLE',
//            //    'DATYPE',
//            //    'KEY'
//            //]";
//            string json = @"[  
//                'DATYPE',
//                'KEY',
//                'TM'
//            ]";

//            JArray Grid1_fields = JArray.Parse(json);
//            var list = Session[Models.SessionValue._sArchiveBrr] as List<Models.ArchiveBrr>;
//            foreach (var n in list)
//            {
//                n.AID = int.Parse(aid);
//                n.DATABLE = strs[1];
//                n.DATYPE = strs[2];
//            }
//            ViewBag.db = list.ToArray();
//            Session[Models.SessionValue._sArchiveBrr] = list;
//            //var grid = UIHelper.Grid("grid_Archive");
//            //grid.DataSource(list, Grid1_fields);
//            return View();
//        }
//        #region 申请电子借阅的方法
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult btnBrr_Click(FormCollection values)
//        {
//            V_USER user = Models.SessionValue.GetUserSession(Session);

//            var list = Session[Models.SessionValue._sArchiveBrr] as List<Models.ArchiveBrr>;
//            List<T_BRRDZ> listBrr = new List<T_BRRDZ>();//
//            int status = -2;
//            string resurt = "";
//            foreach (var p in list)
//            {
//                status=TBrr_LinqDal.Query_BrrDZ(user.U_ID,int.Parse(p.DAID));//if (status == 0 || status == 3) //1未申请，可以直接借阅；3查阅结束，可以借阅
//                if (status == 1)//1已申请，还未审批
//                {
//                    resurt = resurt + p.TM + "这卷档案已借阅，请等待审批！";
//                }
//                else if(status==2)//2在查阅中
//                {
//                    resurt = resurt + p.TM + "这卷档案在查阅中，无需借阅！";
//                }
//            }
//            if (resurt.Count() == 0)
//            {
//                foreach (var n in list)
//                {
//                    T_BRRDZ en = new T_BRRDZ();
//                    en.BE_AID = int.Parse(n.DAID);
//                    en.BE_ARID = n.AID;
//                    en.BE_ARCHIVEKEY = n.KEY;
//                    //en.BE_HOPEDATE = DateTime.Now.AddDays(5);
//                    en.BE_DATE = DateTime.Now;
//                    en.BE_INFO = n.DATABLE;
//                    en.BE_NAME = user.U_REALNAME;
//                    en.BE_REMARK = values["txt_Remark"];
//                    en.BE_STATUS = (int)EnumShenGoBrrStatus.Sq;
//                    en.BE_SPVALUE = (int)EnumShenGoSpValue.No;
//                    en.BE_USERID = user.U_ID;
//                    en.BE_TYPE = n.DATYPE;
//                    en.BE_V01 = values["cmb_Type"];
//                    en.BE_V02 = n.TM;
//                    ///是否有卷内目录
//                    en.BE_V03 = n.JNML;
//                    en.BE_LDSTATUS = (int)EnumShenGoSpValue.No;
//                    listBrr.Add(en);
//                }
//                if (TBrr_LinqDal.Add_BrrDZ(listBrr) == -1)
//                    Alert.ShowInTop("保存失败!");
//                else
//                {
//                    Alert.ShowInTop("申请成功!");
//                    ////关闭当前窗口
//                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
//                }
//            }
//            else 
//            {
//                Alert.ShowInTop(resurt+"请修改后再借阅！");
//            }
//            return UIHelper.Result();
//        }
//        #endregion

//        /// <summary>
//        /// 删除某一条电子借阅
//        /// </summary>
//        /// <param name="rowIndex"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult btn_DelDZ(int rowIndex)
//        {
//            var list = Session[Models.SessionValue._sArchiveBrr] as List<Models.ArchiveBrr>;
//            list.RemoveAt(rowIndex);
//            Session[Models.SessionValue._sArchiveBrr] = list;

//            string json = @"[  
//                'DATYPE',  
//                'KEY',  
//                'TM'
//            ]";

//            JArray Grid1_fields = JArray.Parse(json);
//            var grid = UIHelper.Grid("grid_Archive");
//            grid.DataSource(list, Grid1_fields);
//            return UIHelper.Result();
//        }

//        /// <summary>
//        /// 显示某个档案的详细信息
//        /// </summary>
//        /// <param name="aid"></param>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public ActionResult ShowArchiveInfo(string aid, string id)
//        {
//            var list = TSysAr_LinqDal.Query_GridColumns(int.Parse(aid), (int)(EnumShenGoStatus.Effective), (int)(EnumShenGoStatus.Effective), null, null, null, null);
//            ///词典赋值
//            var listType = TSysAr_LinqDal.Query_WordType();
//            foreach (var n in listType)
//            {
//                var nn = Models.ComItems.InitComboBoxQueryWord(n);
//                ViewData.Add(n, nn);
//            }
//            ///查询出的档案 
//            ViewBag.Archive = VArchive_LinqDal.Query_WS(int.Parse(id));
//            return View(list);
//        }

       

//        /// <summary>
//        /// 选择了类型
//        /// </summary>
//        /// <param name="cmb_Type"></param>
//        /// <param name="cmb_Type_text"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult cmb_Type_SelectedIndexChanged(string cmb_Type, string cmb_Type_text)
//        {
//            if (cmb_Type == "5")
//            {
//                var cmb = UIHelper.DropDownList("cmb_Word");
//                cmb.Enabled(true);
//            }
//            else
//            {
//                var cmb = UIHelper.DropDownList("cmb_Word");
//                cmb.Enabled(false);
//            }
//            return UIHelper.Result();
//        }


//        /// <summary>
//        /// 建立字段
//        /// </summary>
//        /// <param name="aid"></param>
//        /// <returns></returns>
//        public ActionResult ShowCreateFields(string aid, string id, string name, string order, string show, string edit, string query, string tongji)
//        {
//            ViewBag.Words = Models.ComItems.InitComboBoxQueryWordType();
//            ViewBag.Items = Models.ComItems.InitComboBoxFieldsType();
//            ViewBag.AID = aid;
//            ViewBag.NAME = name;
//            ViewBag.ORDER = order;
//            ViewBag.Show = show;
//            ViewBag.Edit = edit;
//            ViewBag.Query = query;
//            ViewBag.Tongji = tongji;
//            ///修改字段
//            if (id != null && id != "")
//                ViewBag.ID = id;
//            else
//            {
//                ViewBag.ID = "";
//            }

//            return View();
//        }

//        /// <summary>
//        /// 添加字段
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult btnAdd_Click(FormCollection values)
//        {
//            int aid = int.Parse(values["txt_AID"]);
//            int id = 0;
//            if (values["txt_ID"] != "")
//                id = int.Parse(values["txt_ID"]);
//            ////获取此类型所有的设定过的字段，然后匹配WS_X开头的，如果没有设定的字段，给本次设定
//            var list = TSysAr_LinqDal.Query_SysArchiveFields(aid);

//            if (values["txt_Name"] == null || values["txt_Name"] == "")
//            {
//                Alert.ShowInParent("字段名称不能为空!");
//                return UIHelper.Result();
//            }
//            if (id == 0)
//            {
//                T_WSAJ en = new T_WSAJ();
//                //T_SYFIELDS en = new T_SYFIELDS();
//                Type type = en.GetType();
//                foreach (var n in type.GetProperties())
//                {
//                    if (n.Name.StartsWith("WS_X"))
//                    {
//                        bool b = false;
//                        foreach (var s in list)
//                        {
//                            if (n.Name == s)
//                            {
//                                b = true;
//                                break;
//                            }
//                        }
//                        if (!b)
//                        {
//                            ///如果此WS_X开头的字段在字段对应表中不存在，
//                            #region
//                            T_SYFIELDS ds = new T_SYFIELDS();
//                            ds.ARID = aid;
//                            ds.Fields = values["txt_Name"];
//                            ds.Type = int.Parse(values["cmb_Type"]);
//                            ds.Name = n.Name;
//                            if (values["txt_Order"] == null || values["txt_Order"] == "")
//                                ds.OrderBy = null;
//                            else
//                                ds.OrderBy = int.Parse(values["txt_Order"]);
//                            ds.Query = int.Parse(values["cmb_Query"]);
//                            ds.Show = int.Parse(values["cmb_Show"]);
//                            ///是否可著录 
//                            ds.I02 = int.Parse(values["cmb_Writer"]);
//                            ///是否可统计
//                            ds.I03 = int.Parse(values["cmb_Tongji"]);
//                            ///否系统字段
//                            ds.I04 = 0;
//                            ds.STATUS = 1;
//                            ///是否是词典
//                            ds.I01 = (ds.Type == 5 ? 1 : 0);
//                            ///词典的内容
//                            if (ds.I01 == 1)
//                                ds.V01 = values["cmb_Word_text"];
//                            #endregion
//                            ///添加字段
//                            id = TSysAr_LinqDal.Add_SysArchiveFields(ds);
//                            if (id == 0)
//                                Alert.ShowInParent("字段名称或Fields重复!");
//                            else if (id == -1)
//                                Alert.ShowInParent("添加失败!");
//                            else
//                            {
//                                Alert.ShowInParent("添加成功!");
//                            }
//                            break;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                ////修改
//                #region
//                T_SYFIELDS ds = new T_SYFIELDS();
//                ds.ARID = aid;
//                ds.Fields = values["txt_Name"];
//                ////ds.Type = int.Parse(values["cmb_Type"]); 不动
//                ////ds.Name = n.Name;  Name不动
//                if (values["txt_Order"] == null || values["txt_Order"] == "")
//                    ds.OrderBy = null;
//                else
//                    ds.OrderBy = int.Parse(values["txt_Order"]);
//                ds.Query = int.Parse(values["cmb_Query"]);
//                ds.Show = int.Parse(values["cmb_Show"]);
//                ///是否可著录 
//                ds.I02 = int.Parse(values["cmb_Writer"]);
//                ///是否可统计
//                ds.I03 = int.Parse(values["cmb_Tongji"]);
//                ///否系统字段
//                ////ds.I04 = 0; 不动
//                ds.STATUS = 1;
//                ///是否是词典 不动
//                ///ds.I01 = (ds.Type == 5 ? 1 : 0);
//                ///词典的内容
//                ///if (ds.I01 == 1)
//                ////     ds.V01 = values["cmb_Type_text"];
//                #endregion
//                ///添加字段
//                int value = TSysAr_LinqDal.Modify_SysArchiveFields(id, ds);
//                if (value == 0)
//                    Alert.ShowInParent("字段名称或Fields重复!");
//                else if (value == -1)
//                    Alert.ShowInParent("修改失败!");
//                else
//                {
//                    Alert.ShowInParent("修改成功!");
//                }
//            }
//            return UIHelper.Result();
//        }

        /// <summary>
        /// 上传界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUploadFile(string aid, string wsid)
        {
            ViewBag.AID = aid;
            ViewBag.ID = wsid;
            ViewBag.INFO = "";
            ////判断是否已经有附件了 
            var en = TDocument_LinqDal.Get_DocumentWSAJ(int.Parse(wsid));
            if (en != null)
                ViewBag.INFO = "此档案已经有附件了，如果再次上传则之前的附件将被覆盖";

            return View();
        }

        public ActionResult ShowUploadFileNew(string aid, string wsid)
        {
            return View();
        }

        /// <summary>
        /// 上传界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowDownloadFile(string FileName)
        {
            ViewBag.DownFileName = FileName;

            return View();
        }

        public FileStreamResult DownloadFile(string FileName)
        {
            return File(new System.IO.FileStream(FileName, System.IO.FileMode.Open), "text/plain", Guid.NewGuid().ToString() + ".xls");
        } 
        /// <summary>
        /// 上传界面(文书)
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUploadFileWS(string mid, string id)
        {
            ViewBag.AID = mid;
            ViewBag.ID = id;
            ViewBag.INFO = "";
            ////判断是否已经有附件了 
            var en = TDocument_LinqDal.Get_DocumentMG(int.Parse(id));
            if (en != null)
                ViewBag.INFO = "此档案已经有附件了，如果再次上传则之前的附件将被覆盖";

            return View();
        }
        /// <summary>
        /// 上传界面(卷内)
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUploadFileJN(string aid, string jnid)
        {
            ViewBag.AID = aid;
            ViewBag.ID = jnid;
            ViewBag.INFO = "";
            ////判断是否已经有附件了 
            var en = TDocument_LinqDal.Get_DocumentWSJN(int.Parse(jnid));
            if (en != null)
                ViewBag.INFO = "此档案已经有附件了，如果再次上传则之前的附件将被覆盖";

            return View();
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnUploadFile_Click(HttpPostedFileBase upload_File, FormCollection values)
        {
            string wsid = values["txt_ID"];
           // string aid = values["txt_AID"];
            var em = TArchive_LinqDal.Query_WSAJ1(int.Parse(wsid));
            string aid = em.WS1_MID.ToString();
            //string path = ConfigurationManager.AppSettings["pdf"].ToString();
            if (upload_File != null)
            {
                string fileName = upload_File.FileName;
                string type = fileName.Substring(fileName.LastIndexOf(".") + 1);
                // string path=confi
                //if (type.ToUpper() != "PDF")
                ////if (!ValidateFileType(fileName))
                //{
                //    // 清空文件上传组件
                //    UIHelper.FileUpload("upload_File").Reset();

                //    Alert.ShowInParent("系统目前只支持PDF格式的附件!");
                //}
                //else
                

                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                    upload_File.SaveAs(Server.MapPath("~/res/pdf" + "/" + fileName));

                    T_DOCUMENT en = new T_DOCUMENT();
                    en.DOC_ATYPE = int.Parse(aid);
                    en.DOC_AID = int.Parse(wsid);
                    en.DOC_FILE = fileName;
                    en.DOC_PATH = "~/res/pdf";
                    en.DOC_NID = 0;
                    en.DOC_TYPE = "pdf";
                    en.DOC_STATUS = 1;
                    en.DOC_NAME = fileName;
                    en.DOC_V01 = em.WS1_JH;//档号
                    en.DOC_V02 = em.WS1_TM;//题名
                    int value = TDocument_LinqDal.Add_DocumentWSAJ(en.DOC_ATYPE, en.DOC_AID, en);
                    if (value == -1)
                        Alert.ShowInParent("上传附件失败!");
                    else
                    {
                        if(fileName.EndsWith("pdf")==true)
                        {
                            O2S.Components.PDFView4NET.PDFDocument pdf = new O2S.Components.PDFView4NET.PDFDocument();
                            pdf.Load(upload_File.InputStream);
                            string ss = "";
                            for (int N = 0; N < pdf.PageCount; N++)
                            {
                                string s = pdf.Pages[N].ExtractText();
                                s = s.Replace(" ", "");
                                s = s.Replace("\n", "");
                                s = s.Replace("\r", "");
                                ss = ss + s;
                            }
                            if (ss != "")
                            {
                                VText_LinqDal.AddTEXT1(int.Parse(wsid), ss);
                            }
                        }

                        Alert.ShowInParent("上传附件成功!");
                    }
                       

                    // 清空表单字段
                    UIHelper.SimpleForm("form_UploadFile").Reset();
                
            }

            return UIHelper.Result();
        }
        ////添加水印
        //public static string PDFWatermark(Stream inputfilepath, string outputfilepath, string ModelPicName)//, float top, float left)
        //{
        //    iTextSharp.text.pdf.PdfReader pdfReader = null;
        //    iTextSharp.text.pdf.PdfStamper pdfStamper = null;
        //    try
        //    {
        //        pdfReader = new iTextSharp.text.pdf.PdfReader(inputfilepath);
        //        int numberOfPages = pdfReader.NumberOfPages;
        //        iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
        //        float width = psize.Width;
        //        float height = psize.Height;
        //        pdfStamper = new iTextSharp.text.pdf.PdfStamper(pdfReader, new FileStream(outputfilepath, FileMode.Create));
        //        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ModelPicName);
        //        iTextSharp.text.pdf.PdfGState gs = new iTextSharp.text.pdf.PdfGState();
        //        image.RotationDegrees = 45;
        //        float left = width / 2 - 100;
        //        float top = height / 2 - 200;
        //        image.SetAbsolutePosition(left, top);
        //        image.ScaleAbsolute(360, 100);
        //        for (int i = 1; i <= numberOfPages; i++)
        //        {
        //            iTextSharp.text.pdf.PdfContentByte waterMarkContent = pdfStamper.GetOverContent(i);
        //            gs.FillOpacity = 0.3f;
        //            waterMarkContent.SetGState(gs);
        //            waterMarkContent.AddImage(image, true);
        //        }
        //        return outputfilepath;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message.Trim();
                 
        //    }
        //    finally
        //    {
        //        if (pdfStamper != null)
        //            pdfStamper.Close();

        //        if (pdfReader != null)
        //            pdfReader.Close();
        //    }
        //}
        /// <summary>
        /// 上传附件(卷内)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JNbtnUploadFile_Click(HttpPostedFileBase upload_File, FormCollection values)
        {
            string jnid = values["txt_ID"];

            //var em = TArchive_LinqDal.Query_Archive(int.Parse(wsid));
            var em = TArchive_LinqDal.Query_VWSJN(int.Parse(jnid));
            int? aid = em.WS1_MID;
            string wsid = em.WS1_ID.ToString();
              
            if (upload_File != null)
            {
                string fileName = upload_File.FileName;
                string type = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (type.ToUpper() != "PDF")
                //if (!ValidateFileType(fileName))
                {
                    // 清空文件上传组件
                    UIHelper.FileUpload("upload_File").Reset();

                    Alert.ShowInParent("系统目前只支持PDF格式的附件!");
                }
                else
                {
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    upload_File.SaveAs(Server.MapPath("~/res/pdf/" + fileName));

                    T_DOCUMENT en = new T_DOCUMENT();
                    en.DOC_ATYPE = (int)aid;
                    en.DOC_AID = int.Parse(wsid);
                    en.DOC_FILE = fileName;
                    en.DOC_PATH = "~/res/pdf";
                    //en.DOC_PATH = "~/res/pdf";
                    en.DOC_NID = int.Parse(jnid);
                    en.DOC_TYPE = "pdf";
                    en.DOC_STATUS = 1;
                    en.DOC_NAME = fileName;
                    en.DOC_V01 = em.WS1_JH;//档号
                    en.DOC_V02 = em.JN1_Name;//题名
                    int value = TDocument_LinqDal.Add_DocumentWSJN(en.DOC_ATYPE, en.DOC_NID, en);
                    if (value == -1)
                        Alert.ShowInParent("上传附件失败!");
                    else
                    {
                        O2S.Components.PDFView4NET.PDFDocument pdf = new O2S.Components.PDFView4NET.PDFDocument();
                        pdf.Load(upload_File.InputStream);
                        string ss = "";
                        for (int N = 0; N < pdf.PageCount; N++)
                        {
                            string s = pdf.Pages[N].ExtractText();
                            s = s.Replace(" ", "");
                            s = s.Replace("\n", "");
                            s = s.Replace("\r", "");
                            ss = ss + s;
                        }
                        if (ss != "")
                        {
                            VText_LinqDal.AddTEXT(int.Parse(jnid), ss);
                        }
                        Alert.ShowInParent("上传附件成功!");
                    }                        
                    // 清空表单字段
                    UIHelper.SimpleForm("form_UploadFile").Reset();
                }
            }

            return UIHelper.Result();
        }
        /// <summary>
        /// 上传附件(文书)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WSbtnUploadFile_Click(HttpPostedFileBase upload_File, FormCollection values)
        {
            string id = values["txt_ID"];
            string aid = values["txt_AID"];
            //var em = TArchive_LinqDal.Query_Archive(int.Parse(wsid));
            var em = TArchive_LinqDal.Query_MG(int.Parse(id));
           // int? aid = em.WS1_MID;
            //string wsid = em.WS1_ID.ToString();

            if (upload_File != null)
            {
                string fileName = upload_File.FileName;
                string type = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (type.ToUpper() != "PDF")
                //if (!ValidateFileType(fileName))
                {
                    // 清空文件上传组件
                    UIHelper.FileUpload("upload_File").Reset();

                    Alert.ShowInParent("系统目前只支持PDF格式的附件!");
                }
                else
                {
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    upload_File.SaveAs(Server.MapPath("~/res/pdf/" + fileName));

                    T_DOCUMENT en = new T_DOCUMENT();
                    en.DOC_ATYPE =int.Parse (aid);
                    //en.DOC_AID = int.Parse(wsid);
                    en.DOC_I01 =int.Parse( id);
                    en.DOC_FILE = fileName;
                    en.DOC_PATH = "~/res/pdf";
                    //en.DOC_PATH = "~/res/pdf";
                    //en.DOC_NID = int.Parse(jnid);
                    en.DOC_TYPE = "pdf";
                    en.DOC_STATUS = 1;
                    en.DOC_NAME = fileName;
                    en.DOC_V01 = em.Message_WH;//档号
                    en.DOC_V02 = em.Message_Title;//题名
                    int value = TDocument_LinqDal.Add_DocumentMG(en.DOC_ATYPE, (int)en.DOC_I01, en);
                    if (value == -1)
                        Alert.ShowInParent("上传附件失败!");
                    else
                    {
                        O2S.Components.PDFView4NET.PDFDocument pdf = new O2S.Components.PDFView4NET.PDFDocument();
                        pdf.Load(upload_File.InputStream);
                        string ss = "";
                        for (int N = 0; N < pdf.PageCount; N++)
                        {
                            string s = pdf.Pages[N].ExtractText();
                            s = s.Replace(" ", "");
                            s = s.Replace("\n", "");
                            s = s.Replace("\r", "");
                            ss = ss + s;
                        }
                        if (ss != "")
                        {
                            VText_LinqDal.AddTEXT2(int.Parse(id), ss);
                        }
                        Alert.ShowInParent("上传附件成功!");
                    }
                    // 清空表单字段
                    UIHelper.SimpleForm("form_UploadFile").Reset();
                }
            }

            return UIHelper.Result();
        }
    }
}
