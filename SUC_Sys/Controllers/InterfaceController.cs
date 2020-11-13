using Common;
using SUC_DAL;
using SUC_DataEntity;
using SUC_EntityBLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SUC_Sys.Controllers
{
    public class InterfaceController : Controller
    {
        //
        // GET: /SYManage/Interface/
        private BLL_UavInfo bll = new BLL_UavInfo();
        private BLL_UavSortieData bllsortie = new BLL_UavSortieData();
        private BLL_UavSortieDataSQL sortiebll = new BLL_UavSortieDataSQL();
        private BLL_UavDevice devicebll = new BLL_UavDevice();

        //数据接口
        [HttpGet]
        public ActionResult UavInfoAdd()
        {
            ResultRes res = new ResultRes();
            

            SUC_UavInfo model = new SUC_UavInfo();
            model.UavInfoId = Guid.NewGuid();
            string uavSerialNO = Request.QueryString["uavSerialNO"] ?? "";
            string sortie = Request.QueryString["sortie"] ?? "";
            string videoAddr = Request.QueryString["videoAddr"] ?? "";
            string longitude = Request.QueryString["longitude"] ?? "";
            string latitude = Request.QueryString["latitude"] ?? "";
            string altitude = Request.QueryString["altitude"] ?? "";
            Decimal temperature = Convert.ToDecimal(Request.QueryString["temperature"] ?? "");
            Decimal humidity = Convert.ToDecimal(Request.QueryString["humidity"] ?? "");
            string speed = Request.QueryString["speed"] ?? "";
            string atmosPressure = Request.QueryString["atmosPressure"] ?? "";
            string remark = Request.QueryString["remark"] ?? "";
            string reccreateby = Request.QueryString["reccreateby"] ?? "";

            model.UavSerialNO = uavSerialNO;
            model.OperationDate = DateTime.Now;
            model.Sortie = sortie;
            model.VideoAddr = videoAddr;
            model.Longitude = longitude;
            model.Latitude = latitude;
            model.Altitude = altitude;
            model.Temperature = temperature;
            model.Speed = speed;
            model.AtmosPressure = atmosPressure;
            model.Humidity = humidity;
            model.Remark = remark;
            model.Rec_CreateBy = reccreateby;
            model.Rec_CreateTime = DateTime.Now;
            model.Disabled = 0;
            bll.Add(model);
            res.Msg = "操作成功。";
            res.IsSuccess = true;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #region 架次信息添加
        [HttpGet]
        public ActionResult SortieAdd()
        {
            Common.ResultRes res = new Common.ResultRes();
            SUC_UavSortieData model = new SUC_UavSortieData();

            string state = Request.QueryString["state"] ?? "";
            string uavSerialNO = Request.QueryString["uavSerialNO"] ?? "";

            if (state == "1")
            {
                model.UavSortieId = Guid.NewGuid();
                string sortie = Request.QueryString["sortie"] ?? "";
                string videoAddr = Request.QueryString["videoAddr"] ?? "";
                string historyAddr = Request.QueryString["historyAddr"] ?? "";
                string OperateAddr = Request.QueryString["operateAddr"] ?? "";
                ViewData["operateAddr"] = Request.QueryString["operateAddr"] ?? "";
                string WorkContent = Request.QueryString["workContent"] ?? "";
                string remark = Request.QueryString["remark"] ?? "";
                string reccreateby = Request.QueryString["reccreateby"] ?? "";
                model.UavSerialNO = uavSerialNO;
                model.InitialOperTime = DateTime.Now;
                model.OperationEndTime = DateTime.Now;
                model.Sortie = sortie;
                model.VideoAddr = videoAddr;
                model.UavState = state;
                model.HistoryAddr = historyAddr;
                model.OperateAddr = OperateAddr;
                model.WorkContents = WorkContent;
                model.Remark = remark;
                model.Rec_CreateBy = reccreateby;
                model.Rec_CreateTime = DateTime.Now;
                model.Disabled = 0;
                bllsortie.Add(model);
                string workstate = "0";
                devicebll.UpdateDeviceWorkState(uavSerialNO, workstate);
            }
            if (state == "-1")
            {
                return View("数据出错");
            }
            if (state != null)
            {
                res.IsSuccess = true;
                return View();
            }
            else
            {
                res = new Common.ResultRes();
                res.IsSuccess = true;
                return View(res);
            }


        }
        #endregion
        #region 架次信息更新
        public ActionResult SortieUpdate()
        {
            ResultRes res = new ResultRes();
            SUC_UavSortieData model = new SUC_UavSortieData();
            string state = Request.QueryString["state"] ?? "";
            string uavSerialNO = Request.QueryString["uavSerialNO"] ?? "";

            if (state == "0")
            {
                string historyAddr = Request.QueryString["historyAddr"] ?? "";
                DateTime time = DateTime.Now;
                sortiebll.UpdateSortieState(uavSerialNO, historyAddr, time);
                string workstate = "1";
                devicebll.UpdateDeviceWorkState(uavSerialNO, workstate);

            }

            res.Msg = "操作成功。";
            res.IsSuccess = true;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
            {
               
              
               
            }
  
      
          


    }
}
