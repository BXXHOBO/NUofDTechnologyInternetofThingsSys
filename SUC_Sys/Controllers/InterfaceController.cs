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
        [HttpGet]
       
        public ActionResult Index()
        {
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

            return View();
        }


             protected void Page_Load(object sender, EventArgs e)
            {
               
              
               
            }
  
      
          


    }
}
