
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
public class Cld_UavInfo  
 { 
    public Cld_UavInfo() 
    { } 
    #region Model 
     public Guid UavInfoId {set;get;}
     public string UavSerialNO {set;get;}
     public DateTime? OperationDate {set;get;}
     public string Sortie {set;get;}
     public string VideoAddr {set;get;}
     public string Longitude {set;get;}
     public string Latitude {set;get;}
     public string Altitude {set;get;}
     public decimal? Temperature {set;get;}
     public decimal? Humidity {set;get;}
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
     strSql.Append("insert into Cld_UavInfo(");
     strSql.Append("UavInfoId,UavSerialNO,OperationDate,Sortie,VideoAddr,Longitude,Latitude,Altitude,Temperature,Humidity,Remark,Disabled,Rec_CreateTime,Rec_CreateBy,Rec_ModifyTime,Rec_ModifyBy");
     strSql.Append(") values ("); 
     strSql.Append("@UavInfoId,@UavSerialNO,@OperationDate,@Sortie,@VideoAddr,@Longitude,@Latitude,@Altitude,@Temperature,@Humidity,@Remark,@Disabled,@Rec_CreateTime,@Rec_CreateBy,@Rec_ModifyTime,@Rec_ModifyBy"); 
     strSql.Append(")");
     strSql.Append(";select @@IDENTITY");  
     SqlParameter[] parameters = {
      new SqlParameter("@UavInfoId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@UavSerialNO",SqlDbType.VarChar,50), 
      new SqlParameter("@OperationDate",SqlDbType.DateTime), 
      new SqlParameter("@Sortie",SqlDbType.VarChar,50), 
      new SqlParameter("@VideoAddr",SqlDbType.VarChar,100), 
      new SqlParameter("@Longitude",SqlDbType.VarChar,50), 
      new SqlParameter("@Latitude",SqlDbType.VarChar,50), 
      new SqlParameter("@Altitude",SqlDbType.VarChar,50), 
      new SqlParameter("@Temperature",SqlDbType.Decimal), 
      new SqlParameter("@Humidity",SqlDbType.Decimal), 
      new SqlParameter("@Remark",SqlDbType.VarChar,3000), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@Rec_CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_CreateBy",SqlDbType.VarChar,50), 
      new SqlParameter("@Rec_ModifyTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_ModifyBy",SqlDbType.VarChar,50) 
     };  
     parameters[0].Value = UavInfoId; 
     parameters[1].Value = UavSerialNO; 
     parameters[2].Value = OperationDate; 
     parameters[3].Value = Sortie; 
     parameters[4].Value = VideoAddr; 
     parameters[5].Value = Longitude; 
     parameters[6].Value = Latitude; 
     parameters[7].Value = Altitude; 
     parameters[8].Value = Temperature; 
     parameters[9].Value = Humidity; 
     parameters[10].Value = Remark; 
     parameters[11].Value = Disabled; 
     parameters[12].Value = Rec_CreateTime; 
     parameters[13].Value = Rec_CreateBy; 
     parameters[14].Value = Rec_ModifyTime; 
     parameters[15].Value = Rec_ModifyBy; 
     object obj = SUC_DAL.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
     strSql.Append("update Cld_UavInfo set ");
     strSql.Append("UavInfoId=@UavInfoId,");
     strSql.Append("UavSerialNO=@UavSerialNO,");
     strSql.Append("OperationDate=@OperationDate,");
     strSql.Append("Sortie=@Sortie,");
     strSql.Append("VideoAddr=@VideoAddr,");
     strSql.Append("Longitude=@Longitude,");
     strSql.Append("Latitude=@Latitude,");
     strSql.Append("Altitude=@Altitude,");
     strSql.Append("Temperature=@Temperature,");
     strSql.Append("Humidity=@Humidity,");
     strSql.Append("Remark=@Remark,");
     strSql.Append("Disabled=@Disabled,");
     strSql.Append("Rec_CreateTime=@Rec_CreateTime,");
     strSql.Append("Rec_CreateBy=@Rec_CreateBy,");
     strSql.Append("Rec_ModifyTime=@Rec_ModifyTime,");
     strSql.Append("Rec_ModifyBy=@Rec_ModifyBy");
     strSql.Append(" where ID=@Id");
     SqlParameter[] parameters = { 
      new SqlParameter("@UavInfoId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@UavSerialNO",SqlDbType.VarChar,50), 
      new SqlParameter("@OperationDate",SqlDbType.DateTime), 
      new SqlParameter("@Sortie",SqlDbType.VarChar,50), 
      new SqlParameter("@VideoAddr",SqlDbType.VarChar,100), 
      new SqlParameter("@Longitude",SqlDbType.VarChar,50), 
      new SqlParameter("@Latitude",SqlDbType.VarChar,50), 
      new SqlParameter("@Altitude",SqlDbType.VarChar,50), 
      new SqlParameter("@Temperature",SqlDbType.Decimal), 
      new SqlParameter("@Humidity",SqlDbType.Decimal), 
      new SqlParameter("@Remark",SqlDbType.VarChar,3000), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@Rec_CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_CreateBy",SqlDbType.VarChar,50), 
      new SqlParameter("@Rec_ModifyTime",SqlDbType.DateTime), 
      new SqlParameter("@Rec_ModifyBy",SqlDbType.VarChar,50) 
      };
         parameters[0].Value = UavInfoId; 
         parameters[1].Value = UavSerialNO; 
         parameters[2].Value = OperationDate; 
         parameters[3].Value = Sortie; 
         parameters[4].Value = VideoAddr; 
         parameters[5].Value = Longitude; 
         parameters[6].Value = Latitude; 
         parameters[7].Value = Altitude; 
         parameters[8].Value = Temperature; 
         parameters[9].Value = Humidity; 
         parameters[10].Value = Remark; 
         parameters[11].Value = Disabled; 
         parameters[12].Value = Rec_CreateTime; 
         parameters[13].Value = Rec_CreateBy; 
         parameters[14].Value = Rec_ModifyTime; 
         parameters[15].Value = Rec_ModifyBy; 
       return SUC_DAL.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
     }

/// <summary> 
/// 获取数据列表 
/// </summary> 
public DataSet GetList(string strWhere) 
{ 
    StringBuilder strSql = new StringBuilder(); 
    strSql.Append(" select * from Cld_UavInfo ");
    if(strWhere.Trim() != "") 
    {
        strSql.Append(" where " + strWhere);
    }
  return SUC_DAL.DbHelperSQL.Query(strSql.ToString()); 
}
/// <summary> 
/// 获取架次数据列表 
/// </summary> 
public DataSet GetTemperatureInfoBySortie(string sortie)
{

    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat("SELECT DATEPART(SS,Rec_CreateTime) as Createsecond,Temperature FROM [ZFine].[dbo].[Cld_UavInfo] WHERE Sortie='{0}'", sortie);
   
    return SUC_DAL.DbHelperSQL.Query(strSql.ToString());
}
public DataSet GetTimeInfoBySortie(string UavSerialNO,string sortie)
{

    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat("SELECT Rec_CreateTime,Temperature FROM [ZFine].[dbo].[Cld_UavInfo] WHERE UavSerialNO='{0}' and Sortie='{1}'", UavSerialNO, sortie);

    return SUC_DAL.DbHelperSQL.Query(strSql.ToString());
}
public DataSet GetTempandHumidityInfoBySortie(string sortie)
{

    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat("SELECT Rec_CreateTime,Temperature,Humidity FROM [ZFine].[dbo].[Cld_UavInfo] WHERE Sortie='{0}'", sortie);

    return SUC_DAL.DbHelperSQL.Query(strSql.ToString());
}

public DataSet GetHumidityInfoBySortie(string UavSerialNO, string sortie)
{

    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat("SELECT Rec_CreateTime,Humidity FROM [ZFine].[dbo].[Cld_UavInfo] WHERE UavSerialNO='{0}'and Sortie='{1}'", UavSerialNO, sortie);

    return SUC_DAL.DbHelperSQL.Query(strSql.ToString());
}
/// <summary> 
/// 获取19年每个月温湿度的平均值 
/// </summary> 
public DataSet GetAvgTandHInfo(int year)
{

    StringBuilder strSql = new StringBuilder();
    strSql.AppendFormat("SELECT DATEPART(MM,Rec_CreateTime) AS 'Month',AVG(Temperature) AS 'Tempavg',AVG(Humidity) AS 'Humavg' FROM Cld_UavInfo WHERE Year(Rec_CreateTime)={0} group by  MONTH(Rec_CreateTime) order by MONTH(Rec_CreateTime)", year);

    return SUC_DAL.DbHelperSQL.Query(strSql.ToString());
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
         strSql = string.Format("select top {0} * from Cld_UavInfo where id not in (select top {1} id from Cld_UavInfo where {2} order by id desc) and ({2}) order by id desc", PageSize, PageSize * (PageIndex - 1), strWhere); 
        DataSet ds = SUC_DAL.DbHelperSQL.Query(strSql); 
          string strSql2 = string.Format("select id from Cld_UavInfo where {0}", strWhere);
    DataSet dsCount = SUC_DAL.DbHelperSQL.Query(strSql2);
    try
    {
      Recordcount = dsCount.Tables[0].Rows.Count;
    }
    catch
    {
      Recordcount = 0;
    }
    return SUC_DAL.DbHelperSQL.Query(strSql.ToString());
}
/// <summary> 
/// 批量删除数据 
/// </summary> 
public int BatchDelete(string _idstr) 
{
    StringBuilder strSql = new StringBuilder(); 
    strSql.Append(" delete from Cld_UavInfo ");
    strSql.Append(string.Format(" where UavInfoId in ({0}) ", _idstr)); 
    return SUC_DAL.DbHelperSQL.ExecuteSql(strSql.ToString()); 
}
#endregion 方法成员
}
}

