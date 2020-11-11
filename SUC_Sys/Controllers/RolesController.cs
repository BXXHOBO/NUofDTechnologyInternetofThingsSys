using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using SUC_DataEntity;
using SUC_EntityBLL;

namespace SUC_Sys.Controllers
{
    public class RolesController : Controller
    {
        SUC_EntityBLL.BLL_Oper op = new SUC_EntityBLL.BLL_Oper();
        BLL_Sef bLL_Sef = new BLL_Sef();

        // GET: /Roles/角色

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

        public JsonResult GetRolesList()
        {
            List<t_roles> list = op.GetRole().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(string id)
        {
            t_roles model = new t_roles();
            ViewData["NewNo"] = "";
            if (id != null)
            {
                model = op.GetRoleByNo(id);
            }
            else
            {
                SUC_EntityBLL.Helper.BLL_CodeHelper codehelper = new SUC_EntityBLL.Helper.BLL_CodeHelper();
                ViewData["NewNo"] = codehelper.CreateNo("t_roles", 5);
            }
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        //获取角色编号
        public JsonResult GetRolesNo()
        {
            ResultRes res = new ResultRes();
            SUC_EntityBLL.Helper.BLL_CodeHelper codehelper = new SUC_EntityBLL.Helper.BLL_CodeHelper();
            res.ResultValue= codehelper.CreateNo("t_roles", 5);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //修改数据
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult ModifyRoles()
        {
            ResultRes res = new ResultRes();
            string _User = string.Empty;
            t_user t_user = new t_user();
            if (Session["Users"] != null)
                t_user = Session["Users"] as t_user;
            else
                Redirect("login.html");
            //int ID = Int32.Parse(Request["ID"]);
            string RoleCode = Request["RoleCode"];
            string RoleName = Request["RoleName"];
            string opType = Request["opType"];
            t_roles model = op.GetRoleByNo(RoleCode);
            if (model == null) {
                model = new t_roles();
                model.RoleCode = RoleCode;
                model.CreateBy = t_user.UserId;
                model.CreateTime = DateTime.Now;
            } 
            model.RoleName = RoleName;
            model.ModifyBy = t_user.UserId;
            model.ModifyTime = DateTime.Now;
            try
            {
                if (opType == "Add")
                {
                    res.IsSuccess = op.AddRole(model);
                }
                else {
                    res.IsSuccess = op.UpdateRole(model);
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
        public JsonResult DeleteRolesById(string id)
        {
            ResultRes res = new ResultRes();
            string ids = Request["ids"] ?? "";
            ids = ids != "" ? ids : id;
            string _User = string.Empty;
            t_user user = null;
            if (Session["Users"] != null)
                user = (Session["Users"] as t_user);
            else
                Redirect("login.html");
            try
            {
                res.IsSuccess = op.DelRoles(ids, user.UserId);
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
        //public JsonResult SelectTree()
        //{

        //     string RoleCode= Request["RoleCode"];
        //    BLL_t_role_menuZzc bLL_T = new BLL_t_role_menuZzc();
        //    var y= bLL_T.SelectId(RoleCode);

        //    List<DtreeDataModel> dtree = new List<DtreeDataModel>();
        //    List<SUC_Courseware> tRM_s = bLL_Sef.SelectSef1();

        //    for (int i = 0; i < tRM_s.Count; i++)
        //    {
        //        string r;
        //        if (y.CoursewareCode!=""&& y.CoursewareCode!=null)
        //        {

        //        if (y.CoursewareCode.Contains(tRM_s[i].CoursewareCode))
        //        {
        //            r = "1";
        //        }
        //        else
        //        {
        //            r = "0";
        //        }

        //        }
        //        else
        //        {
        //            r = "0";
        //        }
        //        DtreeDataModel d1 = new DtreeDataModel();
        //        d1.id = tRM_s[i].CoursewareId.ToString() + "," + tRM_s[i].CoursewareCode;
        //        d1.title = tRM_s[i].CoursewareName;
        //        d1.IsWebPub = tRM_s[i].IsWebPub;
        //        d1.checkArr.@checked= r;
        //        if (bLL_Sef.SelectSefCount2(tRM_s[i].CoursewareId) > 0)
        //        {
        //            d1.last = false;
        //            ForAssign(tRM_s[i].CoursewareId, d1, RoleCode);
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
        //public List<DtreeDataModel> ForAssign(Guid guid, DtreeDataModel model,string RoleCode)
        //{
        //    BLL_t_role_menuZzc bLL_T = new BLL_t_role_menuZzc();
        //    var y = bLL_T.SelectId(RoleCode);
        //    List<DtreeDataModel> dtree = new List<DtreeDataModel>();
        //    List<SUC_Courseware> tRM_s = bLL_Sef.SelectSefId3(guid);
        //    for (int i = 0; i < tRM_s.Count; i++)
        //    {
        //        string r;
        //        if (y.CoursewareCode != ""&& y.CoursewareCode != null)
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
        //        d1.id = tRM_s[i].CoursewareId.ToString()+","+ tRM_s[i].CoursewareCode;
        //        d1.title = tRM_s[i].CoursewareName;
        //        d1.IsWebPub = tRM_s[i].IsWebPub;
        //        d1.checkArr.@checked =r;
        //        if (bLL_Sef.SelectSefCount2(tRM_s[i].CoursewareId) > 0)
        //        {
        //            d1.last = false;
        //            ForAssign(tRM_s[i].CoursewareId, d1, RoleCode);
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
        //public string UpdatePower()
        //{
        //    BLL_t_role_menuZzc role_MenuZzc = new BLL_t_role_menuZzc();
        //    string CodeId= Request["codeId"];
        //    string Code = Request["Code"];
        //    CodeId = CodeId.Substring(0, CodeId.LastIndexOf(","));
            
        //    if (role_MenuZzc.UpdateCoursewareCode(Code, CodeId)>0)
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
        //public string BoolPower1()
        //{
        //    t_user entity = (t_user)Session["Users"];
        //    return entity.UserRoles;
        //}
    }
}