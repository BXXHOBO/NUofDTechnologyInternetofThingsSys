<%@ WebHandler Language="C#" Class="Ajax" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class Ajax : IHttpHandler {
    HttpContext ctx;
    public void ProcessRequest (HttpContext context) {
        ctx = context;
        var w = YYCMS.Request.GetFormString("w");
        var siteCode = YYCMS.Request.GetFormString("siteCode");
        var TypeValue = YYCMS.Request.GetFormString("TypeValue");
        var startDate = YYCMS.Request.GetFormString("startDate");
        var endDate = YYCMS.Request.GetFormString("endDate");
        switch (w)
        {
            case "GetDeviceTypeInfoByMonth": GetDeviceTypeInfoByMonth(); break;
            case "GetDeviceTypeInfoForPie": GetDeviceTypeInfoForPie(); break;
            case "GetMaintenanceStatusInfoForPie": GetMaintenanceStatusInfoForPie(); break;
            case "GetDeviceTypeInfo": GetDeviceTypeInfo(); break;
            case "GetAQIGradeGroup": GetAQIGradeGroup(); break;
            case "GetTemperatureValue": GetTemperatureValue(); break;
            case "GetHumidityValue": GetHumidityValue(); break;
            case "GetSiteDataPolluteValue": GetSiteDataPolluteValue(); break;
            case "GetSiteDataChangeValueInThisDay": GetSiteDataChangeValueInThisDay(siteCode); break;
            case "GetSiteDataOfEachHour": GetSiteDataOfEachHour(siteCode); break;
            case "GetSiteDataPolluteRankingValue": GetSiteDataPolluteRankingValue(TypeValue); break;
            case "GetPM25andPM10Rate": GetPM25andPM10Rate(startDate, endDate); break;
           


        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public void  GetDeviceTypeInfoByMonth()
    {
        var year = YYCMS.Request.GetFormInt("year", 0);
        YYCMS.EPS_Device Dal = new YYCMS.EPS_Device();
        DataSet ds = Dal.GetDeviceTypeInfoByYear(year);
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"datamonths\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}月\"", dr["DeviceMonth"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}月\",", dr["DeviceMonth"].ToString());
            }

            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["DeviceSumType"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["DeviceSumType"].ToString());
            }
            j++;
        }
        sb.Append("]");
        sb.Append("}");
        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }
    public void GetDeviceTypeInfoForPie()
    {

        YYCMS.EPS_Device Dal = new YYCMS.EPS_Device();
        DataSet ds = Dal.GetDeviceTypeInfoForPie();

        StringBuilder sb = new StringBuilder();
        //{"dataproname":["打印机","电视机"],"dataitems":[{value:335, name:"直接访问"},{value:335, name:"直接访问"}]}
        sb.Append("{");
        sb.Append("\"dataproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["DeviceType"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["DeviceType"].ToString());
            }
            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sb.Append("{");

            sb.AppendFormat("\"value\":{0}, \"name\":\"{1}\"", dr["DeviceTypeCounts"].ToString(), dr["DeviceType"].ToString());

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.Append("}");
            }
            else
            {
                sb.Append("},");
            }

            j++;
        }
        sb.Append("]");

        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }

    public void GetMaintenanceStatusInfoForPie()
    {

        YYCMS.EPS_DevLifecycle Dal = new YYCMS.EPS_DevLifecycle();
        DataSet ds = Dal.GetMaintenanceStatusInfoForPie();

        StringBuilder sb = new StringBuilder();
        //{"dataproname":["打印机","电视机"],"dataitems":[{value:335, name:"直接访问"},{value:335, name:"直接访问"}]}
        sb.Append("{");
        sb.Append("\"dataproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["MaintenanceStatus"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["MaintenanceStatus"].ToString());
            }
            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sb.Append("{");

            sb.AppendFormat("\"value\":{0}, \"name\":\"{1}\"", dr["MaintenanceStatusCounts"].ToString(), dr["MaintenanceStatus"].ToString());

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.Append("}");
            }
            else
            {
                sb.Append("},");
            }

            j++;
        }
        sb.Append("]");

        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }
    public void GetDeviceTypeInfo()
    {
        YYCMS.EPS_DevLifecycle Dal = new YYCMS.EPS_DevLifecycle();
        DataSet ds = Dal.GetDeviceTypeInfo();

        StringBuilder sb = new StringBuilder();

        sb.Append("{");
        sb.Append("\"dataproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["DeviceType"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["DeviceType"].ToString());
            }
            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sb.Append("{");

            sb.AppendFormat("\"value\":{0}, \"name\":\"{1}\"", dr["DeviceTypeCounts"].ToString(), dr["DeviceType"].ToString());

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.Append("}");
            }
            else
            {
                sb.Append("},");
            }

            j++;
        }
        sb.Append("]");

        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }

    public void GetAQIGradeGroup()
    {
        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        DataSet ds = Dal.GetSumAQIGradeForMonth();

        StringBuilder sb = new StringBuilder();

        sb.Append("{");
        sb.Append("\"dataproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}月\"", dr["month_name"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}月\",", dr["month_name"].ToString());
            }
            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems1\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["nice"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["nice"].ToString());
            }

            j++;
        }
        sb.Append("],");
        sb.Append("\"dataitems2\":[");
        int k = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (k == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["good"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["good"].ToString());
            }

            k++;
        }
        sb.Append("],");
        sb.Append("\"dataitems3\":[");
        int l = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (l == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["mild"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["mild"].ToString());
            }

            l++;
        }
        sb.Append("],");
        sb.Append("\"dataitems4\":[");
        int m = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (m == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["middle"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["middle"].ToString());
            }

            m++;
        }
        sb.Append("],");
        sb.Append("\"dataitems5\":[");
        int n = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (n == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["serious"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["serious"].ToString());
            }

            n++;
        }
        sb.Append("],");
        sb.Append("\"dataitems6\":[");
        int r = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (r == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["severe"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["severe"].ToString());
            }

            r++;
        }
        sb.Append("]");
        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }

    public void GetTemperatureValue()
    {

        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        DataSet ds = Dal.GetTemperatureForDay();
        DataRow dr = ds.Tables[0].Rows[0];

        string datatemp = dr["Temperature"].ToString();

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(datatemp.ToString());
    }
    public void GetHumidityValue()
    {

        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        DataSet ds = Dal.GetTemperatureForDay();
        DataRow dr = ds.Tables[0].Rows[0];

        string datatemp = dr["Humidity"].ToString();

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(datatemp.ToString());
    }

    public void GetSiteDataPolluteValue()
    {
        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        DataSet ds = Dal.GetSiteDataPolluteNum();

        StringBuilder sb = new StringBuilder();

        sb.Append("{");
        sb.Append("\"dataproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["SiteName"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["SiteName"].ToString());
            }
            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["Num"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["Num"].ToString());
            }

            j++;
        }
        sb.Append("]");

        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }
    public void GetSiteDataPolluteRankingValue(string TypeValue)
    {
        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        int dataType = Convert.ToInt32(TypeValue);
        DataSet ds = Dal.GetSiteDataPollutionRankingNum(dataType);

        StringBuilder sb = new StringBuilder();

        sb.Append("{");
        sb.Append("\"dataproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["SiteName"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["SiteName"].ToString());
            }
            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["Num"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["Num"].ToString());
            }

            j++;
        }
        sb.Append("]");

        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }
    public void GetSiteDataChangeValueInThisDay(string siteCode)
    {
        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        DataSet ds = Dal.GetSiteDataChangeInThisDay(siteCode);

        StringBuilder sb = new StringBuilder();

        sb.Append("{");
        sb.Append("\"dataproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}时\"", dr["hour_name"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}时\",", dr["hour_name"].ToString());
            }
            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems1\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["PM_25"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["PM_25"].ToString());
            }

            j++;
        }
        sb.Append("],");
        sb.Append("\"dataitems2\":[");
        int k = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (k == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["PM10"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["PM10"].ToString());
            }

            k++;
        }
        sb.Append("],");
        sb.Append("\"dataitems3\":[");
        int l = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (l == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["SO2"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["SO2"].ToString());
            }

            l++;
        }
        sb.Append("],");
        sb.Append("\"dataitems4\":[");
        int m = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (m == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["NO2"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["NO2"].ToString());
            }

            m++;
        }
        sb.Append("],");
        sb.Append("\"dataitems5\":[");
        int n = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (n == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["CO"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["CO"].ToString());
            }

            n++;
        }
        sb.Append("],");
        sb.Append("\"dataitems6\":[");
        int r = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {


            if (r == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["O3"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["O3"].ToString());
            }

            r++;
        }
        sb.Append("]");
        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }
    public void GetSiteDataOfEachHour(string siteCode)
    {
        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        DataSet ds = Dal.GetSiteDataOfEachHour(siteCode);

        StringBuilder sb = new StringBuilder();

        sb.Append("{");
        sb.Append("\"yaxisproname\":[");
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}-{1}-{2}\"", dr["year_name"].ToString(), dr["month_name"].ToString(), dr["day_name"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}-{1}-{2}\",", dr["year_name"].ToString(), dr["month_name"].ToString(), dr["day_name"].ToString());
            }
            i++;
        }
        sb.Append("],");

        sb.Append("\"dataitems\":[");
        int j = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (j == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("[{0},{1},{2}]", dr["hour_name"].ToString(), dr["day_name"].ToString(), dr["AQI"].ToString());
            }
            else
            {
                sb.AppendFormat("[{0},{1},{2}],", dr["hour_name"].ToString(), dr["day_name"].ToString(), dr["AQI"].ToString());
            }

            j++;
        }
        sb.Append("]");
        sb.Append("}");

        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());

    }


    public void GetPM25andPM10Rate(string startDate,string endDate)
    {
        YYCMS.EPS_SiteData Dal = new YYCMS.EPS_SiteData();
        StringBuilder sb = new StringBuilder();


        if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate) && startDate != "" && endDate != "")
        {
            DateTime startdate = Convert.ToDateTime(startDate);
            DateTime enddate = Convert.ToDateTime(endDate);
            DataSet SiteNameTable = Dal.GetSiteNameCount(startdate, enddate);

            string siteName = "";
            int[] data = new int[1000];
            //string[] data = new string[24];
            sb.Append("{");
            sb.Append("\"dataxname\":[");

            int i = 0;
            foreach (DataRow dr in SiteNameTable.Tables[0].Rows)
            {
                if (i == (SiteNameTable.Tables[0].Rows.Count - 1))
                {
                    siteName = dr["SiteName"].ToString();
                    DataSet ds = Dal.GetPM25andPM10Rate(startdate, enddate, siteName);
                    sb.Append("{");
                    sb.AppendFormat("\"name\":\"{0}\",\"type\":\"line\",\"data\":", dr["SiteName"].ToString());
                    sb.Append("[");

                    int j = 0;
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {



                        if (j == (ds.Tables[0].Rows.Count - 1))
                        {
                            float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                            //data[j] = (percent * 100).ToString();
                            string percent1 = percent.ToString("F2");
                            double percent2 = System.Convert.ToDouble(percent1);
                            data[j] =(int)(percent2* 100) ;
                            //data[j] = Convert.ToInt32(percent * 100);

                            sb.AppendFormat("{0}", data[j]);
                        }
                        else
                        {
                            string pm25 = dr1["PM_25"].ToString();
                            if(string.IsNullOrEmpty(pm25)||pm25=="")
                            {
                                data[j]= 0;
                            }
                            else
                            {
                                float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                                string percent1 = percent.ToString("F2");
                                double percent2 = System.Convert.ToDouble(percent1);
                                data[j] =(int)(percent2* 100) ;
                            }


                            sb.AppendFormat("{0},", data[j].ToString());
                        }

                        j++;
                    }

                    sb.Append("]");

                    sb.Append("}");
                }
                else
                {
                    siteName = dr["SiteName"].ToString();
                    DataSet ds = Dal.GetPM25andPM10Rate(startdate, enddate, siteName);
                    sb.Append("{");
                    sb.AppendFormat("\"name\":\"{0}\",\"type\":\"line\",\"data\":", dr["SiteName"].ToString());
                    sb.Append("[");
                    int j = 0;
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        if (j == (ds.Tables[0].Rows.Count - 1))
                        {
                            float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                            string percent1 = percent.ToString("F2");
                            double percent2 = System.Convert.ToDouble(percent1);
                            data[j] =(int)(percent2* 100) ;

                            sb.AppendFormat("{0}", data[j]);
                        }
                        else
                        {
                            string pm25 = dr1["PM_25"].ToString();
                            if(string.IsNullOrEmpty(pm25)||pm25=="")
                            {
                                data[j]= 0;
                            }
                            else
                            {
                                float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                                string percent1 = percent.ToString("F2");
                                double percent2 = System.Convert.ToDouble(percent1);
                                data[j] =(int)(percent2* 100) ;
                            }


                            sb.AppendFormat("{0},", data[j].ToString());
                        }

                        j++;
                    }
                    sb.Append("]");

                    sb.Append("},");
                }
                i++;
            }
            sb.Append("]");
            sb.Append("}");

        }
        else
        {

            DataSet SiteNameTable = Dal.GetSiteNameTotal();

            string siteName = "";
            int[] data = new int[1000];
            //string[] data = new string[24];
            sb.Append("{");
            sb.Append("\"dataxname\":[");

            int i = 0;
            foreach (DataRow dr in SiteNameTable.Tables[0].Rows)
            {
                if (i == (SiteNameTable.Tables[0].Rows.Count - 1))
                {
                    siteName = dr["SiteName"].ToString();
                    DataSet ds = Dal.GetPM25andPM10RateValue(siteName);
                    sb.Append("{");
                    sb.AppendFormat("\"name\":\"{0}\",\"type\":\"line\",\"data\":", dr["SiteName"].ToString());
                    sb.Append("[");
                    int j = 0;
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {

                        if (j == (ds.Tables[0].Rows.Count - 1))
                        {
                            float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                            string percent1 = percent.ToString("F2");
                            double percent2 = System.Convert.ToDouble(percent1);
                            data[j] =(int)(percent2* 100) ;

                            sb.AppendFormat("{0}", data[j]);
                        }
                        else
                        {
                            string pm25 = dr1["PM_25"].ToString();
                            if(string.IsNullOrEmpty(pm25)||pm25=="")
                            {
                                data[j]= 0;
                            }
                            else
                            {
                                float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                                string percent1 = percent.ToString("F2");
                                double percent2 = System.Convert.ToDouble(percent1);
                                data[j] =(int)(percent2* 100) ;
                            }

                            sb.AppendFormat("{0},", data[j].ToString());
                        }

                        j++;
                    }

                    sb.Append("]");

                    sb.Append("}");
                }
                else
                {
                    siteName = dr["SiteName"].ToString();
                    DataSet ds = Dal.GetPM25andPM10RateValue(siteName);
                    sb.Append("{");
                    sb.AppendFormat("\"name\":\"{0}\",\"type\":\"line\",\"data\":", dr["SiteName"].ToString());
                    sb.Append("[");
                    int j = 0;
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        if (j == (ds.Tables[0].Rows.Count - 1))
                        {
                            float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                            //data[j] = (percent * 100).ToString();

                            data[j] = Convert.ToInt32(percent * 100);

                            sb.AppendFormat("{0}", data[j]);
                        }
                        else
                        {
                            string pm25 = dr1["PM_25"].ToString();
                            if(string.IsNullOrEmpty(pm25)||pm25=="")
                            {
                                data[j]= 0;
                            }
                            else
                            {
                                float percent = Convert.ToSingle(dr1["PM_25"].ToString()) / Convert.ToSingle(dr1["PM10"].ToString());
                                string percent1 = percent.ToString("F2");
                                double percent2 = System.Convert.ToDouble(percent1);
                                data[j] =(int)(percent2* 100) ;
                            }


                            sb.AppendFormat("{0},", data[j].ToString());
                        }

                        j++;
                    }
                    sb.Append("]");

                    sb.Append("},");
                }
                i++;
            }
            sb.Append("]");
            sb.Append("}");

        }

        ctx.Response.ContentType = "text/json";
        //ctx.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(o1));
        ctx.Response.Write(sb.ToString());


    }
   

}