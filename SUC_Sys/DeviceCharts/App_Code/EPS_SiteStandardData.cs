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
///EPS_SiteStandardData类 
/// </summary>  
public class EPS_SiteStandardData  
 { 
    public EPS_SiteStandardData() 
    { } 
    #region Model 
     public Guid SiteStandardDataId {set;get;}
     public string CityName {set;get;}
     public string SiteCode {set;get;}
     public string SiteName {set;get;}
     public DateTime ReleaseTime {set;get;}
     public float AQI {set;get;}
     public float PM_25 {set;get;}
     public float PM10 {set;get;}
     public float SO2 {set;get;}
     public float NO2 {set;get;}
     public float O3 {set;get;}
     public float CO {set;get;}
     public string Remark {set;get;}
     public int? CreateBy {set;get;}
     public DateTime CreateTime {set;get;}
     public int? ModifyBy {set;get;}
     public DateTime? ModifyTime {set;get;}
    #endregion Model 
    #region 方法成员 
  /// <summary> 
  /// 增加一条数据 
  /// </summary> 
  public int Add() 
  {
     StringBuilder strSql = new StringBuilder(); 
     strSql.Append("insert into EPS_SiteStandardData(");
     strSql.Append("SiteStandardDataId,CityName,SiteCode,SiteName,ReleaseTime,AQI,PM_25,PM10,SO2,NO2,O3,CO,Remark,CreateBy,CreateTime,ModifyBy,ModifyTime");
     strSql.Append(") values ("); 
     strSql.Append("@SiteStandardDataId,@CityName,@SiteCode,@SiteName,@ReleaseTime,@AQI,@PM_25,@PM10,@SO2,@NO2,@O3,@CO,@Remark,@CreateBy,@CreateTime,@ModifyBy,@ModifyTime"); 
     strSql.Append(")");
     strSql.Append(";select @@IDENTITY");  
     SqlParameter[] parameters = {
      new SqlParameter("@SiteStandardDataId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@CityName",SqlDbType.VarChar,50), 
      new SqlParameter("@SiteCode",SqlDbType.VarChar,30), 
      new SqlParameter("@SiteName",SqlDbType.VarChar,50), 
      new SqlParameter("@ReleaseTime",SqlDbType.DateTime), 
      new SqlParameter("@AQI",SqlDbType.Float), 
      new SqlParameter("@PM_25",SqlDbType.Float), 
      new SqlParameter("@PM10",SqlDbType.Float), 
      new SqlParameter("@SO2",SqlDbType.Float), 
      new SqlParameter("@NO2",SqlDbType.Float), 
      new SqlParameter("@O3",SqlDbType.Float), 
      new SqlParameter("@CO",SqlDbType.Float), 
      new SqlParameter("@Remark",SqlDbType.VarChar,400), 
      new SqlParameter("@CreateBy",SqlDbType.Int), 
      new SqlParameter("@CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@ModifyBy",SqlDbType.Int), 
      new SqlParameter("@ModifyTime",SqlDbType.DateTime) 
     };
     parameters[0].Value = SiteStandardDataId;
     parameters[1].Value = CityName;
     parameters[2].Value = SiteCode;
     parameters[3].Value = SiteName;
     parameters[4].Value = ReleaseTime;
     parameters[5].Value = AQI;
     parameters[6].Value = PM_25;
     parameters[7].Value = PM10;
     parameters[8].Value = SO2;
     parameters[9].Value = NO2;
     parameters[10].Value = O3;
     parameters[11].Value = CO;
     parameters[12].Value = Remark;
     parameters[13].Value = CreateBy;
     parameters[14].Value = CreateTime;
     parameters[15].Value = ModifyBy;
     parameters[16].Value = ModifyTime;  
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
     strSql.Append("update EPS_SiteStandardData set ");
     strSql.Append("SiteStandardDataId=@SiteStandardDataId,");
     strSql.Append("CityName=@CityName,");
     strSql.Append("SiteCode=@SiteCode,");
     strSql.Append("SiteName=@SiteName,");
     strSql.Append("ReleaseTime=@ReleaseTime,");
     strSql.Append("AQI=@AQI,");
     strSql.Append("PM_25=@PM_25,");
     strSql.Append("PM10=@PM10,");
     strSql.Append("SO2=@SO2,");
     strSql.Append("NO2=@NO2,");
     strSql.Append("O3=@O3,");
     strSql.Append("CO=@CO,");
     strSql.Append("Remark=@Remark,");
     strSql.Append("CreateBy=@CreateBy,");
     strSql.Append("CreateTime=@CreateTime,");
     strSql.Append("ModifyBy=@ModifyBy,");
     strSql.Append("ModifyTime=@ModifyTime");
     strSql.Append(" where ID=@Id");
     SqlParameter[] parameters = { 
      new SqlParameter("@SiteStandardDataId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@CityName",SqlDbType.VarChar,50), 
      new SqlParameter("@SiteCode",SqlDbType.VarChar,30), 
      new SqlParameter("@SiteName",SqlDbType.VarChar,50), 
      new SqlParameter("@ReleaseTime",SqlDbType.DateTime), 
      new SqlParameter("@AQI",SqlDbType.Float), 
      new SqlParameter("@PM_25",SqlDbType.Float), 
      new SqlParameter("@PM10",SqlDbType.Float), 
      new SqlParameter("@SO2",SqlDbType.Float), 
      new SqlParameter("@NO2",SqlDbType.Float), 
      new SqlParameter("@O3",SqlDbType.Float), 
      new SqlParameter("@CO",SqlDbType.Float), 
      new SqlParameter("@Remark",SqlDbType.VarChar,400), 
      new SqlParameter("@CreateBy",SqlDbType.Int), 
      new SqlParameter("@CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@ModifyBy",SqlDbType.Int), 
      new SqlParameter("@ModifyTime",SqlDbType.DateTime) 
      };
     parameters[0].Value = SiteStandardDataId; 
     parameters[1].Value = CityName; 
     parameters[2].Value = SiteCode; 
     parameters[3].Value = SiteName; 
     parameters[4].Value = ReleaseTime; 
     parameters[5].Value = AQI; 
     parameters[6].Value = PM_25; 
     parameters[7].Value = PM10; 
     parameters[8].Value = SO2; 
     parameters[9].Value = NO2; 
     parameters[10].Value = O3; 
     parameters[11].Value = CO; 
     parameters[12].Value = Remark; 
     parameters[13].Value = CreateBy; 
     parameters[14].Value = CreateTime; 
     parameters[15].Value = ModifyBy; 
     parameters[16].Value = ModifyTime; 
       return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
     }
/// <summary> 
/// 删除一条数据 
/// </summary> 
public int delete() 
{ 
    StringBuilder strSql = new StringBuilder(); 
    strSql.Append("delete from EPS_SiteStandardData ");
    strSql.Append(" where SiteStandardDataId=@SiteStandardDataId ");
    SqlParameter[] parameters = { 
    new SqlParameter("@SiteStandardDataId", SqlDbType.Int,4) 
    }; 
    parameters[0].Value = SiteStandardDataId;
    return  DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
} 
/// <summary> 
/// 获取数据列表 
/// </summary> 
public DataSet GetList(string strWhere) 
{ 
    StringBuilder strSql = new StringBuilder(); 
    strSql.Append(" select * from EPS_SiteStandardData ");
    if(strWhere.Trim() != "") 
    {
    strSql.Append(" where " + strWhere);
    }
    return  DbHelperSQL.Query(strSql.ToString()); 
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
    strSql = string.Format("select top {0} * from EPS_SiteStandardData where id not in (select top {1} id from EPS_SiteStandardData where {2} order by id desc) and ({2}) order by id desc", PageSize, PageSize * (PageIndex - 1), strWhere); 
    DataSet ds = DbHelperSQL.Query(strSql); 
    string strSql2 = string.Format("select id from EPS_SiteStandardData where {0}", strWhere);
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
    strSql.Append(" delete from EPS_SiteStandardData ");
    strSql.Append(string.Format(" where ID in ({0}) ", _idstr)); 
    return DbHelperSQL.ExecuteSql(strSql.ToString()); 
}



#endregion 方法成员
}
}

