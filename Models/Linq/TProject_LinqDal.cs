using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    public class TProject_LinqDal
    {
        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_ProjectInfo Query_Project(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_ID == id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 添加项目信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_Project(T_ProjectInfo en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var count = conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_BH == en.PR_BH).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_ProjectInfo.Add(en);
                    conn.SaveChanges();
                    return en.PR_ID;
                }
            }
            catch { return -1; }
        }
        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_Project(int id, T_ProjectInfo en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_ID == id);
                foreach (var nn in n)
                {
                    nn.PR_Name = en.PR_Name;
                    nn.PR_BH = en.PR_BH;
                    nn.PR_BuildUnit = en.PR_BuildUnit;
                    nn.PR_BuildUnitPhone = en.PR_BuildUnitPhone;
                    nn.PR_BuildUnitAddress = en.PR_BuildUnitAddress;
                    nn.PR_BuildAdress = en.PR_BuildAdress;
                    nn.PR_DesignUnit = en.PR_DesignUnit;
                    nn.PR_ConstructionUnit = en.PR_ConstructionUnit;
                    nn.PR_SupervisionName = en.PR_SupervisionName;
                    nn.PR_TotalArea = en.PR_TotalArea;
                    nn.PR_StartDay = en.PR_StartDay;
                    nn.PR_EndDay = en.PR_EndDay;
                    nn.PR_StorageDay = en.PR_StorageDay;
                    nn.PR_BGQX = en.PR_BGQX;
                    nn.PR_TotalInvest = en.PR_TotalInvest;
                    nn.PR_ARReceiver = en.PR_ARReceiver;
                    nn.PR_RKReceiver = en.PR_RKReceiver;
                    nn.PR_TotalNum = en.PR_TotalNum;
                    nn.PR_FileNum = en.PR_FileNum;
                    nn.PR_ImageNum = en.PR_ImageNum;
                    nn.PR_PhotoNum = en.PR_PhotoNum;
                    nn.PR_VideoNum = en.PR_VideoNum;
                    nn.PR_VoiceNum = en.PR_VoiceNum;
                    nn.PR_BZUnit = en.PR_BZUnit;
                    nn.PR_BZDay = en.PR_BZDay;
                    nn.PR_ProAccounts = en.PR_ProAccounts;
                    nn.PR_ProAccountsUnit = en.PR_ProAccountsUnit;
                    nn.PR_MID = en.PR_MID;
                }
                conn.SaveChanges();
                return 1;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 获取单体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_SINGLEINFO Query_SINGLE(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_SINGLEINFO.Select(c => c).Where(c => c.SG_ID == id).Single();
            }
            catch { return null; }
        }
        /// <summary>
        /// 添加单体信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Add_SINGLE(T_SINGLEINFO en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var count = conn.T_SINGLEINFO.Select(c => c).Where(c => c.SG_PID == en.SG_PID && c.SG_Name == en.SG_Name).Count();
                if (count > 0)
                    return 0;
                else
                {
                    conn.T_SINGLEINFO.Add(en);
                    conn.SaveChanges();
                    return en.SG_ID;
                }
            }
            catch { return -1; }
        }
        /// <summary>
        /// 修改单体信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static int Modify_SINGLE(int id, T_SINGLEINFO en)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var n = conn.T_SINGLEINFO.Select(c => c).Where(c => c.SG_ID == id);
                foreach (var nn in n)
                {
                    nn.SG_Name = en.SG_Name;
                    nn.SG_Xnum = en.SG_Xnum;
                    nn.SG_Type = en.SG_Type;
                    nn.SG_Area = en.SG_Area;
                    nn.SG_Form = en.SG_Form;
                    nn.SG_Bnum = en.SG_Bnum;
                    nn.SG_High = en.SG_High;
                    nn.SG_Znum = en.SG_Znum;
                    nn.SG_Deep = en.SG_Deep;
                    nn.SG_Underfloor = en.SG_Underfloor;
                    nn.SG_Overfloor = en.SG_Overfloor;
                    nn.SG_CZ = en.SG_CZ;
                    nn.SG_Length = en.SG_Length;
                    nn.SG_Vnum = en.SG_Vnum;
                    nn.SG_PD = en.SG_PD;
                    nn.SG_Norms = en.SG_Norms;
                    nn.SG_Dunit = en.SG_Dunit;
                    nn.SG_Wunit = en.SG_Wunit;
                    nn.SG_Junit = en.SG_Junit;
                    nn.SG_PID = en.SG_PID;
                }
                conn.SaveChanges();
                return 1;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 获取T_PRO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_ProjectInfo Query_PRName(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                return conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_ID == id).Single();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 删除某一个项目 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete_PRO(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_ProjectInfo.Select(c => c).Where(c => c.PR_ID == id);
                //string SGPID = query.Single().PR_ID.ToString();
                //string WS1_PID = query.Single().PR_BH;
                conn.T_ProjectInfo.RemoveRange(query);
                conn.SaveChanges();
                
                
                //var query1 = conn.T_SINGLEINFO.Select(c => c).Where(c => c.SG_PID == SGPID);
                //conn.T_SINGLEINFO.RemoveRange(query1);
                //conn.SaveChanges();
                
                //var query2 = conn.T_WSAJ1.Select(c => c).Where(c => c.WS1_PID == WS1_PID);
                //if (query2.Count() > 0) 
                //{ 
                //    string WS1JH = query2.Single().WS1_JH;
                //    conn.T_WSAJ1.RemoveRange(query2);
                //    conn.SaveChanges();
                
                //    var query3 = conn.T_WSJN1.Select(c => c).Where(c => c.JN1_AJID == WS1JH);
                //    if (query3.Count() > 0) 
                //    { 
                //        int jnid = query3.Single().JN1_ID;
                //        conn.T_WSJN1.RemoveRange(query3);
                //        conn.SaveChanges();

                //        var query4 = conn.T_DOCUMENT.Select(c => c).Where(c => c.DOC_NID == jnid);
                //        if (query2.Count() > 0) 
                //        { 
                //            int DocID = query4.Single().DOC_ID;
                //            conn.T_DOCUMENT.RemoveRange(query4);
                //            conn.SaveChanges();

                //            var query5 = conn.T_TEXT.Select(c => c).Where(c => c.TEXT_DOCID == DocID);
                //            conn.T_TEXT.RemoveRange(query5);
                //            conn.SaveChanges();
                //        }
                //    }
                //}
                return 1;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 删除某一个单体 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete_SG(int id)
        {
            try
            {
                GGArchiveSystemEntities conn = new GGArchiveSystemEntities();
                var query = conn.T_SINGLEINFO.Select(c => c).Where(c => c.SG_ID == id);
                conn.T_SINGLEINFO.RemoveRange(query);
                conn.SaveChanges();
                return 1;
            }
            catch { return -1; }
        }
    }
}
