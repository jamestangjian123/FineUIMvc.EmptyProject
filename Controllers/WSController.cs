using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
// 以下为本系统的需要引用的命名空间
using FineUIMvc;
using Newtonsoft.Json.Linq;
using FineUIMvc.EmptyProject.Models;
using System.Data;
using FineUIMvc.EmptyProject.Models.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Reflection;
using System.Web;
using System.Text;
using Aspose.Cells;


namespace FineUIMvc.EmptyProject.Controllers
{
    public class WSController : Controller
    {
        // GET: WS
        //public ActionResult Index(string aid, string name)
        //{

        //    var user = Models.SessionValue.GetUserSession(Session) as V_USER;
        //    string[] viewDA = user.ROLE_V04 == null ? null : user.ROLE_V04.Split(',');
        //    string[] modifyDA = user.ROLE_V05 == null ? null : user.ROLE_V05.Split(',');
        //    bool bView = false;
        //    bool bModify = false;

        //    if(viewDA != null)
        //        bView = viewDA.Select(c => c).Where(c => c == aid).Count() > 0 ? true : false;
        //    if(modifyDA != null)
        //        bModify = modifyDA.Select(c => c).Where(c => c == aid).Count() > 0 ? true : false;

        //    ViewBag.Edit = bModify;
        //    //if(viewDA != null)
        //    //{
        //    //    foreach(var n in viewDA)
        //    //    {
        //    //        if (n == aid)
        //    //        {
        //    //            bView = true;
        //    //            break;
        //    //        }
        //    //}

        //    ///建立grid 列
        //    var list = Models.GridColumns.InitGridColumns(int.Parse(aid), true, bModify);
        //    ViewBag.AID = aid;
        //    ViewBag.NAME = name;
        //    ViewBag.Grid1Columns = list;

        //    ViewBag.UBM = user.U_V01;

        //    ///页面加载时先查询第一次
        //    Models.ArchivePagerWhere pager = new Models.ArchivePagerWhere();
        //    pager.TableName = "V_WS";
        //    pager.Order = "WS_DH";
        //    pager.Select = "*";
        //    pager.iStart = 0;
        //    pager.iEnd = Commons.iPagerRow;
        //    pager.Where = "where WS_AID=" + aid;

        //    Session.Add(Models.SessionValue._sArchivePager, pager);

        //    int count = 0;
        //    var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
        //    // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
        //    ViewBag.Grid1RecordCount = count;
        //    // 2.获取当前分页数据
        //    ViewBag.Grid1DataSource = dt;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);

        //    ////词典
        //    ViewBag.WD = Get_WD();

        //    ////跨部门浏览
        //    ViewBag.UBMVQX = user.ROLE_V04;
        //    ////跨部门检索
        //    ViewBag.UBMMQX = user.ROLE_V05;


        //    return View();
        //}

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

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="Grid1_fields"></param>
        /// <param name="Grid1_pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Grid1_PageIndexChanged(JArray Grid1_fields, int Grid1_pageIndex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[Models.SessionValue._sArchivePagerAJ] as Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[Models.SessionValue._sArchivePager] = pager;
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
        /// 分页
        /// </summary>
        /// <param name="Grid1_fields"></param>
        /// <param name="Grid1_pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GridJN_PageIndexChanged(JArray Grid1_fields, int Grid1_pageIndex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[Models.SessionValue._sArchivePagerJN] as Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[Models.SessionValue._sArchivePagerJN] = pager;
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
        /// 添加档案或修改档案刷新 
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
                var pager = Session[Models.SessionValue._sArchivePager] as Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * pageindex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[Models.SessionValue._sArchivePager] = pager;
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
        /// 添加档案或修改档案刷新 (卷内上传)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult windAddJN_Close(JArray Grid1_fields, int pageindex)
        {
            var grid1 = UIHelper.Grid("Grid1");

            //if(Session[Models.SessionValue._sIsQuery] != null && "1" == Session[Models.SessionValue._sIsQuery].ToString())
            {
                int count = 0;
                var pager = Session[Models.SessionValue._sArchivePagerJN] as Models.ArchivePagerWhere;
                pager.iStart = Commons.iPagerRow * pageindex;
                if (pager.iStart > 0)
                    pager.iStart++;
                pager.iEnd = pager.iStart + Commons.iPagerRow;
                Session[Models.SessionValue._sArchivePager] = pager;
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
        /// 电子借阅 btnleadingout_Click
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnBrrDZ_Click(JArray selected)
        {
            ///电子借阅清单
            List<Models.ArchiveBrr> list = new List<Models.ArchiveBrr>();
            foreach (JArray item in selected)
            {
                list.Add(new Models.ArchiveBrr() { DAID = item[0].ToString(), KEY = item[1].ToString(), TM = item[2].ToString(), JNML = item[6].ToString() });
            }
            Session.Add(Models.SessionValue._sArchiveBrr, list);
            UIHelper.Window("win_DZ").Show();

            //sb.Append("</table>");
            return UIHelper.Result();
        }
        /// <summary>
        /// 导出数据(word)
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [STAThread]
        public ActionResult btnleadingoutWORD_Click(FormCollection values)
        {
            string WSJN1_AJID = values["txt_AID"];
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var Qu = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_AJID == WSJN1_AJID).ToList();
            if (Qu.Count == 0)
            {
                Alert.ShowInParent("无数据导出");
            }
            else
            {
                int i = ImportWord(WSJN1_AJID);
                // if (i == 1)
                if (i == 1)
                {
                    Alert.ShowInParent("导出成功!");
                }
                else
                {
                    Alert.ShowInParent("导出失败!");
                    //Alert.ShowInParent(i);
                }
            }
            return UIHelper.Result();
        }
        /// <summary>
        /// 导出word
        /// </summary>
        public int ImportWord(string ajid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var WSAJ = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_ZRZ, c.JN1_Name, c.JN1_RQ, c.JN1_YS, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == ajid).ToList();
                DataTable query = Exce_Word1.ConvertToDataTable(WSAJ);


                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                bool exist = System.IO.Directory.Exists("D:\\导出数据");
                if (exist == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\导出数据");
                }
                string filename = "D:\\导出数据\\" + date + ".xls";
                Workbook workbook = new Workbook();
                string fileUrl = Server.MapPath("~/res/xls/打印模板.xlsx");
                workbook.Open(fileUrl);
                Worksheet sheet = workbook.Worksheets[0]; //工作表 
                Cells cells = sheet.Cells;//单元格 
                cells[1, 0].PutValue(query.Rows[0]["JN1_AJID"].ToString());
                for (int i = 0; i < query.Rows.Count; i++)
                {
                    for (int k = 0; k < query.Columns.Count - 2; k++)
                    {
                        if (query.Columns[k].ColumnName == query.Columns["JN1_RQ"].ColumnName)
                        {
                            if (query.Rows[i][k].ToString() != "")
                            { cells[2 + i, k + 1].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[2 + i, k + 1].PutValue(query.Rows[i][k].ToString()); }
                        }
                        else
                            cells[2 + i, k + 1].PutValue(query.Rows[i][k].ToString());
                    }
                    //cells.SetRowHeight(1 + i, 24);
                }

                workbook.Save(filename);

                System.IO.FileInfo file = new System.IO.FileInfo(filename);//创建一个文件对象
                Response.Clear();//清除所有缓存区的内容
                Response.Charset = "GB2312";//定义输出字符集
                Response.ContentEncoding = Encoding.Default;//输出内容的编码为默认编码
                Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);//添加头信息。为“文件下载/另存为”指定默认文件名称
                Response.AddHeader("Content-Length", file.Length.ToString());//添加头文件，指定文件的大小，让浏览器显示文件下载的速度
                Response.WriteFile(file.FullName);// 把文件流发送到客户端
                Response.End();//将当前所有缓冲区的输出内容发送到客户端，并停止页面的执行
                return 1;
            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// WSAJ导出的数据的线程（无上级）
        /// </summary>
        public int ImportDialog(int mid)//int mid
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var WSAJ = conn.T_WSAJ1.Select(c => new { c.WS1_JH, c.WS1_TM, c.WS1_YEAR, c.WS1_BGQX, c.WS1_GDRQ, c.WS1_HJBH, c.WS1_BZ, c.WS1_PID, c.WS1_MID }).Where(c => c.WS1_MID == mid).ToList(); ;
                DataTable query = Exce_Word1.ConvertToDataTable(WSAJ);// ConvertToDataTable();
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                bool exist = System.IO.Directory.Exists("D:\\导出数据");
                if (exist == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\导出数据");
                }
                string path = "D:\\导出数据\\" + date + ".xls";
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0]; //工作表 
                sheet.Name = "T_WSAJ1";
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
                        if (query.Columns[k].ColumnName == query.Columns["WS1_GDRQ"].ColumnName)
                        {
                            if (query.Rows[i][k].ToString() != null || query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        }
                        // cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd"));
                        else
                            cells[1 + i, k].PutValue(query.Rows[i][k].ToString());
                    }
                    cells.SetRowHeight(1 + i, 24);
                }

                workbook.Worksheets.Add("T_WSJN1");
                Worksheet sheet1 = workbook.Worksheets[1]; //工作表 
                Cells cells1 = sheet1.Cells;//单元格 
                string AJID = query.Rows[0]["WS1_JH"].ToString();
                var Res = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
                for (int nn = 1; nn < query.Rows.Count; nn++)
                {
                    AJID = query.Rows[nn]["WS1_JH"].ToString();
                    var aa = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
                    Res.AddRange(aa);
                }
                DataTable query1 = Exce_Word1.ConvertToDataTable(Res);
                for (int i = 0; i < query1.Columns.Count; i++)
                {
                    cells1[0, i].PutValue(query1.Columns[i].ColumnName);
                    cells1.SetRowHeight(0, 25);
                }
                for (int i = 0; i < query1.Rows.Count; i++)
                {
                    for (int k = 0; k < query1.Columns.Count; k++)
                    {
                        if (query1.Columns[k].ColumnName == query1.Columns["JN1_RQ"].ColumnName)
                        {
                            if (query1.Rows[i][k].ToString() != "")
                            {
                                cells1[1 + i, k].PutValue(((DateTime)query1.Rows[i][k]).ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                cells1[1 + i, k].PutValue(query1.Rows[i][k].ToString());
                            }
                        }
                        // cells1[1 + i, k].PutValue(((DateTime)query1.Rows[i][k]).ToString("yyyy-MM-dd"));
                        else
                            cells1[1 + i, k].PutValue(query1.Rows[i][k].ToString());
                    }
                    cells1.SetRowHeight(1 + i, 24);
                }

                workbook.Save(path);

                System.IO.FileInfo file = new System.IO.FileInfo(path);//创建一个文件对象
                Response.Clear();//清除所有缓存区的内容
                Response.Charset = "GB2312";//定义输出字符集
                Response.ContentEncoding = Encoding.Default;//输出内容的编码为默认编码
                Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);//添加头信息。为“文件下载/另存为”指定默认文件名称
                Response.AddHeader("Content-Length", file.Length.ToString());//添加头文件，指定文件的大小，让浏览器显示文件下载的速度
                Response.WriteFile(file.FullName);// 把文件流发送到客户端
                Response.End();//将当前所有缓冲区的输出内容发送到客户端，并停止页面的执行
                return 1;
            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// WSAJ导出的数据的线程（有项目级）
        /// </summary>
        public int ImportDialog1(string bh)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var WSAJ = conn.T_WSAJ1.Select(c => new { c.WS1_JH, c.WS1_TM, c.WS1_YEAR, c.WS1_BGQX, c.WS1_GDRQ, c.WS1_HJBH, c.WS1_BZ, c.WS1_PID, c.WS1_MID }).Where(c => c.WS1_PID == bh).ToList();
                DataTable query = Exce_Word1.ConvertToDataTable(WSAJ);// ConvertToDataTable();

                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                bool exist = System.IO.Directory.Exists("D:\\导出数据");
                if (exist == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\导出数据");
                }
                string path = "D:\\导出数据\\" + date + ".xls";


                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0]; //工作表 
                sheet.Name = "T_WSAJ1";
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
                        if (query.Columns[k].ColumnName == query.Columns["WS1_GDRQ"].ColumnName)
                        {
                            if (query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        }
                        // cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd"));
                        else
                            cells[1 + i, k].PutValue(query.Rows[i][k].ToString());
                    }
                    cells.SetRowHeight(1 + i, 24);
                }

                workbook.Worksheets.Add("T_WSJN1");
                Worksheet sheet1 = workbook.Worksheets[1]; //工作表 
                Cells cells1 = sheet1.Cells;//单元格 
                string AJID = query.Rows[0]["WS1_JH"].ToString();
                var Res = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
                for (int nn = 1; nn < query.Rows.Count; nn++)
                {
                    AJID = query.Rows[nn]["WS1_JH"].ToString();
                    var aa = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == AJID).ToList();
                    Res.AddRange(aa);
                }

                DataTable query1 = Exce_Word1.ConvertToDataTable(Res);
                for (int i = 0; i < query1.Columns.Count; i++)
                {
                    cells1[0, i].PutValue(query1.Columns[i].ColumnName);
                    cells1.SetRowHeight(0, 25);
                }
                for (int i = 0; i < query1.Rows.Count; i++)
                {
                    for (int k = 0; k < query1.Columns.Count; k++)
                    {
                        if (query1.Columns[k].ColumnName == query1.Columns["JN1_RQ"].ColumnName)
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

                workbook.Save(path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);//创建一个文件对象
                Response.Clear();//清除所有缓存区的内容
                Response.Charset = "GB2312";//定义输出字符集
                Response.ContentEncoding = Encoding.Default;//输出内容的编码为默认编码
                Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);//添加头信息。为“文件下载/另存为”指定默认文件名称
                Response.AddHeader("Content-Length", file.Length.ToString());//添加头文件，指定文件的大小，让浏览器显示文件下载的速度
                Response.WriteFile(file.FullName);// 把文件流发送到客户端
                Response.End();//将当前所有缓冲区的输出内容发送到客户端，并停止页面的执行
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// WSJN导出的数据的线程
        /// </summary>
        public int ImportDialog2(string ajid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var WSAJ = conn.T_WSJN1.Select(c => new { c.JN1_XH, c.JN1_BH, c.JN1_Name, c.JN1_YS, c.JN1_ZRZ, c.JN1_RQ, c.JN1_Remark, c.JN1_AJID }).Where(c => c.JN1_AJID == ajid).ToList();
                DataTable query = Exce_Word1.ConvertToDataTable(WSAJ);// ConvertToDataTable();
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");

                bool exist = System.IO.Directory.Exists("D:\\导出数据");
                if (exist == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\导出数据");
                }
                string path = "D:\\导出数据\\" + date + ".xls";

                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0]; //工作表 
                sheet.Name = "T_WSJN1";
                Cells cells = sheet.Cells;//单元格 
                for (int i = 0; i < query.Columns.Count; i++)
                {
                    cells[0, i].PutValue(query.Columns[i].ColumnName);
                    //  cells[1, i].SetStyle(style2);
                    cells.SetRowHeight(0, 25);
                }
                for (int i = 0; i < query.Rows.Count; i++)
                {
                    for (int k = 0; k < query.Columns.Count; k++)
                    {
                        if (query.Columns[k].ColumnName == query.Columns["JN1_RQ"].ColumnName)
                        {
                            if (query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        }
                        // cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd"));
                        else
                            cells[1 + i, k].PutValue(query.Rows[i][k].ToString());
                        //cells[2 + i, k].SetStyle(style3);
                    }
                    cells.SetRowHeight(1 + i, 24);
                }

                workbook.Save(path);

                System.IO.FileInfo file = new System.IO.FileInfo(path);//创建一个文件对象
                Response.Clear();//清除所有缓存区的内容
                Response.Charset = "GB2312";//定义输出字符集
                Response.ContentEncoding = Encoding.Default;//输出内容的编码为默认编码
                Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);//添加头信息。为“文件下载/另存为”指定默认文件名称
                Response.AddHeader("Content-Length", file.Length.ToString());//添加头文件，指定文件的大小，让浏览器显示文件下载的速度
                Response.WriteFile(file.FullName);// 把文件流发送到客户端
                Response.End();//将当前所有缓冲区的输出内容发送到客户端，并停止页面的执行
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// message导出的数据的线程
        /// </summary>
        public int ImportDialog3(int mid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var Message = conn.T_Message.Select(c => new { c.Message_NO, c.Message_WH, c.Message_Title, c.Message_BGQX, c.Message_Date, c.Message_Type, c.Message_SendUnit, c.Message_ReceiveUnit, c.Message_Sender, c.Message_Remark, c.MG_mid }).Where(c => c.MG_mid == mid).ToList();
                DataTable query = Exce_Word1.ConvertToDataTable(Message);// ConvertToDataTable();
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                bool exist = System.IO.Directory.Exists("D:\\导出数据");
                if (exist == false)
                {
                    System.IO.Directory.CreateDirectory("D:\\导出数据");
                }
                string path = "D:\\导出数据\\" + date + ".xls";

                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0]; //工作表 
                sheet.Name = "T_Message";
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
                        if (query.Columns[k].ColumnName == query.Columns["Message_Date"].ColumnName)
                        {
                            if (query.Rows[i][k].ToString() != "")
                            { cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd")); }
                            else
                            { cells[1 + i, k].PutValue(query.Rows[i][k].ToString()); }
                        }
                        //   cells[1 + i, k].PutValue(((DateTime)query.Rows[i][k]).ToString("yyyy-MM-dd"));
                        else
                            cells[1 + i, k].PutValue(query.Rows[i][k].ToString());
                    }
                    cells.SetRowHeight(1 + i, 24);
                }

                workbook.Save(path);

                System.IO.FileInfo file = new System.IO.FileInfo(path);//创建一个文件对象
                Response.Clear();//清除所有缓存区的内容
                Response.Charset = "GB2312";//定义输出字符集
                Response.ContentEncoding = Encoding.Default;//输出内容的编码为默认编码
                Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);//添加头信息。为“文件下载/另存为”指定默认文件名称
                Response.AddHeader("Content-Length", file.Length.ToString());//添加头文件，指定文件的大小，让浏览器显示文件下载的速度
                Response.WriteFile(file.FullName);// 把文件流发送到客户端
                Response.End();//将当前所有缓冲区的输出内容发送到客户端，并停止页面的执行
                return 1;
            }
            catch //Exception ex
            {
                return -1;
            }
        }
        /// <summary>
        /// 导出数据(案卷级)
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [STAThread]
        public ActionResult btnleadingout_Click(FormCollection values)
        {
            string Project_BH = values["txt_AID"];
            int MODEL_ID = int.Parse(values["txt_MID"]);
            if (Project_BH == "-1")//无上级
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var Qu = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_MID == MODEL_ID).ToList();
                if (Qu.Count == 0)
                {
                    Alert.ShowInParent("无数据导出");
                }
                else
                {
                    int i = ImportDialog(MODEL_ID);
                    if (i == 1)
                    {
                        Alert.ShowInParent("导出成功!");
                    }
                    else
                    {
                        Alert.ShowInParent("导出失败!");
                    }
                    //Thread importThread = new Thread(new ThreadStart(ImportDialog));
                    //importThread.SetApartmentState(ApartmentState.STA);
                    //importThread.Start();
                }
            }
            else
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var Qu = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_PID == Project_BH).ToList();
                if (Qu.Count == 0)
                {
                    Alert.ShowInParent("无数据导出");
                }
                else
                {
                    int i = ImportDialog1(Project_BH);
                    if (i == 1)
                    {
                        Alert.ShowInParent("导出成功!");
                    }
                    else
                    {
                        Alert.ShowInParent("导出失败!");
                    }
                    // Thread importThread = new Thread(new ThreadStart(ImportDialog1));
                    //importThread.SetApartmentState(ApartmentState.STA);
                    // importThread.Start();
                }
            }

            return UIHelper.Result();
        }
        /// <summary>
        /// 导出数据(卷内级)
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnleadingoutJN_Click(FormCollection values)//FormCollection values
        {
            string WSJN1_AJID = values["txt_AID"];
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var Qu = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_AJID == WSJN1_AJID).ToList();
            if (Qu.Count == 0)
            {
                Alert.ShowInParent("无数据导出");
                return UIHelper.Result();
            }
            else
            {
                int i = ImportDialog2(WSJN1_AJID);
                if (i == 1)
                {
                    Alert.ShowInParent("导出成功!");
                }
                else
                {
                    Alert.ShowInParent("导出失败!");
                }
                return UIHelper.Result();
            }
        }
        /// <summary>
        /// 导出数据(公文级)
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [STAThread]
        public ActionResult btnleadingoutMG_Click(FormCollection values)
        {
            int MODEL_ID = int.Parse(values["txt_MID"]);
            GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
            var Qu = conn.T_Message.Select(c => c).Where(c => c.MG_mid == MODEL_ID).ToList();
            if (Qu.Count == 0)
            {
                Alert.ShowInParent("无数据导出");
            }
            else
            {
                int i = ImportDialog3(MODEL_ID);
                //if (i == 1)
                if (i == 1)
                {
                    Alert.ShowInParent("导出成功!");
                }
                else
                {
                    Alert.ShowInParent("导出失败!");
                    // Alert.ShowInParent(i);
                }
            }
            return UIHelper.Result();
        }
        /// <summary>
        /// 电子借阅(卷内)
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnBrrDZJN_Click(JArray selected)
        {
            ///电子借阅清单
            List<Models.ArchiveBrr> list = new List<Models.ArchiveBrr>();
            foreach (JArray item in selected)
            {
                list.Add(new Models.ArchiveBrr() { DAID = item[0].ToString(), KEY = item[1].ToString(), TM = item[2].ToString(), JNML = item[5].ToString() });
            }
            Session.Add(Models.SessionValue._sArchiveBrr, list);
            UIHelper.Window("win_DZ").Show();

            //sb.Append("</table>");
            return UIHelper.Result();
        }
        /// <summary>
        /// 对外借阅
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnToBUser_Click(JArray selected)
        {
            ///电子借阅清单
            List<Models.ArchiveBrr> list = new List<Models.ArchiveBrr>();
            foreach (JArray item in selected)
            {
                list.Add(new Models.ArchiveBrr() { DAID = item[0].ToString(), KEY = item[1].ToString(), TM = item[2].ToString(), JNML = item[6].ToString() });
            }
            Session.Add(Models.SessionValue._sArchiveBrr, list);
            UIHelper.Window("win_Out").Show();

            //sb.Append("</table>");
            return UIHelper.Result();
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
        /// 添加收藏
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult btnDocument_Click(JArray selected)
        //{
        //    List<T_MDOC> list = new List<T_MDOC>();
        //    var dt = DateTime.Now;
        //    V_USER user = Models.SessionValue.GetUserSession(Session); 
        //    foreach (JArray item in selected)
        //    {
        //        T_MDOC doc = new T_MDOC();
        //        doc.MDOC_AID = int.Parse(item[0].ToString());
        //        doc.MDOC_ARID = int.Parse(item[4].ToString());
        //        doc.MDOC_ATYPE = item[3].ToString();
        //        doc.MDOC_DATE = dt;
        //        doc.MDOC_DH = item[1].ToString();
        //        doc.MDOC_STATUS = 1;
        //        doc.MDOC_TM = item[2].ToString();
        //        doc.MDOC_UID = user.U_ID;
        //        list.Add(doc);
        //        //list.Add(new Models.ArchiveBrr() { DAID = item[0].ToString(), KEY = item[1].ToString() });
        //    }
        //    int value = TMDoc_LinqDal.Add_TMdoc(list);
        //    if (value == -1)
        //        Alert.ShowInParent("收藏失败!");
        //    else
        //        Alert.ShowInParent("收藏成功!");
        //    return UIHelper.Result();
        //}
        public ActionResult WSAJ(string name, string aid, string mid)
        {
            var user = Models.SessionValue.GetUserSession(Session) as V_USER;
            bool bView;
            int count = 0;
            Models.ArchivePagerWhere pager = new Models.ArchivePagerWhere();
            if (mid == "" || mid == null)
            {
                pager.TableName = "T_WSAJ1";
                pager.Order = "WS1_ID";
                pager.Select = "*";
                pager.iStart = 0;
                pager.iEnd = Commons.iPagerRow;
                pager.Where = "where WS1_PID='" + aid + "'";
                ViewBag.AID = aid;
                ViewBag.MID = name;
                bView = TUser_LinqDal.Query_MGQX(name, user.U_ID);
                ViewBag.VIEW = bView.ToString();
            }
            else
            {
                pager.TableName = "T_WSAJ1";
                pager.Order = "WS1_ID";
                pager.Select = "*";
                pager.iStart = 0;
                pager.iEnd = Commons.iPagerRow;
                pager.Where = "where WS1_MID=" + mid;
                ViewBag.NAME = name;
                ViewBag.AID = "-1";
                ViewBag.MID = mid;
                bView = TUser_LinqDal.Query_MGQX(mid, user.U_ID);
                ViewBag.VIEW = bView.ToString();
            }
            var list = Models.GridColumns.InitAJQueryColumns();
            ViewBag.Grid1Columns = list;
            ViewBag.BName = "添加案卷";
            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            ViewBag.Grid1RecordCount = count;
            // 2.获取当前分页数据
            ViewBag.Grid1DataSource = dt;
            Session.Add(Models.SessionValue._sArchivePagerAJ, pager);
            return View();
        }
        public ActionResult WSJN1(string aid, string arid)
        {
            var user = Models.SessionValue.GetUserSession(Session) as V_USER;
            bool bView = TUser_LinqDal.Query_QX(aid, user.U_ID);

            int count = 0;
            var list = Models.GridColumns.InitJNQueryColumns(bView);
            ViewBag.Grid1Columns = list;
            ViewBag.AID = aid;
            //ViewBag.ARID = arid;
            //ViewBag.MID = aid;
            Models.ArchivePagerWhere pager = new Models.ArchivePagerWhere();
            pager.TableName = "T_WSJN1";
            pager.Order = "JN1_XH";
            pager.Select = "*";
            pager.iStart = 0;
            pager.iEnd = Commons.iPagerRow;
            pager.Where = "where JN1_AJID='" + aid + "'";

            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            ViewBag.Grid1RecordCount = count;
            // 2.获取当前分页数据
            ViewBag.Grid1DataSource = dt;
            Session.Add(Models.SessionValue._sArchivePagerJN, pager);


            return View();
        }
        /// <summary>
        /// WSAJ信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public ActionResult WSAJINFO(string pid, string mid, string ajid)
        {
            ViewBag.ajid = "";
            ViewBag.PID = pid;
            ViewBag.MID = mid;

            var listType = TSysAr_LinqDal.Query_WordType();
            foreach (var n in listType)
            {
                var nn = FineUIMvc.EmptyProject.Models.ComItems.InitComboBoxQueryWord(n);
                ViewData.Add(n, nn);
            }
            if (ajid == null || ajid == "")
            {
                ////新建卷内ID
                //ViewBag.StartDay = "";
                //ViewBag.EndDay = "";
                //ViewBag.StorageDay = "";
                //ViewBag.BZDay = "";
                ViewBag.GDRQ = "";
            }
            else
            {
                ////修改卷内ID
                ViewBag.WS1_ID = ajid;
                var en = TArchive_LinqDal.Query_WSAJ1(int.Parse(ajid));
                ViewBag.JH = en.WS1_JH.ToString();

                ViewBag.TM = en.WS1_TM.ToString();
                if (en.WS1_BGQX == null)
                    ViewBag.BGQX = "";
                else
                    ViewBag.BGQX = Get_NUM(en.WS1_BGQX.ToString());
                if (en.WS1_YEAR == null)
                    ViewBag.YEAR = "";
                else
                    ViewBag.YEAR = en.WS1_YEAR.ToString();
                if (en.WS1_GDRQ == null)
                    ViewBag.GDRQ = "";
                else
                    ViewBag.GDRQ = Convert.ToDateTime(en.WS1_GDRQ.ToString());
                if (en.WS1_BZ == null)
                    ViewBag.BZ = "";
                else
                    ViewBag.BZ = en.WS1_BZ.ToString();
                if (en.WS1_HJBH == null)
                    ViewBag.HJBH = "";
                else
                    ViewBag.HJBH = en.WS1_HJBH.ToString();
            }

            return View();
        }
        /// <summary>
        /// 获取BGXQ的数字
        /// </summary>
        private string Get_NUM(string a)
        {

            if (a == "永久")
                return "1";
            else if (a == "10年")
                return "2";
            else
                return "3";
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
                    return "10年";
                else
                    return "30年";

            }
            catch { return null; }
        }

        /// <summary>
        /// 添加WSAJ信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWSAJINFO(FormCollection values)
        {
            string mid = values["txt_MID"];
            string pid = values["txt_PID"];
            if (pid == "" && mid == "")
            {
                Alert.ShowInParent("未获取案卷信息!");
                return UIHelper.Result();
            }

            T_WSAJ1 en = new T_WSAJ1();
            en.WS1_JH = values["txt_JH"];
            en.WS1_TM = values["txt_TM"];
            if (values["cmb_BGQX"] == "" || values["cmb_BGQX"] == null)
                en.WS1_BGQX = null;
            else
                en.WS1_BGQX = Get_TEXT(values["cmb_BGQX"]);
            en.WS1_YEAR = values["txt_YEAR"];
            if (values["txt_GDRQ"] == "" || values["txt_GDRQ"] == null)
                en.WS1_GDRQ = null;
            else
                en.WS1_GDRQ = Convert.ToDateTime(values["txt_GDRQ"]);
            en.WS1_BZ = values["txt_BZ"];
            if (values["txt_HJBH"] == "" || values["txt_HJBH"] == null)
                en.WS1_HJBH = null;
            else
                en.WS1_HJBH = values["txt_HJBH"];
            en.WS1_V01 = "1";
            string prid = values["txt_ID"];
            int value = -1;
            if (prid == null || prid == "")
            {
                en.WS1_MID = int.Parse(mid);
                en.WS1_PID = pid;
                value = TArchive_LinqDal.Add_WSAJ1(en);
                if (value == 0)
                    Alert.ShowInParent("重复的序号!");
                else if (value == -1)
                    Alert.ShowInParent("添加失败!");
                else
                    Alert.ShowInParent("添加成功!");
            }
            else
            {
                value = TArchive_LinqDal.Modify_WSAJ1(int.Parse(prid), en);
                if (value == -1)
                    Alert.ShowInParent("修改失败!");
                else
                    Alert.ShowInParent("修改成功!");
            }
            return UIHelper.Result();
        }
        /// <summary>
        /// WSJN1信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public ActionResult WSJN1INFO(string ajid, string jnid)
        {
            ViewBag.JN1_ID = "";
            ViewBag.AJID = ajid;

            var listType = TSysAr_LinqDal.Query_WordType();
            foreach (var n in listType)
            {
                var nn = FineUIMvc.EmptyProject.Models.ComItems.InitComboBoxQueryWord(n);
                ViewData.Add(n, nn);
            }
            if (jnid == null || jnid == "")
            {
                ////新建卷内ID
                //ViewBag.StartDay = "";
                //ViewBag.EndDay = "";
                //ViewBag.StorageDay = "";
                //ViewBag.BZDay = "";
                ViewBag.RQ = "";
            }
            else
            {
                ////修改卷内ID
                ViewBag.JN1_ID = jnid;
                var en = TArchive_LinqDal.Query_WSJN1(int.Parse(jnid));
                ViewBag.Name = en.JN1_Name.ToString();
                if (en.JN1_XH == null)
                    ViewBag.XH = "";
                else
                    ViewBag.XH = en.JN1_XH.ToString();
                if (en.JN1_BH == null)
                    ViewBag.BH = "";
                else
                    ViewBag.BH = en.JN1_BH.ToString();
                if (en.JN1_YS == null)
                    ViewBag.YS = "";
                else
                    ViewBag.YS = en.JN1_YS.ToString();
                //if (en.JN1_Start == null)
                //    ViewBag.Start = "";
                //else
                //    ViewBag.Start = en.JN1_Start.ToString();
                //if (en.JN1_End == null)
                //    ViewBag.End = "";
                //else
                //    ViewBag.End = en.JN1_End.ToString();
                //if (en.JN1_Type == null)
                //    ViewBag.Type = "";
                //else
                //    ViewBag.Type = en.JN1_Type.ToString();
                if (en.JN1_ZRZ == null)
                    ViewBag.ZRZ = "";
                else
                    ViewBag.ZRZ = en.JN1_ZRZ.ToString();

                if (en.JN1_RQ == null)
                    ViewBag.RQ = "";
                else
                    ViewBag.RQ = Convert.ToDateTime(en.JN1_RQ.ToString());
                if (en.JN1_Remark == null)
                    ViewBag.Remark = "";
                else
                    ViewBag.Remark = en.JN1_Remark.ToString();
            }

            return View();
        }
        /// <summary>
        /// 添加WSJN1信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWSJN1INFO(FormCollection values)
        {
            string ajid = values["txt_AJID"];
            string jnid = values["txt_ID"];
            if (ajid == "" && jnid == "")
            {
                Alert.ShowInParent("未获取案卷信息!");
                return UIHelper.Result();
            }

            T_WSJN1 en = new T_WSJN1();
            en.JN1_Name = values["txt_Name"];
            if (values["txt_XH"] == "" || values["txt_XH"] == null)
                en.JN1_XH = null;
            else
                en.JN1_XH = int.Parse(values["txt_XH"]);
            en.JN1_BH = values["txt_BH"];
            if (values["txt_YS"] == "" || values["txt_YS"] == null)
                en.JN1_YS = null;
            else
                en.JN1_YS = int.Parse(values["txt_YS"]);
            //if (values["txt_Start"] == "" || values["txt_Start"] == null)
            //    en.JN1_Start = null;
            //else
            ////    en.JN1_Start = int.Parse(values["txt_Start"]);
            //if (values["txt_End"] == "" || values["txt_End"] == null)
            //    en.JN1_End = null;
            //else
            //    en.JN1_End = int.Parse(values["txt_End"]);
            //en.JN1_Type = values["txt_Type"];
            en.JN1_ZRZ = values["txt_ZRZ"];
            if (values["txt_RQ"] == "" || values["txt_RQ"] == null)
                en.JN1_RQ = null;
            else
                en.JN1_RQ = Convert.ToDateTime(values["txt_RQ"]);
            en.JN1_Remark = values["txt_Remark"];
            if (values["txt_AJID"] == "" || values["txt_AJID"] == null)
                en.JN1_AJID = null;
            else
                en.JN1_AJID = values["txt_AJID"];
            string prid = values["txt_ID"];
            int value = -1;
            if (prid == null || prid == "")
            {
                value = TArchive_LinqDal.Add_WSJN1(en);
                if (value == 0)
                    Alert.ShowInParent("重复的序号!");
                else if (value == -1)
                    Alert.ShowInParent("添加失败!");
                else
                    Alert.ShowInParent("添加成功!");
            }
            else
            {
                value = TArchive_LinqDal.Modify_WSJN1(int.Parse(prid), en);
                if (value == -1)
                    Alert.ShowInParent("修改失败!");
                else
                    Alert.ShowInParent("修改成功!");
            }
            return UIHelper.Result();
        }
        /// <summary>
        /// 添加aj或修改aj刷新 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult windAddAJ_Close(JArray Grid1_fields, int pageindex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            int count = 0;
            var pager = Session[Models.SessionValue._sArchivePagerAJ] as Models.ArchivePagerWhere;
            pager.iStart = Commons.iPagerRow * pageindex;
            if (pager.iStart > 0)
                pager.iStart++;
            pager.iEnd = pager.iStart + Commons.iPagerRow;
            Session[Models.SessionValue._sArchivePagerAJ] = pager;
            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            grid1.RecordCount(count);
            grid1.DataSource(dt, Grid1_fields);
            return UIHelper.Result();
        }
        /// <summary>
        /// 添加jn或修改jn刷新 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult windAddJN1_Close(JArray Grid1_fields, int pageindex)
        {
            var grid1 = UIHelper.Grid("Grid1");
            int count = 0;
            var pager = Session[Models.SessionValue._sArchivePagerJN] as Models.ArchivePagerWhere;
            pager.iStart = Commons.iPagerRow * pageindex;
            if (pager.iStart > 0)
                pager.iStart++;
            pager.iEnd = pager.iStart + Commons.iPagerRow;
            Session[Models.SessionValue._sArchivePagerJN] = pager;
            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            grid1.RecordCount(count);
            grid1.DataSource(dt, Grid1_fields);
            return UIHelper.Result();
        }
        /// <summary>
        /// 文书卷内
        /// </summary>
        /// <param name="aid">案卷ID</param>
        /// <returns></returns>
        public ActionResult WSJN(string aid, string arid)
        {
            var user = Models.SessionValue.GetUserSession(Session) as V_USER;
            string[] viewDA = user.ROLE_V04 == null ? null : user.ROLE_V04.Split(',');
            string[] modifyDA = user.ROLE_V05 == null ? null : user.ROLE_V05.Split(',');
            bool bView = false;
            bool bModify = false;

            if (viewDA != null)
                bView = viewDA.Select(c => c).Where(c => c == arid).Count() > 0 ? true : false;
            if (modifyDA != null)
                bModify = modifyDA.Select(c => c).Where(c => c == arid).Count() > 0 ? true : false;
            ViewBag.Edit = bModify;
            ViewBag.View = bView;

            int? pid = null;
            if (aid != null && aid != "")
                pid = int.Parse(aid);
            int count = 0;
            //ViewBag.Grid1DataSource = TArchive_LinqDal.Query_WSJN(pid, null, null, null, 0, 1000,ref count, null, null);

            var list = Models.GridColumns.InitJNMLColumns(bView, bModify);
            ViewBag.Grid1Columns = list;
            ViewBag.AID = aid;
            ViewBag.ARID = arid;

            Models.ArchivePagerWhere pager = new Models.ArchivePagerWhere();
            pager.TableName = "V_JN";
            pager.Order = "JN_XH";
            pager.Select = "*";
            pager.iStart = 0;
            pager.iEnd = Commons.iPagerRow;
            pager.Where = "where JN_AJID=" + aid;

            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            ViewBag.Grid1RecordCount = count;
            // 2.获取当前分页数据
            ViewBag.Grid1DataSource = dt;
            Session.Add(Models.SessionValue._sArchivePager, pager);
            /*
            var list = Models.GridColumns.InitGridColumns(int.Parse(aid), bView, bModify);
            ViewBag.AID = aid;
            ViewBag.NAME = name;
            ViewBag.Grid1Columns = list; 
             */

            return View();
        }

        /// <summary>
        /// 编辑卷内目录
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="jnid"></param>
        /// <returns></returns>
        //public ActionResult WSJNInfo(string aid, string jnid)
        //{
        //    ViewBag.JNID = "";
        //    ViewBag.AJID = aid;
        //    if(jnid == null || jnid == "")
        //    {
        //        ////新建卷内ID
        //    }
        //    else
        //    {
        //        ////修改卷内ID
        //        ViewBag.JNID = jnid;
        //        var en=TArchive_LinqDal.Query_ArchiveOF(int.Parse(jnid));
        //        if (en.JN_XH == null)
        //            ViewBag.XH = "";
        //        else
        //            ViewBag.XH = en.JN_XH.ToString();
        //        if (en.JN_WH==null)
        //            ViewBag.WH = "";
        //        else
        //            ViewBag.WH = en.JN_WH.ToString();
        //        if (en.JN_ZRZ==null)
        //            ViewBag.ZRZ = "";
        //        else
        //            ViewBag.ZRZ=en.JN_ZRZ.ToString();
        //        ViewBag.TM = en.JN_TM.ToString();
        //        if (en.JN_REMARK == null)
        //            ViewBag.REMARK = "";
        //        else
        //            ViewBag.REMARK = en.JN_REMARK.ToString();
        //        if (en.JN_YH==null)
        //            ViewBag.YH = "";
        //        else
        //            ViewBag.YH=en.JN_YH.ToString();
        //       // ViewBag.RQ = en.JN_DATE.ToString();
        //    }

        //    return View();
        //}

        /// <summary>
        /// 添加文书卷内目录
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddWSJN(FormCollection values)
        //{
        //    string ajid = values["txt_AID"];
        //    if (ajid == "") {
        //        Alert.ShowInParent("未获取案卷信息!");
        //        return UIHelper.Result();
        //    }

        //    string xh = values["txt_XH"];
        //    int ixh = 0;
        //    if(xh == "")
        //    {
        //        Alert.ShowInParent("序号不能为空!");
        //        return UIHelper.Result();
        //    }
        //    else
        //    {
        //        try
        //        {
        //            ixh = int.Parse(xh);
        //        }
        //        catch {
        //            Alert.ShowInParent("序号必须为数字!");
        //            return UIHelper.Result();
        //        }
        //    }

        //    T_WSJN en = new T_WSJN();
        //    en.JN_AJID = int.Parse(ajid);
        //    en.JN_XH = ixh;
        //    en.JN_YH = values["txt_YC"];
        //    en.JN_ZRZ = values["txt_ZRZ"];
        //    en.JN_WH = values["txt_WH"];
        //    en.JN_REMARK = values["txt_BZ"];
        //    en.JN_STATUS = 1;
        //    if (values["txt_RQ"] == null || values["txt_RQ"] == "")
        //        en.JN_DATE = null;
        //    else
        //         en.JN_DATE = DateTime.Parse(values["txt_RQ"]);
        //    en.JN_TM = values["txt_TM"];
        //    en.JN_YH = values["txt_YC"];//en.JN_YH = values["txt_YC"];

        //    string jnid = values["txt_ID"];
        //    int value = -1;
        //    if (jnid == null || jnid == "")
        //    {
        //        value = TArchive_LinqDal.Add_WSJN(en);
        //        if (value == 0)
        //            Alert.ShowInParent("重复的序号!");
        //        else if (value == -1)
        //            Alert.ShowInParent("添加失败!");
        //        else
        //            Alert.ShowInParent("添加成功!");
        //    }
        //    else
        //    {
        //        value = TArchive_LinqDal.Modify_WSJN(int.Parse(jnid), en);
        //        if (value == -1)
        //            Alert.ShowInParent("修改失败!");
        //        else
        //            Alert.ShowInParent("修改成功!");
        //    }
        //    return UIHelper.Result();       
        //}


        /// <summary>
        /// 文书卷内窗口关闭
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult windowJN_Close(JArray Grid1_fields, string pid)
        //{
        //    var grid1 = UIHelper.Grid("Grid1");
        //    int count = 0;
        //    var list = TArchive_LinqDal.Query_WSJN(int.Parse(pid), null, null, null, 0, 1000, ref count, null, null);
        //    grid1.DataSource(list, Grid1_fields); 
        //    return UIHelper.Result();
        //}

        /// <summary>
        /// 卷内目录的查询与显示
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        //public ActionResult WSJN_QUERY(string aid)
        //{
        //    string[] strs = new string[] { "", "", "", "", aid };
        //    Session["WSJNQuery"] = strs;

        //    var list = Models.GridColumns.InitJNMLQueryColumns();
        //    ViewBag.Grid1Columns = list;
        //    int count = 0;
        //    var items = TArchive_LinqDal.Query_WSJN(null, null, null, null, 0, Commons.iPagerRow, ref count, null, int.Parse(aid));
        //    ViewBag.Grid1RecordCount = count;
        //    // 2.获取当前分页数据
        //    ViewBag.Grid1DataSource = items;   //DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, recordCount: recordCount);
        //    ViewBag.AID = aid;
        //    return View();
        //}

        /// <summary>
        /// 查询卷内目录
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult  btnMLtQuery_Click(JArray GridML_Query_fields, string dh, string wh, string zrz, string tm, string aid)
        //{
        //    var grid1 = UIHelper.Grid("GridML_Query");
        //    string[] strs = new string[] { dh, wh, zrz, tm, aid };
        //    Session["WSJNQuery"] = strs;

        //    int count = 0;
        //    var items = TArchive_LinqDal.Query_WSJN(null, zrz, wh, tm, 0, Commons.iPagerRow, ref count, dh, int.Parse(aid));
        //    grid1.RecordCount(count);
        //    // 2.获取当前分页数据
        //    grid1.DataSource(items, GridML_Query_fields);
        //    return UIHelper.Result();
        //}

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="Grid1_fields"></param>
        /// <param name="Grid1_pageIndex"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GridMLQuery_PageIndexChanged(JArray GridML_Query_fields, int GridML_Query_pageIndex)
        //{
        //    //string[] strs = new string[] { dh, wh, zrz, tm, aid };
        //    string[] strs = Session["WSJNQuery"] as string[];
        //    if (strs == null)
        //        return UIHelper.Result();

        //    var grid1 = UIHelper.Grid("GridML_Query");
        //    int count = 0;
        //    var items = TArchive_LinqDal.Query_WSJN(null, strs[2], strs[1], strs[3], Commons.iPagerRow * GridML_Query_pageIndex, Commons.iPagerRow, ref count, strs[0], int.Parse(strs[4]));
        //    grid1.RecordCount(count);
        //    // 2.获取当前分页数据
        //    grid1.DataSource(items, GridML_Query_fields);showPDF
        //    //Commons.iPagerRow* GridNowView_pageIndex +1


        //    return UIHelper.Result();
        //}

        /// <summary>
        /// 显示电子文件
        /// </summary>
        /// <param name="type">0. 案卷或一文一件, 1.卷内目录，2.公文</param>
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

                if (url.EndsWith("mp4"))
                {
                    var en = TDocument_LinqDal.Get_DocumentWSAJ(int.Parse(id));

                    url = "../../res/generic/web/ViewFilevideo.html?file=" + en.DOC_FILE;
                }
                else
                {
                    url = "../../res/generic/web/viewer.html?file=" + url;
                }

               
                UIHelper.Window("win_MLPic").Show(url, "电子文件");
            }
            catch { Alert.ShowInParent("无电子文件!"); }

            //return View("ViewFilevideo");

            return UIHelper.Result();
        }

        /// <summary>
        /// 删除WSAJ(与公文共用)
        /// </summary>
        /// <param name="opID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnWSDel_ClickAJ(JArray Grid1_fields, int Grid1_pageIndex, string opID)
        {
            try
            {
                int value = TArchive_LinqDal.Delete_WSAJ(int.Parse(opID));
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
                        var pager = Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerAJ] as FineUIMvc.EmptyProject.Models.ArchivePagerWhere;
                        pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                        if (pager.iStart > 0)
                            pager.iStart++;
                        pager.iEnd = pager.iStart + Commons.iPagerRow;
                        Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerAJ] = pager;
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
        /// 删除公文
        /// </summary>
        /// <param name="opID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnWSDel_ClickMG(JArray Grid1_fields, int Grid1_pageIndex, string opID)
        {
            try
            {
                int value = TArchive_LinqDal.Delete_mg(int.Parse(opID));
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
                        var pager = Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerAJ] as FineUIMvc.EmptyProject.Models.ArchivePagerWhere;
                        pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                        if (pager.iStart > 0)
                            pager.iStart++;
                        pager.iEnd = pager.iStart + Commons.iPagerRow;
                        Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerAJ] = pager;
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
        /// 删除WAJN1
        /// </summary>
        /// <param name="opID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnWSDel_ClickJN(JArray Grid1_fields, int Grid1_pageIndex, string opID)
        {
            try
            {
                int value = TArchive_LinqDal.Delete_WSJN(int.Parse(opID));
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
                        var pager = Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerJN] as FineUIMvc.EmptyProject.Models.ArchivePagerWhere;
                        pager.iStart = Commons.iPagerRow * Grid1_pageIndex;
                        if (pager.iStart > 0)
                            pager.iStart++;
                        pager.iEnd = pager.iStart + Commons.iPagerRow;
                        Session[FineUIMvc.EmptyProject.Models.SessionValue._sArchivePagerJN] = pager;
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
        /// 公文页面
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="name"></param>
        /// <returns></returns>j
        public ActionResult Message(string mid, string name)
        {
            var user = Models.SessionValue.GetUserSession(Session) as V_USER;
            bool bView = TUser_LinqDal.Query_MGQX(mid, user.U_ID);
            ViewBag.VIEW = bView.ToString();
            int count = 0;
            ViewBag.MID = mid;
            ViewBag.Name = name;
            var list = Models.GridColumns.InitMGQueryColumns();
            ViewBag.Grid1Columns = list;

            Models.ArchivePagerWhere pager = new Models.ArchivePagerWhere();
            pager.TableName = "T_Message";
            pager.Order = "Message_ID";
            pager.Select = "*";
            pager.iStart = 0;
            pager.iEnd = Commons.iPagerRow;
            pager.Where = "where MG_mid='" + mid + "'";

            var dt = TArchive_LinqDal.Query_Archive(pager.TableName, pager.Where, pager.Select, pager.Order, pager.iStart, pager.iEnd, ref count);
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            ViewBag.Grid1RecordCount = count;
            // 2.获取当前分页数据
            ViewBag.Grid1DataSource = dt;
            Session.Add(Models.SessionValue._sArchivePagerAJ, pager);
            return View();
        }
        /// <summary>
        /// 公文信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public ActionResult MessageInfo(string mid, string id)
        {
            ViewBag.id = "";
            ViewBag.MID = mid;

            var listType = TSysAr_LinqDal.Query_WordType();
            foreach (var n in listType)
            {
                var nn = FineUIMvc.EmptyProject.Models.ComItems.InitComboBoxQueryWord(n);
                ViewData.Add(n, nn);
            }
            if (id == null || id == "")
            {
                ////新建卷内ID
                //ViewBag.StartDay = "";
                //ViewBag.EndDay = "";
                //ViewBag.StorageDay = "";
                //ViewBag.BZDay = "";
                ViewBag.Date = "";
            }
            else
            {
                ////修改卷内ID
                ViewBag.ID = id;
                var en = TArchive_LinqDal.Query_MG(int.Parse(id));
                //ViewBag.JH = en.WS1_JH.ToString();
                if (en.Message_Title == null)
                    ViewBag.Title = "";
                else
                    ViewBag.Title = en.Message_Title.ToString();
                if (en.Message_BGQX == null)
                    ViewBag.BGQX = "";
                else
                    ViewBag.BGQX = Get_NUM(en.Message_BGQX.ToString());
                if (en.Message_Sender == null)
                    ViewBag.Sender = "";
                else
                    ViewBag.Sender = en.Message_Sender.ToString();
                if (en.Message_Date == null)
                    ViewBag.Date = "";
                else
                    ViewBag.Date = Convert.ToDateTime(en.Message_Date.ToString());
                if (en.Message_Remark == null)
                    ViewBag.Remark = "";
                else
                    ViewBag.Remark = en.Message_Remark.ToString();
                if (en.Message_NO == null)
                    ViewBag.NO = "";
                else
                    ViewBag.NO = en.Message_NO.ToString();
                if (en.Message_WH == null)
                    ViewBag.WH = "";
                else
                    ViewBag.WH = en.Message_WH.ToString();
                if (en.Message_Type == null)
                    ViewBag.Type = "";
                else
                    ViewBag.Type = en.Message_Type.ToString();
                if (en.Message_SendUnit == null)
                    ViewBag.SendUnit = "";
                else
                    ViewBag.SendUnit = en.Message_SendUnit.ToString();
                if (en.Message_ReceiveUnit == null)
                    ViewBag.ReceiveUnit = "";
                else
                    ViewBag.ReceiveUnit = en.Message_ReceiveUnit.ToString();
            }

            return View();
        }
        /// <summary>
        /// 添加Message信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMessageInfo(FormCollection values)
        {
            string mid = values["txt_MID"];
            // string pid = values["txt_PID"];
            if (mid == "")
            {
                Alert.ShowInParent("未获取案卷信息!");
                return UIHelper.Result();
            }

            T_Message en = new T_Message();
            en.Message_NO = values["txt_NO"].ToString();
            en.Message_WH = values["txt_WH"].ToString();
            en.Message_Title = values["txt_Title"].ToString();
            if (mid == "3")
                en.Message_Type = "接收";
            else
                en.Message_Type = "发送";
            // en.Message_Type = values["txt_Type"].ToString();
            en.Message_SendUnit = values["txt_SendUnit"].ToString();
            en.Message_ReceiveUnit = values["txt_ReceiveUnit"].ToString();
            en.Message_Sender = values["txt_Sender"].ToString();
            en.Message_Remark = values["txt_Remark"].ToString();
            if (values["cmb_BGQX"] == "" || values["cmb_BGQX"] == null)
                en.Message_BGQX = null;
            else
                en.Message_BGQX = Get_TEXT(values["cmb_BGQX"]);
            if (values["txt_Date"] == "" || values["txt_Date"] == null)
                en.Message_Date = null;
            else
                en.Message_Date = Convert.ToDateTime(values["txt_Date"]);


            string prid = values["txt_ID"];
            int value = -1;
            if (prid == null || prid == "")
            {
                en.MG_mid = int.Parse(mid);
                value = TArchive_LinqDal.Add_T_Message(en);
                if (value == 0)
                    Alert.ShowInParent("重复的序号!");
                else if (value == -1)
                    Alert.ShowInParent("添加失败!");
                else
                    Alert.ShowInParent("添加成功!");
            }
            else
            {
                value = TArchive_LinqDal.Modify_T_Message(int.Parse(prid), en);
                if (value == -1)
                    Alert.ShowInParent("修改失败!");
                else
                    Alert.ShowInParent("修改成功!");
            }
            return UIHelper.Result();
        }
        public ActionResult Import(string mid, string name)
        {
            ViewBag.NAME = name;
            return View();
        }
        public ActionResult Import1(string mid, string name, string path)
        {
            ViewBag.NAME = name;

            //ViewBag.PATH = FILEPATH;
            return View();
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnUploadFileXLS_Click(HttpPostedFileBase upload_File, FormCollection values)
        {
            if (upload_File != null)
            {
                string fileName = upload_File.FileName;
                string type = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (type.ToUpper() != "XLS")
                //if (!ValidateFileType(fileName))
                {
                    // 清空文件上传组件
                    UIHelper.FileUpload("upload_File").Reset();

                    Alert.ShowInParent("系统目前只支持XLS格式的文件导入!");
                }
                else
                {
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                    fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                    upload_File.SaveAs(Server.MapPath("~/res/xls/" + fileName));
                    string fn = Server.MapPath("~/res/xls/" + fileName);
                    int i = TArchive_LinqDal.AddXLS(fn);
                    if (i == 1)
                    { Alert.ShowInParent("导入成功"); }
                    else
                    { Alert.ShowInParent("导入失败"); }
                }
            }
            return UIHelper.Result();
        }
        /// <summary>
        /// 一文一件WSAJ
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Openfile()
        {
            string fullname = "D:\\案卷导入pdf";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fullname);
            if (!di.Exists)
                di.Create();
            System.IO.FileInfo[] ds = di.GetFiles("*.pdf", System.IO.SearchOption.AllDirectories);

            string filename = "";
            string path = "";
            string filepath = Server.MapPath("/res/pdf/");
            foreach (var d in ds)
            {
                filename = d.Name;
                path = d.FullName;
                string pdfname = filename.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                pdfname = DateTime.Now.Ticks.ToString() + "_" + pdfname;

                if (TArchive_LinqDal.AddOneFile(path, filename, pdfname) == 1)//修改数据库信息以及添加文字
                {
                    TArchive_LinqDal.Addpdf(path, filepath + pdfname);//上传附件
                }
            }
            Alert.ShowInParent("导入成功!");
            return UIHelper.Result();
        }
        /// <summary>
        /// 一文一件Message
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpenfileMG()
        {
            string fullname = "D:\\公文导入pdf";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fullname);
            if (!di.Exists)
                di.Create();
            System.IO.FileInfo[] ds = di.GetFiles("*.pdf", System.IO.SearchOption.AllDirectories);
            string filename = "";
            string path = "";
            string filepath = Server.MapPath("/res/pdf/");
            foreach (var d in ds)
            {
                filename = d.Name;
                path = d.FullName;
                string pdfname = filename.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                pdfname = DateTime.Now.Ticks.ToString() + "_" + pdfname;
                if (TArchive_LinqDal.AddOneFileMG(path, filename, pdfname) == 1)//修改数据库信息以及添加文字
                {
                    TArchive_LinqDal.Addpdf(path, filepath + pdfname);//上传附件
                }
            }
            Alert.ShowInParent("导入成功!");
            return UIHelper.Result();
        }
        /// <summary>
        /// 卷内
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpenfileJN()
        {
            string fullname = "D:\\卷内导入pdf";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fullname);
            if (!di.Exists)
                di.Create();
            System.IO.FileInfo[] ds = di.GetFiles("*.pdf", System.IO.SearchOption.AllDirectories);
            string filename = "";
            string path = "";
            string filepath = Server.MapPath("/res/pdf/");
            foreach (var d in ds)
            {
                filename = d.Name;
                path = d.FullName;
                string pdfname = filename.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                pdfname = DateTime.Now.Ticks.ToString() + "_" + pdfname;
                if (TArchive_LinqDal.AddJNFile(path, filename, pdfname) == 1)//修改数据库信息以及添加文字
                {
                    TArchive_LinqDal.Addpdf(path, filepath + pdfname);//上传附件
                }
            }
            Alert.ShowInParent("导入成功!");
            return UIHelper.Result();
        }
        //private string FILEPATH = "";
        /// <summary>
        /// 上传XLS
        /// </summary>
        //public void toxls()
        //{
        //    System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
        //    dialog.Title = "请选择文件夹";
        //    dialog.Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";
        //    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //       TArchive_LinqDal.AddXLS(dialog.FileName);
        //    }
        //}
        //private string FILEPATH = "";
        ///// <summary>
        ///// 上传pdf(一文一件WSAJ)
        ///// </summary>
        //public void th()
        //{
        //    System.Windows.Forms.ListView list = new System.Windows.Forms.ListView();
        //    System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();

        //    if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        //string  FILEPATH = fd.SelectedPath;
        //        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fd.SelectedPath);
        //        System.IO.FileInfo[] ds = di.GetFiles("*.pdf");
        //        foreach (var d in ds)
        //            list.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { d.Name, d.FullName, "" }));
        //    }
        //    //string filename= list.Items[1].SubItems[0].Text;
        //    //string path = list.Items[1].SubItems[1].Text;
        //    string filename="";
        //    string path = "";
        //    string filepath = Server.MapPath("/res/pdf/");
        //    for (int i = 0; i < list.Items.Count; i++) 
        //    {
        //         filename= list.Items[i].SubItems[0].Text;
        //         path = list.Items[i].SubItems[1].Text;

        //        TArchive_LinqDal.Addpdf(path,filepath+filename);//上传附件
        //        TArchive_LinqDal.AddOneFile(path, filename);//修改数据库信息以及添加文字
        //    }
        //}
        ///// <summary>
        ///// 上传pdf(一文一件Message)
        ///// </summary>
        //public void th1()
        //{
        //    System.Windows.Forms.ListView list = new System.Windows.Forms.ListView();
        //    System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();

        //    if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        //string  FILEPATH = fd.SelectedPath;
        //        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fd.SelectedPath);
        //        System.IO.FileInfo[] ds = di.GetFiles("*.pdf");
        //        foreach (var d in ds)
        //            list.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { d.Name, d.FullName, "" }));
        //    }
        //    //string filename= list.Items[1].SubItems[0].Text;
        //    //string path = list.Items[1].SubItems[1].Text;
        //    string filename = "";
        //    string path = "";
        //    string filepath = Server.MapPath("/res/pdf/");
        //    for (int i = 0; i < list.Items.Count; i++)
        //    {
        //        filename = list.Items[i].SubItems[0].Text;
        //        path = list.Items[i].SubItems[1].Text;

        //        TArchive_LinqDal.Addpdf(path, filepath + filename);//上传附件
        //        TArchive_LinqDal.AddOneFileMG(path, filename);//修改数据库信息以及添加文字
        //    }
        //}
        ///// <summary>
        ///// 上传pdf(卷内)
        ///// </summary>
        //public void th2()
        //{
        //    System.Windows.Forms.ListView list = new System.Windows.Forms.ListView();
        //    System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();

        //    if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        //string  FILEPATH = fd.SelectedPath;
        //        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fd.SelectedPath);
        //        System.IO.FileInfo[] ds = di.GetFiles("*.pdf");
        //        foreach (var d in ds)
        //            list.Items.Add(new System.Windows.Forms.ListViewItem(new string[] { d.Name, d.FullName, "" }));
        //    }
        //    //string filename= list.Items[1].SubItems[0].Text;
        //    //string path = list.Items[1].SubItems[1].Text;
        //    string filename = "";
        //    string path = "";
        //    string filepath = Server.MapPath("/res/pdf/");
        //    for (int i = 0; i < list.Items.Count; i++)
        //    {
        //        filename = list.Items[i].SubItems[0].Text;
        //        path = list.Items[i].SubItems[1].Text;

        //        TArchive_LinqDal.Addpdf(path, filepath + filename);//上传附件
        //        TArchive_LinqDal.AddJNFile(path, filename);//修改数据库信息以及添加文字
        //    }
        //}
    }
}