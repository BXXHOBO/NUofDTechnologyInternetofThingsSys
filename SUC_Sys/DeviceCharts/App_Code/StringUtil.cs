using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Data;

namespace YYCMS
{
    public class StringUtil
    {
        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="inputString">需要处理的字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns>截取指定长度的字符串</returns>
        public static string CutString(string inputString, int len)
        {

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";

            return tempString;
        }
        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string GetSpacesString(int spacesCount)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < spacesCount; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成指定数量的自定义符号
        /// </summary>
        public static string GetSpacesString(int spacesCount,string htmlstr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < spacesCount; i++)
            {
                sb.Append(htmlstr);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
        /// </summary>
        /// <param name="allChar"></param>
        /// <param name="CodeCount"></param>
        /// <returns></returns>
        private string GetRandomCode(string allChar, int CodeCount)
        {
            //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }


        /// <summary>
        /// 字符过滤
        /// </summary>
        /// <param name="AnyTypeString"></param>
        /// <param name="strReg"></param>
        /// <returns></returns>
        public static string ReturnCNStringfromAnyTypeString(string AnyTypeString, string strReg)
        {
            Regex r = new Regex(strReg);
            string str = null;
            char[] cStr = AnyTypeString.ToCharArray();
            for (int i = 0; i < cStr.Length; i++)
            {
                str = str + r.Match(cStr[i].ToString(), 0, 1);
            }
            return str;
        }


        /// <summary>
        /// 过滤html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlClear(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;

        }

        /// <summary>
        /// 修改特殊字符
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns></returns>
        static public string CheckStr(string str)
        {
            #region
            return str.Replace("<br />\r\n", "\r\n").Replace("&", "&amp;").Replace("'", "&apos;").Replace(@"""", "&quot;").Replace(" ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(" ", "&nbsp;").Replace(" where ", " wh&#101;re ").
                Replace(" select ", " sel&#101;ct ").Replace(" insert ", " ins&#101;rt ").Replace(" create ", " cr&#101;ate ").Replace(" drop ", " dro&#112 ").
                Replace(" alter ", " alt&#101;r ").Replace(" delete ", " del&#101;te ").Replace(" update ", " up&#100;ate ").Replace(" or ", " o&#114; ").Replace("\"", @"&#34;").
                Replace("\r\n", "<br />\r\n");
            #endregion
        }

        /// <summary>
        /// 恢复特殊字符
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns></returns>
        static public string UnCheckStr(string str)
        {
            #region
            return str.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&quot;", @"""").Replace("&nbsp;", " ").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", " ").Replace(" wh&#101;re ", " where ").
                Replace(" sel&#101;ct ", " select ").Replace(" ins&#101;rt ", " insert ").Replace(" cr&#101;ate ", " create ").Replace(" dro&#112 ", " drop ").
                Replace(" alt&#101;r ", " alter ").Replace(" del&#101;te ", " delete ").Replace(" up&#100;ate ", " update ").Replace(" o&#114; ", " or ").Replace(@"&#34;", "\"");
            #endregion
        }



        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="str">所要截取的字符串</param>
        /// <param name="num">截取字符串的长度</param>
        /// <returns></returns>
        static public string GetSubString(string str, int num)
        {
            #region
            return (str.Length > num) ? str.Substring(0, num) + "..." : str;
            #endregion
        }

        /// <summary>
        /// 过滤输入信息
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public static string InputText(string text, int maxLength)
        {
            #region
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);//截断字符串
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            return text;
            #endregion
        }

        /// <summary>
        /// 获取汉字第一个拼音，一个中文字符串每个字拼音的第一个字母组成的字符串
        /// </summary>
        /// <param name="input">中文字符串</param>
        /// <returns>每个字拼音的第一个字母组成的字符串</returns>
        static public string getSpells(string input)
        {
            #region
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += getSpell(input.Substring(i, 1));
            }
            return reVal;
            #endregion
        }

        /// <summary>
        /// 获取汉字拼音的第一个字母
        /// </summary>
        /// <param name="cn">中文字符串</param>
        /// <returns>字符串第一个字拼音的第一个字母</returns>
        static public string getSpell(string cn)
        {
            #region
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
            #endregion
        }


        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        static public string GetQuanJiao(string BJstr)
        {
            #region
            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            string strNew = new string(c);
            return strNew;

            #endregion
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetBanJiao(string QJstr)
        {
            #region
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;
            #endregion
        }

        /// <summary>
        /// 删除最后结尾的一个逗号//前提是必须含有
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }
        /// <summary>
        /// 根据新闻标题的属性设置返回设置后的标题
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="TitleColor">标题颜色</param>
        /// <param name="IsB">是否粗体</param>
        /// <param name="IsI">是否斜体</param>
        /// <param name="TitleNum">返回标题字数</param>
        /// <returns>返回设置后的标题</returns>
        public static string GetColorTitleSubStr(string Title, string TitleColor, int IsB, int IsI, int TitleNum)
        {
            string Return_title = "";
            string FormatTitle = Title;//此处去除title外的html更好
            if (FormatTitle != null && FormatTitle != string.Empty)
            {
                FormatTitle = GetSubString(FormatTitle, TitleNum);
                if (IsB == 1)
                {
                    FormatTitle = "<b>" + FormatTitle + "</b>";
                }
                if (IsI == 1)
                {
                    FormatTitle = "<i>" + FormatTitle + "</i>";
                }
                if (TitleColor != null && TitleColor != string.Empty)
                {
                    FormatTitle = "<font style=\"color:" + TitleColor + ";\">" + FormatTitle + "</font>";
                }
                Return_title = FormatTitle;
            }
            return Return_title;
        }


        /// <summary> 
        /// 转换人民币大小金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + "整";
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }


        /// <summary> 
        /// 转换人民币大小金额  (一个重载，将字符串先转换成数字在调用CmycurD)
        /// </summary> 
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param> 
        /// <returns></returns> 
        public static string CmycurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }

        /// <summary>
        /// 过滤危险字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string SqlEncode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt");
            str = str.Replace("'", "''");
            str = str.Replace("*", "");
            str = str.Replace("\n", "<br/>");
            str = str.Replace("\r\n", "<br/>");
            //str = str.Replace("?","");
            str = str.Replace("select", "");
            str = str.Replace("insert", "");
            str = str.Replace("update", "");
            str = str.Replace("delete", "");
            str = str.Replace("create", "");
            str = str.Replace("drop", "");
            str = str.Replace("delcare", "");
            if (str.Trim().ToString() == "") { str = "无"; }
            return str.Trim();
        }
        /// <summary>
        /// 去除HTML代码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        /// <summary>
        /// 去除所有标签
        /// </summary>
        /// <param name="stringToStrip"></param>
        /// <returns></returns>
        public static string StripAllTags(string stringToStrip)
        {

            stringToStrip = Regex.Replace(stringToStrip, "</p(?:\\s*)>(?:\\s*)<p(?:\\s*)>", "\n\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "<br(?:\\s*)/>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = StripHtmlXmlTags(stringToStrip);
            return stringToStrip;

        }

        /// <summary>
        /// 取得随机数
        /// </summary>
        /// <param name="randNumLength">随机数的长度</param>
        /// <returns></returns>
        public static string GetRandNum(int randNumLength)
        {
            System.Random randNum = new System.Random(unchecked((int)DateTime.Now.Ticks));
            StringBuilder sb = new StringBuilder(randNumLength);
            for (int i = 0; i < randNumLength; i++)
            {
                sb.Append(randNum.Next(0, 9));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="VcodeNum">随机数长度</param>
        /// <returns></returns>
        public static string RndNum(int VcodeNum)
        {
            string Vchar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(new Char[] { ',' });
            string VNum = "";
            int temp = -1;

            Random rand = new Random();

            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(35);
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum);
                }
                temp = t;
                VNum += VcArray[t];
            }
            return VNum;
        }


        /// <summary>
        /// 获取当前日期是星期几
        /// </summary>
        /// <returns></returns>
        public static string GetWeek()
        {
            string dt = DateTime.Today.DayOfWeek.ToString();
            string week = "";
            //根据取得的星期英文单词返回汉字
            switch (dt)
            {
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;
            }
            return week;
        }
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        static public string GenerateCheckCode()
        {
            #region
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));

            return checkCode;
            #endregion
        }

        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="PasswordString">要加密的字符串</param>
        /// <param name="PasswordFormat">要加密的类别</param>
        /// <returns></returns>
        static public string EncryptPassword(string PasswordString, string PasswordFormat)
        {
            string passWord = "";
            #region
            switch (PasswordFormat)
            {
                case "SHA1":
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
                        break;
                    }
                case "MD5":
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5");
                        break;
                    }
                default:
                    {
                        passWord = string.Empty;
                        break;
                    }
            }
            return passWord;
            #endregion
        }
        /// <summary>
        /// 格式化日期,不显示时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public string FormartDateTime(object obj)
        {
            return ((DateTime)obj).ToString("yyyy-MM-dd");//'yyyy-mm-dd hh24:mi
        }
        /// <summary>
        /// 格式化日期时间，按照12小时制显示时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public string FormartDateTimeTwo(object obj)
        {
            return ((DateTime)obj).ToString("yyyy-mm-dd hh:m");//'
        }

        /// <summary>
        /// 返回MD5加密后的密文
        /// </summary>
        /// <param name="str">原文字符串</param>
        /// <param name="code">密文长度：16/32 ,非16则返回32位的密文</param>
        /// <returns></returns>
        public static string Md5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
        }
        /// <summary>
        /// 加密/解密方法：（对称加密：简单方法）
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "123456");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "123456");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        ////返回以字节方式载取的字符串
        //public static string GetSubString(string str, int length)
        //{
        //    string temp = str;
        //    int j = 0;
        //    int k = 0;
        //    for (int i = 0; i < temp.Length; i++)
        //    {
        //        if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
        //            j += 2;
        //        else
        //            j++;
        //        if (j >= length)
        //        {
        //            return temp.Substring(0, k - 1);
        //        }
        //        k++;
        //    }
        //    return temp;
        //}

        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            #region
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
            #endregion
        }

        /// <summary>
        /// 选比较大的值
        /// </summary>
        /// <param name="int1"></param>
        /// <param name="int2"></param>
        /// <returns></returns>
        public static int Max(int int1, int int2)
        {
            return int1 > int2 ? int1 : int2;

        }
        /// <summary>
        /// 选比较小的值
        /// </summary>
        /// <param name="int1"></param>
        /// <param name="int2"></param>
        /// <returns></returns>
        public static int Min(int int1, int int2)
        {
            return int1 < int2 ? int1 : int2;

        }

        /// <summary>
        /// 将dt转化成Json数据 格式如 table[{id:1,title:'体育'},id:2,title:'娱乐'}]
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DtToSON(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("recordcount:" + dt.Rows.Count + ",table: [");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                    jsonBuilder.Append(",");
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j > 0)
                        jsonBuilder.Append(",");
                    jsonBuilder.Append(dt.Columns[j].ColumnName.ToLower() + ": '" + dt.Rows[i][j].ToString().Replace("\t", " ").Replace("\r", " ").Replace("\n", " ").Replace("\'", "\\\'") + "'");
                }
                jsonBuilder.Append("}");
            }
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
        /// <summary>
        /// 返回当前日期和时间组成的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeString()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()+DateTime.Now.Millisecond.ToString();
        }

    }
}
