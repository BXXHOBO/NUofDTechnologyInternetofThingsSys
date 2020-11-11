using Common;
using SUC_DataEntity;
using SUC_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUC_Sys.Controllers
{
    public class MenuController : Controller
    {

        BLL_Menu bll_menu = new BLL_Menu();


        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult GetMenus()
        {

            string orderStatus = Request["orderStatus"]; //订单状态
            String search = Request["search"]; //搜索字段
            string start = Request["start"]; //开始时间
            string end = Request["end"]; //结束时间
            int pageIndex = Convert.ToInt32(Request["page"]); //页码
            int pageSize = Convert.ToInt32(Request["rows"]); //每页显示数
            string sidx = Request["sidx"]; //排序字段
            string sord = Request["sord"]; //升降序
            if (!(String.IsNullOrEmpty(search)))
            {
                search = search.Trim();
            }

            var pages = bll_menu.GetMenus(start, end, search, pageIndex, pageSize, sidx, sord);
            return Json(pages,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据编号获取菜单
        /// </summary>
        /// <param name="menuCode">菜单编号</param>
        /// <returns></returns>
        public ActionResult GetMenuByCode(string menuCode)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(menuCode))
            {
                res.Msg = "菜单编号不可为空。";
            }
            var menu = bll_menu.GetMenuByCode(menuCode);
           
            if (menu == null)
            {
                res.Msg = "未获取到有效数据。";
            }
            else
            {
                res.Msg = "操作成功。";
                res.ResultValue = menu;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 提交菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Submit(t_menu menu)
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(menu.MenuName) || string.IsNullOrEmpty(menu.LinkUrl))
            {
                res.IsSuccess = false;
                res.Msg = "菜单标题，链接不能为空。";
                return Json(res);
            }

            var entity = bll_menu.SubmitMenu(menu, user);
            if (entity != null)
            {
                res.Msg = "操作成功。";
            }
            else
            {
                res.IsSuccess = false;
                res.Msg = "操作失败。";
            }
            return Json(res);
        }

        /// <summary>
        /// 假删除菜单
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Delete(int menuId)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (menuId == 0)
            {
                res.IsSuccess = false;
                res.Msg = "菜单id不能为0";
                return Json(res);
            }

            var success = bll_menu.Delete(menuId);
            if (success)
            {
                res.Msg = "菜单删除成功";
            }
            else
            {
                res.IsSuccess = false;
                res.Msg = "菜单删除失败";
            }
            return Json(res);
        }
        
    }
}