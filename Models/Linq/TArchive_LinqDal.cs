using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Aspose.Cells;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 通用的档案操作类
    /// </summary>
    public class TArchive_LinqDal
    {
        public static int Add_Archive(string sql)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                conn.Database.ExecuteSqlCommand(sql, new string[] { "" } );
                return 1;
            }
            catch(Exception ex) { if (ex.Message.Contains("重复")) return 0; else return -1; }
        }

        public static int Modify_Archive(string sql)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                conn.Database.ExecuteSqlCommand(sql, new string[] { "" });
                return 1;
            }
            catch (Exception ex) { if (ex.Message.Contains("重复")) return 0; else return -1; }
        }

        //public static int Query_WSArchiveID(string dh)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var en = conn.T_WSAJ.Select(c => c).Where(c => c.WS_DH == dh).Single();
        //        return en.WS_ID;
        //    }
        //    catch (Exception ex) { return -1; }
        //}

        public static DataTable Query_Archive(string table, string where, string select, string order, int start, int end, ref int count)
        {
            GGArchiveSystemEntities database = new GGArchiveSystemEntities();

            string sql = Get_SelectSql(table, where, select, order, start, end);

            SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            try
            {
                conn.ConnectionString = database.Database.Connection.ConnectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                ///总数
                cmd.CommandText = "select count(1) from " + table + " " + where;
                count = int.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = sql;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                conn.Close();//连接需要关闭  
                conn.Dispose();
                return dt;
            }
            catch(Exception ex) {
                conn.Close();
                conn.Dispose();
                return null;
            }
        }

        ///// <summary>
        ///// 获取某一个档案
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static T_WSAJ Query_Archive(int id)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        return conn.T_WSAJ.Select(c => c).Where(c => c.WS_ID == id).Single();
        //    }
        //    catch { return null; }
        //}
        ///// <summary>
        ///// 获取某一个档案中某一卷
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static T_WSJN Query_ArchiveOF(int id)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        return conn.T_WSJN.Select(c => c).Where(c => c.JN_ID == id).Single();
        //    }
        //    catch { return null; }
        //}

        /// <summary>
        /// 获取VWSJN
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static V_WSJN Query_VWSJN(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.V_WSJN.Select(c => c).Where(c => c.JN1_ID == id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取公文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_Message Query_MG(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_Message.Select(c => c).Where(c => c.Message_ID == id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取WSAJ1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_WSAJ1 Query_WSAJ1(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_ID == id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 获取WSJN1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_WSJN1 Query_WSJN1(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_WSJN1.Select(c => c).Where(c => c.JN1_ID == id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 添加公文信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_T_Message(T_Message en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var count = conn.T_Message.Select(c => c).Where(c => c.MG_mid == en.MG_mid && c.Message_WH == en.Message_WH).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_Message.Add(en);
                    conn.SaveChanges();
                    return en.Message_ID;
                }
            }
            catch { return -1; }
        }
        /// <summary>
        /// 修改公文信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_T_Message(int id, T_Message en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_Message.Select(c => c).Where(c => c.Message_ID == id);
                foreach (var nn in n)
                {
                    nn.Message_NO = en.Message_NO;
                    nn.Message_WH = en.Message_WH;
                    nn.Message_Title = en.Message_Title;
                    nn.Message_Date = en.Message_Date;
                    nn.Message_Type = en.Message_Type;
                    nn.Message_SendUnit = en.Message_SendUnit;

                    nn.Message_ReceiveUnit = en.Message_ReceiveUnit;
                    nn.Message_Sender = en.Message_Sender;
                    nn.Message_Remark = en.Message_Remark;
                }
                var query = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_I01 == id);
                if (query.Count() > 0)
                {
                    foreach (var nn in query)
                    {
                        nn.DOC_V01 = en.Message_WH;
                        nn.DOC_V02 = en.Message_Title;
                    }
                    conn.SaveChanges();
                }
                conn.SaveChanges();
                return 1;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 添加WSAJ1信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_WSAJ1(T_WSAJ1 en)
        {
            try
            {                
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int count=-1;
                if(en.WS1_MID==null)
                    count = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_PID == en.WS1_PID && c.WS1_JH == en.WS1_JH).Count();
                else
                    count = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_MID == en.WS1_MID && c.WS1_JH == en.WS1_JH).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_WSAJ1.Add(en);
                    conn.SaveChanges();
                    return en.WS1_ID;
                }
            }
            catch { return -1; }
        }
        /// <summary>
        /// 修改WSAJ1信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_WSAJ1(int id, T_WSAJ1 en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_ID == id);
                foreach (var nn in n)
                {
                    nn.WS1_JH = en.WS1_JH;
                    nn.WS1_TM = en.WS1_TM;
                    nn.WS1_YEAR = en.WS1_YEAR;
                    nn.WS1_BGQX = en.WS1_BGQX;
                    nn.WS1_GDRQ = en.WS1_GDRQ;
                    nn.WS1_BZ = en.WS1_BZ;
                    nn.WS1_HJBH = en.WS1_HJBH;
                }
                var query = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_AID == id);
                if (query.Count() > 0)
                {
                    foreach (var nn in query)
                    {
                        nn.DOC_V01 = en.WS1_JH;
                        nn.DOC_V02 = en.WS1_TM;
                    }
                    conn.SaveChanges();
                }
                conn.SaveChanges();

                return 1;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 添加WSJN1信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_WSJN1(T_WSJN1 en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var count = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_AJID == en.JN1_AJID && c.JN1_BH == en.JN1_BH).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_WSJN1.Add(en);
                    conn.SaveChanges();
                    return en.JN1_ID;
                }
            }
            catch { return -1; }
        }
        /// <summary>
        /// 修改WSJN1信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_WSJN1(int id, T_WSJN1 en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_ID == id);
                string dh=n.Single().JN1_AJID;
                foreach (var nn in n)
                {
                    nn.JN1_Name = en.JN1_Name;
                    nn.JN1_XH = en.JN1_XH;
                    nn.JN1_BH = en.JN1_BH;
                    nn.JN1_YS = en.JN1_YS;
                    nn.JN1_ZRZ = en.JN1_ZRZ;
                    nn.JN1_RQ = en.JN1_RQ;
                    nn.JN1_Remark = en.JN1_Remark;
                }
                conn.SaveChanges();
                var query = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_NID == id);
                if (query.Count() > 0)
                {
                    foreach (var nn in query)
                    {
                        nn.DOC_V01=dh;
                        nn.DOC_V02 = en.JN1_Name;
                    }
                    conn.SaveChanges();
                }
                return 1;
            }
            catch { return -1; }
        }




        ///// <summary>
        ///// 删除某一个档案 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static int Delete_Archive(int id)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var query = conn.T_WSAJ.Select(c => c).Where(c => c.WS_ID == id);
        //        conn.T_WSAJ.RemoveRange(query);
        //        conn.SaveChanges();
        //        return 1;
        //    }
        //    catch { return -1; }
        //}
        ///// <summary>
        ///// 删除某一个档案的某一卷
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static int Delete_ArchiveOF(int id) 
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var query = conn.T_WSJN.Select(c => c).Where(c => c.JN_ID == id);
        //        conn.T_WSJN.RemoveRange(query);
        //        conn.SaveChanges();
        //        return 1;
        //    }
        //    catch { return -1; }
        //}
        public static int Delete_mg(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_Message.Select(c => c).Where(c => c.Message_ID == id);
                //string WS1JH = query.Single().WS1_JH;
                conn.T_Message.RemoveRange(query);
                conn.SaveChanges();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 删除WSAJ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete_WSAJ(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_ID == id);
                //string WS1JH = query.Single().WS1_JH;
                conn.T_WSAJ1.RemoveRange(query);
                conn.SaveChanges();
                //var query1 = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_AJID == WS1JH);
                //if (query1.Count() > 0) 
                //{
                //    int jnid = query1.Single().JN1_ID;            
                //    conn.T_WSJN1.RemoveRange(query1);
                //    conn.SaveChanges();

                //    var query2 = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_NID == jnid);
                //    if (query2.Count() > 0) 
                //    {
                //        int DocID = query2.Single().DOC_ID;
                //        conn.T_DOCUMENT.RemoveRange(query2);
                //        conn.SaveChanges();

                //        var query3 = conn.T_TEXT.Select(c => c).Where(c => c.TEXT_DOCID == DocID);
                //        conn.T_TEXT.RemoveRange(query3);
                //        conn.SaveChanges();
                //    }
                //}
                return 1;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 删除WSJN
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete_WSJN(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_ID == id);
                conn.T_WSJN1.RemoveRange(query);
                conn.SaveChanges();

                var query1 = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_NID == id);
                if (query1.Count() > 0) 
                { 
                    int DocID = query1.Single().DOC_ID;
                    conn.T_DOCUMENT.RemoveRange(query1);
                    conn.SaveChanges();

                    var query2 = conn.T_TEXT.Select(c => c).Where(c => c.TEXT_DOCID == DocID);
                    conn.T_TEXT.RemoveRange(query2);
                    conn.SaveChanges();
                }
                return 1;
            }
            catch { return -1; }
        }
        private static string Get_SelectSql(string table,string where, string select, string order, int start, int end)
        {
            string sql = "select " + select + " from " +
                "(select row_number() over(order by " + order + ")rownumber," + select + " from " + table + " " + where + ")a " +
                "where rownumber between " + start.ToString() + " and " + end.ToString();
            return sql;
        }

        ///// <summary>
        ///// 鉴定档案，登记销毁
        ///// </summary>
        ///// <returns></returns>
        //public static bool JD_Archive(List<int> list, string name)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        foreach (var n in list)
        //        {
        //            var nn = conn.T_WSAJ.Select(c => c).Where(c => c.WS_ID == n);
        //            foreach (var ns in nn)
        //            {
        //                ///销毁登记人
        //                ns.WS_V01 = name;
        //                ns.WS_D01 = DateTime.Now;
        //                ns.WS_JD = 1;
        //            }
        //        }
        //        conn.SaveChanges();
        //    }
        //    catch { return false; }
        //    return true;
        //}

        ///// <summary>
        ///// 获取此文件的电子文件
        ///// </summary>
        ///// <param name="aid"></param>
        ///// <returns></returns>
        //public static string Get_File(int aid)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var n = conn.T_WSAJ.Where(c => c.WS_ID == aid).Select(c => new { c.WS_V10 }).Single();
        //        return n.WS_V10;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 获取卷内级数据
        ///// </summary>
        ///// <returns></returns>
        //public static IEnumerable<V_JN> Query_WSJN(int? aid, string zrz, string wh, string tm, int startrow, int pagesize, ref int count, string dh, int? atypeid)
        //{
        //    count = 0;
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var query = conn.V_JN.Select(c => c);
        //        if (aid != null)
        //            query = query.Where(c => c.JN_AJID == aid);
        //        if (zrz != null && zrz != "")
        //            query = query.Where(c => c.JN_ZRZ.Contains(zrz));
        //        if (wh != null && wh != "")
        //            query = query.Where(c => c.JN_WH.Contains(wh));
        //        if (tm != null && tm != "")
        //            query = query.Where(c => c.JN_TM.Contains(tm));
        //        if (dh != null && dh != "")
        //            query = query.Where(c => c.WS_DH.Contains(dh));
        //        if (atypeid != null && atypeid > 0)
        //            query = query.Where(c => c.WS_AID == atypeid);
        //        ///有密级的不允许查询卷内目录
        //        query = query.Where(c => c.MJ == "无" || c.MJ == null || c.MJ == "");

        //        count = query.Count();
        //        return query.OrderBy(c => c.JN_AJID).OrderBy(c=>c.JN_XH).Skip(startrow).Take(pagesize);
        //    }
        //    catch { return null; }
        //}

        ///// <summary>
        ///// 添加卷内目录
        ///// </summary>
        ///// <param name="en"></param>
        ///// <returns></returns>
        //public static int Add_WSJN(T_WSJN en)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var count = conn.T_WSJN.Select(c => c).Where(c => c.JN_AJID == en.JN_AJID && c.JN_XH == en.JN_XH).Count();
        //        if (count > 0)
        //            return 0;
        //        else
        //        {
        //            conn.T_WSJN.Add(en);
        //            conn.SaveChanges();
        //            return en.JN_ID;
        //        }
        //    }
        //    catch { return -1; }
        //}

        //public static int Modify_WSJN(int id, T_WSJN en)
        //{
        //    try
        //    {
        //        GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
        //        var n = conn.T_WSJN.Select(c => c).Where(c => c.JN_ID == id);
        //        foreach(var nn in n)
        //        {
        //            nn.JN_REMARK = en.JN_REMARK;
        //            nn.JN_STATUS = en.JN_STATUS;
        //            nn.JN_TM = en.JN_TM;
        //            nn.JN_DATE = en.JN_DATE;
        //            nn.JN_WH = en.JN_WH;
        //            nn.JN_XH = en.JN_XH;
        //            nn.JN_YH = en.JN_YH;
        //            nn.JN_ZRZ = en.JN_ZRZ;
        //        }
        //        conn.SaveChanges();
        //        return 1;
        //        ///var count = conn.T_WSJN.Select(c=>c).Where(c=>c.JN_ID != id && c.JN_AJID == en.JN_AJID && c.JN_XH = en.JN_XH
        //    }
        //    catch { return -1; }
        //}
        /// <summary>
        /// 批量上传都调用这个方法
        /// </summary>
        /// <param name="path">所在电脑路径</param>
        /// <param name="filename">服务器路径</param>
        /// <returns></returns>
        public static int Addpdf(string path,string filename) 
        {
            try
            {
                Byte[] MeaningFile;
                System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                int size = Convert.ToInt32(stream.Length);
                MeaningFile = new Byte[size];
                stream.Read(MeaningFile, 0, size);
                stream.Close();
                System.IO.FileStream fos = null;
                //if (!System.IO.Directory.Exists(filePath))
                //{
                //    System.IO.Directory.CreateDirectory(filePath);
                //}
                fos = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                fos.Write(MeaningFile, 0, MeaningFile.Length);
                fos.Close();
                return 1;
            }
            catch 
            {
                return -1;
            }
        }
        /// <summary>
        /// 上传一文一件（WSAJ）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int AddOneFile(string path, string FN, string pafname)
        {
            try
            {
                string fn = FN.Replace(".pdf", "");
                string fileName = pafname;
                //fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_JH == fn);
                foreach (var nn in n)
                {
                    nn.WS1_V01 = fileName;
                }
                conn.SaveChanges();
                int ajid = -1;
                int mid = -1;
                string WSJH = "";
                string WSTM = "";
                var doc = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_JH == fn);
                foreach (var aa in doc)
                {
                    ajid = aa.WS1_ID;
                    mid = (int)aa.WS1_MID;
                    WSJH = aa.WS1_JH;
                    WSTM = aa.WS1_TM;
                }
                var en2 = TDocument_LinqDal.Get_DocumentWSAJ(ajid);
                if (en2 != null)//此档案已经有附件了
                {
                    var querys = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_AID == ajid);
                    foreach (var t in querys)
                    {
                        t.DOC_FILE = fileName;
                        t.DOC_NAME = fileName;
                        t.DOC_PATH = "~/res/pdf";
                    }
                    conn.SaveChanges();
                }
                else
                {
                    T_DOCUMENT en1 = new T_DOCUMENT();
                    en1.DOC_ATYPE = mid;
                    en1.DOC_AID = ajid;
                    en1.DOC_FILE = fileName;
                    en1.DOC_PATH = "~/res/pdf";
                    //en1.DOC_NID = jnid;
                    en1.DOC_TYPE = "pdf";
                    en1.DOC_STATUS = 1;
                    en1.DOC_NAME = fileName;
                    en1.DOC_V01 = WSJH;//em.WS1_JH;档号
                    en1.DOC_V02 = WSTM;//em.WS1_TM;题名
                    conn.T_DOCUMENT.Add(en1);
                    conn.SaveChanges();
                }
                O2S.Components.PDFView4NET.PDFDocument pdf = new O2S.Components.PDFView4NET.PDFDocument();
                pdf.Load(path);
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
                    VText_LinqDal.AddTEXT1(ajid, ss);
                }
                return 1;
            }
            catch 
            {
                return -1;
            }

        }
        /// <summary>
        /// 上传一文一件（Message）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int AddOneFileMG(string path,string FN,string pafname)
        {
            try
            {
                string fn = FN.Replace(".pdf", "");
                string fileName  = pafname;;
                //fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_Message.Select(c => c).Where(c => c.Message_WH == fn);
                foreach (var nn in n)
                {
                    nn.MG_V01 = fileName;
                }
                conn.SaveChanges();
                int mgid = -1;
                int mid = -1;
                string MWH = "";
                string MTM = "";
                var doc = conn.T_Message.Select(c => c).Where(c => c.Message_WH == fn);
                foreach (var aa in doc)
                {
                    mgid = aa.Message_ID;
                    mid = (int)aa.MG_mid;
                    MWH = aa.Message_WH;
                    MTM = aa.Message_Title;
                }
                var en2 = TDocument_LinqDal.Get_DocumentMG(mgid);
                if (en2 != null)//此档案已经有附件了
                {
                    var querys = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_I01 == mgid);
                    foreach (var t in querys)
                    {
                        t.DOC_FILE = fileName;
                        t.DOC_NAME = fileName;
                        t.DOC_PATH = "~/res/pdf";
                    }
                    conn.SaveChanges();
                }
                else
                {
                    T_DOCUMENT en1 = new T_DOCUMENT();
                    en1.DOC_ATYPE = mid;
                    en1.DOC_I01 = mgid;
                    en1.DOC_FILE = fileName;
                    en1.DOC_PATH = "~/res/pdf";
                    //en1.DOC_NID = jnid;
                    en1.DOC_TYPE = "pdf";
                    en1.DOC_STATUS = 1;
                    en1.DOC_NAME = fileName;
                    en1.DOC_V01 = MWH;//em.WS1_JH;档号
                    en1.DOC_V02 = MTM;//em.WS1_TM;题名
                    conn.T_DOCUMENT.Add(en1);
                    conn.SaveChanges();
                }
                O2S.Components.PDFView4NET.PDFDocument pdf = new O2S.Components.PDFView4NET.PDFDocument();
                pdf.Load(path);
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
                    VText_LinqDal.AddTEXT2(mgid, ss);
                }
                return 1;
            }
            catch 
            {
                return -1;
            }

        }
        /// <summary>
        /// 上传卷内
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int AddJNFile(string path,string FN,string pafname)
        {
            try
            {
                string fn = FN.Replace(".pdf", "");
                string fileName = pafname;
                //fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_BH == fn);
                foreach (var nn in n)
                {
                    nn.JN1_Scan = fileName;
                }
                conn.SaveChanges();
                int ajid = -1;
                int mid = -1;
                int jnid = -1;
                string JBH = "";
                string JTM = "";
                var doc = conn.V_WSJN.Select(c => c).Where(c => c.JN1_BH == fn);
                foreach (var aa in doc)
                {
                    ajid = (int)aa.WS1_ID;
                    mid = (int)aa.WS1_MID;
                    jnid = aa.JN1_ID;
                    JBH = aa.JN1_BH;
                    JTM = aa.JN1_Name;
                }
                var en2 = TDocument_LinqDal.Get_DocumentWSJN(jnid);
                if (en2 != null)//此档案已经有附件了
                {
                    var querys = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_NID == jnid);
                    foreach (var t in querys)
                    {
                        t.DOC_FILE = fileName;
                        t.DOC_NAME = fileName;
                        t.DOC_PATH = "~/res/pdf";
                    }
                    conn.SaveChanges();
                }
                else
                {
                    T_DOCUMENT en1 = new T_DOCUMENT();
                    en1.DOC_ATYPE = mid;
                    en1.DOC_AID = ajid;
                    en1.DOC_FILE = fileName;
                    en1.DOC_PATH = "~/res/pdf";
                    en1.DOC_NID = jnid;
                    en1.DOC_TYPE = "pdf";
                    en1.DOC_STATUS = 1;
                    en1.DOC_NAME = fileName;
                    en1.DOC_V01 = JBH;//em.WS1_JH;档号
                    en1.DOC_V02 = JTM;//em.WS1_TM;题名
                    conn.T_DOCUMENT.Add(en1);
                    conn.SaveChanges();
                }
                O2S.Components.PDFView4NET.PDFDocument pdf = new O2S.Components.PDFView4NET.PDFDocument();
                pdf.Load(path);
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
                    VText_LinqDal.AddTEXT(jnid, ss);
                }
                return 1;
            }
            catch 
            {
                return -1;
            }

        }
        public static int AddXLS(string fileUrl)//System.IO.Stream fileUrl
        {
            //office2007之前 仅支持.xls
            const string cmdText = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            //支持.xls和.xlsx，即包括office2010等版本的   HDR=Yes代表第一行是标题，不是数据；
            //const string cmdText = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            System.Data.DataTable dt = null;
            //建立连接D
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(string.Format(cmdText, fileUrl));
            try
            {
                //打开连接
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                Workbook workbook = new Workbook();
                workbook.Open(fileUrl);
               // Worksheet sheet = workbook.Worksheets[0];
              //  Excel.Worksheet tempSheet = (Excel.Worksheet)xBook.Sheets[2];
                int ooooo = workbook.Worksheets.Count;
                string[] format = { "yyyyMMdd", "yyyy/MM/dd", "yyyy:MM:dd" };
                DateTime message_Date;
                for (int i = 0; i < ooooo; i++)
                {
                    string ttttt = workbook.Worksheets[i].Name;
                    if (ttttt == "T_Project")
                    {
                        string strSql = "select * from [" + ttttt+"$" + "]";
                        OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dt = RemoveEmpty(ds.Tables[0]);
                        foreach (DataRow dr in dt.Rows)
                        {
                            T_ProjectInfo en = new T_ProjectInfo();
                            en.PR_Name = dr["PR_Name"].ToString().Trim();
                            en.PR_BuildUnit = dr["PR_BuildUnit"].ToString().Trim();
                            en.PR_BuildUnitPhone = dr["PR_BuildUnitPhone"].ToString().Trim();
                            en.PR_BuildUnitAddress = dr["PR_BuildUnitAddress"].ToString().Trim();
                            en.PR_BuildAdress = dr["PR_BuildAdress"].ToString().Trim();
                            en.PR_DesignUnit = dr["PR_DesignUnit"].ToString().Trim();
                            en.PR_ConstructionUnit = dr["PR_ConstructionUnit"].ToString().Trim();
                            en.PR_SupervisionName = dr["PR_SupervisionName"].ToString().Trim();
                            en.PR_TotalArea = dr["PR_TotalArea"].ToString().Trim();
                            if (dr["PR_StartDay"].ToString().Trim() == null || dr["PR_StartDay"].ToString().Trim() == "")
                                en.PR_StartDay = null;
                            else
                            {
                                if (DateTime.TryParseExact(dr["PR_StartDay"].ToString().Trim(), format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, out message_Date))
                                {
                                    en.PR_StartDay = message_Date;
                                }
                                else
                                {
                                    en.PR_StartDay = null;
                                }
                            }
                            if (dr["PR_EndDay"].ToString().Trim() == null || dr["PR_EndDay"].ToString().Trim() == "")
                                en.PR_EndDay = null;
                            else
                            {
                                if (DateTime.TryParseExact(dr["PR_EndDay"].ToString().Trim(), format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, out message_Date))
                                {
                                    en.PR_EndDay = message_Date;
                                }
                                else
                                {
                                    en.PR_EndDay = null;
                                }
                            }
                            if (dr["PR_StorageDay"].ToString().Trim() == null || dr["PR_StorageDay"].ToString().Trim() == "")
                                en.PR_StorageDay = null;
                            else
                            {
                                if (DateTime.TryParseExact(dr["PR_StorageDay"].ToString().Trim(), format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, out message_Date))
                                {
                                    en.PR_StorageDay = message_Date;
                                }
                                else
                                {
                                    en.PR_StorageDay = null;
                                }
                            }
                            
                            en.PR_BGQX = dr["PR_BGQX"].ToString().Trim();
                            en.PR_TotalInvest = dr["PR_TotalInvest"].ToString().Trim();
                            en.PR_ARReceiver = dr["PR_ARReceiver"].ToString().Trim();
                            en.PR_RKReceiver = dr["PR_RKReceiver"].ToString().Trim();
                            if (dr["PR_TotalNum"].ToString().Trim() == null || dr["PR_TotalNum"].ToString().Trim() == "")
                                en.PR_TotalNum = null;
                            else
                                en.PR_TotalNum = int.Parse(dr["PR_TotalNum"].ToString().Trim());
                            if (dr["PR_FileNum"].ToString().Trim() == null || dr["PR_FileNum"].ToString().Trim() == "")
                                en.PR_FileNum = null;
                            else
                                en.PR_FileNum = int.Parse(dr["PR_FileNum"].ToString().Trim());
                            if (dr["PR_ImageNum"].ToString().Trim() == null || dr["PR_ImageNum"].ToString().Trim() == "")
                                en.PR_ImageNum = null;
                            else
                                en.PR_ImageNum = int.Parse(dr["PR_ImageNum"].ToString().Trim());
                            if (dr["PR_PhotoNum"].ToString().Trim() == null || dr["PR_PhotoNum"].ToString().Trim() == "")
                                en.PR_PhotoNum = null;
                            else
                                en.PR_PhotoNum = int.Parse(dr["PR_PhotoNum"].ToString().Trim());
                            if (dr["PR_VideoNum"].ToString().Trim() == null || dr["PR_VideoNum"].ToString().Trim() == "")
                                en.PR_VideoNum = null;
                            else
                                en.PR_VideoNum = int.Parse(dr["PR_VideoNum"].ToString().Trim());
                            if (dr["PR_VoiceNum"].ToString().Trim() == null || dr["PR_VoiceNum"].ToString().Trim() == "")
                                en.PR_VoiceNum = null;
                            else
                                en.PR_VoiceNum = int.Parse(dr["PR_VoiceNum"].ToString().Trim());
                            if (dr["PR_BZDay"].ToString().Trim() == null || dr["PR_BZDay"].ToString().Trim() == "")
                                en.PR_BZDay = null;
                            else
                            {
                                if (DateTime.TryParseExact(dr["PR_BZDay"].ToString().Trim(), format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, out message_Date))
                                {
                                    en.PR_BZDay = message_Date;
                                }
                                else
                                {
                                    en.PR_BZDay = null;
                                }
                            }

                            en.PR_ProAccounts = dr["PR_ProAccounts"].ToString().Trim();
                            en.PR_ProAccountsUnit = dr["PR_ProAccountsUnit"].ToString().Trim();
                            if (dr["PR_MID"].ToString().Trim() == null || dr["PR_MID"].ToString().Trim() == "")
                                en.PR_MID = null;
                            else
                                en.PR_MID = int.Parse(dr["PR_MID"].ToString().Trim());
                            en.PR_BH = dr["PR_BH"].ToString().Trim();
                            TProject_LinqDal.Add_Project(en);
                        }
                    }
                    else if (ttttt == "T_WSAJ1") 
                    {
                        string strSql = "select * from [" + ttttt+"$" + "]";
                        OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dt = RemoveEmpty(ds.Tables[0]);
                        foreach (DataRow dr in dt.Rows)
                        {
                            T_WSAJ1 en = new T_WSAJ1();
                            en.WS1_JH = dr["WS1_JH"].ToString().Trim();
                            en.WS1_TM = dr["WS1_TM"].ToString().Trim();                           
                            en.WS1_BGQX = dr["WS1_BGQX"].ToString().Trim();
                            en.WS1_YEAR = dr["WS1_YEAR"].ToString().Trim();
                            if (dr["WS1_GDRQ"].ToString().Trim() == null || dr["WS1_GDRQ"].ToString().Trim() == "")
                                en.WS1_GDRQ  = null;
                            else
                            {
                                if (DateTime.TryParseExact(dr["WS1_GDRQ"].ToString().Trim(), format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, out message_Date))
                                {
                                    en.WS1_GDRQ = message_Date;
                                }
                                else
                                {
                                    en.WS1_GDRQ = null;
                                }
                            }
                            en.WS1_BZ = dr["WS1_BZ"].ToString().Trim();
                            en.WS1_HJBH = dr["WS1_HJBH"].ToString().Trim();
                            en.WS1_PID = dr["WS1_PID"].ToString().Trim();
                            if (dr["WS1_MID"].ToString().Trim() == null || dr["WS1_MID"].ToString().Trim() == "")
                                en.WS1_MID = null;
                            else
                                en.WS1_MID =int.Parse(dr["WS1_MID"].ToString().Trim());
                            //en.WS1_V01 = dr["WS1_V01"].ToString().Trim();
                            TArchive_LinqDal.Add_WSAJ1(en);
                        }
                    }
                    else if (ttttt == "T_WSJN1")
                    {
                        string strSql = "select * from [" + ttttt + "$" + "]";
                        OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dt = RemoveEmpty(ds.Tables[0]);
                        foreach (DataRow dr in dt.Rows)
                        {
                            T_WSJN1 en = new T_WSJN1();
                            en.JN1_Name = dr["JN1_Name"].ToString().Trim();
                            if (dr["JN1_XH"].ToString().Trim() == null || dr["JN1_XH"].ToString().Trim() == "")
                                en.JN1_XH = null;
                            else
                                en.JN1_XH = int.Parse(dr["JN1_XH"].ToString().Trim());
                            en.JN1_BH = dr["JN1_BH"].ToString().Trim();
                            if (dr["JN1_YS"].ToString().Trim() == null || dr["JN1_YS"].ToString().Trim() == "")
                                en.JN1_YS = null;
                            else
                                en.JN1_YS = int.Parse(dr["JN1_YS"].ToString().Trim());
                            if (dr["JN1_RQ"].ToString().Trim() == null || dr["JN1_RQ"].ToString().Trim() == "")
                                en.JN1_RQ = null;
                            else
                            {
                                if (DateTime.TryParseExact(dr["JN1_RQ"].ToString().Trim(), format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, out message_Date))
                                {
                                    en.JN1_RQ = message_Date;
                                }
                                else
                                {
                                    en.JN1_RQ = null;
                                }
                            }
                            en.JN1_ZRZ = dr["JN1_ZRZ"].ToString().Trim();
                            en.JN1_Remark = dr["JN1_Remark"].ToString().Trim();
                            en.JN1_AJID = dr["JN1_AJID"].ToString().Trim();
                            TArchive_LinqDal.Add_WSJN1(en);
                        }
                    }
                    else if (ttttt == "T_Message")
                    {
                        string strSql = "select * from [" + ttttt + "$" + "]";
                        OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dt = RemoveEmpty(ds.Tables[0]);
                        foreach (DataRow dr in dt.Rows)
                        {
                            T_Message en = new T_Message();
                            en.Message_NO = dr["Message_NO"].ToString().Trim();
                            en.Message_WH = dr["Message_WH"].ToString().Trim();
                            en.Message_Title = dr["Message_Title"].ToString().Trim();
                            en.Message_Type = dr["Message_Type"].ToString().Trim();
                            en.Message_SendUnit = dr["Message_SendUnit"].ToString().Trim();
                            en.Message_ReceiveUnit = dr["Message_ReceiveUnit"].ToString().Trim();
                            en.Message_Sender = dr["Message_Sender"].ToString().Trim();
                            en.Message_Remark = dr["Message_Remark"].ToString().Trim();
                            en.Message_BGQX = dr["Message_BGQX"].ToString().Trim();
                            if (dr["Message_Date"].ToString().Trim() == null || dr["Message_Date"].ToString().Trim() == "")
                                en.Message_Date = null;
                            else
                            {
                                if (DateTime.TryParseExact(dr["Message_Date"].ToString().Trim(), format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, out message_Date))
                                {
                                    en.Message_Date = message_Date;
                                }
                                else
                                {
                                    en.Message_Date = null;
                                }
                            }
                            if (dr["MG_mid"].ToString().Trim() == null || dr["MG_mid"].ToString().Trim() == "")
                                en.MG_mid = 0;
                            else
                                en.MG_mid = int.Parse(dr["MG_mid"].ToString().Trim());
                            TArchive_LinqDal.Add_T_Message(en);
                        }
                    }
                }
                return 1;

            }

            catch //(Exception exc)
            {
              //  throw exc;
                return -1;//exc.ToString();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
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
        public static string Get_TEXT(string a)
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
        public static DataTable RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
            return dt;
        }
    }
}
