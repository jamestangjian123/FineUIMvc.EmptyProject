using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 电子文档
    /// </summary>
    public class TDocument_LinqDal
    {
        /// <summary>
        /// 案卷或一文一件级的电子文件
        /// </summary>
        /// <param name="ajid"></param>
        /// <returns></returns>
        public static T_DOCUMENT  Get_DocumentWSAJ(int ajid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_AID == ajid).FirstOrDefault();
            }
            catch { return null; }
        }

        /// <summary>
        /// 卷内目录的电子文件
        /// </summary>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public static T_DOCUMENT Get_DocumentWSJN(int jnid)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_NID == jnid).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 公文档案的电子文件
        /// </summary>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public static T_DOCUMENT Get_DocumentMG(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_I01 == id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 添加附件，先修改案卷状态，再添加附件信息
        /// </summary>
        /// <param name="atypeid"></param>
        /// <param name="ajid"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static int Add_DocumentWSAJ(int atypeid, int ajid, T_DOCUMENT doc)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_ID == ajid);
                foreach (var n in query)
                    n.WS1_V01 = doc.DOC_FILE;
                conn.SaveChanges();

                var en = TDocument_LinqDal.Get_DocumentWSAJ(ajid);
                if (en != null)//此档案已经有附件了
                {
                    var querys = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_AID == ajid);
                    foreach (var n in querys)
                    {
                        n.DOC_FILE = doc.DOC_FILE;
                        n.DOC_NAME = doc.DOC_NAME;
                        n.DOC_PATH = doc.DOC_PATH;
                    }
                    conn.SaveChanges();
                }
                else
                {
                    conn.T_DOCUMENT.Add(doc);
                    conn.SaveChanges();
                }
                return doc.DOC_ID;

                //var querys = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_AID == ajid);
                //conn.T_DOCUMENT.RemoveRange(querys);
                //conn.SaveChanges();
                //conn.T_DOCUMENT.Add(doc);
                //conn.SaveChanges();
                //return doc.DOC_ID;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 添加附件，先修改案卷状态，再添加附件信息（卷内）
        /// </summary>
        /// <param name="atypeid"></param>
        /// <param name="ajid"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static int Add_DocumentWSJN(int atypeid, int jnid, T_DOCUMENT doc)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_ID == jnid);
                foreach (var n in query)
                    n.JN1_Scan = doc.DOC_FILE;
                conn.SaveChanges();

                var en = TDocument_LinqDal.Get_DocumentWSJN(jnid);
                if (en != null)//此档案已经有附件了
                {
                    var querys = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_ATYPE == atypeid && c.DOC_NID == jnid);
                    foreach (var n in querys)
                    {
                        n.DOC_FILE = doc.DOC_FILE;
                        n.DOC_NAME = doc.DOC_NAME;
                        n.DOC_PATH = doc.DOC_PATH;
                    }
                    conn.SaveChanges();
                }
                else
                {
                    conn.T_DOCUMENT.Add(doc);
                    conn.SaveChanges();
                }
                return doc.DOC_ID;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 添加附件，先修改案卷状态，再添加附件信息（文书）
        /// </summary>
        /// <param name="atypeid"></param>
        /// <param name="ajid"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static int Add_DocumentMG(int atypeid, int id, T_DOCUMENT doc)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_Message.Select(c => c).Where(c => c.Message_ID == id);
                foreach (var n in query)
                    n.MG_V01 = doc.DOC_FILE;
                conn.SaveChanges();

                var en = TDocument_LinqDal.Get_DocumentMG(id);
                if (en != null)//此档案已经有附件了
                {
                    var querys = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_ATYPE == atypeid && c.DOC_I01 == id);
                    foreach (var n in querys)
                    {
                        n.DOC_FILE = doc.DOC_FILE;
                        n.DOC_NAME = doc.DOC_NAME;
                        n.DOC_PATH = doc.DOC_PATH;

                    }
                    conn.SaveChanges();
                }
                else
                {
                    conn.T_DOCUMENT.Add(doc);
                    conn.SaveChanges();
                }
                return doc.DOC_ID;
            }
            catch { return -1; }
        }
    }
}
