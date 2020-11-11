using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EpsDict
    {
        public static string LOG_URL = "D:/360MoveData/Users/JinQian/Desktop/logs";//日志路径

        public static int LOG_COUNT = 10;//文件数量

        public static int LOG_SIZE = 1024;//文件大小 M

        public static int LOG_STATUS_NORMAL = 0; //未超时

        public static int LOG_STATUS_OVER_TIME = 1; //超时

        public static string ORDER_SITE_DICT = "site";//站点

        public static string ORDER_IS_DICT = "is";//获取是否

        public static string ORDER_STATUS_DICT = "statu";// 任务状态

        public static string ORDER_TYPE_DICT = "ordertype";// 工单类型

        public static string ORDER_PROJECT_DICT = "project";//项目明细

        public static string ORDER_DETAIL_STATUS_DICT = "dtailsstaus";//项目明细

        public static string AQIGrade_DICT = "三级";//AQI等级

    }
}
