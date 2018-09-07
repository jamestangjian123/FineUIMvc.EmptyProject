using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    public class VText_LinqDal
    {
        /// <summary>
        /// 全文检索
        /// </summary>
        /// <param name="content"></param>
        /// <param name="start"></param>
        /// <param name="row"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<V_TEXT1> Query_Text(string content, string content1, int start, int row, out int count)
        {
            count = 0;
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query =  conn.V_TEXT1.Select(c => c);
                if(content1.Length>0 &&content.Length>0)
                    query = conn.V_TEXT1.Select(c => c).Where(c => c.TEXT_CONTENT.Contains(content) && c.TEXT_CONTENT.Contains(content1));      
                else 
                    query = conn.V_TEXT1.Select(c => c).Where(c => c.TEXT_CONTENT.Contains(content));
                count = query.Count();

                return query.OrderByDescending(c => c.DOC_ID).Skip(start).Take(row).ToList();
            }
            catch { return null; }
        }
        /// <summary>
        /// T_text中doc_id
        /// </summary>
        /// <param name="jnid"></param>
        /// <returns></returns>
        public static T_TEXT Get_TEXT(int doc_id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_TEXT.Select(c => c).Where(c => c.TEXT_DOCID == doc_id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        ///保存text(卷内)
        /// </summary>
        public static int AddTEXT(int jnid,string content)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int doc_id = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_NID == jnid).Single().DOC_ID;//doc_id

                var en = VText_LinqDal.Get_TEXT(doc_id);
                if (en == null)
                {
                    T_TEXT en1 = new T_TEXT();
                    en1.TEXT_DOCID = doc_id;
                    en1.TEXT_CONTENT = content;
                    en1.TEXT_PAGENUM = 1;
                    en1.TEXT_STATUS = 1;
                    conn.T_TEXT.Add(en1); ;
                    conn.SaveChanges();
                }
                else
                {
                    var en2 = conn.T_TEXT.Select(c => c).Where(c => c.TEXT_DOCID == doc_id);
                    foreach (var n in en2)
                    {
                        n.TEXT_CONTENT = content;
                    }
                    conn.SaveChanges();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        ///保存text(文书)
        /// </summary>
        public static int AddTEXT2(int id, string content)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int doc_id = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_I01 == id).Single().DOC_ID;//doc_id

                var en = VText_LinqDal.Get_TEXT(doc_id);
                if (en == null)
                {
                    T_TEXT en1 = new T_TEXT();
                    en1.TEXT_DOCID = doc_id;
                    en1.TEXT_CONTENT = content;
                    en1.TEXT_PAGENUM = 1;
                    en1.TEXT_STATUS = 1;
                    conn.T_TEXT.Add(en1); ;
                    conn.SaveChanges();
                }
                else
                {
                    var en2 = conn.T_TEXT.Select(c => c).Where(c => c.TEXT_DOCID == doc_id);
                    foreach (var n in en2)
                    {
                        n.TEXT_CONTENT = content;
                    }
                    conn.SaveChanges();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        ///保存text(案卷)
        /// </summary>
        public static int AddTEXT1(int wsid, string content)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                int doc_id = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_AID == wsid).Single().DOC_ID;//doc_id

                var en = VText_LinqDal.Get_TEXT(doc_id);
                if (en == null)
                {
                    T_TEXT en1 = new T_TEXT();
                    en1.TEXT_DOCID = doc_id;
                    en1.TEXT_CONTENT = content;
                    en1.TEXT_PAGENUM = 1;
                    en1.TEXT_STATUS = 1;
                    conn.T_TEXT.Add(en1); ;
                    conn.SaveChanges();
                }
                else
                {
                    var en2 = conn.T_TEXT.Select(c => c).Where(c => c.TEXT_DOCID == doc_id);
                    foreach (var n in en2)
                    {
                        n.TEXT_CONTENT = content;
                    }
                    conn.SaveChanges();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
