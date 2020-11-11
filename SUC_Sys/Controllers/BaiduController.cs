using SY_DataEntity;
using SY_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZFine.Code;
using ZFine.Domain.Entity.SystemManage;

namespace ZFine.Web.Areas.SYManage.Controllers
{
    public class BaiduController : ControllerBase
    {
        //
        // GET: /SYManage/Baidu/
        private BLL_UavInfo bll = new BLL_UavInfo();
        [HttpGet]
        public override ActionResult Index()
        {
           
            Cld_UavInfo model = bll.GetLastMonitorData();
            return View(model); 

        }

        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public override ActionResult Form()
        {

            string UavSerialNO = Request.QueryString["canshu1"] ?? "";
            string sortie = Request.QueryString["canshu2"] ?? "";

            ViewData["uavSerialNO"] = Request.QueryString["canshu1"] ?? "";
            ViewData["sortie"] = Request.QueryString["canshu2"] ?? "";
            Common.ResultRes res = new Common.ResultRes();
            //BLL_UavInfo bll = new BLL_UavInfo();
            List<Cld_UavInfo> dt = bll.GetUavListByUandS(UavSerialNO,sortie);
            SY_EntityBLL.BLL_UavInfo bllData = new SY_EntityBLL.BLL_UavInfo();
            Cld_UavInfo model = bllData.GetInfoByUavSerialNOandSortie(UavSerialNO,sortie);
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
        public override ActionResult Details()
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
