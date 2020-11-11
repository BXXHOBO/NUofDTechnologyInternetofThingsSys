
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SUC_DAL;

namespace SUC_DAL
{
    public class BLL_UavDevice
    { 
       
      
        public int UpdateDeviceWorkState(string uavSerialNO,string workstate)//将设备状态修改
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE SUC_UavData ");
            strSql.Append(string.Format(" SET WorkState='{0}'", workstate));
            strSql.Append(string.Format(" WHERE UavSerialNO='{0}'", uavSerialNO));
            return DbHelperSQL.ExecuteSql(strSql.ToString());

        }
    }
}
