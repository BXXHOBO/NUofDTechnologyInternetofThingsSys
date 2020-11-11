using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUC_DataEntity;
using SUC_EntityBLL;

namespace TRM_Sys.Controllers
{
    public class CoursewareManagementController : Controller
    {
        BLL_Courseware BLL_Courseware = new BLL_Courseware();


        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult Index()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
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

            var pages = BLL_Courseware.GetCourseware(start, end, search, pageIndex, pageSize, sidx, sord);
          
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据编号获取菜单
        /// </summary>
        /// <param name="menuCode">菜单编号</param>
        /// <returns></returns>
        public ActionResult GetMenuByCode(Guid UserCode)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(UserCode.ToString()))
            {
                res.Msg = "教师编号不可为空。";
            }
            var menu = BLL_Courseware.GetCoursewareByCode(UserCode);

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
        public ActionResult Submit(SUC_Courseware menu)
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(menu.CoursewareCode))
            {
                res.IsSuccess = false;
                res.Msg = "编号不能为空";
                return Json(res);
            }

            var entity = BLL_Courseware.SubmitMenu(menu, user);
            if (entity >0)
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
        public ActionResult Delete()
        {
            string menuId= Request["MenuId"];
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            Guid g = new Guid(menuId);
            var success = BLL_Courseware.Delete(g);
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
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        public int AddData(SUC_Courseware tRM_Courseware)
        {
            string CoursewareId = Request["CoursewareId"];
            string CoursewareCode = Request["CoursewareCode"];
            string UserCode = Request["UserCode"];
            string P_CoursewareCode = Request["P_CoursewareCode"];
            string IsWebPub = Request["IsWebPub"];
            string Icon = Request["Icon"];
            string SortID = Request["SortID"];
            string CoursewarePath = Request["CoursewarePath"];

            Guid g = new Guid(CoursewareId);

            int i= BLL_Courseware.AddCourseware(g, CoursewareCode, UserCode, P_CoursewareCode, IsWebPub, Icon,Convert.ToInt32(SortID), CoursewarePath);
            if (i>0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <returns></returns>
        public string PassAudit()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity.UserRoles!= "R00034" && entity.UserRoles != "R00024")
            {
                return "-2";
            }
            string CoursewareId = Request["CoursewareId"];
            Guid guid = new Guid(CoursewareId);
            var cour= BLL_Courseware.SelectId(guid);
            if (cour.State==2||cour.State==4 && entity.UserRoles != "R00024")
            {
                return "-3";
            }

            bool b= BLL_Courseware.UpdateId(guid, entity.UserRoles);
            if (b)
            {
                return "1";
            }
            else
            {
                return "-1";
            }
        }

        /// <summary>
        /// 不通过审核
        /// </summary>
        /// <returns></returns>
        public string OustAudit()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity.UserRoles != "R00034" && entity.UserRoles != "R00024")
            {
                return "-2";
            }

            string CoursewareId = Request["CoursewareId"];
            string txtOust = Request["txtOust"];
            Guid guid = new Guid(CoursewareId);

            var cour = BLL_Courseware.SelectId(guid);
            if (cour.State == 2 || cour.State == 4 && entity.UserRoles != "R00024")
            {
                return "-3";
            }

            var t = BLL_Courseware.OustUpdateId(guid,entity.UserRoles,txtOust);
            if (t)
            {
                return "1";
            }
            else
            {
                return "-1";
            }
        }

    }
}