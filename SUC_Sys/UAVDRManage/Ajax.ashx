<%@ WebHandler Language="C#" Class="Ajax" %>

using System;
using System.Web;
using System.Data;
using System.Text;
public class Ajax : IHttpHandler {

    HttpContext ctx;
    
    public void ProcessRequest (HttpContext context) {
        ctx = context;
        var w = Common.Request.GetFormString("w");
        switch (w)
        {
            case "GetTempBySortie": GetTempBySortie(); break;
            case "GetHumidityBySortie": GetHumidityBySortie(); break;
            case "GetTempandHumidityBySortie": GetTempandHumidityBySortie(); break;
            case "GetTandHavgInfo": GetTandHavgInfo(); break;
            case "GetUavWorkStateInfo": GetUavWorkStateInfo(); break;
        }
            
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

  
    public void GetTempBySortie()
    {
        var UavSerialNO = Common.Request.GetFormString("UavSerialNO");
        var Sortie = Common.Request.GetFormString("Sortie");
        YYCMS.Cld_UavInfo Dal = new YYCMS.Cld_UavInfo();
        //DataSet ds = Dal.GetTemperatureInfoBySortie(sortie);
        DataSet ds = Dal.GetTimeInfoBySortie(UavSerialNO,Sortie);
         StringBuilder sb = new StringBuilder();
         sb.Append("{");
         sb.Append("\"datatime\":[");
             

             int i = 0;
             foreach (DataRow dr in ds.Tables[0].Rows)
             {
                 if (i == (ds.Tables[0].Rows.Count - 1))
                 {
                     sb.AppendFormat("\"{0}\"", dr["Rec_CreateTime"].ToString());
                 }
                 else
                 {
                     sb.AppendFormat("\"{0}\",", dr["Rec_CreateTime"].ToString());
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
                     sb.AppendFormat("{0}", dr["Temperature"].ToString());
                 }
                 else
                 {
                     sb.AppendFormat("{0},", dr["Temperature"].ToString());
                 }
                 j++;
             }
             sb.Append("]");

         sb.Append("}");
         ctx.Response.ContentType = "text/json";
         ctx.Response.Write(sb.ToString());
    }

    public void GetHumidityBySortie()
    {
        var sortie = Common.Request.GetFormString("Sortie");
        var UavSerialNO = Common.Request.GetFormString("UavSerialNO");
        YYCMS.Cld_UavInfo Dal = new YYCMS.Cld_UavInfo();


        DataSet ds = Dal.GetHumidityInfoBySortie(UavSerialNO,sortie);
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"datatime\":[");


        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["Rec_CreateTime"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["Rec_CreateTime"].ToString());
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
                sb.AppendFormat("{0}", dr["Humidity"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["Humidity"].ToString());
            }
            j++;
        }
        sb.Append("]");

        sb.Append("}");
        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }

    public void GetTempandHumidityBySortie()
    {
        var sortie = Common.Request.GetFormString("sortie");
        YYCMS.Cld_UavInfo Dal = new YYCMS.Cld_UavInfo();

        DataSet ds = Dal.GetTempandHumidityInfoBySortie(sortie);
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"datatime\":[");


        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["Rec_CreateTime"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["Rec_CreateTime"].ToString());
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
                sb.AppendFormat("{0}", dr["Temperature"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["Temperature"].ToString());
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
                sb.AppendFormat("{0}", dr["Humidity"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["Humidity"].ToString());
            }
            k++;
        }
        sb.Append("]");
        sb.Append("}");
        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }
    public void GetTandHavgInfo()
    {
       
        YYCMS.Cld_UavInfo Dal = new YYCMS.Cld_UavInfo();
        DateTime time = DateTime.Now;
        int year = Convert.ToInt32(time.Year.ToString("d4"));
        
        DataSet ds = Dal.GetAvgTandHInfo(year);
        
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"datatime\":[");


        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (i == (ds.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}月\"", dr["Month"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}月\",", dr["Month"].ToString());
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
                sb.AppendFormat("{0}", dr["Tempavg"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["Tempavg"].ToString());
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
                sb.AppendFormat("{0}", dr["Humavg"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["Humavg"].ToString());
            }
            k++;
        }
        sb.Append("]");
        sb.Append("}");
        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }

    public void GetUavWorkStateInfo()//获取设备作业信息值
    {

        YYCMS.Cld_UavData Dal = new YYCMS.Cld_UavData();

        string state1 = "0";
        string state2 = "1";
        string state3 = "2";

        DataSet ds1 = Dal.GetWorkUavInfo(state1);
        DataSet ds2 = Dal.GetWorkUavInfo(state2);
        DataSet ds3 = Dal.GetWorkUavInfo(state3);
        
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"dataitems1\":[");


        int i = 0;
        foreach (DataRow dr in ds1.Tables[0].Rows)
        {
            if (i == (ds1.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("\"{0}\"", dr["WorkState1"].ToString());
            }
            else
            {
                sb.AppendFormat("\"{0}\",", dr["WorkState1"].ToString());
            }

            i++;
        }
        sb.Append("],");
        sb.Append("\"dataitems2\":[");
        int j = 0;
        foreach (DataRow dr in ds2.Tables[0].Rows)
        {
            if (j == (ds2.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["WorkState1"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["WorkState1"].ToString());
            }
            j++;
        }
        sb.Append("],");
        sb.Append("\"dataitems3\":[");
        int k = 0;
        foreach (DataRow dr in ds3.Tables[0].Rows)
        {
            if (k == (ds3.Tables[0].Rows.Count - 1))
            {
                sb.AppendFormat("{0}", dr["WorkState1"].ToString());
            }
            else
            {
                sb.AppendFormat("{0},", dr["WorkState1"].ToString());
            }
            k++;
        }
        sb.Append("]");
        sb.Append("}");
        ctx.Response.ContentType = "text/json";
        ctx.Response.Write(sb.ToString());
    }
    
}