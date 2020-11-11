using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUC_DataEntity;
using SUC_EntityBLL;

namespace SUC_Sys.Controllers
{
    public class AuditOfficeController : Controller
    {
        // GET: Audit
        BLL_AuditOffice bll_auditoffice = new BLL_AuditOffice();

        BLL_Teacher bll_Teacher = new BLL_Teacher();

        #region 教研室信息表
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
        public ActionResult GetAuditOffices()
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

            var pages = bll_auditoffice.GetAuditOffice(start, end, search, pageIndex, pageSize, sidx, sord);
            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据编号获取菜单
        /// </summary>
        /// <param name="auditofficeCode">教研室编号</param>
        /// <returns></returns>
        public ActionResult GetAuditOfficeByCode(string auditofficeCode)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(auditofficeCode))
            {
                res.Msg = "教研室编号不可为空。";
            }
            var menu = bll_auditoffice.GetAuditOfficeByCode(auditofficeCode);

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
        public ActionResult Submit(SUC_AuditOffice auditoffice, string editFlag)
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = new ResultRes();
            res.IsSuccess = true;
            if (string.IsNullOrEmpty(auditoffice.AuditOfficeName))
            {
                res.IsSuccess = false;
                res.Msg = "教研室名称不能为空。";
                return Json(res);
            }

            var entity = bll_auditoffice.SubmitMenu(auditoffice, user, editFlag);
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
        public ActionResult Delete(string auditofficeId)
        {
            ResultRes res = new ResultRes();
            res.IsSuccess = true;

            if (auditofficeId == null)
            {
                res.IsSuccess = false;
                res.Msg = "id不能为空";
                return Json(res);
            }
            Guid AuditOfficeId = Guid.Parse(auditofficeId);
            var success = bll_auditoffice.Delete(AuditOfficeId);
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

        #region 教学审核
        public ActionResult File()
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
        #endregion



        #region 教师详细表
        public ActionResult Teacher()
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
        public ActionResult GetTeachers()
        {

            string Status = Request["State"]; //订单状态
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

            var pages = bll_Teacher.GetTeachar(start, end, search, pageIndex, pageSize, sidx, sord);
            return Json(pages, JsonRequestBehavior.AllowGet);
        }


        #endregion
        #region 进入任务详情，获取任务详情信息，需要获取日志
        //[AcceptVerbs(HttpVerbs.Get)]
        //[ValidateInput(false)]
        //public ActionResult Edit()
        //{
        //    t_user user = (t_user)Session["Users"];
        //    if (user == null)
        //    {
        //        RedirectToAction("Login", "Account");
        //    }
        //    String orderCodeId = Request.QueryString["OrderCode"];
        //    //String orderCodeId1 = Request["OrderCode"];
        //    if (!(String.IsNullOrEmpty(orderCodeId)))
        //    {
        //        orderCodeId = orderCodeId.Trim();
        //    }
        //    OrderModel model = bll_Teacher.GetAllDetail(orderCodeId);
        //    if (null != model)
        //    {
        //        ViewData["orderId"] = model.Order.OrderId;
        //        ViewData["projects"] = DictList(EpsDict.ORDER_PROJECT_DICT); ;
        //        ViewData["detailstatus"] = DictList(EpsDict.ORDER_DETAIL_STATUS_DICT);
        //    }
        //    return View(model);
        //}
        #endregion

        /// <summary>
        /// 下载excel
        /// </summary>
        /// <returns></returns>
        public FileResult DownLoad()
        {
            var path = Server.MapPath("~/ExcelFiles/Template");
            var file = System.IO.Directory.GetFiles(path).OrderByDescending(t => new FileInfo(t).CreationTime).FirstOrDefault();
            return File(file, "application/vnd.ms-excel", new FileInfo(file).Name);
        }

        #region 导入课程

        [HttpPost]
        public ActionResult ExcelToUpload()
        {
            Common.ResultRes res = new Common.ResultRes();
            DataTable excelTable = new DataTable();
            HttpPostedFileBase mypost = Request.Files["file"];
            SUC_AuditOffice auditofficeModel = new SUC_AuditOffice();//教研室信息表
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
                            res.Msg = "导入Excel文件格式";
                            return Json(res, JsonRequestBehavior.AllowGet);
                        }



                        string fileName = Guid.NewGuid().ToString() + extensionName;
                        string path = "~/ExcelFiles/AuditOffice/" + Path.GetFileName(fileName);

                        mypost.SaveAs(Server.MapPath(path));
                        //excelTable = ExcelHelper.GetExcelDataTable(path);


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
                            //int entitycode = 0;
                           
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //for (int j = 0; j < dt.Columns.Count; j++)
                                //{
                                //auditofficeModel.AuditOfficeCode = dt.Rows[i][0].ToString();//教研室编号
                                auditofficeModel.AuditOfficeName = dt.Rows[i][0].ToString();//教研室名称
                                auditofficeModel.Remark = dt.Rows[i][1].ToString();//备注
                                ////根据任课教师名称获取用户编号
                                //t_user userModel = bll_user.GetUserCode(dt.Rows[i][3].ToString());
                                ////根据班级名称获取授课班级
                                //SUC_ClassInfo classModel = bll_class.GetClassInfoCode(dt.Rows[i][4].ToString());
                                ////根据课程类别名称获取类别编号
                                //SUC_CourseType typeModel = bll_course.GetCourseByCode(dt.Rows[i][6].ToString());

                                //courseModel.AuditOfficeCode = dt.Rows[i][5].ToString();//审核教研室
                                //courseModel.CourseTypeCode = typeModel.CourseTypeCode;//课程类别

                                

                                string editFlag = "Add";
                                string q = "JYS";
                                var r = DateTime.Now;
                                string y = r.Year.ToString();
                                string m = r.Month.ToString();
                                string d = r.Day.ToString();
                                string x = r.Hour.ToString();
                                string f = r.Minute.ToString();
                                string k = r.Second.ToString();
                                string l = r.Millisecond.ToString();
                                auditofficeModel.AuditOfficeCode = q + y + m + d + x + f + k+l;

                                auditofficeModel.AuditOfficeName = auditofficeModel.AuditOfficeName;
                                auditofficeModel.Remark = auditofficeModel.Remark;

                                //auditofficeModel.AuditOfficeCode = entitycode++.ToString();

                                var entity = bll_auditoffice.SubmitMenu(auditofficeModel, user, editFlag);//新增教研室信息表
                               




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
                    res.Msg = ex.Message;

                }
            }
            else
            {
                res.Msg = "请选择文件";
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}