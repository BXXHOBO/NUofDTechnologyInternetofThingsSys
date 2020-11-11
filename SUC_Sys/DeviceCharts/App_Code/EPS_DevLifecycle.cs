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
///EPS_DevLifecycle类 
/// </summary>  
public class EPS_DevLifecycle  
 { 
    public EPS_DevLifecycle() 
    { } 
    #region Model 
     public Guid DevLifecycleId {set;get;}
     public string DeviceCode {set;get;}
     public string DeviceName {set;get;}
     public string DeviceType {set;get;}
     public string Supplier {set;get;}
     public DateTime? PurchaseTime {set;get;}
     public string BeUsed {set;get;}
     public string MaintenanceStatus {set;get;}
     public decimal? Voltage {set;get;}
     public string Remark {set;get;}
     public int? Disabled {set;get;}
     public DateTime? Rec_CreateTime {set;get;}
     public string Rec_CreateBy {set;get;}
     public DateTime? Rec_ModifyTime {set;get;}
     public string Rec_ModifyBy {set;get;}
    #endregion Model 
    #region 方法成员 
  /// <summary> 
  /// 增加一条数据 
  /// </summary> 
  public int Add() 
  {
     StringBuilder strSql = new StringBuilder(); 
     strSql.Append("insert into EPS_DevLifecycle(");
     strSql.Append("DevLifecycleId,DeviceCode,DeviceName,DeviceType,Supplier,PurchaseTime,BeUsed,MaintenanceStatus,Voltage,Remark,Disabled,Rec_CreateTime,Rec_CreateBy,Rec_ModifyTime,Rec_ModifyBy");
     strSql.Append(") values ("); 
     strSql.Append("@DevLifecycleId,@DeviceCode,@DeviceName,@DeviceType,@Supplier,@PurchaseTime,@BeUsed,@MaintenanceStatus,@Voltage,@Remark,@Disabled,@Rec_CreateTime,@Rec_CreateBy,@Rec_ModifyTime,@Rec_ModifyBy"); 
     strSql.Append(")");
     strSql.Append(";select @@IDENTITY");  
     SqlParameter[] parameters = {
      new SqlParameter("@DevLifecycleId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@DeviceCode",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceName",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceType",SqlDbType.VarChar,50), 
      new SqlParameter("@Supplier",SqlDbType.VarChar,100), 
      new SqlParameter("@PurchaseTime",SqlDbType.DateTime), 
      new SqlParameter("@BeUsed",SqlDbType.VarChar,50), 
      new SqlParameter("@MaintenanceStatus",SqlDbType.VarChar,50), 
      new SqlParameter("@Voltage",SqlDbType.Decimal), 
      new SqlParameter("@Remark",SqlDbType.VarChar,2000), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@Rec_CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_CreateBy",SqlDbType.VarChar,50), 
      new SqlParameter("@Rec_ModifyTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_ModifyBy",SqlDbType.VarChar,50) 
     };
     parameters[0].Value = DevLifecycleId;
     parameters[1].Value = DeviceCode;
     parameters[2].Value = DeviceName;
     parameters[3].Value = DeviceType;
     parameters[4].Value = Supplier;
     parameters[5].Value = PurchaseTime;
     parameters[6].Value = BeUsed;
     parameters[7].Value = MaintenanceStatus;
     parameters[8].Value = Voltage;
     parameters[9].Value = Remark;
     parameters[10].Value = Disabled;
     parameters[11].Value = Rec_CreateTime;
     parameters[12].Value = Rec_CreateBy;
     parameters[13].Value = Rec_ModifyTime;
     parameters[14].Value = Rec_ModifyBy; 
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
     strSql.Append("update EPS_DevLifecycle set ");
     strSql.Append("DevLifecycleId=@DevLifecycleId,");
     strSql.Append("DeviceCode=@DeviceCode,");
     strSql.Append("DeviceName=@DeviceName,");
     strSql.Append("DeviceType=@DeviceType,");
     strSql.Append("Supplier=@Supplier,");
     strSql.Append("PurchaseTime=@PurchaseTime,");
     strSql.Append("BeUsed=@BeUsed,");
     strSql.Append("MaintenanceStatus=@MaintenanceStatus,");
     strSql.Append("Voltage=@Voltage,");
     strSql.Append("Remark=@Remark,");
     strSql.Append("Disabled=@Disabled,");
     strSql.Append("Rec_CreateTime=@Rec_CreateTime,");
     strSql.Append("Rec_CreateBy=@Rec_CreateBy,");
     strSql.Append("Rec_ModifyTime=@Rec_ModifyTime,");
     strSql.Append("Rec_ModifyBy=@Rec_ModifyBy");
     strSql.Append(" where ID=@Id");
     SqlParameter[] parameters = { 
      new SqlParameter("@DevLifecycleId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@DeviceCode",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceName",SqlDbType.VarChar,50), 
      new SqlParameter("@DeviceType",SqlDbType.VarChar,50), 
      new SqlParameter("@Supplier",SqlDbType.VarChar,100), 
      new SqlParameter("@PurchaseTime",SqlDbType.DateTime), 
      new SqlParameter("@BeUsed",SqlDbType.VarChar,50), 
      new SqlParameter("@MaintenanceStatus",SqlDbType.VarChar,50), 
      new SqlParameter("@Voltage",SqlDbType.Decimal), 
      new SqlParameter("@Remark",SqlDbType.VarChar,2000), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@Rec_CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_CreateBy",SqlDbType.VarChar,50), 
      new SqlParameter("@Rec_ModifyTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_ModifyBy",SqlDbType.VarChar,50) 
      };
     parameters[0].Value = DevLifecycleId; 
     parameters[1].Value = DeviceCode; 
     parameters[2].Value = DeviceName; 
     parameters[3].Value = DeviceType; 
     parameters[4].Value = Supplier; 
     parameters[5].Value = PurchaseTime; 
     parameters[6].Value = BeUsed; 
     parameters[7].Value = MaintenanceStatus; 
     parameters[8].Value = Voltage; 
     parameters[9].Value = Remark; 
     parameters[10].Value = Disabled; 
     parameters[11].Value = Rec_CreateTime; 
     parameters[12].Value = Rec_CreateBy; 
     parameters[13].Value = Rec_ModifyTime; 
     parameters[14].Value = Rec_ModifyBy; 
       return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
     }

/// <summary> 
/// 获取数据列表 
/// </summary> 
public DataSet GetList(string strWhere) 
{ 
StringBuilder strSql = new StringBuilder(); 
strSql.Append(" select * from EPS_DevLifecycle ");
if(strWhere.Trim() != "") 
{
strSql.Append(" where " + strWhere);
}
return  DbHelperSQL.Query(strSql.ToString()); 
}

/// <summary> 
/// 获取数据用于做饼状图 
/// </summary> 
public DataSet GetDeviceTypeInfo()
{
    StringBuilder strSql = new StringBuilder();
    //strSql.AppendFormat(" SELECT DeviceType,COUNT(DeviceType) as DeviceTypeCounts   FROM [EPS_GridDB].[dbo].[EPS_DevLifecycle] where Disabled=0 and CONVERT(INT,MaintenanceStatus)!=2 group by DeviceType ");
    strSql.AppendFormat("SELECT DeviceType,COUNT(DeviceType) as DeviceTypeCounts   FROM [EPS_GridDB].[dbo].[EPS_DevLifecycle] where Disabled=0 and MaintenanceStatus!='已报废' group by DeviceType");
    return DbHelperSQL.Query(strSql.ToString());
}

/// 获取数据用于做环形图 
/// </summary> 
public DataSet GetMaintenanceStatusInfoForPie()
{
    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat(" SELECT MaintenanceStatus,COUNT(MaintenanceStatus) as MaintenanceStatusCounts   FROM [EPS_GridDB].[dbo].[EPS_DevLifecycle] where Disabled=0  group by MaintenanceStatus ");
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
strSql = string.Format("select top {0} * from EPS_DevLifecycle where id not in (select top {1} id from EPS_DevLifecycle where {2} order by id desc) and ({2}) order by id desc", PageSize, PageSize * (PageIndex - 1), strWhere); 
DataSet ds = DbHelperSQL.Query(strSql); 
string strSql2 = string.Format("select id from EPS_DevLifecycle where {0}", strWhere);
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
strSql.Append(" delete from EPS_DevLifecycle ");
strSql.Append(string.Format(" where ID in ({0}) ", _idstr)); 
return DbHelperSQL.ExecuteSql(strSql.ToString()); 
}
#endregion 方法成员
}
}

