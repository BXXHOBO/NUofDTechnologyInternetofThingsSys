using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using SUC_EntityBLL;
using System.Data;

namespace ZFine.Web.Areas.Interface
{
    /// <summary>
    /// WechatPushHandler 的摘要说明
    /// </summary>
    public class WechatPushHandler : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Params["type"];
            string json = "No Type!";
            switch (action)
            {
                case "WeChatPush":
                    json = WeChatPush(context);
                    break;
                case "SendToWeChat":
                    json=SendToWeChat(context);
                    break;

            }
            context.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(json);
        }

        public void ServerChanPush(int flag ,string text, string desp)
        {
            ResultRes res = new ResultRes();


            DataSet ds = SY_BLL.BLL_UserDataGet.GetWeChatIdOfUser();
            string sendkey = ds.Tables[0].Rows[0][0].ToString();

            //string sendkey = "orPQ802JlyxMTtvgw1AdfrEzI_8MFe8ASZaAgL";
            //string sendkey = "orPQ802JlyxMTtvgw1AdfrEzI_8MFe8ASZaAgL,orPQ801B6cAPRWJypSGxPjea0ryo8B6jfVYimc,";
            //string url = string.Format("https://pushbear.ftqq.com/sub?sendkey={0}&text={1}&desp={2}", sendkey, text, desp);

            string sendUrl = string.Format("http://wxmsg.dingliqc.com/send?userIds={0}&msg={1}&title={2}", sendkey, desp,text);

            string getRes = GetHttpResponse(sendUrl, 6000);
            if (getRes != null)
            {
                res.IsSuccess = true;
                res.Msg = string.Empty;
            }
            else
            {
                res.IsSuccess = false;
                res.Msg = string.Empty;
            }
        }
        /** **/
        public void ServerChanPushOnly(int flag, string text, string desp,String id)
        {
            ResultRes res = new ResultRes();


            DataSet ds = SY_BLL.BLL_UserDataGet.GetWeChatIdOfUserId(id);
            string sendkey = ds.Tables[0].Rows[0][0].ToString();

            //string sendkey = "orPQ802JlyxMTtvgw1AdfrEzI_8MFe8ASZaAgL";
            //string sendkey = "orPQ802JlyxMTtvgw1AdfrEzI_8MFe8ASZaAgL,orPQ801B6cAPRWJypSGxPjea0ryo8B6jfVYimc,";
            //string url = string.Format("https://pushbear.ftqq.com/sub?sendkey={0}&text={1}&desp={2}", sendkey, text, desp);

            string sendUrl = string.Format("http://wxmsg.dingliqc.com/send?userIds={0}&msg={1}&title={2}", sendkey, desp, text);

            string getRes = GetHttpResponse(sendUrl, 6000);
            if (getRes != null)
            {
                res.IsSuccess = true;
                res.Msg = string.Empty;
            }
            else
            {
                res.IsSuccess = false;
                res.Msg = string.Empty;
            }
        }
   

        #region 微信发送Server酱
        private string WeChatPush(HttpContext context)
        {
            ResultRes res = new ResultRes();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string sendkey = "2643-1b52f17faa2dd0c2d41bd190f5bc9b28";

                string text = context.Request.Params["text"] ?? "";
                string desp = context.Request.Params["desp"] ?? "";
                if (string.IsNullOrEmpty(text))
                {
                    res.IsSuccess = false;
                    res.Msg = "text不能为空！";
                }
                else if (string.IsNullOrEmpty(desp))
                {
                    res.IsSuccess = false;
                    res.Msg = "desp不能为空！";
                }
                else
                {
                    string url = string.Format("https://pushbear.ftqq.com/sub?sendkey={0}&text={1}&desp={2}", sendkey, text, desp);
                    string getRes = GetHttpResponse(url, 6000);
                    if (getRes != null)
                    {
                        res.IsSuccess = true;
                        res.Msg = string.Empty;
                    }
                    else
                    {
                        res.IsSuccess = false;
                        res.Msg = string.Empty;
                    }
                }
                string json = jss.Serialize(res);
                return json;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Msg = "发送异常" + ex.Message;
                string json = jss.Serialize(res);
                return json;
            }
        }
        #endregion 微信发送Server酱

        ///
        /// Get请求
        /// 
        /// 
        /// 字符串
        private string GetHttpResponse(string url, int Timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = "DefaultUserAgent";
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #region 微信发送pusher
        private string SendToWeChat(HttpContext context)
        {
           

            ResultRes res = new ResultRes();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string sendkey = "orPQ802JlyxMTtvgw1AdfrEzI_8MFe8ASZaAgL";

                string text = context.Request.Params["text"] ?? "";
                string desp = context.Request.Params["desp"] ?? "";
                if (string.IsNullOrEmpty(text))
                {
                    res.IsSuccess = false;
                    res.Msg = "text不能为空！";
                }
                else if (string.IsNullOrEmpty(desp))
                {
                    res.IsSuccess = false;
                    res.Msg = "desp不能为空！";
                }
                else
                {
                    string sendUrl = string.Format("http://wxmsg.dingliqc.com/send?userIds={0}&msg={1}&title={2}", sendkey,desp,text);
                    string getRes = GetHttpResponse(sendUrl, 6000);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sendUrl);

                    request.Method = "GET";
                    request.ContentType = "text/html;charset=UTF-8";
                    request.UserAgent = null;

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();

                    if (getRes != null)
                    {
                        res.IsSuccess = true;
                        res.Msg = string.Empty;
                    }
                    else
                    {
                        res.IsSuccess = false;
                        res.Msg = string.Empty;
                    }
                }
                string json = jss.Serialize(res);
                return json;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Msg = "发送异常" + ex.Message;
                string json = jss.Serialize(res);
                return json;
            }
           
        }
        #endregion
    }
}