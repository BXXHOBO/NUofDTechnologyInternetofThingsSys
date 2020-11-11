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
///EPS_Device类 
/// </summary>  
public class EPS_Device  
 { 
    public EPS_Device() 
    { } 
    #region Model 
     public Guid DeviceId {set;get;}
     public string DeviceCode {set;get;}
     public string DeviceName {set;get;}
     public string DeviceType {set;get;}
     public int DeviceStatus {set;get;}
     public string Province {set;get;}
     public string City {set;get;}
     public string Area {set;get;}
     public string SiteCode {set;get;}
     public decimal Longitude {set;get;}
     public decimal Latitude {set;get;}
     public int Disabled {set;get;}
     public int CreateBy {set;get;}
     public DateTime CreateTime {set;get;}
     public int ModifyBy {set;get;}
     public DateTime ModifyTime {set;get;}
    #endregion Model 
    #region 方法成员 
  /// <summary> 
  /// 增加一条数据 
  /// </summary> 
  public int Add() 
  {
     StringBuilder strSql = new StringBuilder(); 
     strSql.Append("insert into EPS_Device(");
     strSql.Append("DeviceId,DeviceCode,DeviceName,DeviceType,DeviceStatus,Province,City,Area,SiteCode,Longitude,Latitude,Disabled,CreateBy,CreateTime,ModifyBy,ModifyTime");
     strSql.Append(") values ("); 
     strSql.Append("@DeviceId,@DeviceCode,@DeviceName,@DeviceType,@DeviceStatus,@Province,@City,@Area,@SiteCode,@Longitude,@Latitude,@Disabled,@CreateBy,@CreateTime,@ModifyBy,@ModifyTime"); 
     strSql.Append(")");
     strSql.Append(";select @@IDENTITY");  
     SqlParameter[] parameters = {
      new SqlParameter("@DeviceId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@DeviceCode",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceName",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceType",SqlDbType.Int), 
      new SqlParameter("@DeviceStatus",SqlDbType.Int), 
      new SqlParameter("@Province",SqlDbType.VarChar,50), 
      new SqlParameter("@City",SqlDbType.VarChar,50), 
      new SqlParameter("@Area",SqlDbType.VarChar,50), 
      new SqlParameter("@SiteCode",SqlDbType.VarChar,20), 
      new SqlParameter("@Longitude",SqlDbType.Decimal), 
      new SqlParameter("@Latitude",SqlDbType.Decimal), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@CreateBy",SqlDbType.Int), 
      new SqlParameter("@CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@ModifyBy",SqlDbType.Int), 
      new SqlParameter("@ModifyTime",SqlDbType.DateTime) 
     };  
     parameters[0].Value = DeviceId; 
     parameters[1].Value = DeviceCode; 
     parameters[2].Value = DeviceName; 
     parameters[3].Value = DeviceType; 
     parameters[4].Value = DeviceStatus; 
     parameters[5].Value = Province; 
     parameters[6].Value = City; 
     parameters[7].Value = Area; 
     parameters[8].Value = SiteCode; 
     parameters[9].Value = Longitude; 
     parameters[10].Value = Latitude; 
     parameters[11].Value = Disabled; 
     parameters[12].Value = CreateBy; 
     parameters[13].Value = CreateTime; 
     parameters[14].Value = ModifyBy; 
     parameters[15].Value = ModifyTime; 
     object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
     if (obj == null)
     {
          return 1;
     }
     else
     {
          return Convert.ToInt32(obj);
     }
  }
     /// <summary> 
     /// 修改一条数据
     /// </summary> 
     public int Update() 
     {
     StringBuilder strSql = new StringBuilder(); 
     strSql.Append("update EPS_Device set ");
     strSql.Append("DeviceId=@DeviceId,");
     strSql.Append("DeviceCode=@DeviceCode,");
     strSql.Append("DeviceName=@DeviceName,");
     strSql.Append("DeviceType=@DeviceType,");
     strSql.Append("DeviceStatus=@DeviceStatus,");
     strSql.Append("Province=@Province,");
     strSql.Append("City=@City,");
     strSql.Append("Area=@Area,");
     strSql.Append("SiteCode=@SiteCode,");
     strSql.Append("Longitude=@Longitude,");
     strSql.Append("Latitude=@Latitude,");
     strSql.Append("Disabled=@Disabled,");
     strSql.Append("CreateBy=@CreateBy,");
     strSql.Append("CreateTime=@CreateTime,");
     strSql.Append("ModifyBy=@ModifyBy,");
     strSql.Append("ModifyTime=@ModifyTime");
     strSql.Append(" where ID=@Id");
     SqlParameter[] parameters = { 
      new SqlParameter("@DeviceId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@DeviceCode",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceName",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceType",SqlDbType.Int), 
      new SqlParameter("@DeviceStatus",SqlDbType.Int), 
      new SqlParameter("@Province",SqlDbType.VarChar,50), 
      new SqlParameter("@City",SqlDbType.VarChar,50), 
      new SqlParameter("@Area",SqlDbType.VarChar,50), 
      new SqlParameter("@SiteCode",SqlDbType.VarChar,20), 
      new SqlParameter("@Longitude",SqlDbType.Decimal), 
      new SqlParameter("@Latitude",SqlDbType.Decimal), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@CreateBy",SqlDbType.Int), 
      new SqlParameter("@CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@ModifyBy",SqlDbType.Int), 
      new SqlParameter("@ModifyTime",SqlDbType.DateTime) 
      };
     parameters[0].Value = DeviceId; 
     parameters[1].Value = DeviceCode; 
     parameters[2].Value = DeviceName; 
     parameters[3].Value = DeviceType; 
     parameters[4].Value = DeviceStatus; 
     parameters[5].Value = Province; 
     parameters[6].Value = City; 
     parameters[7].Value = Area; 
     parameters[8].Value = SiteCode; 
     parameters[9].Value = Longitude; 
     parameters[10].Value = Latitude; 
     parameters[11].Value = Disabled; 
     parameters[12].Value = CreateBy; 
     parameters[13].Value = CreateTime; 
     parameters[14].Value = ModifyBy; 
     parameters[15].Value = ModifyTime; 
       return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
     }
/// <summary> 
/// 删除一条数据 
/// </summary> 
public int delete() 
{ 
StringBuilder strSql = new StringBuilder(); 
strSql.Append("delete from EPS_Device ");
strSql.Append(" where DeviceId=@DeviceId ");
SqlParameter[] parameters = { 
new SqlParameter("@DeviceId", SqlDbType.Int,4) 
};
parameters[0].Value = DeviceId;
return  DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
} 
/// <summary> 
/// 获取数据列表 
/// </summary> 
public DataSet GetList(string strWhere) 
{ 
StringBuilder strSql = new StringBuilder(); 
strSql.Append(" select * from EPS_Device ");
if(strWhere.Trim() != "") 
{
strSql.Append(" where " + strWhere);
}
return  DbHelperSQL.Query(strSql.ToString()); 
}

/// <summary> 
/// 获取数据列表 
/// </summary> 
public DataSet GetDeviceTypeInfoByYear(int year)
{
    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat(" SELECT DATEPART(MONTH,CreateTime) as DeviceMonth,COUNT(DeviceType) as DeviceSumType  FROM [EPS_GridDB].[dbo].[EPS_Device]  where DATEPART(year,CreateTime)='{0}' and Disabled=0 group by DATEPART(MONTH,CreateTime) ",year);

    return DbHelperSQL.Query(strSql.ToString());
}
/// <summary> 
/// 获取数据用于做饼状图 
/// </summary> 
public DataSet GetDeviceTypeInfoForPie()
{
    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat(" SELECT DeviceType,COUNT(DeviceType) as DeviceTypeCounts   FROM [EPS_GridDB].[dbo].[EPS_Device] where Disabled=0  group by DeviceType ");
    return DbHelperSQL.Query(strSql.ToString());
}


/// <summary> 
/// 分页查询 
/// </summary> 
/// <param name="PageIndex">当前第几页</param> 
/// <param name="PageSize">每页条数</param> 
/// <param name="strWhere">条件</param> 
/// <param name="Recordcount">记录总条数</param> 
/// <returns></returns> 
public DataSet Pager(int PageIndex, int PageSize, string strWhere, out int Recordcount) 
 {
string strSql =string.Empty; 
if(string.IsNullOrEmpty(strWhere)) 
{
strWhere = " 1=1 ";
}
strSql = string.Format("select top {0} * from EPS_Device where id not in (select top {1} id from EPS_Device where {2} order by id desc) and ({2}) order by id desc", PageSize, PageSize * (PageIndex - 1), strWhere); 
DataSet ds = DbHelperSQL.Query(strSql); 
string strSql2 = string.Format("select id from EPS_Device where {0}", strWhere);
DataSet dsCount = DbHelperSQL.Query(strSql2);
try
{
Recordcount = dsCount.Tables[0].Rows.Count;
}
catch
{
Recordcount = 0;
}
return  DbHelperSQL.Query(strSql.ToString());
}
/// <summary> 
/// 批量删除数据 
/// </summary> 
public int BatchDelete(string _idstr) 
{
StringBuilder strSql = new StringBuilder(); 
strSql.Append(" delete from EPS_Device ");
strSql.Append(string.Format(" where ID in ({0}) ", _idstr)); 
return DbHelperSQL.ExecuteSql(strSql.ToString()); 
}
#endregion 方法成员
}
}

