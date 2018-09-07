using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// 以下为本系统的需要引用的命名空间
using FineUIMvc;
using FineUIMvc.EmptyProject.Models;

namespace FineUIMvc.EmptyProject.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var user = Models.SessionValue.GetUserSession(Session);
            ViewBag.userName = user.U_REALNAME;

            var nodes = CreateModel();
            ViewBag.Menus = nodes.ToArray();
            //ViewBag.TreeMenuNodes = nodes.ToArray();
            return View();
        }



        #region 建立 INDEX 页面的模块
        private List<ControlBase> CreateModel()
        {
            var user = Models.SessionValue.GetUserSession(Session);
            var listQx = TQX_LinqDal.Query_QX(user.U_ROLEID);

            ///获取模块
            var list = TModel_LinqDal.Query_Model((int)EnumShenGoStatus.Effective).ToList();
            var listChild = new List<T_MODEL>();
            var listMain = new List<T_MODEL>();
            foreach (var en in list)
            {
                if (en.MD_PID > 0)
                {
                    //foreach(var n in listQx)
                    //{
                    //    if(n.QX_MID == en.MD_PID && n.QX_STATUS == 1)
                    //    {
                            listChild.Add(en);
                    //        break;
                    //    }
                    //}
                }
                else if(en.MD_PID == 0)
                {
                    //foreach(var n in listQx)
                    //{
                    //    if(n.QX_MID == en.MD_ID && n.QX_STATUS == 1)
                    //    {
                            listMain.Add(en);
                    //        break;
                    //    }
                    //}
                }
            }

            List<ControlBase> AccordionList = new List<ControlBase>();
            Accordion at = new Accordion();
            at.ID = "myAccordion";
            at.ShowBorder = false;
            at.ShowHeader = false;
            at.ShowCollapseTool = true;
            foreach (var en in listMain)
            {
                if (en.MD_PID == 0)
                {
                    AccordionPane panel = new AccordionPane();
                    panel.Title = en.MD_NAME;
                    panel.IconUrl = en.MD_ICON;
                    //panel.IconUrl = Url.Content("~/res/images/16/1.png");
                    panel.ID = "P" + en.MD_ID.ToString();
                    panel.BodyPadding = "2px 5px";
                    panel.Layout = LayoutType.Fit;

                    Tree tree = new Tree();
                    tree.Layout = LayoutType.Fit;
                    tree.ShowBorder = false;
                    tree.ShowHeader = false;
                    tree.ID = "T" + en.MD_ID.ToString();

                    if (en.MD_ISARCHIVE == 1)
                    {
                        //var nodes = CreateArchive();
                        //foreach (var n in nodes)
                        //    tree.Nodes.Add(n);
                    }
                    else if (en.MD_ISARCHIVE == 2)
                    {
                        //var nodes = CreateArchiveTongJi();
                        //foreach (var n in nodes)
                        //    tree.Nodes.Add(n);
                    }
                    else
                    {
                        foreach (var child in listChild)
                        {
                            if (child.MD_PID == en.MD_ID)
                            {
                                TreeNode item = new TreeNode();
                                item.NodeID = "N" + child.MD_ID.ToString();
                                item.Text = child.MD_NAME;
                                item.IconUrl = child.MD_ICON;
                                item.Leaf = true;
                                item.Target = "mainPage";
                                item.NavigateUrl = (child.MD_PATH == null ? (child.MD_PATH + "?name=" + child.MD_NAME) : (child.MD_PATH + "?mid=" + child.MD_ID + "&name=" + child.MD_NAME));
                                tree.Nodes.Add(item);
                            }
                        }
                    }
                    if (tree.Nodes.Count > 0)
                        panel.Items.Add(tree);
                    //Label lbl = new Label();
                    //lbl.Text = "面板中的文本";
                    //panel.Items.Add(lbl);
                    
                    at.Panes.Add(panel);
                }
            }
            AccordionList.Add(at);

            return AccordionList;
        }
        //private IList<TreeNode> CreateModel_Old()
        //{
        //    ///档案类型
        //    var archiveNode = CreateArchive();
        //    ///档案统计
        //    var archiveTongJiNode = CreateArchiveTongJi();

        //    ///树节点
        //    IList<TreeNode> nodes = new List<TreeNode>();
        //    var list = TModel_LinqDal.Query_Model((int)EnumShenGoStatus.Effective);
        //    var listChild = new List<T_MODEL>();
        //    if(list != null)
        //    {
        //        foreach(var en in list)
        //        {
        //            if (en.MD_PID == 0)
        //            {
        //                TreeNode tn = new TreeNode();
        //                tn.IconUrl = en.MD_ICON;
        //                tn.Text = en.MD_NAME;
        //                tn.NavigateUrl = en.MD_PATH==null?"":en.MD_PATH;
        //                tn.NodeID = en.MD_ID.ToString();

        //                if (en.MD_ISARCHIVE == 1)
        //                    foreach (var nn in archiveNode)
        //                        tn.Nodes.Add(nn);
        //                else if (en.MD_ISARCHIVE == 2)
        //                    foreach (var nn in archiveTongJiNode)
        //                        tn.Nodes.Add(nn);

        //                nodes.Add(tn);
        //            }
        //            else
        //                listChild.Add(en);
        //        }

        //        foreach(var en in listChild)
        //        {
        //            foreach(var node in nodes)
        //            {
        //                if(node.NodeID == en.MD_PID.ToString())
        //                {
        //                    TreeNode tn = new TreeNode();
        //                    tn.IconUrl = en.MD_ICON;
        //                   // tn.Leaf = true;
        //                    tn.Text = en.MD_NAME;
        //                    tn.NodeID = en.MD_ID.ToString();
        //                    tn.NavigateUrl = en.MD_PATH == null ? "" : en.MD_PATH;
        //                    node.Nodes.Add(tn);
        //                }
        //            }
        //        }
        //    }
        //    return nodes;
        //}

        /// <summary>
        /// 在模块中属于 档案类型的，增加 档案树 信息
        /// </summary>
        /// <param name="listTree"></param>
        //private IList<TreeNode> CreateArchive()
        //{
        //    IList<TreeNode> archiveNode = new List<TreeNode>();

        //    var listArchive = TSysAr_LinqDal.Query_SysAR((int)EnumShenGoStatus.Effective, (int)EnumShenGoStatus.Effective);
        //    IList<TreeNode> listNodes = new List<TreeNode>();
        //    var listChild = new List<T_SYSAR>();
        //    foreach (var en in listArchive)
        //    {
        //        if (en.AR_PID == 0)
        //        {
        //            TreeNode tn = new TreeNode();
        //            tn.Text = en.AR_NAME;
        //            tn.Leaf = false;
        //            //tn.NavigateUrl = ""; en.V01; //en.MD_PATH == null ? "" : en.MD_PATH;
        //            tn.IconUrl = "~/res/images/model/ar.png";
        //            tn.NodeID = "A:" + en.AR_ID.ToString();
        //            archiveNode.Add(tn);
        //        }
        //        else
        //            listChild.Add(en);
        //    }

            ////return archiveNode;

        //    foreach (var en in listChild)
        //    {
        //        foreach (var node in archiveNode)
        //        {
        //            if (node.NodeID == "A:" + en.AR_PID.ToString())
        //            {
        //                TreeNode tn = new TreeNode();
        //                //tn.IconUrl = en.AR_NAME;
        //                // tn.Leaf = true;
        //                tn.Leaf = false;
        //                tn.Text = en.AR_NAME;
        //                tn.IconUrl = "~/res/images/model/ar2.png";
        //                tn.NodeID = "A:" + en.AR_ID.ToString();
        //                tn.Target = "mainPage";
        //                tn.NavigateUrl = (en.AR_V01==null?("?name=" + en.AR_NAME):(en.AR_V01 + "?aid=" + en.AR_ID + "&name=" + en.AR_NAME)); //en.MD_PATH == null ? "" : en.MD_PATH;
        //                node.Nodes.Add(tn);
        //            }
        //        }
        //    }
        //    return archiveNode;
        //}
        /// <summary>
        /// 在模块中属于 档案统计的， 增加 档案树 信息
        /// </summary>
        /// <returns></returns>
        //private IList<TreeNode> CreateArchiveTongJi()
        //{
        //    IList<TreeNode> archiveNode = new List<TreeNode>();

        //    var listArchive = TSysAr_LinqDal.Query_SysAR((int)EnumShenGoStatus.Effective, (int)EnumShenGoStatus.Effective);
        //    IList<TreeNode> listNodes = new List<TreeNode>();
        //    var listChild = new List<T_SYSAR>();
        //    foreach (var en in listArchive)
        //    {
        //        if (en.AR_PID == 0)
        //        {
        //            TreeNode tn = new TreeNode();
        //            tn.Text = en.AR_NAME;
        //            tn.Leaf = false;
        //            //tn.NavigateUrl = ""; en.V01; //en.MD_PATH == null ? "" : en.MD_PATH;
        //            tn.NodeID = "T:" + en.AR_ID.ToString();
        //            tn.IconUrl = "~/res/images/model/ar.png";
        //            archiveNode.Add(tn);
        //        }
        //        else
        //            listChild.Add(en);
        //    }

        //    ////return archiveNode;

        //    foreach (var en in listChild)
        //    {
        //        foreach (var node in archiveNode)
        //        {
        //            if (node.NodeID == "T:" + en.AR_PID.ToString())
        //            {
        //                TreeNode tn = new TreeNode();
        //                //tn.IconUrl = en.AR_NAME;
        //                // tn.Leaf = true;
        //                tn.Leaf = false;
        //                tn.Text = en.AR_NAME;
        //                tn.NodeID = "T:" + en.AR_ID.ToString();
        //                tn.IconUrl = "~/res/images/model/ar2.png";
        //                tn.Target = "mainPage";
        //                tn.NavigateUrl = (en.AR_V02== null ? "" : (en.AR_V02 + "?id=" + en.AR_ID)); //en.MD_PATH == null ? "" : en.MD_PATH;
        //                node.Nodes.Add(tn);
        //            }
        //        }
        //    }
        //    return archiveNode;
        //}
        #endregion

        public ActionResult Hello()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnHello_Click()
        {
            Alert.Show("你好 FineUI！", MessageBoxIcon.Warning);

            return UIHelper.Result();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnLogin_Click(string tbxUserName, string tbxPassword)
        {

            var user = TUser_LinqDal.Get_LoginUser(tbxUserName, tbxPassword);
            if (user == null)
                Alert.ShowInTop("用户名或密码错误!");
            else
            {
                Models.SessionValue.AddUserSession(Session, user);
            }

            /*if (tbxUserName == "admin" && tbxPassword == "admin")
            {
                ShowNotify("成功登录！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }*/

            return UIHelper.Result();
        }

        // GET: Themes
        public ActionResult Themes()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnSavePassword_Click(FormCollection values)
        {
            var user = Models.SessionValue.GetUserSession(Session);
            if (user == null)
            {
                Alert.ShowInParent("长时间未操作，请重新登录!");
                //System.Threading.Thread.Sleep(2000);
                //return RedirectToAction("Index", "Login");
            }
            else
            {
                string s1 = values["txtPassword1"];
                string s2 = values["txtPassword2"];
                if (s1 == null || s1 == "" || s2 == null || s2 == "")
                    Alert.ShowInParent("密码不能为空!");
                else if (s1 != s2)
                    Alert.ShowInParent("两次密码不一致!");
                else
                {
                    int value = TUser_LinqDal.Modify_Password(user.U_ID, s1);
                    if (value == 0)
                        Alert.ShowInParent("用户ID重复，请联系管理员！");
                    else if (value == -1)
                        Alert.ShowInParent("修改失败！");
                    else
                    {
                        Alert.ShowInParent("修改成功，请重新登录！");
                        //System.Threading.Thread.Sleep(2000);
                        //return RedirectToAction("Index", "Login");
                    }
                }
            }
            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult btnReLogin_Click()
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("确定要重新登录吗？ ",
                    String.Empty,
                    MessageBoxIcon.Question,
                    "confirmOKCallback();",
                    ""));

            return UIHelper.Result();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginOut()
        {
            return RedirectToAction("Index", "Login");
        }

            /// <summary>
            /// 修改密码
            /// </summary>
            /// <returns></returns>
            public ActionResult Password()
        {
            return View();
        }

        /// <summary>
        /// 页面没有开发
        /// </summary>
        /// <returns></returns>
        public ActionResult NoPage()
        {
            return View();
        }
    }
}