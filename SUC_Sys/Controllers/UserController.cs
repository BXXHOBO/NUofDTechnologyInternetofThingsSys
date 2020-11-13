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
            model.UserName = userInfo.UserName ?? "";
            model.Email = userInfo.Email ?? "";
            model.Sex = userInfo.Sex ?? "0";
            model.Telephone = userInfo.Telephone ?? "";
            model.Addr = userInfo.Addr ?? "";
            model.Remark = userInfo.Remark ?? "";
            model.UserRoles = userInfo.UserRoles ?? "";
            model.HireDate = userInfo.HireDate;
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
        /// 判断是否是管理员
        /// </summary>
        /// <returns></returns>
        public string BoolPower1()
        {
            t_user entity = (t_user)Session["Users"];
            return entity.IsAdmin;
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