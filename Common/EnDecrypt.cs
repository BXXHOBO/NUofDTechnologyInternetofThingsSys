using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Common
{
    public class EnDecrypt
    {


        #region===========MD5加密(不可逆)

        /// <summary>
        /// MD5加密(不可逆)
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <returns>返回加密字符</returns>
        public static string MD5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] inputText = Encoding.Default.GetBytes(text);
            byte[] buffer = md5.ComputeHash(inputText);
            md5.Clear();
            string str = string.Empty;
            for (int i = 0; i < buffer.Length; i++)
            {
                str += buffer[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
        #endregion


        /// <summary>
        /// 数字换掉
        /// </summary>
        /// <param name="strpwd">pwd</param>
        /// <returns>pwd</returns>
        public static string ReplaceString(string strpwd)
        {
            string strpwd1 = strpwd.ToString();
            strpwd1 = strpwd1.Replace('0', ')');
            strpwd1 = strpwd1.Replace('1', '!');
            strpwd1 = strpwd1.Replace('2', '@');
            strpwd1 = strpwd1.Replace('3', '#');
            strpwd1 = strpwd1.Replace('4', '$');
            strpwd1 = strpwd1.Replace('5', '%');
            strpwd1 = strpwd1.Replace('6', '^');
            strpwd1 = strpwd1.Replace('7', '&');
            strpwd1 = strpwd1.Replace('8', '*');
            strpwd1 = strpwd1.Replace('9', '(');
            return strpwd1.ToString();
        }


        #region ==========加密(可逆)==========

        /// <summary>
        /// 加密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>加密(可逆)</returns>
        public static string Encrypt(string text)
        {
            return Encrypt(text, "ghiegcge");
        }
        /// <summary>
        /// 加密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="sKey">key</param>
        /// <returns>加密(可逆)</returns>
        public static string Encrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inpuText = Encoding.Default.GetBytes(text);

            des.IV = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile("sKey", "md5").Substring(0, 8));
            des.Key = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile("sKey", "md5").Substring(0, 8));

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inpuText, 0, inpuText.Length);
            cs.FlushFinalBlock();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        #endregion


        #region ============解密(可逆)=========
        /// <summary>
        /// 解密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>解密(可逆)</returns>
        public static string Decrypt(string text)
        {
            return Decrypt(text, "ghiegcge");
        }
        /// <summary>
        /// 解密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="sKey">key</param>
        /// <returns>解密(可逆)</returns>
        public static string Decrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputText = new byte[text.Length / 2];
            int i, x;
            for (x = 0; x < text.Length / 2; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputText[x] = (byte)i;
            }
            des.IV = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile("sKey", "md5").Substring(0, 8));
            des.Key = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile("sKey", "md5").Substring(0, 8));

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputText, 0, inputText.Length);
            cs.FlushFinalBlock();

            return Encoding.Default.GetString(ms.ToArray());

        }
        #endregion

        public static string QuestionType(string State)
        {
            string Str = string.Empty;
            switch (State)
            {
                //1.现场解答、2=现场调解、3=临时庇护、4=伤情鉴定、5=法律援助
                case "1":
                    Str = "现场解答";
                    break;
                case "2":
                    Str = "现场调解";
                    break;
                case "3":
                    Str = "临时庇护";
                    break;
                case "4":
                    Str = "伤情鉴定";
                    break;
                case "5":
                    Str = "法律援助";
                    break;
            }

            return Str;
        }


        #region 导入数据记录日志
        /// <summary>
        /// 导入数据记录
        /// </summary>
        /// <param name="Part">部分(1.开始部分2.中间部分3.结尾部分)</param>
        /// <param name="Count">总个数</param>
        /// <param name="messages">出错信息</param>
        /// <param name="correctnum">成功个数</param>
        public static void RecordLog(string Part, int Count, string messages, int correctnum)
        {
            var logPath = HttpContext.Current.Server.MapPath("/upload/Excel/recordlog");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            var logFilePath = string.Format("{0}/logs.txt", logPath);
            StreamWriter writer = null;
            try
            {
                switch (Part)
                {
                    case "0":
                        writer = new StreamWriter(logFilePath, true, Encoding.UTF8);
                        writer.WriteLine("------------------------------设备数据导入记录---------------------------");
                        writer.WriteLine("-------------------------------------------------------------------------");
                        writer.WriteLine("开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        writer.WriteLine("本次共导入" + Count + "条数据。");
                        writer.WriteLine("错误信息：" + messages);
                        writer.WriteLine("出错时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        break;
                    case "1":
                        writer = new StreamWriter(logFilePath, true, Encoding.UTF8);
                        writer.WriteLine("------------------------------设备数据导入记录---------------------------");
                        writer.WriteLine("-------------------------------------------------------------------------");
                        writer.WriteLine("开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        writer.WriteLine("本次共导入" + Count + "条数据。");
                        break;
                    case "2":
                        writer = new StreamWriter(logFilePath, true, Encoding.UTF8);
                        writer.WriteLine("错误信息：" + messages);
                        writer.WriteLine("出错时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        break;
                    case "3":
                        writer = new StreamWriter(logFilePath, true, Encoding.UTF8);
                        writer.WriteLine("总计：导入成功" + correctnum + "条数据，失败" + (Count - correctnum) + "条数据！");
                        writer.WriteLine("结束时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        writer.WriteLine("-------------------------------------------------------------------------");
                        writer.WriteLine();
                        break;
                    case "9":
                        writer = new StreamWriter(logFilePath, true, Encoding.UTF8);
                        writer.WriteLine("错误信息：" + messages);
                        writer.WriteLine("出错时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        writer.WriteLine("总计：导入成功" + correctnum + "条数据，失败" + (Count - correctnum) + "条数据！");
                        writer.WriteLine("结束时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        writer.WriteLine("-------------------------------------------------------------------------");
                        writer.WriteLine();
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        #endregion



    }
}
