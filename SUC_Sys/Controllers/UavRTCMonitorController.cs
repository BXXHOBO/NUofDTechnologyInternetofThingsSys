
using SUC_DataEntity;
using SUC_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZFine.Web.Areas.SYManage.Controllers
{
    public class UavRTCMonitorController : Controller
    {
        //
        // GET: /SYManage/UavRTCMonitor/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        private BLL_UavData bllDate = new BLL_UavData();
        private BLL_UavSortieData bll = new BLL_UavSortieData();
        [HttpGet]
        public ActionResult Form()
        {
           
            return View();
        }
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Contrail()
        {

            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetUavWorkAddrInfo()//获取架次表的巡检飞机的地址
        {
            Common.ResultRes res = new Common.ResultRes();

            //DateTime datetime = DateTime.Now;
            //int Sec = int.Parse(datetime.Second.ToString("d2"));
            //int Min = int.Parse(datetime.Minute.ToString("d2"));
            //int Hour = int.Parse(datetime.Hour.ToString("d2"));
            //int alltime = Sec + Min * 60 + Hour * 3600;//把时间转换成秒
            //int firsttime = alltime - 3600;
            //TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(firsttime));
            //string Sec1 = ts.Seconds.ToString("d2");
            //string Min1 = ts.Minutes.ToString("d2");
            //string Hour1 = ts.Hours.ToString("d2");
            //string defaultYear = datetime.Year.ToString("d4");
            //string defaultMonth = datetime.Month.ToString("d2");
            //int defaultDay = int.Parse(datetime.AddHours(-24).Day.ToString("d2")) + 1;
            //string day1 = defaultDay.ToString("d2");
            //string resultfirstdate = defaultYear + "-" + defaultMonth + "-" + day1 + " " + Hour1 + ":" + Min1 + ":" + Sec1;
            //DateTime date1 = Convert.ToDateTime(resultfirstdate);

            //List<Cld_UavSortieData> wbList = bll.GetLatestData(date1);

            List<SUC_UavSortieData> wbList = bll.GetUavWorkData();
            if (wbList != null)
            {
                res.IsSuccess = true;
                res.ResultValue = wbList;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                res = new Common.ResultRes();
                res.IsSuccess = true;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetLineNumberInfo()//获取表的总行数值
        {
            Common.ResultRes res = new Common.ResultRes();
            List<SUC_UavSortieData> pipelist = new List<SUC_UavSortieData>();
            var wbList = bll.GetUavListById().Distinct().ToList();
            int nums = wbList.Count;
            res.ResultValue = nums;
            res.IsSuccess = true;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetNumOfUavWorkInfo()//获取表的巡检飞机的数量
        {
            Common.ResultRes res = new Common.ResultRes();
            List<SUC_UavData> list = new List<SUC_UavData>();
            var wbList = bllDate.GetUavListByUavWork().Distinct().ToList();
            int nums = wbList.Count;
            res.ResultValue = nums;
            res.IsSuccess = true;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
