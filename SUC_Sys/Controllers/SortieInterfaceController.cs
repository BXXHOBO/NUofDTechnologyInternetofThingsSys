using SUC_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SUC_DAL;
using SUC_DataEntity;

namespace SUC_Sys.Controllers
{
    public class SortieInterfaceController : Controller
    {
        //
        // GET: /SYManage/SortieInterface/
        private BLL_UavSortieData bll = new BLL_UavSortieData();

        private BLL_UavDevice devicebll = new BLL_UavDevice();
        public ActionResult Index()
        {
            Common.ResultRes res = new Common.ResultRes();
            SUC_UavSortieData model = new SUC_UavSortieData();
          
            string state = Request.QueryString["state"] ?? "";
            string uavSerialNO = Request.QueryString["uavSerialNO"] ?? "";
           
            if (state=="1")
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
                bll.Add(model);
                string workstate = "0";
                devicebll.UpdateDeviceWorkState(uavSerialNO,workstate);
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
      


    }
}
