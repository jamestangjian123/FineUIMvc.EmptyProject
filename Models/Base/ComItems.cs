using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FineUIMvc;

namespace FineUIMvc.EmptyProject.Models
{
    public class ComItems
    {
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static ListItem[] InitComboBoxQueryArchive(int aid)
        { 
            List<ListItem> items = new List<ListItem>();
            var list = TQuery_LinqDal.Query_GridColumns(aid);
            if (list != null)
            {
                foreach (var n in list)
                {
                    ListItem item = new ListItem();
                    item.Text = n.Fields;
                    item.Value = n.ID.ToString() + ";" + n.Type.ToString() + ";" + n.Name + ";" +""+ ";" + n.Fields;
                    items.Add(item);
                }
            }
            return items.ToArray();
        }

        /// <summary>
        /// 显示字段
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        //public static ListItem[] InitComboBoxShowArchive(int aid)
        //{
        //    List<ListItem> items = new List<ListItem>();
        //    var list = TSysAr_LinqDal.Query_GridColumns(aid,  (int)EnumShenGoStatus.Effective, (int)EnumShenGoStatus.Effective, null, null, null, null);
        //    if (list != null)
        //    {
        //        foreach (var n in list)
        //        {
        //            ListItem item = new ListItem();
        //            item.Text = n.Fields;
        //            item.Value = n.ID.ToString() + ";" + n.Type.ToString() + ";" + n.Name + ";" + (n.V01 == null ? "" : n.V01) + ";" + n.Fields;
        //            items.Add(item);
        //        }
        //    }
        //    return items.ToArray();
        //}

        /// <summary>
        /// 统计字段
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        //public static ListItem[] InitComboBoxTongjiArchive(int aid)
        //{
        //    List<ListItem> items = new List<ListItem>();
        //    var list = TSysAr_LinqDal.Query_GridColumns(aid, (int)EnumShenGoStatus.Effective, null, null, null, (int)EnumShenGoStatus.Effective, null);
        //    if (list != null)
        //    {
        //        foreach (var n in list)
        //        {
        //            ListItem item = new ListItem();
        //            item.Text = n.Fields;
        //            item.Value = n.ID.ToString() + ";" + n.Type.ToString() + ";" + n.Name + ";" + (n.V01 == null ? "" : n.V01) + ";" + n.Fields;
        //            items.Add(item);
        //        }
        //    }
        //    return items.ToArray();
        //}

        /// <summary>
        /// 查询的条件项
        /// </summary>
        /// <param name="type">1. 文字， 2. 3. 数字或日期， 5. 词典</param>
        /// <returns></returns>
        public static ListItem[] InitComboBoxQueryWhere(int type)
        {
            List<ListItem> list = new List<ListItem>();
            switch(type)
            {
                case 1:
                    list.Add(new ListItem() { Text = "等于", Value = "=" });
                    list.Add(new ListItem() { Text = "包含", Value = "like" });
                    break;
                case 2:
                case 3:
                    list.Add(new ListItem() { Text = "等于", Value = "=" });
                    list.Add(new ListItem() { Text = "大于等于", Value = ">=" });
                    list.Add(new ListItem() { Text = "大于", Value = ">" });
                    list.Add(new ListItem() { Text = "小于等于", Value = "<=" });
                    list.Add(new ListItem() { Text = "小于", Value = "<" });
                    break;
                case 5:
                    list.Add(new ListItem() { Text = "等于", Value = "=" });
                    break;
                default:
                    break;
            }
            if (list.Count > 0)
                list[0].Selected = true;
            return list.ToArray();
        }

        /// <summary>
        /// 获得字段类型
        /// </summary>
        /// <returns></returns>
        public static ListItem[] InitComboBoxFieldsType()
        {
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem() { Text = "文本", Value="1"});
            list.Add(new ListItem() { Text = "数字", Value="2"});
            list.Add(new ListItem() { Text = "日期", Value="3"});
            list.Add(new ListItem() { Text = "词典", Value="5"});
            return list.ToArray();
        }

        /// <summary>
        /// 根据词典内容
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ListItem[] InitComboBoxQueryWord(string type)
        {
            List<ListItem> items = new List<ListItem>();
            var list = TSysAr_LinqDal.Query_Word(type, (int)EnumShenGoStatus.Effective);
            if (list != null)
            {
                foreach (var n in list)
                {
                    ListItem item = new ListItem();
                    item.Text = n.WD_NAME;
                    item.Value = n.WD_ID.ToString();
                    items.Add(item);
                }
            }
            if (items.Count > 0)
                items[0].Selected = true;
            return items.ToArray();
        }

        /// <summary>
        /// 词典类型
        /// </summary>
        /// <returns></returns>
        public static ListItem[] InitComboBoxQueryWordType()
        {
            List<ListItem> items = new List<ListItem>();
            var list = TSysAr_LinqDal.Query_WordType();
            if(list != null)
            {
                foreach(var n in list)
                {
                    ListItem item = new ListItem();
                    item.Text = n;
                    item.Value = n;
                    items.Add(item);
                }
            }
            if (items.Count > 0)
                items[0].Selected = true;
            return items.ToArray();
        }


        /// <summary>
        /// 当前正在借阅的来访者
        /// </summary>
        /// <returns></returns>
        //public static ListItem[] InitComboBoxBUserNow()
        //{
        //    List<ListItem> items = new List<ListItem>();
        //    int count = 0;
        //    var list = TBUser_LinqDal.Get_BUser((int)EnumShenGoStatus.Effective, 0, 1000, out count);
        //    if (list != null)
        //    {
        //        foreach (var n in list)
        //        {
        //            ListItem item = new ListItem();
        //            item.Text = n.BU_NAME;
        //            item.Value = n.BU_ID.ToString();
        //            items.Add(item);
        //        }
        //    }
        //    if (items.Count > 0)
        //        items[0].Selected = true;
        //    return items.ToArray();
        //}

        /// <summary>
        /// 角色
        /// </summary>
        /// <returns></returns>
        public static ListItem[] InitComboBoxRole()
        {
            List<ListItem> items = new List<ListItem>();
            int count = 0;

            var list = TRole_LinqDal.Query_Role(null, 0, 1000, out count);
            if (list != null)
            {
                foreach (var n in list)
                {
                    ListItem item = new ListItem();
                    item.Text = n.ROLE_NAME;
                    item.Value = n.ROLE_ID.ToString();
                    items.Add(item);
                }
            }
            //if (items.Count > 0)
            //    items[0].Selected = true;
            return items.ToArray();
        }

        /// <summary>
        /// 档案主节点
        /// </summary>
        /// <returns></returns>
        //public static ListItem[] InitComboBoxArchiveType()
        //{
        //    ///获取档案类型 
        //    List<ListItem> items = new List<ListItem>();
        //    var list = TSysAr_LinqDal.Query_SysAR(1, 1);
        //    foreach(var n in list)
        //    {
        //        if(n.AR_PID != 0)
        //        {
        //            string text = "";
        //            foreach(var nn in list)
        //            {
        //                if(n.AR_PID == nn.AR_ID)
        //                {
        //                    text = "(" + nn.AR_NAME + ")";
        //                    break;
        //                }
        //            }

        //            ListItem item = new ListItem();
        //            item.Text = n.AR_NAME + text;
        //            item.Value = n.AR_ID.ToString();
        //            items.Add(item);
        //        }
        //    }
        //    return items.ToArray();
        //}
        /// <summary>
        /// 1级部门主节点
        /// </summary>
        /// <returns></returns>
        public static ListItem[] InitComboBoxDepatmentType()
        {
            ///获取人事类型 
            List<ListItem> items = new List<ListItem>();
            var list = TUser_LinqDal.Query_ROLE(1);
            foreach (var n in list)
            {
                if (n.ROLE_RID != -1)
                {
                    //string text = "";
                    //foreach (var nn in list)
                    //{
                    //    if (n.ROLE_RID == nn.ROLE_ID)
                    //    {
                    //        text = "(" + nn.ROLE_NAME + ")";
                    //        break;
                    //    }
                    //}

                    ListItem item = new ListItem();
                    item.Text = n.ROLE_NAME;//+ text;
                    item.Value = n.ROLE_ID.ToString();
                    items.Add(item);
                }
            }
            return items.ToArray();
        }
    }


}