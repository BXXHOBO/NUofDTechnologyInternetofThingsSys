using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Common.Http
{
    public class StreamHelper
    {
        /* - - - - - - - - - - - - - - - - - - - - - - - -  
 * Stream 和 byte[] 之间的转换 
 * - - - - - - - - - - - - - - - - - - - - - - - */
        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public static string StreamToString(Stream stream ,Encoding encoding)
        {
            return encoding.GetString(StreamToBytes(stream));
        }

        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }


        /* - - - - - - - - - - - - - - - - - - - - - - - -  
         * Stream 和 文件之间的转换 
         * - - - - - - - - - - - - - - - - - - - - - - - */
        /// <summary> 
        /// 将 Stream 写入文件 
        /// </summary> 
        public void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary> 
        /// 从文件读取 Stream 
        /// </summary> 
        public Stream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public bool HttpDownload(string url, string LocalPath)
        {
            try
            {
                Uri u = new Uri(url);
                HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(u);
                mRequest.Method = "GET";
                mRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse wr = (HttpWebResponse)mRequest.GetResponse();

                Stream sIn = wr.GetResponseStream();

                FileStream fs = new FileStream(LocalPath, FileMode.Create, FileAccess.Write);

                long length = wr.ContentLength;
                long i = 0;
                decimal j = 0;

                while (i < length)
                {
                    byte[] buffer = new byte[1024];
                    i += sIn.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, buffer.Length);

                    if ((i % 1024) == 0)
                    {
                        j = Math.Round(Convert.ToDecimal((Convert.ToDouble(i) / Convert.ToDouble(length)) * 100), 4);
                        //Response.Write("<br><br><br>" + "当前下载文件大小:" + length.ToString() + "字节   当前下载大小:" + i + "字节 下载进度" + j.ToString() + "%");
                    }
                    else
                    {
                        //Response.Write("<br><br><br>" + "当前下载文件大小:" + length.ToString() + "字节   当前下载大小:" + i + "字节");
                    }
                }

                sIn.Close();
                wr.Close();
                fs.Close();
                return true;
            }
            catch { return false; }
        }

        //public byte[] GetHttpDownloadData(string url, string LocalPath)
        //{
        //    Uri u = new Uri(url);
        //    HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(u);
        //    mRequest.Method = "GET";
        //    mRequest.ContentType = "application/x-www-form-urlencoded";

        //    HttpWebResponse wr = (HttpWebResponse)mRequest.GetResponse();

        //    Stream sIn = wr.GetResponseStream();

        //    byte[] bytes = StreamToBytes(sIn);

        //    sIn.Close();

        //    return bytes;
        //}
    }
}
