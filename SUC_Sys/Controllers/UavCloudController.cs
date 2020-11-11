using SY_DataEntity;
using SY_EntityBLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZFine.Code;
using ZFine.Domain.Entity.SystemManage;
using ZFine.Web.Areas.Interface;

namespace ZFine.Web.Areas.SYManage.Controllers
{
    public class UavCloudController : ControllerBase
    {
       
        //
        // GET: /SYManage/UavCloud/
        private BLL_UavInfo bll = new BLL_UavInfo();
        private BLL_UavSortieData sortiebll = new BLL_UavSortieData();
        public static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["UavCloudConnectionString"].ToString();

        WechatPushHandler push = new WechatPushHandler();
        //string connString = "server=58.247.122.34,65432;uid=sa;pwd=_Password;database=ZFine";
        [HttpGet]
        public override ActionResult Index()
        {
          
            return View();
        }
        [HttpGet]
        public  ActionResult RealTimeIndex()
        {
            Common.ResultRes res = new Common.ResultRes();
            //BLL_UavInfo bll = new BLL_UavInfo();
            //List<Cld_UavInfo> dt = bll.GetDataList();
            //ViewData["canshu1"] = Request.QueryString["canshu1"];
            //ViewData["canshu2"] = Request.QueryString["canshu2"];
            //string UavSerialNO = Request.QueryString["canshu1"];
            //string Sortie = Request.QueryString["canshu2"];
            //SY_EntityBLL.BLL_UavInfo bllData = new SY_EntityBLL.BLL_UavInfo();
            //Cld_UavInfo model = bllData.GetUavLastTimeByUandS(UavSerialNO, Sortie);

            BLL_UavSortieData bll = new BLL_UavSortieData();
            ViewData["canshu1"] = Request.QueryString["canshu1"];
            ViewData["canshu2"] = Request.QueryString["canshu2"];
            string UavSerialNO = Request.QueryString["canshu1"];
            string Sortie = Request.QueryString["canshu2"];
            List<Cld_UavSortieData> dt = bll.GetUavListByNUMandS(UavSerialNO, Sortie);
            Cld_UavSortieData sortiemodel = bll.GetUavSortieByNUMandS(UavSerialNO, Sortie);


             if (dt != null)
            {
                res.IsSuccess = true;
                return View(sortiemodel);
            }
            else
            {
                res = new Common.ResultRes();
                res.IsSuccess = true;
                return View(res);
            }  
        }



        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {

            //DateTime datetime = DateTime.Now;
            //int Sec = int.Parse(datetime.Second.ToString("d2"));
            //int Min = int.Parse(datetime.Minute.ToString("d2"));
            //int Hour = int.Parse(datetime.Hour.ToString("d2"));
            //int alltime =Sec + Min * 60 + Hour * 3600;//把时间转换成秒
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

            //List<Cld_UavSortieData> q = sortiebll.GetLatestList(pagination,date1,keyword);

            List<Cld_UavSortieData> q = sortiebll.GetUavFirstList(pagination, keyword);
            var data = new
            {
                rows = q,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public override ActionResult Form()
        {

       
 
            //实时数据
            Cld_UavInfo model = bll.GetLastMonitorData();
            return View(model); 
           
        }

      
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUavInfoGet(string UavSerialNO, string Sortie)
        {
            Common.ResultRes res = new Common.ResultRes();
            BLL_UavInfo bll = new BLL_UavInfo();

            List<Cld_UavInfo> dt = bll.GetUavListByUandS(UavSerialNO,Sortie);
            SY_EntityBLL.BLL_UavInfo bllData = new SY_EntityBLL.BLL_UavInfo();
            Cld_UavInfo ParameterSetModel = bllData.GetInfoByUavSerialNOandSortie(UavSerialNO, Sortie);
            

            if (dt != null)
            {
                res.IsSuccess = true;
                res.ResultValue = ParameterSetModel;
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
         public JsonResult GetUavInfoALL()
         {
             Common.ResultRes res = new Common.ResultRes();
             BLL_UavInfo bll = new BLL_UavInfo();
              
             List<Cld_UavInfo> dt = bll.GetDataList();
             SY_EntityBLL.BLL_UavInfo bllData = new SY_EntityBLL.BLL_UavInfo();
             Cld_UavInfo ParameterSetModel = bllData.GetLastMonitorData();
            
           
             if (bll != null)
             {
                 res.IsSuccess = true;
                 res.ResultValue = ParameterSetModel;
                 return Json(res, JsonRequestBehavior.AllowGet);
             }
             else
             {
                 res = new Common.ResultRes();
                 res.IsSuccess = true;
                 return Json(res, JsonRequestBehavior.AllowGet);
             }
         }
        
         [AcceptVerbs(HttpVerbs.Post)]
         [ValidateInput(false)]
         public void UpdateData()//SQL语句更新架次表内容
         {
             SqlConnection conn = new SqlConnection(connString);

             string strSql = "INSERT INTO Cld_UavSortieData(UavSortieId,UavSerialNO,Sortie,VideoAddr,InitialOperTime,OperationEndTime,Rec_CreateTime,Rec_CreateBy,Disabled) SELECT UavInfoId,UavSerialNO,Sortie,VideoAddr,InitialOperTime,OperationEndTime,Rec_CreateTime,Rec_CreateBy,Disabled FROM(SELECT UavInfoId,UavSerialNO,Sortie,VideoAddr,Rec_CreateTime,MIN(Rec_CreateTime) OVER(PARTITION BY UavSerialNO,Sortie)AS InitialOperTime,MAX(Rec_CreateTime) OVER(PARTITION BY UavSerialNO,Sortie)AS OperationEndTime,Rec_CreateBy,Disabled FROM Cld_UavInfo) T WHERE InitialOperTime = Rec_CreateTime and  not exists(select * from Cld_UavSortieData WHERE UavSortieId=UavInfoId);";
             SqlCommand cmd = new SqlCommand(strSql, conn);
             conn.Open();
             cmd.ExecuteNonQuery();
             conn.Close();

             Cld_UavSortieData model;
             model = sortiebll.GetData();
            
             string desp = "设备：" + model.UavSerialNO + "\r\n\r\n" +
             "架次：" + model.Sortie + "\r\n\r\n" +
             "创建时间：" + model.Rec_CreateTime + "\r\n\r\n" +
             "创建人：" + model.Rec_CreateBy + "\r\n\r\n";
             try
             {
                 push.ServerChanPush(2, "飞行信息更新通知", desp);
             }
             catch
             {

             }
           
         }

         [AcceptVerbs(HttpVerbs.Get)]
         public JsonResult GetUavUandSInfoGet(string UavSerialNO, string Sortie)
         {
             Common.ResultRes res = new Common.ResultRes();
           
             SY_EntityBLL.BLL_UavInfo bllData = new SY_EntityBLL.BLL_UavInfo();
             Cld_UavInfo ParameterSetModel = bllData.GetUavLastTimeByUandS(UavSerialNO, Sortie);
             BLL_UavInfo bll = new BLL_UavInfo();

             List<Cld_UavInfo> dt = bll.GetDataList();

             if (dt != null)
             {
                 res.IsSuccess = true;
                 res.ResultValue = ParameterSetModel;
                 return Json(res, JsonRequestBehavior.AllowGet);
             }
             else
             {
                 res = new Common.ResultRes();
                 res.IsSuccess = true;
                 return Json(res, JsonRequestBehavior.AllowGet);
             }
         }

    }
}
