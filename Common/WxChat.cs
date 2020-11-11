using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class WxChat
    {
        public void ServerChanPush(int flag, string desp)
        {
            WeChartInterfaceApi.WeChartInterfaceSoapClient model = new WeChartInterfaceApi.WeChartInterfaceSoapClient();
            ResultRes res = new ResultRes();


            string projectCode = "LHCX";
            string sendkey = "";//用户ID，可用于一对一发送
            string appToken = "";
            string topicId = "";
            string url = "";

            DataSet ds = model.GetWeChatPortInfor(projectCode);
            DataRow dr = ds.Tables[0].Rows[0];
            if ((ds.Tables[0].Rows.Count > 0) && (dr["ProjectCode"] != System.DBNull.Value))
            {
                appToken = dr["AppTokenCode"].ToString();
                sendkey = dr["UserWeChartId"].ToString();
                topicId = dr["TopicId"].ToString();
                url = dr["Url"].ToString();
            }
            string sendUrl = string.Format("http://wxpusher.zjiecode.com/api/send/message?appToken={0}&uid={1}&content={2}&contentType=1&topicId={3}&url={4}", appToken, sendkey, desp, topicId, url);

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


    }
}
