

using SUC_DAL;
using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SUC_Sys.Controllers
{
    public class SortieLastInterfaceController : Controller
    {
        //
        // GET: /SYManage/SortieLastInterface/

        private BLL_UavSortieDataSQL sortiebll = new BLL_UavSortieDataSQL();

        private BLL_UavDevice devicebll = new BLL_UavDevice();
        public ActionResult Index()
        {

            SUC_UavSortieData model = new SUC_UavSortieData();
            string state = Request.QueryString["state"] ?? "";
            string uavSerialNO = Request.QueryString["uavSerialNO"] ?? "";

            if (state == "0")
            {

                
                string historyAddr = Request.QueryString["historyAddr"] ?? "";
                DateTime time= DateTime.Now;
                sortiebll.UpdateSortieState(uavSerialNO, historyAddr,time);
                string workstate = "1";
                devicebll.UpdateDeviceWorkState(uavSerialNO, workstate);

            }
          
            return View();
        }

    }
}
