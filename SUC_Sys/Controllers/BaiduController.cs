
using SUC_DataEntity;
using SUC_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SUC_Sys.Controllers
{
    public class BaiduController : Controller
    {
        //
        // GET: /SYManage/Baidu/
        private BLL_UavInfo bll = new BLL_UavInfo();
        [HttpGet]
        public  ActionResult Index()
        {
           
            SUC_UavInfo model = bll.GetLastMonitorData();
            return View(model); 

        }

        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public  ActionResult Form()
        {

            string UavSerialNO = Request.QueryString["canshu1"] ?? "";
            string sortie = Request.QueryString["canshu2"] ?? "";

            ViewData["uavSerialNO"] = Request.QueryString["canshu1"] ?? "";
            ViewData["sortie"] = Request.QueryString["canshu2"] ?? "";
            Common.ResultRes res = new Common.ResultRes();
            //BLL_UavInfo bll = new BLL_UavInfo();
            List<SUC_UavInfo> dt = bll.GetUavListByUandS(UavSerialNO,sortie);
             BLL_UavInfo bllData = new BLL_UavInfo();
            SUC_UavInfo model = bllData.GetInfoByUavSerialNOandSortie(UavSerialNO,sortie);
            ViewData["lastlongitude"] = model.Longitude;
            ViewData["lastlatitude"] = model.Latitude;
            if (dt != null)
            {
                res.IsSuccess = true;
                return View(model);
            }
            else
            {
                res = new Common.ResultRes();

                res.IsSuccess = false;
                return View(res);
            }


        }
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public  ActionResult Details()
        {

            ViewData["uavSerialNO"] = Request.QueryString["canshu1"] ?? "";
            ViewData["sortie"] = Request.QueryString["canshu2"] ?? "";
            return View();

        }

       

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult GetAllPointData()
        {
            Common.ResultRes res = new Common.ResultRes();
            try
            {

                //Cld_UavInfo model = bll.GetData();
                res.ResultValue = bll.GetDataList();
                //res.ResultValue = model;
                res.IsSuccess = true;
            }
            catch (Exception ex)
            {
                res.Msg = ex.Message;
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetSortiePointData(string UavSerialNO, string Sortie)
        {
            Common.ResultRes res = new Common.ResultRes();
          
          
            try
            {

                res.ResultValue = bll.GetUavListByUandS(UavSerialNO,Sortie);
               
                res.IsSuccess = true;
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
