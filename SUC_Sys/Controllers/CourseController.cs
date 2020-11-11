using Common;
using TRM_DataEntity;
using TRM_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;

namespace TRM_Sys.Controllers
{
    public class CourseController : Controller
    {
        BLL_Course bll_course = new BLL_Course();
        BLL_User bll_user = new BLL_User();
        BLL_Class bll_class = new BLL_Class();
        BLL_AuditOffice bll_office = new BLL_AuditOffice();
        // GET: Course
        #region 课程信息表
        // GET: Course
        /// <summary>
        /// 课程主页
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 课程分页
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult GetCourses()
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

            var pages = bll_course.GetCourses(start, end, search, pageIndex, pageSize, sidx, sord);
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据编号获取课程菜单
        /// </summary>
        /// <param name="courseCode">课程编号</param>
        /// <returns></returns>
        public ActionResult GetCourseByCode(string courseCode)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(courseCode))
            {
                res.Msg = "课程编号不可为空。";
            }
            var menu = bll_course.GetCourseByCode(courseCode);

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
        /// 提交课程菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Submit(TRM_Course course, string editFlag)
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(course.CourseName))//|| string.IsNullOrEmpty(course.CourseTeacher)
            {
                res.IsSuccess = false;
                res.Msg = "课程名称不能为空。";
                return Json(res);
            }

            var entity = bll_course.SubmitMenu(course, user, editFlag);
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
        /// 假删除课程菜单
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Delete(string courseId)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;

            if (courseId == null)
            {
                res.IsSuccess = false;
                res.Msg = "id不能为空";
                return Json(res);
            }
            Guid CourseId = Guid.Parse(courseId);
            var success = bll_course.Delete(CourseId);
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

        #region 课程类别表
        /// <summary>
        /// 类别主页
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexType()
        {
            return View();
        }

        /// <summary>
        /// 类别分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCoursesType()
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

            var pages = bll_course.GetCoursesType(start, end, search, pageIndex, pageSize, sidx, sord);
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 类别修改
        /// </summary>
        /// <param name="courseCode"></param>
        /// <returns></returns>
        public ActionResult GetCourseTypeByCode(string courseTypeCode)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(courseTypeCode))
            {
                res.Msg = "类别编号不可为空。";
            }
            var menu = bll_course.GetCourseTypeByCode(courseTypeCode);

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
        public ActionResult SubmitType(SUC_CourseType coursetype, string editFlag)
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(coursetype.CourseTypeName))//|| string.IsNullOrEmpty(course.CourseTeacher)
            {
                res.IsSuccess = false;
                res.Msg = "类别名称不能为空。";
                return Json(res);
            }

            var entity = bll_course.SubmitTypeMenu(coursetype, user, editFlag);
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
        public ActionResult DeleteType(string courseTypeId)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;

            if (courseTypeId == null)
            {
                res.IsSuccess = false;
                res.Msg = "id不能为空";
                return Json(res);
            }
            Guid CourseTypeId = Guid.Parse(courseTypeId);
            var success = bll_course.DeleteType(CourseTypeId);
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

        #region 班级教师课程表

        /// <summary>
        /// 班级教师课程主页
        /// </summary>
        /// <returns></returns>
        public ActionResult ClaCouUser() 
        {
            return View();
        }

        public ActionResult GetCoursesClaCouUser()
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

            var pages = bll_course.GetCoursesClaCouUser(start, end, search, pageIndex, pageSize, sidx, sord);
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据编号获取关联菜单
        /// </summary>
        /// <param name="courseCode">课程编号</param>
        /// <returns></returns>
        public ActionResult GetCourseByClaCouUser(string claTeaCurAssociativeCode)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(claTeaCurAssociativeCode))
            {
                res.Msg = "课程编号不可为空。";
            }
            var menu = bll_course.GetCourseByClaCouUser(claTeaCurAssociativeCode);

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
        /// 提交课程菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>

        public ActionResult SubmitClaCouUser(SUC_ClaTeaCurAssociative course, string editFlag)
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(course.ClassCode))//|| string.IsNullOrEmpty(course.CourseTeacher)
            {
                res.IsSuccess = false;
                res.Msg = "课程名称不能为空。";
                return Json(res);
            }

            var entity = bll_course.SubmitClaCouUser(course, user, editFlag);
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
        /// 假删除课程菜单
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult DeleteClaTeaClaCouUser(string courseId)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;

            if (courseId == null)
            {
                res.IsSuccess = false;
                res.Msg = "id不能为空";
                return Json(res);
            }
            Guid CourseId = Guid.Parse(courseId);
            var success = bll_course.DeleteClaTeaClaCouUser(CourseId);
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

        #region 导入课程

        [HttpPost]
        public ActionResult ExcelToUpload()
        {
            Common.ResultRes res = new Common.ResultRes();
            DataTable excelTable = new DataTable();
            HttpPostedFileBase mypost = Request.Files["file"];
            TRM_Course courseModel = new TRM_Course();//课程表
            SUC_ClaTeaCurAssociative clateacurModel = new SUC_ClaTeaCurAssociative();//班级\教师\课程关联表
                                                                                     //string msg = string.Empty;
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }


            if (Request.Files.Count > 0)
            {
                try
                {
                    //HttpPostedFileBase mypost = Request.Files[0];

                    if (mypost.FileName == "")
                    {
                        res.IsSuccess = false;
                        res.Msg = "请选择导入文件";
                    }
                    else if (mypost.ContentLength > 0)
                    {
                        #region

                        string extensionName = mypost.FileName.Substring(mypost.FileName.LastIndexOf('.')).ToLower();
                        if (!extensionName.Equals(".xlsx") && !extensionName.Equals(".xls"))
                        {
                            res.IsSuccess = false;
                            res.Msg = "导入文件格式";
                            return Json(res, JsonRequestBehavior.AllowGet);
                        }



                        string fileName = Guid.NewGuid().ToString() + extensionName;
                        string path = "~/ExcelFiles/Course/" + Path.GetFileName(fileName);

                        mypost.SaveAs(Server.MapPath(path));

                        ExcelHelper excel_helper = new ExcelHelper(Server.MapPath(path));
                        DataTable dt = excel_helper.ExcelToDataTable("", true);
                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    string f = row[column].ToString();

                                }
                            }

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //for (int j = 0; j < dt.Columns.Count; j++)
                                //{
                                courseModel.SchoolSmester = dt.Rows[i][0].ToString();//学期学年
                                courseModel.CourseCode = dt.Rows[i][1].ToString();//课程代码
                                courseModel.CourseName = dt.Rows[i][2].ToString();//课程名称
                                                                                  //根据任课教师名称获取用户编号
                                t_user userModel = bll_user.GetUserCode(dt.Rows[i][3].ToString());
                                //根据班级名称获取授课班级
                                TRM_ClassInfo classModel = bll_class.GetClassInfoCode(dt.Rows[i][4].ToString());
                                //根据课程类别名称获取类别编号
                                SUC_CourseType typeModel = bll_course.GetCourseTypeCode(dt.Rows[i][6].ToString());

                                //根据审核教研室名称获取编号
                                SUC_AuditOffice officeModel = bll_office.GetOfficeCode(dt.Rows[i][5].ToString());

                                if (officeModel != null)
                                {
                                    courseModel.AuditOfficeCode = officeModel.AuditOfficeCode;//审核教研室
                                }


                                if (typeModel != null)
                                {
                                    courseModel.CourseTypeCode = typeModel.CourseTypeCode;//课程类别
                                }
                                else
                                {
                                    res.IsSuccess = false;
                                    res.Msg = "课程类别名在课程类别表中为空";
                                }


                                string editFlag = "Add";
                                clateacurModel.CourseCode = dt.Rows[i][1].ToString();
                              
                                    if (classModel != null)
                                    {
                                        clateacurModel.ClassCode = classModel.ClassCode;
                                    }
                               
                                
                                else
                                {
                                    res.IsSuccess = false;
                                    res.Msg = "班级名称在班级表中为空";
                                }
                              
                                if (userModel != null)
                                 {
                                      clateacurModel.UserCode = userModel.UserCode;
                                  }
                             
                                else
                                {
                                    res.IsSuccess = false;
                                    res.Msg = "教师名称在用户表中为空";
                                }
                                
                               

                                //根据课程代码查询是否有对应课程
                                var entityCourse = bll_course.GetCourseByCode(dt.Rows[i][1].ToString());
                                if (entityCourse == null)//如果该课程为空则添加课程
                                {
                                    entityCourse = bll_course.SubmitMenu(courseModel, user, editFlag);//新增课程信息表
                                }
                               
                                var entity = bll_course.SubmitClaCouUser(clateacurModel, user, editFlag);//新增课程/班级/教师表内容
                             
                                


                                //string f = dt.Rows[i][j].ToString();

                                //}
                            }

                            res.IsSuccess = true;
                            res.ResultValue = path;
                        }
                        //int irowmax = dt.Rows.Count;

                        #endregion
                    }



                }
                catch (Exception ex)
                {
                    //res.Msg = ex.Message;
                    res.Msg = "上传失败";

                }
            }
            else
            {
                res.Msg = "请选择文件";
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 导出excel模板

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportTemplate()
        {
            try
            {
                t_user user = (t_user)Session["Users"];
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }


                string dirPath = @"E:/DownFile/";//路径
                if (!Directory.Exists(dirPath))//如果不存在就创建 dir 文件夹  
                    Directory.CreateDirectory(dirPath);

                var userId = user.UserId;
                string filePath = dirPath + userId + ".xlsx";
                //删除之前临时文件
                System.IO.File.Delete(filePath);


                //创建excel下载文件
                bll_course.CreateExcel(filePath);

                return File(filePath, "text/plain", "课程信息模板.xlsx"); //课程信息模板.xlsx是客户端保存的名字

            }
            catch (Exception e)
            {
                var content = string.Format("Message:{0} \r\n StackTrace:{1} \r\n {2}", e.Message, e.StackTrace, DateTime.Now.ToString("yyyy-MM-dd"));
                LogPrintHelper.Info(content);
                throw;
            }

        }

        #endregion


    }

}