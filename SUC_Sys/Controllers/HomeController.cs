using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;
using SUC_DataEntity;
using Newtonsoft.Json;

namespace SUC_Sys.Controllers
{
    public class HomeController : Controller
    {
      
      
        SUC_EntityBLL.BLL_Order bll_order = new SUC_EntityBLL.BLL_Order();

        WxChat wxpush = new WxChat();

        public ActionResult Index()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                
                //ViewBag.SiteDatas = sd.GetAllSiteDataList("AQI");
                //ViewBag.UserName = Session["UserNames"];
                return View();
            }


        }
       


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
       
       
       


    }
}