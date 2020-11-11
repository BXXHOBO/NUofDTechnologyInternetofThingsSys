using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUC_DataEntity;
using SUC_EntityBLL;

namespace SUC_Sys.Controllers
{
    public class ClassInfoController : Controller
    {
        BLL_Class bLL_Class = new BLL_Class();

        // GET: ClassInfo
        #region  班级信息表
        /// <summary>
        /// 班级主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 班级分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetClassInfo()
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

            var pages = bLL_Class.GetClassInfo(start, end, search, pageIndex, pageSize, sidx, sord);
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 类别修改
        /// </summary>
        /// <param name="courseCode"></param>
        /// <returns></returns>
        public ActionResult GetClassByCode(string classCode)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(classCode))
            {
                res.Msg = "班级编号不可为空。";
            }
            var menu = bLL_Class.GetClassByCode(classCode);
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
        /// 类别添加
        /// </summary>
        /// <param name="coursetype"></param>
        /// <param name="editFlag"></param>
        /// <returns></returns>
        public ActionResult SubmitClass(SUC_ClassInfo classInfo, string editFlag)
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(classInfo.ClassName))//|| string.IsNullOrEmpty(course.CourseTeacher)
            {
                res.IsSuccess = false;
                res.Msg = "班级名称不能为空。";
                return Json(res);
            }

            var entity = bLL_Class.SubmitClassMenu(classInfo, user, editFlag);
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
        /// 假删除类别菜单
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public ActionResult DeleteClass(string classInfoId)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;

            if (classInfoId == null)
            {
                res.IsSuccess = false;
                res.Msg = "id不能为空";
                return Json(res);
            }
            Guid ClassInfoId = Guid.Parse(classInfoId);
            var success = bLL_Class.DeleteClass(ClassInfoId);
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

        #endregion
    }

}
