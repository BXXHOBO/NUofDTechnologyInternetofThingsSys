using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SUC_DataEntity;
using Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using SUC_EntityBLL;

namespace TRM_Sys.Controllers
{
    public class UserController : Controller
    {
        SUC_EntityBLL.BLL_Oper op = new SUC_EntityBLL.BLL_Oper();

        SUC_EntityBLL.BLL_AuditOffice office = new SUC_EntityBLL.BLL_AuditOffice();
        BLL_Sef bLL_Sef = new BLL_Sef();

        // GET: User
        public ActionResult Index()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<v_user> list = op.GetUser().ToList();
                ViewBag.v_user = list;
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult NewGetUsers(string search)
        {
            bool isAdd = search.Equals("") ? false : true;
            List<v_user> list = op.GetUser().Where(p => p.Disabled == 0).ToList();
            if (isAdd)
            {
                list = op.GetUser().Where(p => p.UserCode.Contains(search) || p.UserName.Contains(search)).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetUserList()
        {
            List<v_user> list = op.GetUser().Where(p => p.Disabled == 0).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(string id)
        {
            string editFlag = Request.QueryString["isEdit"] ?? "";
            ViewData["editFlag"] = editFlag;
            t_user model = new t_user();
            ViewData["RolesTypeList"] = Load();
            ViewData["AuditOfficeList"] = AuditLoad();
            ViewData["NewNo"] = "";
            if (id != null)
            {
                model = op.GetUserByNo(id);
                ViewData["NewNo"] = model.UserCode;
            }
            return View(model);
        }
        public List<SelectListItem> Load()
        {
            List<SelectListItem> lsl = new List<SelectListItem>();
            List<t_roles> list = op.GetRole().ToList();
            lsl.Add(new SelectListItem { Text = "请选择", Value = "" });
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    SelectListItem sl = new SelectListItem();
                    sl.Value = item.RoleCode.ToString();

                    sl.Text = item.RoleName.ToString();

                    lsl.Add(sl);
                }
            }
            return lsl;
        }
        public List<SelectListItem> AuditLoad()
        {
            List<SelectListItem> lsl = new List<SelectListItem>();
            List<SUC_AuditOffice> list = op.GetAuditOffice().ToList();
            lsl.Add(new SelectListItem { Text = "请选择", Value = "" });
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    SelectListItem sl = new SelectListItem();
                    sl.Value = item.AuditOfficeCode.ToString();

                    sl.Text = item.AuditOfficeName.ToString();

                    lsl.Add(sl);
                }
            }
            return lsl;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetRolesList()
        {
            List<t_roles> list = op.GetRole().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetUserNo()
        {
            ResultRes res = new ResultRes();
            SUC_EntityBLL.Helper.BLL_CodeHelper codehelper = new SUC_EntityBLL.Helper.BLL_CodeHelper();
            res.ResultValue = codehelper.CreateNo("t_user", 5);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult ModifyUser()
        {
            ResultRes res = new ResultRes();
            t_user t_user = new t_user();
            if (Session["Users"] != null)
                t_user = Session["Users"] as t_user;
            else
                RedirectToAction("Login", "Account");

            int ID = Int32.Parse(Request["UserID"]);
            string UserCode = Request["UserCode"];
            string UserName = Request["UserName"];
            string UserRoles = Request["UserRoles"];
            string HireDate = Request["HireDate"];
            string Email = Request["Email"];
            string Addr = Request["Addr"];
            string opType = Request["opType"];
            bool isAdd = opType == "Add" ? true : false;
            t_user model;
            if (isAdd)
            {
                model = new t_user();
                model.PassWord = Common.EnDecrypt.MD5(model.UserCode + "888888ytb");
                model.IsAdmin = "0";
                model.CreateBy = t_user.UserId;
                model.CreateTime = DateTime.Now;
            }
            else
            {
                model = op.GetUserByNo(UserCode);
                model.ModifyBy = t_user.UserId;
                model.ModifyTime = DateTime.Now;
            }
            model.UserCode = UserCode;
            model.UserName = UserName;
            model.UserRoles = UserRoles;
            model.HireDate = DateTime.Parse(HireDate);
            model.Email = Email;
            model.Addr = Addr;
            try
            {
                if (isAdd)
                {
                    var tUser = op.GetUserByNo(model.UserCode);
                    if (tUser != null)
                    {
                        res.Msg = "保存失败,该用户名已经存在！";
                        res.IsSuccess = false;
                    }
                    else
                    {
                        res.IsSuccess = op.AddUser(model);
                    }
                }
                else
                {
                    res.IsSuccess = op.UpdateUser(model);
                }
            }
            catch (Exception ex)
            {
                res.Msg = ex.Message;
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult EditByUserInfo(t_user userInfo, string editFlag)
        {
            ResultRes res = new ResultRes();
            t_user user = null;
            if (Session["Users"] != null)
                user = (Session["Users"] as t_user);
            else
                RedirectToAction("Login", "Account");
            string no = userInfo.UserCode ?? "";
            t_user model;
            bool isAdd = editFlag.Equals("1") ? false : true;
            if (isAdd)
            {
                // 添加
                model = new t_user();
                List<t_user> list = op.NewGetUser().ToList();
                var temId = Convert.ToInt32(list.Last().UserId) + 1;
                model.UserId = temId.ToString();
                model.UserCode = userInfo.UserCode;
                var pass = "888888";
                if (userInfo.UserCode.Length < 6)
                {
                    pass = "888888";
                }
                else
                {
                    pass = userInfo.UserCode.Substring(userInfo.UserCode.Length - 6);
                }
                model.PassWord = Common.EnDecrypt.MD5(model.UserCode + pass + "ytb");
                model.IsAdmin = "0";
                model.CreateBy = user.UserId;
                model.CreateTime = DateTime.Now;
            }
            else
            {  // 编辑
                model = op.GetUserByNo(no);
                var PassWord = userInfo.PassWord ?? "";
                if (PassWord != ""&& PassWord != null )
                {
                    if (userInfo.UserCode.Length < 6)
                    {                        
                        PassWord = "888888";
                    }
                    model.PassWord = Common.EnDecrypt.MD5(model.UserCode + PassWord + "ytb");
                }
                model.ModifyBy = user.UserId;
                model.ModifyTime = DateTime.Now;
            }
            //产品基本资料
            model.ExternalType = userInfo.ExternalType;     //是否外聘教师
            model.AuditOfficeCode = userInfo.AuditOfficeCode;//属于哪个教研室
            model.UserName = userInfo.UserName ?? "";
            model.Email = userInfo.Email ?? "";
            model.Sex = userInfo.Sex ?? "0";
            model.Telephone = userInfo.Telephone ?? "";
            model.Addr = userInfo.Addr ?? "";
            model.Remark = userInfo.Remark ?? "";
            model.UserRoles = userInfo.UserRoles ?? "";
            model.HireDate = userInfo.HireDate;
            model.AuditOfficeCode = userInfo.AuditOfficeCode ?? "";
            // model.OpenId = userInfo.OpenId ?? "";
            ViewData["isAdd"] = isAdd;
            try
            {
                if (isAdd)
                {
                    var tUser = op.GetUserByNo(model.UserCode);
                    if (tUser != null)
                    {
                        res.Msg = "保存失败,该用户名已经存在！";
                        res.IsSuccess = false;
                    }
                    else
                    {
                        if (op.AddUser(model))
                        {
                            res.Msg = "新增成功！";
                            res.IsSuccess = true;
                        }
                        else
                        {
                            res.Msg = "新增失败！";
                            res.IsSuccess = false;
                        }
                    }
                }
                else
                {
                    //var tUser = op.GetUserByNo(model.UserCode);
                    List<t_user> list = op.NewGetUser().ToList();
                    var id = model.UserId;
                    var reList = list.Where(p => p.UserCode == model.UserCode && p.UserId != userInfo.UserId).ToList();
                    if (reList.Count != 0)
                    {
                        res.Msg = "修改失败,该账号已存在！";
                        res.IsSuccess = false;
                    }
                    else
                    {
                        if (op.UpdateUser(model))
                        {
                            res.Msg = "修改成功！";
                            res.IsSuccess = true;
                        }
                        else
                        {
                            res.Msg = "修改失败！";
                            res.IsSuccess = false;
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                res.Msg = ex.Message;
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //删除数据
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult DeleteUserById(string id)
        {
            ResultRes res = new ResultRes();
            string ids = Request["ids"] ?? "";
            ids = ids != "" ? ids : id;
            t_user user = null;
            if (Session["Users"] != null)
                user = (Session["Users"] as t_user);
            else
                RedirectToAction("Login", "Account");
            try
            {
                res.IsSuccess = op.DelUser(ids, user.UserId);
            }
            catch (Exception ex)
            {
                res.Msg = ex.Message;
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 权限树的轮询显示
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectTree()
        {
           
            string UserCode = Request["UserCode"];
            BLL_User t_User = new BLL_User();
            var y = t_User.SelectId(UserCode);

            List<DtreeDataModel> dtree = new List<DtreeDataModel>();
            List<SUC_Courseware> tRM_s = bLL_Sef.SelectSef1();

            for (int i = 0; i < tRM_s.Count; i++)
            {
                string r;
                if (y.CoursewareCode != "" && y.CoursewareCode != null)
                {

                    if (y.CoursewareCode.Contains(tRM_s[i].CoursewareCode))
                    {
                        r = "1";
                    }
                    else
                    {
                        r = "0";
                    }

                }
                else
                {
                    r = "0";
                }
                DtreeDataModel d1 = new DtreeDataModel();
                d1.id = tRM_s[i].CoursewareId.ToString() + "," + tRM_s[i].CoursewareCode;
                d1.title = tRM_s[i].CoursewareName;
                d1.IsWebPub = tRM_s[i].IsWebPub;
                d1.checkArr.@checked = r;
                if (bLL_Sef.SelectSefCount2(tRM_s[i].CoursewareId) > 0)
                {
                    d1.last = false;
                    ForAssign(tRM_s[i].CoursewareId, d1, UserCode);
                }
                else
                {
                    d1.last = true;
                }
                d1.parentId = tRM_s[i].P_CoursewareCode;
                dtree.Add(d1);
            }



            return Json(dtree);
            //return Json(q, JsonRequestBehavior.AllowGet); 
        }

        /// <summary>
        /// 查询子类，递归赋值
        /// </summary>
        /// <returns></returns>
        public List<DtreeDataModel> ForAssign(Guid guid, DtreeDataModel model, string UserCode)
        {
            BLL_User t_User = new BLL_User();
            var y = t_User.SelectId(UserCode);
            List<DtreeDataModel> dtree = new List<DtreeDataModel>();
            List<SUC_Courseware> tRM_s = bLL_Sef.SelectSefId3(guid);
            for (int i = 0; i < tRM_s.Count; i++)
            {
                string r;
                if (y.CoursewareCode != "" && y.CoursewareCode != null)
                {
                    if (y.CoursewareCode.Contains(tRM_s[i].CoursewareCode))
                    {
                        r = "1";
                    }
                    else
                    {
                        r = "0";
                    }
                }
                else
                {
                    r = "0";
                }
                DtreeDataModel d1 = new DtreeDataModel();
                d1.id = tRM_s[i].CoursewareId.ToString() + "," + tRM_s[i].CoursewareCode;
                d1.title = tRM_s[i].CoursewareName;
                d1.IsWebPub = tRM_s[i].IsWebPub;
                d1.checkArr.@checked = r;
                if (bLL_Sef.SelectSefCount2(tRM_s[i].CoursewareId) > 0)
                {
                    d1.last = false;
                    ForAssign(tRM_s[i].CoursewareId, d1, UserCode);
                }
                else
                {
                    d1.last = true;
                }
                d1.parentId = tRM_s[i].P_CoursewareCode;
                dtree.Add(d1);
            }
            model.children = dtree;
            return dtree;
        }


        /// <summary>
        /// 修改权限
        /// </summary>
        /// <returns></returns>
        //public string UpdatePower()
        //{
        //    BLL_User bLL_User = new BLL_User();
        //    string CodeId = Request["codeId"];
        //    string Code = Request["Code"];
        //    CodeId = CodeId.Substring(0, CodeId.LastIndexOf(","));

        //    if (bLL_User.UpdateCoursewareCode(Code, CodeId) > 0)
        //    {
        //        return "修改成功";
        //    }
        //    else
        //    {
        //        return "修改失败";
        //    }

        //}

        /// <summary>
        /// 判断是否是管理员
        /// </summary>
        /// <returns></returns>
        public string BoolPower1()
        {
            t_user entity = (t_user)Session["Users"];
            return entity.IsAdmin;
        }

        /// <summary>
        /// 权限树的轮询显示
        /// </summary>
        /// <returns></returns>
        //public JsonResult SelectTree()
        //{
           
        //    string UserCode = Request["UserCode"];
        //    BLL_User t_User = new BLL_User();
        //    var y = t_User.SelectId(UserCode);

        //    List<DtreeDataModel> dtree = new List<DtreeDataModel>();
        //    List<SUC_Courseware> tRM_s = bLL_Sef.SelectSef1();

        //    for (int i = 0; i < tRM_s.Count; i++)
        //    {
        //        string r;
        //        if (y.CoursewareCode != "" && y.CoursewareCode != null)
        //        {

        //            if (y.CoursewareCode.Contains(tRM_s[i].CoursewareCode))
        //            {
        //                r = "1";
        //            }
        //            else
        //            {
        //                r = "0";
        //            }

        //        }
        //        else
        //        {
        //            r = "0";
        //        }
        //        DtreeDataModel d1 = new DtreeDataModel();
        //        d1.id = tRM_s[i].CoursewareId.ToString() + "," + tRM_s[i].CoursewareCode;
        //        d1.title = tRM_s[i].CoursewareName;
        //        d1.IsWebPub = tRM_s[i].IsWebPub;
        //        d1.checkArr.@checked = r;
        //        if (bLL_Sef.SelectSefCount2(tRM_s[i].CoursewareId) > 0)
        //        {
        //            d1.last = false;
        //            ForAssign(tRM_s[i].CoursewareId, d1, UserCode);
        //        }
        //        else
        //        {
        //            d1.last = true;
        //        }
        //        d1.parentId = tRM_s[i].P_CoursewareCode;
        //        dtree.Add(d1);
        //    }



        //    return Json(dtree);
        //    //return Json(q, JsonRequestBehavior.AllowGet); 
        //}

        /// <summary>
        /// 查询子类，递归赋值
        /// </summary>
        /// <returns></returns>
        //public List<DtreeDataModel> ForAssign(Guid guid, DtreeDataModel model, string UserCode)
        //{
        //    BLL_User t_User = new BLL_User();
        //    var y = t_User.SelectId(UserCode);
        //    List<DtreeDataModel> dtree = new List<DtreeDataModel>();
        //    List<SUC_Courseware> tRM_s = bLL_Sef.SelectSefId3(guid);
        //    for (int i = 0; i < tRM_s.Count; i++)
        //    {
        //        string r;
        //        if (y.CoursewareCode != "" && y.CoursewareCode != null)
        //        {
        //            if (y.CoursewareCode.Contains(tRM_s[i].CoursewareCode))
        //            {
        //                r = "1";
        //            }
        //            else
        //            {
        //                r = "0";
        //            }
        //        }
        //        else
        //        {
        //            r = "0";
        //        }
        //        DtreeDataModel d1 = new DtreeDataModel();
        //        d1.id = tRM_s[i].CoursewareId.ToString() + "," + tRM_s[i].CoursewareCode;
        //        d1.title = tRM_s[i].CoursewareName;
        //        d1.IsWebPub = tRM_s[i].IsWebPub;
        //        d1.checkArr.@checked = r;
        //        if (bLL_Sef.SelectSefCount2(tRM_s[i].CoursewareId) > 0)
        //        {
        //            d1.last = false;
        //            ForAssign(tRM_s[i].CoursewareId, d1, UserCode);
        //        }
        //        else
        //        {
        //            d1.last = true;
        //        }
        //        d1.parentId = tRM_s[i].P_CoursewareCode;
        //        dtree.Add(d1);
        //    }
        //    model.children = dtree;
        //    return dtree;
        //}


        /// <summary>
        /// 修改权限
        /// </summary>
        /// <returns></returns>
        public string UpdatePower()
        {
            BLL_User bLL_User = new BLL_User();
            string CodeId = Request["codeId"];
            string Code = Request["Code"];
            CodeId = CodeId.Substring(0, CodeId.LastIndexOf(","));

            if (bLL_User.UpdateCoursewareCode(Code, CodeId) > 0)
            {
                return "修改成功";
            }
            else
            {
                return "修改失败";
            }

        }

        /// <summary>
        /// 判断是否是管理员
        /// </summary>
        /// <returns></returns>
        //public string BoolPower1()
        //{
        //    t_user entity = (t_user)Session["Users"];
        //    return entity.IsAdmin;
        //}
    }
}