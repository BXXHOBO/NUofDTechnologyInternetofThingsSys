using SUC_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUC_DAL
{
    public class BLL_UavSortieDataSQL
    {
        public int UpdateSortieState(string uavSerialNO, string historyAddr,DateTime time)//将设备状态改为运行状态
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE SUC_UavSortieData SET UavState='0'");
            strSql.Append(string.Format(" ,historyAddr='{0}'", historyAddr));
            strSql.Append(string.Format(" ,OperationEndTime='{0}'", time));
            strSql.Append(string.Format(" WHERE UavSerialNO='{0}' and UavState='1'", uavSerialNO));
            return DbHelperSQL.ExecuteSql(strSql.ToString());

        }
    }
}
