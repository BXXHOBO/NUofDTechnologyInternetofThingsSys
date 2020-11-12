using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SUC_Sys.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //默认加载数据
        public ActionResult SearchMap()
        {
            //默认有两个标注
            List<Map> mapList = new List<Map>() {
                new Map(){ CenterLat=108.2849540M, CenterLng=22.86446101M, Zoom=1},
                new Map(){ CenterLat=108.2143900M, CenterLng=22.8398250M, Zoom=2},
                new Map(){ CenterLat=108.3389230M, CenterLng=22.7392360M, Zoom=2},
                new Map(){ CenterLat=108.3942690M, CenterLng=22.8066730M, Zoom=3},
                new Map(){ CenterLat=110.1886470M, CenterLng=25.2980440M, Zoom=3},
                new Map(){ CenterLat=108.3098410M, CenterLng=22.7893230M, Zoom=4},
                new Map(){ CenterLat=108.0717310M, CenterLng=24.7033940M, Zoom=4},
                new Map(){ CenterLat=108.5322380M, CenterLng=22.7332470M, Zoom=5},
                new Map(){ CenterLat=109.4159600M, CenterLng=24.2808630M, Zoom=5},

                new Map(){ CenterLat=108.3310700M, CenterLng=22.8255700M, Zoom=6},
                new Map(){ CenterLat=108.3296930M, CenterLng=22.8626140M, Zoom=6},

                new Map(){ CenterLat=108.4571610M, CenterLng=22.8148210M, Zoom=7},
                new Map(){ CenterLat=108.2812870M, CenterLng=22.8688780M, Zoom=7},

                new Map(){ CenterLat=113.929963M, CenterLng=22.530031M, Zoom=12},
                new Map(){ CenterLat=113.925076M, CenterLng=22.498781M, Zoom=12},
        };
            StringBuilder str = new StringBuilder();
            foreach (Map map in mapList)
            {
                str.Append(string.Format("{0},{1},{2};",
                       map.CenterLat,
                       map.CenterLng,
                       map.Zoom
                      ));
            }
            return Json(str.ToString(), JsonRequestBehavior.AllowGet);
        }


    }
    //标注实体
    public class Map
    {
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? CenterLng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? CenterLat { get; set; }

        /// <summary>
        /// 比列尺
        /// </summary>
        public decimal? Zoom { get; set; }

    }
}