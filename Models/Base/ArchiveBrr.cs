using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineUIMvc.EmptyProject.Models
{
    /// <summary>
    /// 借档类型
    /// </summary>
    public class ArchiveBrr
    {
        /// <summary>
        /// 档案类型ID
        /// </summary>
        public int AID { get; set; }
        /// <summary>
        /// 关键号， 文书就是 WS_DH 以此类推
        /// </summary>
        public string KEY { get; set; }
        /// <summary>
        /// 档案ID
        /// </summary>
        public string DAID { get; set; }
        /// <summary>
        /// 档案名称
        /// </summary>
        public string DATYPE { get; set; }
        /// <summary>
        /// 档案表名
        /// </summary>
        public string DATABLE { get; set; }
        /// <summary>
        /// 档案的题名
        /// </summary>
        public string TM { get; set; }
        /// <summary>
        /// 是否有卷内目录
        /// </summary>
        public string JNML { get; set; }
    }
}