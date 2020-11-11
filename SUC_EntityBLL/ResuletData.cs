using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUC_EntityBLL
{
    public class ResuletData
    {
        public int total { get; set; }
        public List<Result> result { get; set; }

    }

    public class Result
    {
        // 更新时间
        public string JCSJ { get; set; }
        // 站点名称
        public string JCDD { get; set; }
        // 城市名称
        public string SZS { get; set; }
        // 城市编码
        public string SZSDM { get; set; }
        // 空气质量等级
        public string KQJB { get; set; }
        // 经度
        public string JD { get; set; }
        // 纬度
        public string WD { get; set; }
        public string AQI { get; set; }
        public string SO2 { get; set; }
        public string NO2 { get; set; }
        public string CO { get; set; }
        public string O3 { get; set; }
        public string PM25 { get; set; }
        public string PM10 { get; set; }
        // 主要污染物
        public string SYWRW { get; set; }
    }

}