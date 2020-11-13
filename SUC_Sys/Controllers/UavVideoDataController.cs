
using Common;
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
namespace ZFine.Web.Areas.SYManage.Controllers
{
    public class UavVideoDataController : Controller
    {
        //
        // GET: /SYManage/UavVideoData/
        private BLL_UavInfo bll = new BLL_UavInfo();
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]

        public static string GetWebContent(string Url)
       {
  　　         string strResult = "";
  　　    try
  　　    {
  　　　　HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
  　　　　//声明一个HttpWebRequest请求 
  　　　　request.Timeout = 30000;
 　　　　//设置连接超时时间 
 　　　　request.Headers.Set("Pragma", "no-cache");
 　　　　HttpWebResponse response = (HttpWebResponse)request.GetResponse();
 　　　　Stream streamReceive = response.GetResponseStream();
 　　　　Encoding encoding = Encoding.GetEncoding("GB2312");
 　　　　StreamReader streamReader = new StreamReader(streamReceive, encoding);
 　　　　strResult = streamReader.ReadToEnd();
 　    　}
　　     catch
 　　    {
 　　　　  Console.WriteLine("出错");
 　　    }
 　　　　 return strResult;
 　　   }

      


        [HttpGet]
        public ActionResult Form()
        {

            //按架次获取
            string UavSerialNO = Request.QueryString["canshu1"] ?? "";
            string sortie = Request.QueryString["canshu2"] ?? "";
            //StringBuilder sb = new StringBuilder();//要抓取的URL地址 
            //sb.Append("http://58.247.122.34/acuqireUrl/?serialno=tianxuii123456&date=19-4-17-14-46-0");

            //string strWebContent = GetWebContent(sb.ToString());//得到指定Url的源码 
            //ViewData["video"] = strWebContent;//将得到的字符串存储传到前端
            //string s="url\":\"";
            //string e = "\",\"";
            //Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            //ViewData["video1"] = rg.Match(strWebContent);//将得到的页面对应的视频地址发送给前端
           
            Common.ResultRes res = new Common.ResultRes();
            //if (sortie != null && UavSerialNO!=null)
            //{

           
            //    BLL_UavInfo bll = new BLL_UavInfo();
            //    List<Cld_UavInfo> dt = bll.GetUavListByUandS(UavSerialNO,sortie);
            //    SY_EntityBLL.BLL_UavInfo bllData = new SY_EntityBLL.BLL_UavInfo();

              
            //    Cld_UavInfo model = bllData.GetUavLastTimeBySortie(UavSerialNO,sortie);

              
            //    string createtime = "2019-04-19 09:00:00";
            //    DateTime datetime = DateTime.Parse(model.Rec_CreateTime.ToString());
            //    string defaultYear = datetime.Year.ToString("d4");
            //    string defaultMonth = datetime.Month.ToString("d2");
            //    string defaultDay = datetime.AddHours(-24).Day.ToString("d2");
            //    string defaultHour = datetime.Hour.ToString("d2");
            //    string defaultMinute = datetime.Minute.ToString("d2");
            //    string defaultSecond = datetime.Second.ToString("d2");
            //    string defaultYear1 = defaultYear.Remove(0, defaultYear.Length - 2);//取最后字符
            //    string returnDate1="";
            //    string mon1 =(int.Parse(defaultMonth)).ToString();
            //    string day1 = (int.Parse(defaultDay)+1).ToString();
            //    string h1 = (int.Parse(defaultHour)).ToString();
            //    string s1 = (int.Parse(defaultSecond)).ToString();

            //    string m1 = (int.Parse(defaultMinute) - 4).ToString();

            //    returnDate1 = defaultYear1 + "-" + mon1 + "-" + day1 + "-" + h1 + "-" + m1 + "-" + s1;


            //    if(model.Rec_CreateTime.HasValue)
            //    {
            //        createtime = model.Rec_CreateTime.ToString();
            //    }
                
            
           
            //string uavSerialNO = model.UavSerialNO;      
            ////string time = convertor(createtime);


            //string start = "http://58.247.122.34/acuqireUrl/?serialno=";
            ////string start = "http://192.168.11.135/acuqireUrl/?serialno=";
            
            //string date = "&date=";
            ////string address = start + uavSerialNO + date + time;

            //string address = start + uavSerialNO + date + returnDate1;
            ////string result = address.Replace("\\", "");
            //StringBuilder sb = new StringBuilder();//要抓取的URL地址 
            //sb.Append(address);
            //string strWebContent = GetWebContent(sb.ToString());//得到指定Url的源码 
            //ViewData["video"] = address;//将得到的字符串存储传到前端
            //string s = "url\":\"";
            //string e = "\",\"";
            //Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
           
            //ViewData["video1"] = rg.Match(strWebContent);
            //ViewData["lastlongitude"] = model.Longitude;
            //ViewData["lastlatitude"] = model.Latitude;
            
            BLL_UavSortieData sortiebll = new BLL_UavSortieData();
            List<SUC_UavSortieData> dt = sortiebll.GetUavListByNUMandS(UavSerialNO,sortie);
            SUC_UavSortieData sortiemodel = sortiebll.GetUavSortieByNUMandS(UavSerialNO, sortie);
 

           
                
            
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
            //}
            //else
            //{
            //    return View();
            //}
        }
        [HttpGet]
        public ActionResult Details()
        {
          
            return View();
        }

       
          [AcceptVerbs(HttpVerbs.Post)]
          
          [ValidateInput(false)]
          public JsonResult GetVideoTime(string currentTime,string UavSerialNO,string Sortie)
          {
              Common.ResultRes res = new Common.ResultRes();
              int time = int.Parse(currentTime);
              BLL_UavInfo bllData = new BLL_UavInfo();
              SUC_UavInfo Model1 = bllData.GetUavListBysortiefirst(UavSerialNO,Sortie);
              DateTime datetime = DateTime.Parse(Model1.Rec_CreateTime.ToString());
              string defaultYear = datetime.Year.ToString("d4");
              string defaultMonth = datetime.Month.ToString("d2");
              int defaultDay = int.Parse(datetime.Day.ToString("d2"));
              string day1=defaultDay.ToString("d2");
              int Sec = int.Parse(datetime.Second.ToString("d2"));
              int Min = int.Parse(datetime.Minute.ToString("d2"));
              int Hour = int.Parse(datetime.Hour.ToString("d2"));
              int alltime = time + Sec + Min * 60 + Hour * 3600;
              TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(alltime));

              string Sec1 =ts.Seconds.ToString("d2");
              string Min1 =ts.Minutes.ToString("d2");
              string Hour1 =ts.Hours.ToString("d2");
              if(ts.Hours==24)
              {
                  Hour1 = "00";
              }
              if (ts.Hours >24)
              {
                  Hour1 = (ts.Hours - 24).ToString("d2");
                  day1 = (defaultDay+1).ToString("d2");
              }
              string resultdate = defaultYear + "-" + defaultMonth + "-" + day1 + " " + Hour1 + ":" + Min1 + ":" + Sec1;
              DateTime date1 = Convert.ToDateTime(resultdate);
              SUC_UavInfo Model = bllData.GetDataListbyCreateTime(date1, Sortie);

              if (bll != null)
              {
                  res.IsSuccess = true;
                  res.ResultValue = Model;
                  return Json(res, JsonRequestBehavior.AllowGet);
              }
              else
              {
                  res = new Common.ResultRes();
                  res.IsSuccess = true;
                  return Json(res, JsonRequestBehavior.AllowGet);
              }
          }
          public ActionResult GetGridJson(Pagination pagination, string keyword)
          {

              BLL_UavSortieData sortiebll = new BLL_UavSortieData();

              List<SUC_UavSortieData> q = sortiebll.GetUavLatestList(pagination, keyword);
              var data = new
              {
                  rows = q,
                  total = pagination.total,
                  page = pagination.page,
                  records = pagination.records
              };
              return Content(data.ToJson());
          }

    }
}
