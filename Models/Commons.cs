using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineUIMvc.EmptyProject.Models
{
    public class Commons
    {
        public static int iWSAJID = 3;
        public static int iZPAJID = 1;
        public static string _sWDLYMD = "利用目的";
        public static string _sWDJYYY = "借阅原因";

        /// <summary>
        /// 数据库分页数量 其实这个变量最好放在网站或项目里
        /// </summary>
        public static int iPagerRow = 20;
    }

    /// <summary>
    /// 数据库里Status
    /// </summary>
    public enum EnumShenGoStatus
    {
        /// <summary>
        /// 无效的
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// 有效的
        /// </summary>
        Effective = 1
    }

    public enum EnumShenGoFieldsType
    {
        /// <summary>
        /// 整数
        /// </summary>
        Var = 1,
        /// <summary>
        /// 字符
        /// </summary>
        Int = 2,
        /// <summary>
        /// 日期
        /// </summary>
        Datetime = 3,
        /// <summary>
        /// 词典
        /// </summary>
        Word = 5
    }
    
    /// <summary>
    /// 借阅的状态
    /// </summary>
    public enum EnumShenGoBrrStatus
    {
        /// <summary>
        ///  申请
        /// </summary>
        Sq = 1,
        /// <summary>
        ///  在浏览
        /// </summary>
        Brring = 2,
        /// <summary>
        /// 结束
        /// </summary>
        End = 3
    }

    /// <summary>
    /// 审批结果
    /// </summary>
    public enum EnumShenGoSpValue
    {
        /// <summary>
        /// 未审批
        /// </summary>
        No = 0,
        /// <summary>
        /// 审批通过
        /// </summary>
        Ok = 1,
        /// <summary>
        /// 审批不通过
        /// </summary>
        Not = 2
    }

    /// <summary>
    /// 档案归还状态
    /// </summary>
    public enum EnumShenGoBrrEndType
    {
        /// <summary>
        /// 主动归还
        /// </summary>
        Self =1,
        /// <summary>
        /// 管理员回收
        /// </summary>
        Admin = 2,
        /// <summary>
        /// 系统回收
        /// </summary>
        System = 3
    }

    public class CShenGoSystemInfo
    {
        public static string _sTitle = "档案管理系统"; //"ShengGo 综合档案管理系统";
        public static string _sName = "ShengGo ArchiveSystem";
        public static string _sVersion = "版本2.0";
        public static string _sCompany = "江苏瑞档信息技术有限公司";

    }
}
