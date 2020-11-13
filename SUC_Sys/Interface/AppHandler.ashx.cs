using Common;
using SUC_DataEntity;
using SUC_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ZFine.Web.Areas.Interface
{
    /// <summary>
    /// AppHandler 的摘要说明
    /// </summary>
    public class AppHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Params["type"];
            string json = "No Type!";
            switch (action)
            {
                //case "Login":
                //    json = Login(context);
                //    break;
               
            }

            context.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(json);
        }


     

     
 
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}