using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Common
{
    public static class LogPrintHelper
    {
        //读写锁
        static ReaderWriterLockSlim logLock = new ReaderWriterLockSlim();

        #region 读取文本成字符串
        public static String ReadToString(String path)
        {
            StreamReader sr = null;
            try
            {
                logLock.EnterReadLock();
                sr = new StreamReader(path, Encoding.UTF8);
                StringBuilder logsb = new StringBuilder();
                String line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    logsb.AppendLine(line);
                }
                return logsb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            finally
            {
                logLock.ExitReadLock();
                //关闭流
                if (null != sr)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        #endregion

        //写日志
        public static void Info(string content)
        {
            WriteLog(content, EpsDict.LOG_SIZE, EpsDict.LOG_COUNT, EpsDict.LOG_URL);
        }

        #region 记录日志
        //content：需要记录的内容
        //fileSize：文件大小。单位M
        //fileCount：文件数量，超过该数量删除
        //path：存储路径
        public static void WriteLog(String content, int fileSize, int fileCount, String path)
        {
            FileStream fs = null;
            FileStream fs2 = null;
            StreamWriter sw = null;
            try
            {
                logLock.EnterWriteLock();
                String logPath = path;
                String dataString = DateTime.Now.ToString("yyyy-MM-dd");
                path = logPath + "\\logs";
                if (!Directory.Exists(path))
                {//不存在目录则创建
                    Directory.CreateDirectory(path);
                    fs = new FileStream(path, FileMode.Create);
                    //fs.Close();
                }
                else
                {//文件数量，文件大小，写入文件
                    int x = Directory.GetFiles(path).Length;
                    path += "\\";
                    Dictionary<String, DateTime> fileCreatTime = new Dictionary<String, DateTime>();
                    string[] filePathArr = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
                    if (filePathArr.Length == 0)
                    {
                        path += dataString + ".txt";
                        fs = new FileStream(path, FileMode.Create);
                        //fs.Close();
                    }
                    else
                    {
                        for (int i = 0; i < filePathArr.Length; i++)
                        {
                            FileInfo fileinfo1 = new FileInfo(filePathArr[i]);//得到文本信息
                            fileCreatTime[filePathArr[i]] = fileinfo1.CreationTime;
                        }
                        //通过时间进行排序
                        fileCreatTime = fileCreatTime.OrderBy(f => f.Value).ToDictionary(f => f.Key, f => f.Value);
                        FileInfo fileinfo = new FileInfo(fileCreatTime.Last().Key);
                        if (fileinfo.Length < 1024 * 1024 * fileSize)
                        {
                            path = fileCreatTime.Last().Key;
                        }
                        else
                        {
                            path += dataString + "*.txt";
                            fs = new FileStream(path, FileMode.Create);
                            //fs.Close();
                        }

                        if (x > fileCount) //文件数量大于规定的数量，要进行删除
                        {
                            File.Delete(fileCreatTime.First().Key);
                        }
                    }
                }
                fs2 = new FileStream(path, FileMode.Open, FileAccess.Write);
                sw = new StreamWriter(fs2);
                long f1 = fs2.Length;
                fs2.Seek(f1, SeekOrigin.Begin);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "----------->" + content);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                logLock.ExitWriteLock();
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }
                if (fs2 != null)
                {
                    fs2.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        #endregion
    }
}
