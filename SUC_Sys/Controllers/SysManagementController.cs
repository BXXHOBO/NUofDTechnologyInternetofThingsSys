using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SUC_DataEntity;

namespace SUC_Sys.Controllers
{
    public class SysManagementController : Controller
    {
        SUC_EntityBLL.BLL_Oper op = new SUC_EntityBLL.BLL_Oper();
        //
        // GET: /

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity == null)
            {
                RedirectToAction("Login", "Home");
                Response.End();
            }
            else
            {
                List<t_menu> menus = op.GetMenuByRole(((t_user)Session["Users"]).UserRoles).ToList();

                string url = Common.Request.GetRawUrl().ToLower();
                t_menu model = menus.Where(c => Convert.ToString(c.LinkUrl.ToLower()) == url).FirstOrDefault();
                if (model != null)
                {
                    Session["MenuID"] = model.MenuCode;
                }
                ViewBag.t_menu = model;
                return PartialView(menus);
            }
            return PartialView();
        }

        #region 权限配置

        /// <summary>
        /// 权限配置
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRole()
        {
            if (string.IsNullOrEmpty(Request["No"]))
                RedirectToAction("Home");

            string no = Request["No"];

            t_role_menu entity = op.GetAuthorityByRole(no);

            string[] strArray = null;

            if (entity != null && !string.IsNullOrEmpty(entity.MenuCode))
            {
                strArray = entity.MenuCode.Split(',');
            }
            List<t_menu> menu = op.GetMenu().ToList();

            StringBuilder sb = new StringBuilder();
            menu.ForEach(o =>
            {
                if (o.ParentNo == "0")
                {
                    var list = menu.Where(c => c.ParentNo == o.MenuCode);

                    sb.AppendFormat("<tr><th><input type=\"checkbox\" name=\"selectid\" value=\"{1}\" {2}/><b>{0}</b></th>", o.MenuName, o.MenuCode, strArray != null && strArray.Contains(o.MenuCode.ToString()) ? "checked='checked'" : "");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                    foreach (var item in list)
                    {
                        sb.AppendFormat("<tr><th>&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"selectid\" value=\"{1}\" {2}/>{0} </th>", item.MenuName, item.MenuCode, strArray != null && strArray.Contains(item.MenuCode.ToString()) ? "checked='checked'" : "");
                        sb.Append("<td>");

                        var qc = menu.Where(a => a.ParentNo == item.MenuCode && a.IsWebPub == "0").ToList();
                        foreach (var ch in qc)
                        {
                            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"checkbox\" name=\"selectid\" value=\"{0}\" {2}/>{1}", ch.MenuCode, ch.MenuName, (strArray != null && strArray.Contains(ch.MenuCode.ToString())) ? "checked='checked'" : "");
                        }
                        sb.Append("</td>");

                        sb.Append("</tr>");
                    }
                }
            });
            ViewData["No"] = no;
            ViewData["MenuList"] = sb.ToString();
            ViewData["MenuChecked"] = strArray;
            return View(menu);
        }

        /// <summary>
        /// 权限编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUserRole()
        {
            string _FlagStr = Request.Form.Get("selectid");
            string RoleNo = Request.Form["RoleNo"];
            t_user user = null;
            if ((t_user)Session["Users"] != null)
            {
                user = (t_user)Session["Users"];
            }
            else
            {
                RedirectToAction("Login", "Account");
                Response.End();
            }

            t_role_menu entity = op.GetAuthorityByRole(RoleNo);
            if (entity == null)
            {
                t_role_menu trm = new t_role_menu();
                trm.RoleCode = RoleNo;
                trm.MenuCode = _FlagStr;
                trm.CreateBy = user.UserId;
                trm.CreateTime = DateTime.Now;
                op.AddAuthority(trm);

            }
            else
            {
                entity.MenuCode = _FlagStr;
                entity.ModifyBy = user.UserId;
                entity.ModifyTime = DateTime.Now;
                op.UpdateAuthority(entity);

            }
            return RedirectToAction("Index", "Roles");
        }

        #endregion
    }
}