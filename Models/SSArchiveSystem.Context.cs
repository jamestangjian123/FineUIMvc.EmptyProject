﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FineUIMvc.EmptyProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GGArchiveSystemEntities : DbContext
    {
        public GGArchiveSystemEntities()
            : base("name=GGArchiveSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_MODEL> T_MODEL { get; set; }
        public virtual DbSet<T_ROLE> T_ROLE { get; set; }
        public virtual DbSet<T_SINGLEINFO> T_SINGLEINFO { get; set; }
        public virtual DbSet<T_QX> T_QX { get; set; }
        public virtual DbSet<T_Log> T_Log { get; set; }
        public virtual DbSet<T_USER> T_USER { get; set; }
        public virtual DbSet<V_USER> V_USER { get; set; }
        public virtual DbSet<T_DOCUMENT> T_DOCUMENT { get; set; }
        public virtual DbSet<T_ProjectInfo> T_ProjectInfo { get; set; }
        public virtual DbSet<T_TEXT> T_TEXT { get; set; }
        public virtual DbSet<T_WSJN1> T_WSJN1 { get; set; }
        public virtual DbSet<T_Query> T_Query { get; set; }
        public virtual DbSet<T_SYSWD> T_SYSWD { get; set; }
        public virtual DbSet<T_Message> T_Message { get; set; }
        public virtual DbSet<T_WSAJ1> T_WSAJ1 { get; set; }
        public virtual DbSet<V_WSAJ> V_WSAJ { get; set; }
        public virtual DbSet<V_WSJN> V_WSJN { get; set; }
        public virtual DbSet<V_XM> V_XM { get; set; }
        public virtual DbSet<V_TEXT1> V_TEXT1 { get; set; }
    }
}