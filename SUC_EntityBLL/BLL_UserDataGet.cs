using SUC_DataEntity;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SY_BLL
{
    public class BLL_UserDataGet
    {

        /// <summary> 
        /// 获取数据 所有WeChat id
        /// </summary> 
        public static DataSet GetWeChatIdOfUser()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  STUFF((select ','+F_WeChat from [ZFine].[dbo].[sys_User] WHERE F_WeChat is not null for xml path('')),1,1,'') AS F_WeChat");
            //表名前添加dbo
            return  SUC_DAL.DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary> 
        /// 获取数据 指定获取单个WeChat id
        /// </summary> 
        /// 
        /**    **/
        public static DataSet GetWeChatIdOfUserId(String id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  STUFF((select ','+F_WeChat from [ZFine].[dbo].[sys_User] WHERE F_id='"+ id + "'  and  F_WeChat is not null for xml path('')),1,1,'') AS F_WeChat");
            //表名前添加dbo


            return SUC_DAL.DbHelperSQL.Query(strSql.ToString());
            
        }
  

    }
}
