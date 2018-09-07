using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FineUIMvc;
using FineUIMvc.EmptyProject.Models;

namespace FineUIMvc.EmptyProject.Models
{
    public class GridColumns
    {
        /// <summary>
        /// 建立档案列
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="bView">是否可看</param>
        /// <param name="bEdit">是否可编辑</param>
        /// <returns></returns>
        //public static GridColumn[] InitGridColumns(int aid, bool bView, bool bEdit)
        //{
        //    List<GridColumn> columns = new List<GridColumn>();
        //    var list = TSysAr_LinqDal.Query_GridColumns(aid, (int)EnumShenGoStatus.Effective, (int)EnumShenGoStatus.Effective, null, null, null, null);

        //    ///columns.Add(new RowNumberField() { EnablePagingNumber = true, HeaderText = "序号", Width = 50, HeaderTextAlign= TextAlign.Center, TextAlign = TextAlign.Center } );
        //    foreach (var n in list)
        //    {
        //        RenderField field = null;
        //        field = new RenderField();
        //        field.HeaderText = n.Fields;
        //        field.DataField = n.Name;
        //        ////如有内容就是词典
        //        ///field.HeaderToolTip = n.V01;
        //        EnumShenGoFieldsType fieldstype = (EnumShenGoFieldsType)n.Type;
        //        switch(fieldstype)
        //        {
        //            case EnumShenGoFieldsType.Int:
        //                if (n.Name == "WS_JD")
        //                {
        //                    field.ColumnID = n.Name;
        //                    field.FieldType = FieldType.String;
        //                    field.RendererFunction = "renderJD";  
        //                }
        //                else
        //                    field.FieldType = FieldType.Int;
        //                break;
        //            case EnumShenGoFieldsType.Var:
        //                field.FieldType = FieldType.String;
        //                break;
        //            case EnumShenGoFieldsType.Word:
        //                field.FieldType = FieldType.String;
        //                field.ColumnID = n.V01;
        //                field.RendererFunction = "renderWord";
        //                field.HeaderToolTip = n.V01;
        //                break;
        //            case EnumShenGoFieldsType.Datetime:
        //                field.FieldType = FieldType.Date;
        //                field.RendererArgument = "yyyy-MM-dd";
        //                break;
        //            default:
        //                field.FieldType = FieldType.String;
        //                break;
        //        }
                
        //        ///field.RendererFunction = "renderGender";
        //        field.Width = 120;
        //        field.EnableColumnEdit = false;
        //        field.EnableFilter = false;
        //        field.EnableHeaderMenu = false;
        //        columns.Add(field);
        //    }

        //    //field = new RenderField();
        //    //field.HeaderText = "入学年份";
        //    //field.DataField = "EntranceYear";
        //    //field.FieldType = FieldType.Int;
        //    //field.Width = 100;
        //    //columns.Add(field);

        //    //RenderCheckField checkfield = new RenderCheckField();
        //    //checkfield.HeaderText = "是否在校";
        //    //checkfield.DataField = "AtSchool";
        //    //checkfield.RenderAsStaticField = true;
        //    //checkfield.Width = 100;
        //    //columns.Add(checkfield);

        //    //field = new RenderField();
        //    //field.HeaderText = "所学专业";
        //    //field.DataField = "Major";
        //    //field.RendererFunction = "renderMajor";
        //    //field.ExpandUnusedSpace = true;
        //    //columns.Add(field);

        //    //field = new RenderField();
        //    //field.HeaderText = "分组";
        //    //field.DataField = "Group";
        //    //field.RendererFunction = "renderGroup";
        //    //field.Width = 80;
        //    //columns.Add(field);

        //    var field2 = new RenderField();
        //    field2.HeaderText = "档案类型";
        //    field2.DataField = "ARCHIVETYPE";
        //    field2.FieldType = FieldType.String;
        //    field2.Hidden = true;
        //    field2.Width = 5;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    columns.Add(field2);
        //    field2 = new RenderField();
        //    field2.HeaderText = "档案类型ID";
        //    field2.DataField = "WS_AID";
        //    field2.FieldType = FieldType.String;
        //    field2.Hidden = true;
        //    field2.Width = 5;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    columns.Add(field2);
        //    field2 = new RenderField();
        //    field2.HeaderText = "归档部门";
        //    field2.DataField = "GDBM";
        //    field2.FieldType = FieldType.String;
        //    field2.Hidden = true;
        //    field2.Width = 5;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    columns.Add(field2);
        //    field2 = new RenderField();
        //    field2.HeaderText = "密级";
        //    field2.DataField = "MJ";
        //    field2.FieldType = FieldType.String;
        //    field2.Hidden = true;
        //    field2.Width = 5;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    columns.Add(field2);
        //    field2 = new RenderField();
        //    field2.HeaderText = "归档部门ID";
        //    field2.DataField = "WS_I01";
        //    field2.FieldType = FieldType.String;
        //    field2.Hidden = true;
        //    field2.Width = 5;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    columns.Add(field2);

        //    field2 = new RenderField();
        //    field2.HeaderText = "附件";
        //    field2.DataField = "WS_V10";
        //    field2.RendererFunction = "renderFile";
        //    field2.Width = 50;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    if (bView != true)
        //        field2.Hidden = true;
        //    columns.Insert(0, field2);

        //    field2 = new RenderField();
        //    field2.HeaderText = "编辑";
        //    field2.DataField = "WS_ID";
        //    field2.RendererFunction = "renderEdit";
        //    field2.Width = 50;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    if (bEdit != true)
        //        field2.Hidden = true;
        //    columns.Add(field2);

        //    //field2 = new RenderField();
        //    //field2.HeaderText = "上传";
        //    //field2.DataField = "WS_ID";
        //    //field2.RendererFunction = "renderUpload";
        //    //field2.Width = 50;
        //    //field2.EnableColumnEdit = false;
        //    //field2.EnableFilter = false;
        //    //field2.EnableHeaderMenu = false;
        //    //if (bEdit != true)
        //    //    field2.Hidden = true;
        //    //columns.Add(field2);

        //    field2 = new RenderField();
        //    field2.HeaderText = "删除";
        //    field2.DataField = "WS_ID";
        //    field2.RendererFunction = "renderDel";
        //    field2.Width = 50;
        //    field2.EnableColumnEdit = false;
        //    field2.EnableFilter = false;
        //    field2.EnableHeaderMenu = false;
        //    if (bEdit != true)
        //        field2.Hidden = true;
        //    columns.Add(field2);

        //    return columns.ToArray();
        //}

        /// <summary>
        /// 建立词典列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitWDColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "词典类型";
            field.DataField = "WD_TYPE";
            field.FieldType = FieldType.String;
            field.Width = 150;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "词典内容";
            field.DataField = "WD_NAME";
            field.FieldType = FieldType.String;
            field.Width = 150;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "排序";
            field.DataField = "WD_ORDER";
            field.FieldType = FieldType.String;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "是否启用";
            field.DataField = "WD_STATUS";
            field.RendererFunction = "renderStatus";
            field.ExpandUnusedSpace = true;
            field.Width = 80;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "WD_ID";
            field.RendererFunction = "renderEdit";
            field.ExpandUnusedSpace = true;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            //field.Width = 80;
            columns.Add(field);


            return columns.ToArray();
        }

        /// <summary>
        /// 建立电子审批列
        /// </summary>
        /// <param name="showEndType">是否显示归还方式</param>
        /// <returns></returns>
        public static GridColumn[] InitBrrDzSpColumns(bool showEndType)
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "浏览";
            field.DataField = "BE_V03";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderFile";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "状态";
            field.DataField = "BE_STATUS";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderStatus";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "一级审批";
            field.DataField = "BE_SPVALUE";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderSPValue";
            field.ExpandUnusedSpace = true;
            field.Width = 60;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "二级审批";
            field.DataField = "BE_LDSTATUS";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderSPValue";
            field.ExpandUnusedSpace = true;
            field.Width = 60;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            if (showEndType)
            {
                field = new RenderField();
                field.HeaderText = "归还方式";
                field.DataField = "BE_ENDTYPE";
                field.FieldType = FieldType.String;
                field.RendererFunction = "renderEndType";
                field.ExpandUnusedSpace = true;
                field.Width = 100;
                field.EnableColumnEdit = false;
                field.EnableFilter = false;
                field.EnableHeaderMenu = false;
                columns.Add(field);
            }

            field = new RenderField();
            field.HeaderText = "申请人";
            field.DataField = "BE_NAME";
            field.FieldType = FieldType.String;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档案类型";
            field.DataField = "BE_TYPE";
            field.FieldType = FieldType.String;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档号";
            field.DataField = "BE_ARCHIVEKEY";
            field.FieldType = FieldType.String;
            //field.RendererFunction = "renderArchive";
            //field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "BE_V02";
            field.FieldType = FieldType.String;
            //field.RendererFunction = "renderArchive";
            //field.ExpandUnusedSpace = true;
            field.Width = 150;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "申请时间";
            field.DataField = "BE_DATE";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.Width = 150;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.ColumnID = "利用目的";
            field.HeaderText = "利用目的";
            field.DataField = "BE_V01";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderWord";
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "借阅时间";
            field.DataField = "BE_HOPEDATE";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.Width = 150;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.DataField = "BE_REMARK";
            field.FieldType = FieldType.String;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            return columns.ToArray();
        }

        /// <summary>
        /// 建立统计列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitTongJiColumns(List<string> listHeader, List<T_SYSWD> listWDF1, List<T_SYSWD> listWDF2)
        {
            List<GridColumn> columns = new List<GridColumn>();
            var field = new RenderField();
            //field.DataField = "F2";
            field.FieldType = FieldType.String;
            field.HeaderText = listHeader[0];
            field.Width = 100;
            columns.Add(field);

            
            return columns.ToArray();
        }


        /// <summary>
        /// 访客登记
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitVisitorColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "姓名";
            field.DataField = "BU_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "身份证号";
            field.DataField = "BU_CARD";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 150;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "联系电话";
            field.DataField = "BU_PHONE";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "地址";
            field.DataField = "BU_ADDR";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 150;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "单位";
            field.DataField = "BU_DW";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "借阅原因";
            field.DataField = "BU_JYYY";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "登记时间";
            field.DataField = "BU_DATE";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "登记人";
            field.DataField = "BU_USERNAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.DataField = "BU_REMARK";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            //field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "结束借阅";
            field.DataField = "BU_ID";
            field.RendererFunction = "renderEnd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            return columns.ToArray();
        }

        /// <summary>
        /// 查看授权来访者访问的档案
        /// </summary>
        /// <param name="bShowInfo">是否显示详细按钮</param>
        /// <returns></returns>
        public static GridColumn[] InitVisitorArchiveColumns(bool bShowInfo)
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "档号";
            field.DataField = "BR_ARCHIVEKEY";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档案类型";
            field.DataField = "BR_TYPE";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "BR_V01";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 200;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "浏览";
            field.DataField = "BR_V02";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            columns.Insert(0, field);

            return columns.ToArray();
        }

        /// <summary>
        /// 档案鉴定
        /// </summary>
        /// <param name="bJD">是否有销毁记录的列</param>
        /// <returns></returns>
        public static GridColumn[] InitWSJDColumns(bool bXH)
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "档案类型";
            field.DataField = "ARCHIVETYPE";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档号";
            field.DataField = "WS_DH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "年度";
            field.DataField = "WS_ND";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "保管期限";
            field.DataField = "BGQX";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "保存到期年";
            field.DataField = "ENDYEAR";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            if (bXH)
            {
                field = new RenderField();
                field.HeaderText = "销毁登记人";
                field.DataField = "WS_V01";
                field.FieldType = FieldType.String;
                field.ExpandUnusedSpace = true;
                field.Width = 100;
                columns.Add(field);

                field = new RenderField();
                field.HeaderText = "登记时间";
                field.DataField = "WS_D01";
                field.FieldType = FieldType.Date;
                field.RendererArgument = "yyyy-MM-dd";
                field.ExpandUnusedSpace = true;
                field.Width = 100;
                columns.Add(field);
            }

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "WS_TM";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 200;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "密级";
            field.DataField = "MJ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            return columns.ToArray();
        }

        /// <summary>
        /// 档案密级
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitWSMJColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "档案类型";
            field.DataField = "ARCHIVETYPE";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档号";
            field.DataField = "WS_DH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "年度";
            field.DataField = "WS_ND";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "保管期限";
            field.DataField = "BGQX";
            field.RendererFunction = "renderWord";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "WS_TM";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 200;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "密级";
            field.DataField = "MJ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "详细信息";
            field.DataField = "WS_ID";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "浏览";
            field.DataField = "WS_V10";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            columns.Insert(0, field);

            return columns.ToArray();
        }

        /// <summary>
        /// T_AR 列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitSyFieldsColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "字段名";
            field.DataField = "Fields";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "类型";
            field.DataField = "Type"; 
            field.RendererFunction = "renderWord";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "对应词典";
            field.DataField = "V01";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "排序";
            field.DataField = "OrderBy";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "显示";
            field.DataField = "Show";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderStatus";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "查询";
            field.DataField = "Query";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderStatus";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "著录";
            field.DataField = "I02";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderStatus";
            field.ExpandUnusedSpace = true;
            field.Width = 200;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "统计";
            field.DataField = "I03";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderStatus";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "系统字段";
            field.DataField = "I04";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderStatus";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "ID";
            field.FieldType = FieldType.String;
            field.RendererFunction = "renderEdit";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            return columns.ToArray();
        }

        /// <summary>
        /// 收藏夹里的档案 列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitMyDocumentColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "MDOC_AID";
            //field.FieldType = FieldType.String;
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档案类型";
            field.DataField = "MDOC_ATYPE";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档号";
            field.DataField = "MDOC_DH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "MDOC_TM";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 300;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "收藏日期";
            field.DataField = "MDOC_DATE";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "详细信息";
            field.DataField = "MDOC_ARID";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);
            return columns.ToArray();
        }

        /// <summary>
        /// 角色列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitRoleColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "角色名称";
            field.DataField = "ROLE_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "是否启用";
            field.DataField = "ROLE_STATUS";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 80;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "角色说明";
            field.DataField = "ROLE_REMARK";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width =80 ;//;200
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "本部门浏览";
            field.DataField = "ROLE_VIEWPIC";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 80;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            field.Hidden = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "本部门编辑";
            field.DataField = "ROLE_MODIFY";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 80;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            field.Hidden = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "可打印的档案节点";
            field.DataField = "ROLE_V03";
            field.RendererFunction = "renderBM";
            field.ExpandUnusedSpace = true;
            field.Width = 200;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "可浏览的档案节点";
            field.DataField = "ROLE_V04";
            field.RendererFunction = "renderBM";
            field.ExpandUnusedSpace = true;
            field.Width = 200;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "可编辑的档案节点";
            field.DataField = "ROLE_V05";
            field.RendererFunction = "renderBM";
            field.ExpandUnusedSpace = true;
            field.Width = 200;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "角色权限";
            field.DataField = "ROLE_ID";
            field.RendererFunction = "renderQX";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "ROLE_ID";
            field.RendererFunction = "renderEdit";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);
            return columns.ToArray();
        }

        /// <summary>
        /// 用户列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitUserColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "姓名";
            field.DataField = "U_REALNAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "登录名";
            field.DataField = "U_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "角色";
            field.DataField = "ROLE_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            ///原来的职位是ID绑定，现在先简单化
            field = new RenderField();
            field.HeaderText = "部门";
            field.DataField = "U_V01";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "职位";
            field.DataField = "U_ZW";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "电话";
            field.DataField = "U_PHONE";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "邮件";
            field.DataField = "U_EMAIL";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.DataField = "U_REMARK";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "是否绑定IP";
            field.DataField = "U_ISIP";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            field.Hidden = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "IP";
            field.DataField = "U_IP";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            field.Hidden = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "是否启用";
            field.DataField = "U_STATUS";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "修改信息";
            field.DataField = "U_ID";
            field.RendererFunction = "renderModify";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            return columns.ToArray();
        }

        /// <summary>
        /// 日志列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitLogColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "日志类型";
            field.DataField = "Log_Type";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "操作人";
            field.DataField = "Log_UName";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "操作时间";
            field.DataField = "Log_Date";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd HH:mm:ss";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            ///原来的职位是ID绑定，现在先简单化
            field = new RenderField();
            field.HeaderText = "操作内容";
            field.DataField = "Log_Content";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 400;
            columns.Add(field);
            
            return columns.ToArray();
        }

        /// <summary>
        /// 全文检索列
        /// </summary>
        /// <returns></returns>
        //public static GridColumn[] InitQWJSColumns()
        //{
        //    List<GridColumn> columns = new List<GridColumn>();

        //    var field = new RenderField();
        //    //field.HeaderText = "档号";
        //    //field.DataField = "WS1_JH";
        //    //field.FieldType = FieldType.String;
        //    //field.ExpandUnusedSpace = true;
        //    //field.Width = 100;
        //    //field.EnableColumnEdit = false;
        //    //field.EnableFilter = false;
        //    //field.EnableHeaderMenu = false;

        //    field.HeaderText = "案卷档号";
        //    field.DataField = "WS1_JH";
        //    field.FieldType = FieldType.String;
        //    field.ExpandUnusedSpace = true;
        //    field.Width = 100;
        //    field.EnableColumnEdit = false;
        //    field.EnableFilter = false;
        //    field.EnableHeaderMenu = false;
        //    columns.Add(field);

        //    field.HeaderText = "案卷题名";
        //    field.DataField = "WS1_TM";
        //    field.FieldType = FieldType.String;
        //    field.ExpandUnusedSpace = true;
        //    field.Width = 100;
        //    field.EnableColumnEdit = false;
        //    field.EnableFilter = false;
        //    field.EnableHeaderMenu = false;
        //    columns.Add(field);

        //    field = new RenderField();
        //    field.HeaderText = "卷内题名";
        //    field.DataField = "JN1_Name";
        //    field.FieldType = FieldType.String;
        //    field.ExpandUnusedSpace = true;
        //    field.Width = 100;
        //    field.EnableColumnEdit = false;
        //    field.EnableFilter = false;
        //    field.EnableHeaderMenu = false;
        //    columns.Add(field);

        //    field = new RenderField();
        //    field.HeaderText = "所属档案";
        //    field.DataField = "MD_NAME";
        //    field.FieldType = FieldType.String;
        //    field.ExpandUnusedSpace = true;
        //    field.Width = 100;
        //    field.EnableColumnEdit = false;
        //    field.EnableFilter = false;
        //    field.EnableHeaderMenu = false;
        //    columns.Add(field);

        //    field = new RenderField();
        //    field.HeaderText = "附件";
        //    field.DataField = "JN1_Scan";
        //    field.RendererFunction = "renderFile";
        //    field.Width = 50;
        //    field.EnableColumnEdit = false;
        //    field.EnableFilter = false;
        //    field.EnableHeaderMenu = false;
        //    columns.Insert(0, field);

        //    return columns.ToArray();
        //}
        /// <summary>
        /// PDF文字检索列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitQWJSColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();

            field.HeaderText = "档号(文号)";
            field.DataField = "DOC_V01";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "DOC_V02";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "所属档案";
            field.DataField = "MD_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "DOC_NAME";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            return columns.ToArray();
        }
        /// <summary>
        /// 项目检索列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitXMJSColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();

            field.HeaderText = "项目编号";
            field.DataField = "PR_BH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "项目名称";
            field.DataField = "PR_Name";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "建设地址";
            field.DataField = "PR_BuildAdress";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编制日期";
            field.DataField = "PR_BZDay";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "所属档案";
            field.DataField = "MD_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "JN1_Scan";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            field = new RenderField();
            field.HeaderText = "所属档案ID";
            field.DataField = "PR_MID";
            field.FieldType = FieldType.String;
            field.Hidden = true;
            field.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);
            return columns.ToArray();
        }
        /// <summary>
        /// 案卷检索列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitAJJSColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();

            field.HeaderText = "档号";
            field.DataField = "WS1_JH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "案卷题名";
            field.DataField = "WS1_TM";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "保管期限";
            field.DataField = "WS1_BGQX";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "年度";
            field.DataField = "WS1_YEAR";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "所属档案";
            field.DataField = "MD_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "JN1_Scan";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            var field2 = new RenderField();
            field2.HeaderText = "MID";
            field2.DataField = "WS1_MID";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            field2 = new RenderField();
            field2.HeaderText = "看";
            field2.DataField = "WS1_V01";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            return columns.ToArray();
        }
        /// <summary>
        /// 卷内检索列
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitJNJSColumns(bool view)
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();

            field.HeaderText = "卷内编号";
            field.DataField = "JN1_BH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "卷内题名";
            field.DataField = "JN1_Name";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "日期";
            field.DataField = "JN1_RQ";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "责任者";
            field.DataField = "JN1_ZRZ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "所属档案";
            field.DataField = "MD_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "JN1_Scan";
            field.RendererFunction = "renderFile";
            if (view != true)
                field.Hidden = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            return columns.ToArray();
        }

        public static GridColumn[] InitMGJSColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();
            var field = new RenderField();
            field.HeaderText = "序号";
            field.DataField = "Message_NO";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "文号";
            field.DataField = "Message_WH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "标题";
            field.DataField = "Message_Title";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "保管期限";
            field.DataField = "Message_BGQX";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "日期";
            field.DataField = "Message_Date";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "类型";
            field.DataField = "Message_Type";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "发送单位";
            field.DataField = "Message_SendUnit";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "接收单位";
            field.DataField = "Message_ReceiveUnit";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "发送人";
            field.DataField = "Message_Sender";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.DataField = "Message_Remark";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "MG_V01";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            var field2 = new RenderField();
            field2.HeaderText = "公文ID";
            field2.DataField = "Message_ID";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            field2 = new RenderField();
            field2.HeaderText = "MID";
            field2.DataField = "MG_mid";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);
            return columns.ToArray();
    }
        /// <summary>
        /// 从案卷点到卷内目录
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitJNMLColumns(bool bView, bool bEdit)
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "序号";
            field.DataField = "JN_XH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "文号";
            field.DataField = "JN_WH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "责任者";
            field.DataField = "JN_ZRZ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "JN_TM";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "日期";
            field.DataField = "JN_DATE";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "页次";
            field.DataField = "JN_YH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.FieldType = FieldType.String;
            //field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "档案类型";
            field.DataField = "ARCHIVETYPE";
            field.FieldType = FieldType.String;
            field.Hidden = true;
            field.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);
            field = new RenderField();
            field.HeaderText = "档案类型ID";
            field.DataField = "WS_AID";
            field.FieldType = FieldType.String;
            field.Hidden = true;
            field.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);
            field = new RenderField();
            field.HeaderText = "归档部门";
            field.DataField = "GDBM";
            field.FieldType = FieldType.String;
            field.Hidden = true;
            field.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);
            field = new RenderField();
            field.HeaderText = "密级";
            field.DataField = "MJ";
            field.FieldType = FieldType.String;
            field.Hidden = true;
            field.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);
            field = new RenderField();
            field.HeaderText = "归档部门ID";
            field.DataField = "WS_I01";
            field.FieldType = FieldType.String;
            field.Hidden = true;
            field.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "JN_V10";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            if (bView != true)
                field.Hidden = true;
            columns.Insert(0, field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "JN_ID";
            field.RendererFunction = "renderEdit";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            if (bEdit != true)
                field.Hidden = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "上传";
            field.DataField = "JN_ID";
            field.RendererFunction = "renderUpload";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            if (bEdit != true)
                field.Hidden = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "删除";
            field.DataField = "JN_ID";
            field.RendererFunction = "renderDel";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            if (bEdit != true)
                field.Hidden = true;
            columns.Add(field);

            return columns.ToArray();
        }

        /// <summary>
        /// 直接查询到卷内目录
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitJNMLQueryColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "档号";
            field.DataField = "WS_DH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "年度";
            field.DataField = "WS_ND";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "序号";
            field.DataField = "JN_XH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "文号";
            field.DataField = "JN_WH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "责任者";
            field.DataField = "JN_ZRZ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "JN_TM";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 300;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "日期";
            field.DataField = "JN_DATE";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "密级";
            field.DataField = "MJ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "案卷信息";
            field.DataField = "WS_ID";
            field.RendererFunction = "renderInfo";
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "JN_V10";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            var field2 = new RenderField();
            field2.HeaderText = "密级";
            field2.DataField = "MJ";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            ///隐藏列
            field2 = new RenderField();
            field2.HeaderText = "案卷ID";
            field2.DataField = "WS_ID";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            return columns.ToArray();
        }
        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitPROQueryColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "项目名称";
            field.DataField = "PR_Name";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "项目编号";
            field.DataField = "PR_BH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "建设地址";
            field.DataField = "PR_BuildAdress";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "建设单位";
            field.DataField = "PR_BuildUnit";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "设计单位";
            field.DataField = "PR_DesignUnit";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "总面积";
            field.DataField = "PR_TotalArea";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "总投资";
            field.DataField = "PR_TotalInvest";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编制日期";
            field.DataField = "PR_BZDay";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "单体信息";
            field.DataField = "PR_ID";
            field.RendererFunction = "renderUpload";
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "PR_ID";
            field.RendererFunction = "renderEdit";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "删除";
            field.DataField = "PR_ID";
            field.RendererFunction = "renderDel";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "PR_ID";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            var field2 = new RenderField();
            field2.HeaderText = "项目ID";
            field2.DataField = "PR_ID";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            field2 = new RenderField();
            field2.HeaderText = "项目编号";
            field2.DataField = "PR_BH";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            return columns.ToArray();
        }

        /// <summary>
        /// 查询单体信息
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitSingleQueryColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "单体名称";
            field.DataField = "SG_Name";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "许可证号";
            field.DataField = "SG_Xnum";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "结构类型";
            field.DataField = "SG_Type";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "面积";
            field.DataField = "SG_Area";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "形式";
            field.DataField = "SG_Form";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "幢数";
            field.DataField = "SG_Bnum";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "SG_ID";
            field.RendererFunction = "renderEdit";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "删除";
            field.DataField = "SG_ID";
            field.RendererFunction = "renderDel";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            ///隐藏列
            field = new RenderField();
            field.HeaderText = "单体ID";
            field.DataField = "SG_ID";
            field.FieldType = FieldType.String;
            field.Hidden = true;
            field.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);



            return columns.ToArray();
        }
        /// <summary>
        /// 查询公文信息
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitMGQueryColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();
            var field = new RenderField();
            field.HeaderText = "序号";
            field.DataField = "Message_NO";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "文号";
            field.DataField = "Message_WH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "标题";
            field.DataField = "Message_Title";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "保管期限";
            field.DataField = "Message_BGQX";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "日期";
            field.DataField = "Message_Date";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "类型";
            field.DataField = "Message_Type";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "发送单位";
            field.DataField = "Message_SendUnit";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "接收单位";
            field.DataField = "Message_ReceiveUnit";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "发送人";
            field.DataField = "Message_Sender";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.DataField = "Message_Remark";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "Message_ID";
            field.RendererFunction = "renderEdit";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "上传";
            field.DataField = "Message_ID";
            field.RendererFunction = "renderUpload";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "删除";
            field.DataField = "Message_ID";
            field.RendererFunction = "renderDel";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "MG_V01";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            var field2 = new RenderField();
            field2.HeaderText = "公文ID";
            field2.DataField = "Message_ID";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            return columns.ToArray();
        }
        /// <summary>
        /// 查询案卷1信息
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitAJQueryColumns()
        {
            
            List<GridColumn> columns = new List<GridColumn>();
            var field = new RenderField();
            field.HeaderText = "档号";
            field.DataField = "WS1_JH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "案卷题名";
            field.DataField = "WS1_TM";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "年度";
            field.DataField = "WS1_YEAR";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "保管期限";
            field.DataField = "WS1_BGQX";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "归档日期";
            field.DataField = "WS1_GDRQ";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "货架编号";
            field.DataField = "WS1_HJBH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.DataField = "WS1_BZ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            //field = new RenderField();
            //field.HeaderText = "盒号";
            //field.DataField = "WS1_HH";
            //field.FieldType = FieldType.String;
            //field.ExpandUnusedSpace = true;
            //field.Width = 100;
            //field.EnableColumnEdit = false;
            //field.EnableFilter = false;
            //field.EnableHeaderMenu = false;
            //columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "WS1_ID";
            field.RendererFunction = "renderEdit";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "上传";
            field.DataField = "WS1_ID";
            field.RendererFunction = "renderUpload";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "删除";
            field.DataField = "WS1_ID";
            field.RendererFunction = "renderDel";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "WS1_V01";
            field.RendererFunction = "renderFile";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            var field2 = new RenderField();
            field2.HeaderText = "案卷ID";
            field2.DataField = "WS1_ID";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            field2 = new RenderField();
            field2.HeaderText = "是否有卷内";
            field2.DataField = "WS1_V01";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            return columns.ToArray();
        }
        /// <summary>
        /// 查询卷内1信息
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitJNQueryColumns(bool view)
        {
            List<GridColumn> columns = new List<GridColumn>();
            var field = new RenderField();
            field.HeaderText = "序号";
            field.DataField = "JN1_XH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "卷内编号";
            field.DataField = "JN1_BH";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "题名";
            field.DataField = "JN1_Name";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "页数";
            field.DataField = "JN1_YS";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            //field = new RenderField();
            //field.HeaderText = "页号起";
            //field.DataField = "JN1_Start";
            //field.FieldType = FieldType.String;
            //field.ExpandUnusedSpace = true;
            //field.Width = 100;
            //field.EnableColumnEdit = false;
            //field.EnableFilter = false;
            //field.EnableHeaderMenu = false;
            //columns.Add(field);

            //field = new RenderField();
            //field.HeaderText = "页号止";
            //field.DataField = "JN1_End";
            //field.FieldType = FieldType.String;
            //field.ExpandUnusedSpace = true;
            //field.Width = 100;
            //field.EnableColumnEdit = false;
            //field.EnableFilter = false;
            //field.EnableHeaderMenu = false;
            //columns.Add(field);

            //field = new RenderField();
            //field.HeaderText = "图幅";
            //field.DataField = "JN1_Type";
            //field.FieldType = FieldType.String;
            //field.ExpandUnusedSpace = true;
            //field.Width = 100;
            //field.EnableColumnEdit = false;
            //field.EnableFilter = false;
            //field.EnableHeaderMenu = false;
            //columns.Add(field);

            field = new RenderField();
            field.HeaderText = "责任者";
            field.DataField = "JN1_ZRZ";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "日期";
            field.DataField = "JN1_RQ";
            field.FieldType = FieldType.Date;
            field.RendererArgument = "yyyy-MM-dd";
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "备注";
            field.DataField = "JN1_Remark";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "JN1_ID";
            field.RendererFunction = "renderEdit";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "上传";
            field.DataField = "JN1_ID";
            field.RendererFunction = "renderUpload";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "删除";
            field.DataField = "JN1_ID";
            field.RendererFunction = "renderDel";
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "附件";
            field.DataField = "JN1_Scan";
            field.RendererFunction = "renderFile";
            if(view!=true)
                field.Hidden = true;
            field.Width = 50;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Insert(0, field);

            ///隐藏列
            var field2 = new RenderField();
            field2.HeaderText = "卷内ID";
            field2.DataField = "JN1_ID";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);

            field2 = new RenderField();
            field2.HeaderText = "有无卷内目录";
            field2.DataField = "WS1_V01";
            field2.FieldType = FieldType.String;
            field2.Hidden = true;
            field2.Width = 5;
            field.EnableColumnEdit = false;
            field.EnableFilter = false;
            field.EnableHeaderMenu = false;
            columns.Add(field2);


            return columns.ToArray();
        }
        /// <summary>
        /// 库房信息
        /// </summary>
        /// <returns></returns>
        public static GridColumn[] InitKFColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            var field = new RenderField();
            field.HeaderText = "库房名称";
            field.DataField = "KF_NAME";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "库房说明";
            field.DataField = "KF_INFO";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 150;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "位置";
            field.DataField = "KF_V10";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 150;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "楼层";
            field.DataField = "KF_V09";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "高度";
            field.DataField = "KF_GD";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "面积";
            field.DataField = "KF_V08";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 50;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "货架数";
            field.DataField = "KF_HJS";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 80;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "格架数";
            field.DataField = "KF_GJS";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 80;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "可存放档案";
            field.DataField = "KF_ARS";
            field.FieldType = FieldType.String;
            field.ExpandUnusedSpace = true;
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "编辑";
            field.DataField = "KF_ID";
            field.RendererFunction = "renderModify";
            field.Width = 80;
            columns.Add(field);

            return columns.ToArray();
        }

        
    }
}