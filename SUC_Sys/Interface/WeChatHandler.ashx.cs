using Common.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace ZFine.Web.Areas.Interface
{
    /// <summary>
    /// WeChatHandler 的摘要说明
    /// </summary>
    public class WeChatHandler : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            // 该处配置，已经配置到微信公众好端了
            var sr = new StreamReader(context.Request.InputStream);
            var strXmlData = sr.ReadToEnd();

         

            if (string.IsNullOrEmpty(strXmlData)) //客户端可公众号交互了数据后，可获取事件和消息体。获得openid
            {
                //异常日志
            }
            else
            {
                // 保存了openId 
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXmlData);
                string enventKey = doc.SelectSingleNode("/xml/Event").InnerText;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("OK"); //该回复必须有,否则微信会连续操作3次,微信会认为公众号,错误,并且会通知请求的用户,该公众号有问题.
        }
        /// <summary>
        /// 服务端发送程序.必须还要重新调整到服务端.目前就先放在这里.
        /// </summary>
        public void sendWecharMsg()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=ACCESS_TOKEN";
            //   access_token  要自己保存在系统缓存中或者其他地方，2小时微信过期，建议90分钟，每天最多2000次。
            string msg = @" {";  // 消息体按照 具体模板调整
            msg += "  \"touser\":\"OPENID\",";
            msg += "  \"template_id\":\"ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY\",";
            msg += "  \"url\":\"http://weixin.qq.com/download\",  ";
            msg += "   \"miniprogram\":{";
            msg += "     \"appid\":\"xiaochengxuappid12345\",";
            msg += "      \"pagepath\":\"index?foo=bar\"";
            msg += "    },          ";
            msg += "    \"data\":{";
            msg += "\"first\": {";
            msg += "    \"value\":\"恭喜你购买成功！\",";
            msg += "    \"color\":\"#173177\"";
            msg += "   },";
            msg += "   \"keynote1\":{";
            msg += "      \"value\":\"巧克力\",";
            msg += "    \"color\":\"#173177\"";
            msg += "  },";
            msg += "  \"keynote2\": {";
            msg += "     \"value\":\"39.8元\",";
            msg += "     \"color\":\"#173177\"";
            msg += "  },";
            msg += " \"keynote3\": {";
            msg += "    \"value\":\"2014年9月22日\",";
            msg += "     \"color\":\"#173177\"";
            msg += "  },";
            msg += "  \"remark\":{";
            msg += "    \"value\":\"欢迎再次购买！\",";
            msg += "    \"color\":\"#173177\"";
            msg += "  }";

            // TODO post msg to weCharUrl;


            //  Http 方法中的    UploadString post数据

            //  Http 方法中的     getstring 获取ACtoken
            // Http 方法中的 post
            MyHttpClient httpClient = new MyHttpClient();

            string woaapDomain = "";//域名
            string appid = "";// 公众号的APPid
            string apiToken = "";// 公众号的AC Token
            httpClient.Url = string.Format("{0}api/message-template-send?appid={1}&token={2}", woaapDomain, appid, apiToken);
            string strResult = httpClient.UploadString(msg);
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