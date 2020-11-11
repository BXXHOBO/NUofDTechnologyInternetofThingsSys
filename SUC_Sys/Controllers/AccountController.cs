using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using SUC_DataEntity;

namespace SUC_Sys.Controllers
{
    public class AccountController : Controller
    {
        SUC_EntityBLL.BLL_Oper op = new SUC_EntityBLL.BLL_Oper();

        public ActionResult Login()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
               
           
        }
        public ActionResult Login1()
        {
            t_user entity = (t_user)Session["Users"];

            ViewData["username"] = Session["UserNames"];
            ViewData["username"] = Session["PassWord"]; ;

            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
               
                return View();
            }
               
  


        }
      

        public void LoginOn()
        {
            string usercode = Request["username"];
            string password = Common.EnDecrypt.MD5(usercode + Request["password"] + "ytb");
            string password1 = Request["password"];

            try
            {
                t_user user = op.CheckLogin(usercode, password);
                #region 登陆验证
                if (user == null)
                {
                    Response.Write("<script>alert('帐号密码错误！');window.location='/Account/Login'</script>");
                    Response.End();
                }
                else
                {
                    Session["Users"] = user;
                    Session["UserNames"] = user.UserName;
                    Session["RightsList"] = op.GetAuthorityByRole(user.UserRoles);
                    //Response.Write("<script>window.location='/Account/Login1?username=" + usercode + "&password=" + password1+"'</script>");
                    Response.Write("<script>window.location='/Account/Login1'</script>");
                    //op.AddOperationLog("C0035", "0", user.UserCode);
                    //Session["Users"] = user;
                    //Session["UserNames"] = user.UserName;
                    //Session["RightsList"] = op.GetAuthorityByRole(user.UserRoles);
                    //Response.Write("<script>window.location='/Home'</script>");
                }
                #endregion
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public void LoginOn1()
        {
            string usercode = Request["username"];
            string password = Common.EnDecrypt.MD5(usercode + Request["password"] + "ytb");
           
            try
            {
                t_user user = op.CheckLogin(usercode, password);
                #region 登陆验证
                if (user == null)
                {
                    Response.Write("<script>alert('帐号密码错误！');window.location='/Account/Login'</script>");
                    Response.End();
                }
                else
                {
                  

                    //op.AddOperationLog("C0035", "0", user.UserCode);
                    Session["Users"] = user;
                    Session["UserNames"] = user.UserName;
                    Session["RightsList"] = op.GetAuthorityByRole(user.UserRoles);
                    Response.Write("<script>window.location='/Home/Index'</script>");
                }
                #endregion
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }


        public void LoginOut()
        {
            try
            {
                t_user entity = (t_user)Session["Users"];
                if (entity != null)
                {
                    Session["Users"] = null;
                    Session["UserNames"] = "";
                    Session["RightsList"] = "";
                }
                Response.Write("<script>window.location='/Account/Login'</script>");
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModifyPassword()
        {
            ResultRes res = new ResultRes();
            try
            {
                t_user user = new t_user();
                if (Session["Users"] != null)
                    user = (Session["Users"] as t_user);
                else
                    Redirect("login.html");

                string oldpassword = Request["oldpassword"];
                string newpassword = Request["newpassword"];
                string oldPwd = user.PassWord;
                string oldpasswordM = Common.EnDecrypt.MD5(user.UserCode + oldpassword + "ytb");
                if (!oldpasswordM.Equals(user.PassWord))
                {
                    res.Msg = "当前密码不正确！";
                    res.IsSuccess = false;
                }
                else
                {
                    user.PassWord = Common.EnDecrypt.MD5(user.UserCode + newpassword + "ytb");
                    if (op.UpdateUser(user))
                    {
                        res.Msg = "修改成功！";
                        res.IsSuccess = true;
                    }
                    else
                    {
                        res.Msg = "新密码无效修改失败！";
                        res.IsSuccess = false;
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
    }
}