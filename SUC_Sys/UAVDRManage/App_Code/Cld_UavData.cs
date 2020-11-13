
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;
namespace YYCMS
{
/// <summary>  
///Cld_UavInfo类 
/// </summary>  
   public class Cld_UavData 
  { 
  
 
    

/// <summary> 
/// 获取无人机作业数据列表 
/// </summary> 
        public DataSet GetWorkUavInfo(string state) 
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT count(WorkState) as WorkState1 FROM [ZFine].[dbo].[Cld_UavData] WHERE WorkState='{0}'",state);
            return SUC_DAL.DbHelperSQL.Query(strSql.ToString()); 
        }


    }
}

