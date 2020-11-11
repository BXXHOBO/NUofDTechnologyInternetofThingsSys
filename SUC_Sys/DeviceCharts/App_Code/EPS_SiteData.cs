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
///EPS_SiteData类 
/// </summary>  
public class EPS_SiteData  
 { 
    public EPS_SiteData() 
    { } 
    #region Model 
     public Guid SiteDataId {set;get;}
     public string SiteCode {set;get;}
     public string DeviceCode {set;get;}
     public DateTime ReleaseTime {set;get;}
     public decimal AQI {set;get;}
     public string AQIGrade {set;get;}
     public decimal PM_25 {set;get;}
     public decimal PM_25_AQI {set;get;}
     public decimal PM10 {set;get;}
     public decimal PM10_AQI {set;get;}
     public decimal SO2 {set;get;}
     public decimal SO2_AQI {set;get;}
     public decimal NO2 {set;get;}
     public decimal NO2_AQI {set;get;}
     public decimal O3 {set;get;}
     public decimal O3_AQI {set;get;}
     public decimal CO {set;get;}
     public decimal CO_AQI {set;get;}
     public string MainPollutant {set;get;}
     public decimal TVOC {set;get;}
     public decimal Temperature {set;get;}
     public decimal Humidity {set;get;}
     public decimal Press {set;get;}
     public string WindDirection {set;get;}
     public decimal WindSpeed {set;get;}
     public int Disabled {set;get;}
     public string Remark {set;get;}
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
     strSql.Append("insert into EPS_SiteData(");
     strSql.Append("SiteDataId,SiteCode,DeviceCode,ReleaseTime,AQI,AQIGrade,PM_25,PM_25_AQI,PM10,PM10_AQI,SO2,SO2_AQI,NO2,NO2_AQI,O3,O3_AQI,CO,CO_AQI,MainPollutant,TVOC,Temperature,Humidity,Press,WindDirection,WindSpeed,Disabled,Remark,CreateBy,CreateTime,ModifyBy,ModifyTime");
     strSql.Append(") values ("); 
     strSql.Append("@SiteDataId,@SiteCode,@DeviceCode,@ReleaseTime,@AQI,@AQIGrade,@PM_25,@PM_25_AQI,@PM10,@PM10_AQI,@SO2,@SO2_AQI,@NO2,@NO2_AQI,@O3,@O3_AQI,@CO,@CO_AQI,@MainPollutant,@TVOC,@Temperature,@Humidity,@Press,@WindDirection,@WindSpeed,@Disabled,@Remark,@CreateBy,@CreateTime,@ModifyBy,@ModifyTime"); 
     strSql.Append(")");
     strSql.Append(";select @@IDENTITY");  
     SqlParameter[] parameters = {
      new SqlParameter("@SiteDataId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@SiteCode",SqlDbType.VarChar,20), 
      new SqlParameter("@DeviceCode",SqlDbType.VarChar,50), 
      new SqlParameter("@ReleaseTime",SqlDbType.DateTime), 
      new SqlParameter("@AQI",SqlDbType.Decimal), 
      new SqlParameter("@AQIGrade",SqlDbType.VarChar,30), 
      new SqlParameter("@PM_25",SqlDbType.Decimal), 
      new SqlParameter("@PM_25_AQI",SqlDbType.Decimal), 
      new SqlParameter("@PM10",SqlDbType.Decimal), 
      new SqlParameter("@PM10_AQI",SqlDbType.Decimal), 
      new SqlParameter("@SO2",SqlDbType.Decimal), 
      new SqlParameter("@SO2_AQI",SqlDbType.Decimal), 
      new SqlParameter("@NO2",SqlDbType.Decimal), 
      new SqlParameter("@NO2_AQI",SqlDbType.Decimal), 
      new SqlParameter("@O3",SqlDbType.Decimal), 
      new SqlParameter("@O3_AQI",SqlDbType.Decimal), 
      new SqlParameter("@CO",SqlDbType.Decimal), 
      new SqlParameter("@CO_AQI",SqlDbType.Decimal), 
      new SqlParameter("@MainPollutant",SqlDbType.VarChar,300), 
      new SqlParameter("@TVOC",SqlDbType.Decimal), 
      new SqlParameter("@Temperature",SqlDbType.Decimal), 
      new SqlParameter("@Humidity",SqlDbType.Decimal), 
      new SqlParameter("@Press",SqlDbType.Decimal), 
      new SqlParameter("@WindDirection",SqlDbType.VarChar,10), 
      new SqlParameter("@WindSpeed",SqlDbType.Decimal), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@Remark",SqlDbType.VarChar,400), 
      new SqlParameter("@CreateBy",SqlDbType.Int), 
      new SqlParameter("@CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@ModifyBy",SqlDbType.Int), 
      new SqlParameter("@ModifyTime",SqlDbType.DateTime) 
     };
     parameters[0].Value = SiteDataId;
     parameters[1].Value = SiteCode;
     parameters[2].Value = DeviceCode;
     parameters[3].Value = ReleaseTime;
     parameters[4].Value = AQI;
     parameters[5].Value = AQIGrade;
     parameters[6].Value = PM_25;
     parameters[7].Value = PM_25_AQI;
     parameters[8].Value = PM10;
     parameters[9].Value = PM10_AQI;
     parameters[10].Value = SO2;
     parameters[11].Value = SO2_AQI;
     parameters[12].Value = NO2;
     parameters[13].Value = NO2_AQI;
     parameters[14].Value = O3;
     parameters[15].Value = O3_AQI;
     parameters[16].Value = CO;
     parameters[17].Value = CO_AQI;
     parameters[18].Value = MainPollutant;
     parameters[19].Value = TVOC;
     parameters[20].Value = Temperature;
     parameters[21].Value = Humidity;
     parameters[22].Value = Press;
     parameters[23].Value = WindDirection;
     parameters[24].Value = WindSpeed;
     parameters[25].Value = Disabled;
     parameters[26].Value = Remark;
     parameters[27].Value = CreateBy;
     parameters[28].Value = CreateTime;
     parameters[29].Value = ModifyBy;
     parameters[30].Value = ModifyTime; 
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
     strSql.Append("update EPS_SiteData set ");
     strSql.Append("SiteDataId=@SiteDataId,");
     strSql.Append("SiteCode=@SiteCode,");
     strSql.Append("DeviceCode=@DeviceCode,");
     strSql.Append("ReleaseTime=@ReleaseTime,");
     strSql.Append("AQI=@AQI,");
     strSql.Append("AQIGrade=@AQIGrade,");
     strSql.Append("PM_25=@PM_25,");
     strSql.Append("PM_25_AQI=@PM_25_AQI,");
     strSql.Append("PM10=@PM10,");
     strSql.Append("PM10_AQI=@PM10_AQI,");
     strSql.Append("SO2=@SO2,");
     strSql.Append("SO2_AQI=@SO2_AQI,");
     strSql.Append("NO2=@NO2,");
     strSql.Append("NO2_AQI=@NO2_AQI,");
     strSql.Append("O3=@O3,");
     strSql.Append("O3_AQI=@O3_AQI,");
     strSql.Append("CO=@CO,");
     strSql.Append("CO_AQI=@CO_AQI,");
     strSql.Append("MainPollutant=@MainPollutant,");
     strSql.Append("TVOC=@TVOC,");
     strSql.Append("Temperature=@Temperature,");
     strSql.Append("Humidity=@Humidity,");
     strSql.Append("Press=@Press,");
     strSql.Append("WindDirection=@WindDirection,");
     strSql.Append("WindSpeed=@WindSpeed,");
     strSql.Append("Disabled=@Disabled,");
     strSql.Append("Remark=@Remark,");
     strSql.Append("CreateBy=@CreateBy,");
     strSql.Append("CreateTime=@CreateTime,");
     strSql.Append("ModifyBy=@ModifyBy,");
     strSql.Append("ModifyTime=@ModifyTime");
     strSql.Append(" where ID=@Id");
     SqlParameter[] parameters = { 
      new SqlParameter("@SiteDataId",SqlDbType.UniqueIdentifier), 
      new SqlParameter("@SiteCode",SqlDbType.VarChar,20), 
      new SqlParameter("@DeviceCode",SqlDbType.VarChar,50), 
      new SqlParameter("@ReleaseTime",SqlDbType.DateTime), 
      new SqlParameter("@AQI",SqlDbType.Decimal), 
      new SqlParameter("@AQIGrade",SqlDbType.VarChar,30), 
      new SqlParameter("@PM_25",SqlDbType.Decimal), 
      new SqlParameter("@PM_25_AQI",SqlDbType.Decimal), 
      new SqlParameter("@PM10",SqlDbType.Decimal), 
      new SqlParameter("@PM10_AQI",SqlDbType.Decimal), 
      new SqlParameter("@SO2",SqlDbType.Decimal), 
      new SqlParameter("@SO2_AQI",SqlDbType.Decimal), 
      new SqlParameter("@NO2",SqlDbType.Decimal), 
      new SqlParameter("@NO2_AQI",SqlDbType.Decimal), 
      new SqlParameter("@O3",SqlDbType.Decimal), 
      new SqlParameter("@O3_AQI",SqlDbType.Decimal), 
      new SqlParameter("@CO",SqlDbType.Decimal), 
      new SqlParameter("@CO_AQI",SqlDbType.Decimal), 
      new SqlParameter("@MainPollutant",SqlDbType.VarChar,300), 
      new SqlParameter("@TVOC",SqlDbType.Decimal), 
      new SqlParameter("@Temperature",SqlDbType.Decimal), 
      new SqlParameter("@Humidity",SqlDbType.Decimal), 
      new SqlParameter("@Press",SqlDbType.Decimal), 
      new SqlParameter("@WindDirection",SqlDbType.VarChar,10), 
      new SqlParameter("@WindSpeed",SqlDbType.Decimal), 
      new SqlParameter("@Disabled",SqlDbType.Int), 
      new SqlParameter("@Remark",SqlDbType.VarChar,400), 
      new SqlParameter("@CreateBy",SqlDbType.Int), 
      new SqlParameter("@CreateTime",SqlDbType.DateTime), 
      new SqlParameter("@ModifyBy",SqlDbType.Int), 
      new SqlParameter("@ModifyTime",SqlDbType.DateTime) 
      };
     parameters[0].Value = SiteDataId; 
     parameters[1].Value = SiteCode; 
     parameters[2].Value = DeviceCode; 
     parameters[3].Value = ReleaseTime; 
     parameters[4].Value = AQI; 
     parameters[5].Value = AQIGrade; 
     parameters[6].Value = PM_25; 
     parameters[7].Value = PM_25_AQI; 
     parameters[8].Value = PM10; 
     parameters[9].Value = PM10_AQI; 
     parameters[10].Value = SO2; 
     parameters[11].Value = SO2_AQI; 
     parameters[12].Value = NO2; 
     parameters[13].Value = NO2_AQI; 
     parameters[14].Value = O3; 
     parameters[15].Value = O3_AQI; 
     parameters[16].Value = CO; 
     parameters[17].Value = CO_AQI; 
     parameters[18].Value = MainPollutant; 
     parameters[19].Value = TVOC; 
     parameters[20].Value = Temperature; 
     parameters[21].Value = Humidity; 
     parameters[22].Value = Press; 
     parameters[23].Value = WindDirection; 
     parameters[24].Value = WindSpeed; 
     parameters[25].Value = Disabled; 
     parameters[26].Value = Remark; 
     parameters[27].Value = CreateBy; 
     parameters[28].Value = CreateTime; 
     parameters[29].Value = ModifyBy; 
     parameters[30].Value = ModifyTime; 
       return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
     }
/// <summary> 
/// 删除一条数据 
/// </summary> 
public int delete() 
{ 
    StringBuilder strSql = new StringBuilder(); 
    strSql.Append("delete from EPS_SiteData ");
    strSql.Append(" where SiteDataId=@SiteDataId ");
    SqlParameter[] parameters = { 
    new SqlParameter("@SiteDataId", SqlDbType.Int,4) 
     };
    parameters[0].Value = SiteDataId;
    return  DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
} 
/// <summary> 
/// 获取数据列表 
/// </summary> 
public DataSet GetList(string strWhere) 
{ 
    StringBuilder strSql = new StringBuilder(); 
    strSql.Append(" select * from EPS_SiteData ");
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
    strSql = string.Format("select top {0} * from EPS_SiteData where id not in (select top {1} id from EPS_SiteData where {2} order by id desc) and ({2}) order by id desc", PageSize, PageSize * (PageIndex - 1), strWhere); 
    DataSet ds = DbHelperSQL.Query(strSql); 
    string strSql2 = string.Format("select id from EPS_SiteData where {0}", strWhere);
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
    strSql.Append(" delete from EPS_SiteData ");
    strSql.Append(string.Format(" where ID in ({0}) ", _idstr)); 
    return DbHelperSQL.ExecuteSql(strSql.ToString()); 
}
     /// <summary> 
     /// 获取站点24小时数据 
     /// </summary> 
public DataSet GetSiteDataByCode(string strWhere)
{
    StringBuilder strSql = new StringBuilder();
    strSql.Append("select datepart(hh,ReleaseTime) x_name,avg(AQI) AQI,avg(PM_25) PM_25,avg(PM10) PM10,avg(SO2) SO2,avg(NO2) NO2,avg(CO) CO,avg(O3) O3 " +
        "from [EPS_GridDB].[dbo].[EPS_SiteData] where Disabled=0 and CONVERT(varchar(10), ReleaseTime, 120) = (select CONVERT(varchar(10),max(sd.ReleaseTime),120)  releaseTime from [EPS_GridDB].[dbo].[EPS_SiteData] sd where sd.Disabled=0) ");
    if (strWhere.Trim() != "")
    {
        strSql.Append(" and " + strWhere);
    }
    //strSql.Append(string.Format(" and SiteCode={0} ", sitecode));
    strSql.Append(" group by datepart(hh, ReleaseTime) order by datepart(hh, ReleaseTime)");
    return DbHelperSQL.Query(strSql.ToString());
}

     /// <summary> 
     /// 获取所有站点的最新数据
     /// </summary> 
     /// 
public DataSet GetAllSiteDataList()
{
    StringBuilder strSql = new StringBuilder();

    strSql.Append("select sd.SiteCode,s.SiteName,s.Longitude,s.Latitude,CONVERT(varchar(16), sd.ReleaseTime,120) ReleaseTime,sd.AQI,sd.AQIGrade,sd.MainPollutant,sd.PM_25,sd.PM10,sd.SO2,sd.NO2,sd.CO,sd.O3 " +
        "from [EPS_GridDB].[dbo].[EPS_SiteData] sd inner join EPS_Site s on sd.SiteCode = s.SiteCode where sd.Disabled=0" +
        "and ReleaseTime in (select max(ReleaseTime) releaseTime from [EPS_GridDB].[dbo].[EPS_SiteData] where Disabled=0)");
    return DbHelperSQL.Query(strSql.ToString());
}

     /// <summary> 
     /// 获取污染年历数据
     /// </summary> 
     /// 
public DataSet GetPollutionCalendarData()
{
    StringBuilder strSql = new StringBuilder();

    strSql.Append("select substring(CONVERT(varchar(10),sd.ReleaseTime,120),1,4) ReleaseYear,substring(CONVERT(varchar(10),sd.ReleaseTime,120),6,2) ReleaseMonth,"+
    "substring(CONVERT(varchar(10),sd.ReleaseTime,120),9,2) ReleaseDay,round(avg(sd.AQI),0) AQI,'' Color "+
    "from [EPS_GridDB].[dbo].[EPS_SiteData] sd where sd.Disabled=0 and datediff(year,CreateTime,getdate())=0 group by CONVERT(varchar(10), sd.ReleaseTime, 120)");
    return DbHelperSQL.Query(strSql.ToString());
}
/// <summary> 
/// 获取本年度各月的AQIGrade参数
/// </summary> 
/// 
public DataSet GetSumAQIGradeForMonth()
{
    StringBuilder strSql = new StringBuilder();

    strSql.Append(" SELECT datepart(mm,CreateTime) month_name,"+
    　　" SUM(case AQIGrade when '优' then 1 else 0 end) AS nice, "+
	    " '优' as nicename, "+
        " SUM(case AQIGrade when '良' then 1 else 0 end) AS good,"+
		" '良' as goodname,"+
		" SUM(case AQIGrade when '轻度污染' then 1 else 0 end) AS mild,"+
		" '轻度污染' as mildname, "+
		" SUM(case AQIGrade when '中度污染' then 1 else 0 end) AS middle, "+
		" '中度污染' as middlename, "+
		" SUM(case AQIGrade when '重度污染' then 1 else 0 end) AS serious, "+
		" '重度污染' as seriousname, "+
		" SUM(case AQIGrade when '严重污染' then 1 else 0 end) AS severe,"+
		" '严重污染' as severename "+
       " FROM [EPS_GridDB].[dbo].[EPS_SiteData] "+
        " where DateDiff(year,CreateTime, GetDate()) = 0 and Disabled=0 "+
        " GROUP BY datepart(mm, CreateTime) "+
        " ORDER BY datepart(mm, CreateTime);");

    return DbHelperSQL.Query(strSql.ToString());
}
/// <summary> 
/// 获取当天平均温度
/// </summary> 
/// 
public DataSet GetTemperatureForDay()
{
    StringBuilder strSql = new StringBuilder();

    strSql.Append("SELECT cast(AVG(Temperature) as   decimal(18,2))as Temperature,cast(AVG(Humidity) as   decimal(18,2))as Humidity FROM [EPS_GridDB].[dbo].[EPS_SiteData] where DateDiff(day,CreateTime, GetDate()) = 0 and Disabled=0");

    return DbHelperSQL.Query(strSql.ToString());
}
/// <summary> 
/// 获取各地区污染等级超三级及以上数量
/// </summary> 
/// 
public DataSet GetSiteDataPolluteNum()
{
    StringBuilder strSql = new StringBuilder();

    strSql.Append("select s.SiteName,count(*) Num "+
	"from  [EPS_GridDB].[dbo].EPS_SiteData sd "+
	"inner join [EPS_GridDB].[dbo].EPS_Site s on s.SiteCode = sd.SiteCode "+
	"where sd.disabled = 0  and CONVERT(INT,sd.AQI)>100 "+
     "group by s.SiteName order by Num");
    return DbHelperSQL.Query(strSql.ToString());
}

/// <summary> 
/// 获取各地区污染各数据数据排名
/// </summary> 
/// 
public DataSet GetSiteDataPollutionRankingNum(int dateType)
{
            StringBuilder strSql = new StringBuilder();

            var Data = "avg(AQI)";
            if (dateType == 1)
            {
                Data = "avg(AQI)";

            }
            if (dateType == 2)
            {
                Data = "avg(PM10)";
            }
            if (dateType == 3)
            {
                Data = "avg(PM_25)";
            }
            if (dateType == 4)
            {
                Data = "avg(SO2)";
            }
            if (dateType == 5)
            {
                Data = "avg(NO2)";
            }
            if (dateType == 6)
            {
                Data = "avg(CO)";
            }
            if (dateType == 7)
            {
                Data = "avg(O3)";
            }
            if (dateType == 8)
            {
                Data = "avg(CompositeIndex)";
            }
            strSql.Append(string.Format("select s.SiteName,CONVERT(DECIMAL(18,2),{0}) Num " +
            "from  [EPS_GridDB].[dbo].EPS_SiteData sd " +
            "inner join [EPS_GridDB].[dbo].EPS_Site s on s.SiteCode = sd.SiteCode " +
            "where sd.disabled = 0  and DateDiff(dd,sd.ReleaseTime, GetDate()) =0" +
             "group by s.SiteName order by Num", Data));

            //var Data = "CONVERT(INT,sd.AQI)>100";
            //if (dateType == 1)
            //{
            //     Data = "CONVERT(INT,sd.AQI)>100";

            //}
            //if (dateType == 2)
            //{
            //     Data = "CONVERT(INT,sd.PM10)>150";
            //}
            //if (dateType == 3)
            //{
            //    Data = "CONVERT(INT,sd.PM_25)>75";
            //}
            //if (dateType == 4)
            //{
            //    Data = "CONVERT(INT,sd.SO2)>150";
            //}
            //if (dateType == 5)
            //{
            //    Data = "CONVERT(INT,sd.NO2)>80";
            //}
            //if (dateType == 6)
            //{
            //    Data = "CONVERT(INT,sd.CO)>4";
            //}
            //if (dateType == 7)
            //{
            //    Data = "CONVERT(INT,sd.O3)>200";
            //}
            //strSql.Append(string.Format("select s.SiteName,count(*) Num " +
            //"from  [EPS_GridDB].[dbo].EPS_SiteData sd " +
            //"inner join [EPS_GridDB].[dbo].EPS_Site s on s.SiteCode = sd.SiteCode " +
            //"where sd.disabled = 0  and {0} " +
            // "group by s.SiteName order by Num",Data));
            return DbHelperSQL.Query(strSql.ToString());
}



/// <summary> 
/// 获取当天各地区参数的变化趋势
/// </summary> 
/// 
public DataSet GetSiteDataChangeInThisDay(string siteCode)
{
    StringBuilder strSql = new StringBuilder();

    strSql.Append(string.Format("select datepart(hh,ReleaseTime) hour_name,AQI,PM_25,PM10,SO2,NO2,CO,O3 from [EPS_GridDB].[dbo].[EPS_SiteData] where Disabled=0 and DateDiff(dd,ReleaseTime, GetDate()) =0 and SiteCode='{0}' order by CreateTime",siteCode));

    return DbHelperSQL.Query(strSql.ToString());
}


/// <summary> 
/// 获取当月每天每小时的AQI平均数据
/// </summary> 
/// 
public DataSet GetSiteDataOfEachHour(string siteCode)
{
    StringBuilder strSql = new StringBuilder();
    if (!(siteCode==null||siteCode==""))
    {
          strSql.Append(string.Format("select year(ReleaseTime) as year_name, MONTH(ReleaseTime) as month_name, "+
        "DAY(ReleaseTime)as day_name, DATEPART(hour, ReleaseTime)as hour_name,cast(AVG(AQI) as   decimal(18,2)) as AQI " +
        "from [EPS_GridDB].[dbo].EPS_SiteData where Disabled=0 and datediff(mm,CreateTime,getdate())=0 and SiteCode='{0}'"+
         "group by year(ReleaseTime), MONTH(ReleaseTime), DAY(ReleaseTime), DATEPART(hour, ReleaseTime)",siteCode));
    }
    else
    {
          strSql.Append("select year(ReleaseTime) as year_name, MONTH(ReleaseTime) as month_name, "+
        "DAY(ReleaseTime)as day_name, DATEPART(hour, ReleaseTime)as hour_name,cast(AVG(AQI) as   decimal(18,2)) as AQI " +
        "from [EPS_GridDB].[dbo].EPS_SiteData where Disabled=0 and datediff(mm,CreateTime,getdate())=0 "+
         "group by year(ReleaseTime), MONTH(ReleaseTime), DAY(ReleaseTime), DATEPART(hour, ReleaseTime)");
    }


     return DbHelperSQL.Query(strSql.ToString());
}

        public DataSet GetPM25andPM10Rate(DateTime startdate, DateTime enddate, string siteName)
       
        {
             StringBuilder strSql = new StringBuilder();
            if (!(startdate == DateTime.MinValue) && !(enddate == DateTime.MinValue))
            {
                strSql.Append(string.Format("select datepart(hh,sd.ReleaseTime) hour_name,sd.PM10,sd.PM_25,s.SiteName,s.SiteCode from [EPS_GridDB].[dbo].[EPS_Site] s " +
                     "inner join [EPS_GridDB].[dbo].EPS_SiteData sd on s.SiteCode= sd.SiteCode " +
                     "where sd.Disabled=0  and s.Disabled=0 and sd.CreateTime>'{0}' and sd.CreateTime<'{1}'and s.SiteName='{2}' order by sd.CreateTime", startdate, enddate, siteName));

            }

           return DbHelperSQL.Query(strSql.ToString());
        }

       
    public DataSet GetPM25andPM10RateValue(string siteName)
    {
            StringBuilder strSql = new StringBuilder();
            
                    strSql.Append(string.Format("select datepart(hh,sd.ReleaseTime) hour_name,sd.PM10,sd.PM_25,s.SiteName,s.SiteCode from [EPS_GridDB].[dbo].[EPS_Site] s " +
                "inner join [EPS_GridDB].[dbo].EPS_SiteData sd on s.SiteCode= sd.SiteCode " +
                "where sd.Disabled=0  and s.Disabled=0 and datediff(dd,sd.CreateTime,getdate())=0 and s.SiteName='{0}' order by sd.CreateTime", siteName));


            return DbHelperSQL.Query(strSql.ToString());
    }
    public DataSet GetSiteNameCount(DateTime startdate, DateTime enddate)
    {
        StringBuilder strSql = new StringBuilder();
            if (!(startdate == DateTime.MinValue) && !(enddate == DateTime.MinValue))
            {
                strSql.Append(string.Format("select s.SiteName,count(*) total from [EPS_GridDB].[dbo].[EPS_Site] s " +
               "inner join [EPS_GridDB].[dbo].EPS_SiteData sd on s.SiteCode= sd.SiteCode " +
               "where sd.Disabled=0  and s.Disabled=0 and sd.CreateTime>'{0}' and sd.CreateTime<'{1}'  group by s.SiteName  order by s.SiteName", startdate, enddate));

            }


            return DbHelperSQL.Query(strSql.ToString());
    }


        public DataSet GetSiteNameTotal()
        {
            StringBuilder strSql = new StringBuilder();
             strSql.Append("select s.SiteName,count(*) total from [EPS_GridDB].[dbo].[EPS_Site] s " +
                                "inner join [EPS_GridDB].[dbo].EPS_SiteData sd on s.SiteCode= sd.SiteCode " +
                                 "where sd.Disabled=0  and s.Disabled=0 and datediff(dd,sd.CreateTime,getdate())=0 group by s.SiteName  order by s.SiteName");

            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion 方法成员
    }
}

