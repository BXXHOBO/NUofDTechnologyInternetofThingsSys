using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SUC_DataEntity;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SUC_DAL;

namespace SUC_EntityBLL
{
    public class BLL_Order
    {
        static string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["SUC_SYSConnectionString"].ToString();

        #region 新增或者更新任务表: 流程流转
        public String OrderSubmit(SUC_Order dto, t_user user, String flag)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {

                    SUC_Order order = SaveOrder(dto, user);
                    if (null != order)//流程继续流转
                    {
                        if ("submit".Equals(flag))//写日志记录
                        {
                            //写创建工单日志
                            SaveLog(order, user, "任务派发");
                            //String str = LogSubmit(order, user);
                            //if (!string.IsNullOrEmpty(str))
                            //{
                            //    return str;
                            //}
                        }
                    }
                    else
                    {
                        return "新增失败！";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return e.ToString();
                }
            }
            return "";
        }
        #endregion

        #region 新增任务明细表
        public string DetailsSubmit(List<SUC_OrderDetails> list, Guid orderId, t_user u, string editFlag,string dealUserId,string remark)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {
                    SUC_Order order = _DataEntities.SUC_Order.FirstOrDefault(o => o.OrderId == orderId && o.Disabled == 0);
                    if (order == null)
                    {
                        return "未获取到有效数据";
                    }

                    //任务检查详情更新最新的数据
                    if ("save".Equals(editFlag) || list.Count > 0)
                    {
                        //删除工单详情记录
                        bool x = DeleteDetail(orderId, u);
                        //添加新的工单详情记录
                        if (null != list && list.Count > 0 && x)
                        {
                            foreach (SUC_OrderDetails item in list)
                            {
                                item.OrderDetailsId = Guid.NewGuid();
                                item.OrderId = orderId;
                                item.CreateBy = u.UserId;
                                item.CreateTime = DateTime.Now;
                                item.ModifyBy = u.UserId;
                                item.ModifyTime = DateTime.Now;
                                item.Disabled = 0;
                                _DataEntities.SUC_OrderDetails.Add(item);
                            }
                        }
                        _DataEntities.SaveChanges();
                    }

                    if (!"save".Equals(editFlag))
                    {
                        //选择提交的话要进行流程流转
                        if ("submit".Equals(editFlag))
                        {
                        }
                        else if ("accept".Equals(editFlag))
                        {
                            if (order.OrderStatus != 1)
                            {
                                return "任务状态有误";
                            }
                        }
                        else if ("allot".Equals(editFlag))
                        {
                            if (order.OrderStatus != 2)
                            {
                                return "任务状态有误";
                            }
                            order.DealUserId = dealUserId;
                        }
                        else if ("check".Equals(editFlag))
                        {
                            if (order.OrderStatus != 3)
                            {
                                return "任务状态有误";
                            }
                            order.DealUserId = dealUserId;
                            order.Remark = remark;
                        }
                        //记录工单日志
                        LogSubmit(order, u);
                        order.OrderStatus += 1;
                        order.ModifyBy = u.UserId;
                        order.ModifyTime = DateTime.Now;
                        _DataEntities.SaveChanges();
                    }

                }
                catch (Exception e)
                {
                    LogPrintHelper.Info(e.ToString());
                    return e.ToString();
                }
            }
            return "";
        }
        #endregion

        public bool DeleteDetail(Guid orderId, t_user u)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //新增前删除原有的明细列表。没有删除标志，那就只新增
                List<SUC_OrderDetails> oldList = _DataEntities.SUC_OrderDetails.Where(o => o.OrderId == orderId && o.Disabled == 0).ToList();
                if (null != oldList && oldList.Count > 0)
                {
                    foreach (SUC_OrderDetails item in oldList)
                    {
                        item.Disabled = 1;
                        item.ModifyBy = u.UserId;
                        item.ModifyTime = DateTime.Now;
                    }
                    if (!(_DataEntities.SaveChanges() == oldList.Count))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #region 添加任务日志表,同时也伴随着订单表的流程流转
        public String LogSubmit(SUC_Order order, t_user u)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {
                    if (order.OrderStatus <= 4)
                    {
                        if (order.OrderStatus == 0)
                        {
                            SaveLog(order, u, "任务派发");
                        }
                        else if (order.OrderStatus == 1)
                        {
                            SaveLog(order, u, "任务接收");
                        }
                        else if (order.OrderStatus == 2)
                        {
                            SaveLog(order, u, "任务分配");
                        }
                        else if (order.OrderStatus == 3)
                        {
                            SaveLog(order, u, "任务审核");
                        }
                        else if (order.OrderStatus == 4)
                        {
                            SaveLog(order, u, "任务反馈");
                        }
                    }
                    else
                    {
                        //任务审核通过
                        return "任务已经通过 无法操作";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return e.ToString();
                }
                return "";
            }
        }
        #endregion

        public void SaveLog(SUC_Order order, t_user u, string remake)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                SUC_OrderLog log = new SUC_OrderLog
                {
                    OrderLogId = Guid.NewGuid(),
                    OrderId = order.OrderId,         
                    Remark = remake,
                    CreateBy = u.UserId,
                    CreateTime = DateTime.Now,
                    ModifyBy = u.UserId,
                    ModifyTime = DateTime.Now,
                    Disabled = 0
                };
                //if ((DateTime.Compare((DateTime)order.FinishTime, DateTime.Now)) < 0)
                //{
                //    log.OrderLogStatus = EpsDict.LOG_STATUS_OVER_TIME;//超时
                //}
                //else
                //{
                //    log.OrderLogStatus = EpsDict.LOG_STATUS_NORMAL;//未超时
                //}
                _DataEntities.SUC_OrderLog.Add(log);
                _DataEntities.SaveChanges();
            }
        }


        #region 任务列表查询
        public PageRes GetOrderList(string start, string end, String search, string orderStatus, int pageIndex, int pageSize, string sidx, string sord)
       {
            PageRes res = new PageRes();
            try
            {
                List<SqlParameter> _list = new List<SqlParameter>();
                string sql = "select row_number() over(order by so.CreateTime desc) as RowNo," +
                            "so.orderid," +
                            "so.OrderCode," +
                            "so.orderName," +
                            "CONVERT(varchar,so.FinishTime,120) FinishTime," +
                            "(select u.UserName from t_user u where u.userid = so.dealuserid) dealuserid," +
                            "(select ss.SiteName from EPS_Site ss where ss.SiteCode = so.SiteCode) SiteName," +
                            "so.OrderStatus, " +
                            "case so.OrderStatus when '0' then '任务派发' when '1' then '任务接收' when '2' then '任务分配' when '3' then '任务审核' when '4' then '任务反馈'  when '5' then '任务办结' end as OrderStatusStr," +
                            "case so.IsNormal when '0' then '是' when '1' then '否'  end as IsNormal ," +
                            "case so.IsSupplement when '0' then '是' when '1' then '否'  end as IsSupplement, " +
                            "case so.OrderType when '1' then '无人机勘查' when '2' then '网格员现场勘查'  end as OrderType," +
                            "so.Remark, " +
                            "so.CreateTime " +
                            "from SUC_Order so " +
                            "where so.Disabled = 0";
                if (!string.IsNullOrEmpty(search))
                {
                    search = "%" + search + "%";
                    sql += " and (so.OrderCode like @search ";
                    sql += " or so.orderName like @search) ";
                    _list.Add(new SqlParameter("@search", search));
                }
                if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                { 
                    sql += " and CreateTime >= @start and CreateTime <= @end ";
                    _list.Add(new SqlParameter("@start", start));
                    _list.Add(new SqlParameter("@end", end));
                }
                if (!string.IsNullOrEmpty(orderStatus))
                {
                    if (orderStatus == "98")
                    {
                        sql += " and so.OrderStatus in(2,3,4) ";
                    }
                    else if (orderStatus == "99")
                    {
                        sql += " and so.OrderStatus = 5 ";
                    }
                    else
                    {
                        sql += " and so.OrderStatus=@orderStatus ";
                        _list.Add(new SqlParameter("@orderStatus", orderStatus));
                    }
                   
                }

                //if (!string.IsNullOrEmpty(createBy))
                //{
                //    sql += "and so.CreateBy=@createBy ";
                //    _list.Add(new SqlParameter("@createBy", createBy));
                //}
                //if (!string.IsNullOrEmpty(date))
                //{
                //    sql += "and so.CreateTime=@date ";
                //    date = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                //    _list.Add(new SqlParameter("@date", date));
                //}
                SqlParameter[] _param = _list.ToArray();
                res.page = pageIndex;
                res.pageSize = pageSize;
                //总数
                string countSql = "select count(*) tCount from(" + sql + ")t";
                DataTable dtCount = SqlHelper.ExecuteDatatable(strCon, CommandType.Text, countSql, _param);
                int tCount = dtCount != null ? Convert.ToInt32(dtCount.Rows[0]["tCount"]) : 0;
                res.total = tCount;
                //数据
                sql = "select top " + pageSize + " * from( " + sql + ") temp_row where RowNo> ((" + pageIndex + " - 1) * " + pageSize + ")";
                //排序
                if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                {
                    //string orderbyStr = $" order by {sidx} {sord} ";
                    string orderbyStr = string.Format(" order by {0} {1} ", sidx, sord);
                    sql += orderbyStr;
                }
                DataTable dt = SqlHelper.ExecuteDatatable(strCon, CommandType.Text, sql, _param);
                List<OrderData> list = new List<OrderData>();
                foreach (DataRow item in dt.Rows)
                {
                    OrderData sd = new OrderData
                    {
                        OrderId = (Guid)item["orderid"],//id
                        RowNo = Convert.ToInt32(item["RowNo"]),//序号
                        OrderCode = item["OrderCode"].ToString(),//工单号
                        SiteName = item["SiteName"].ToString(),//站点名称
                        OrderStatus = Convert.ToInt32(item["OrderStatus"]),//序号
                        OrderStatusStr = item["OrderStatusStr"].ToString(),//工单状态
                        FinishTimeStr = item["FinishTime"] != DBNull.Value ? Convert.ToDateTime(item["FinishTime"]).ToString() : "",//完成时间
                        OrderTypeStr = item["OrderType"].ToString(),//工单类型
                        OrderName = item["orderName"].ToString(),//工单标题
                        UserName = item["dealuserid"].ToString(),//处理人
                        IsSupplementStr = item["IsSupplement"].ToString(),//是否补录
                        IsNormalStr = item["IsNormal"].ToString(),//是否采样
                        Remark = item["Remark"].ToString()//工单内容
                    };
                    list.Add(sd);
                }
                res.rows = list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return res;
        }

        public object GetOrderByCode(string orderCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var order = _DataEntities.SUC_Order.Where(o => o.OrderCode == orderCode && o.Disabled == 0).FirstOrDefault();
                if (order == null)
                {
                    return null;
                }
                else
                {
                    var CreateTime = order.CreateTime != null ? order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") : "";
                    //var FinishTime = order.FinishTime != null ? order.FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

                    var result =  new
                    {
                        order.CreateBy,
                        CreateTime = CreateTime,
                        order.DealUserId,
                        order.Disabled,
                        //FinishTime = FinishTime,
                        //order.IsNormal,
                        order.IsSupplement,
                        order.OrderCode,
                        order.OrderId,
                        order.OrderName,
                        order.OrderStatus,
                        order.OrderType,
                        order.Remark,
                        //order.SiteCode,
                        //order.SiteDataId
                    };
                    return result;
                }
            }
        }
        #endregion
        #region 获取任务下派数量
        public int GetTaskNum()
        {
            List<SqlParameter> _list = new List<SqlParameter>();
            string sql = "select * from [EPS_GridDB].[dbo].SUC_Order where Disabled=0 and OrderStatus=1";
            SqlParameter[] _param = _list.ToArray();
            string countSql = "select count(*) tCount from(" + sql + ")t";
            DataTable dtCount = SUC_DAL.SqlHelper.ExecuteDatatable(strCon, CommandType.Text, countSql, _param);
            int tCount = dtCount != null ? Convert.ToInt32(dtCount.Rows[0]["tCount"]) : 0;
            return tCount;
        }

        #endregion

        #region 删除任务
        public ResultRes DeleteOrder(String orderCode, t_user u)
        {
            ResultRes res = new ResultRes();
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {
                    res.IsSuccess = true;
                    res.Msg = "工单删除成功！";
                    List<SUC_Order> orderlist = _DataEntities.SUC_Order.Where(o => o.OrderCode == orderCode && o.Disabled == 0).ToList();
                    if (orderlist.Count > 0 && null != orderlist)
                    {
                        //逻辑删除
                        foreach (SUC_Order item1 in orderlist)
                        {
                            item1.Disabled = 1;
                            item1.ModifyBy = u.UserId;
                            item1.ModifyTime = DateTime.Now;
                            _DataEntities.SaveChanges();
                            //删除明细
                            List<SUC_OrderDetails> detalslist = _DataEntities.SUC_OrderDetails
                                .Where(o => o.OrderId == item1.OrderId && o.Disabled == 0).ToList();
                            if (detalslist.Count > 0 && null != detalslist)
                            {
                                foreach (SUC_OrderDetails item2 in detalslist)
                                {
                                    item2.Disabled = 1;
                                    item2.ModifyBy = u.UserId;
                                    item2.ModifyTime = DateTime.Now;
                                    _DataEntities.SaveChanges();
                                }
                            }
                            //删除日志
                            List<SUC_OrderLog> loglist = _DataEntities.SUC_OrderLog
                                .Where(o => o.OrderId == item1.OrderId && o.Disabled == 0).ToList();
                            if (loglist.Count > 0 && null != loglist)
                            {
                                foreach (SUC_OrderLog item3 in loglist)
                                {
                                    item3.Disabled = 1;
                                    item3.ModifyBy = u.UserId;
                                    item3.ModifyTime = DateTime.Now;
                                    _DataEntities.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Msg = e.ToString();
            }
            return res;
        }
        #endregion

        //保存任务 order
        public SUC_Order SaveOrder(SUC_Order dto, t_user user)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                SUC_Order order = _DataEntities.SUC_Order.
                        FirstOrDefault(o => o.OrderId == dto.OrderId && o.Disabled == 0);
                if (null != order)//update
                {
                    order.OrderCode = dto.OrderCode;
                    //order.SiteCode = dto.SiteCode;
                    //order.OrderName = dto.OrderName;
                    //order.OrderType = dto.OrderType;
                    //order.IsNormal = dto.IsNormal;
                    order.IsSupplement = dto.IsSupplement;
                    //order.FinishTime = dto.FinishTime;
                    order.Disabled = dto.Disabled;
                    order.Remark = dto.Remark;
                    order.ModifyBy = user.UserId;
                    order.ModifyTime = DateTime.Now;
                    //根据当前站点找到站点负责人并且派发任务给他
                    //EPS_Site site = _DataEntities.EPS_Site.Where(z => z.SiteCode == dto.SiteCode && z.Disabled != 1).ToList().FirstOrDefault();
                    //if (null != site)
                    //{
                    //    order.DealUserId = site.UserId;
                    //}
                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return order;
                    }
                }
                else//insert
                {
                    dto.OrderId = Guid.NewGuid();
                    dto.Disabled = 0;
                    dto.ModifyBy = user.UserId;
                    dto.ModifyTime = DateTime.Now;
                    dto.CreateBy = user.UserId;
                    dto.CreateTime = DateTime.Now;
                    //0:任务派发 1：任务接收 2：任务分配 3：任务审核 4:任务反馈
                    //现在流程创建直接完成下派工单，到上报工单步骤，如果需要分配下派工单再改成0
                    dto.OrderStatus = 1;
                    //根据当前站点找到站点负责人并且派发任务给他
                    //EPS_Site site = _DataEntities.EPS_Site.Where(z => z.SiteCode == dto.SiteCode && z.Disabled != 1).ToList().FirstOrDefault();
                    //if (null != site)
                    //{
                    //    dto.DealUserId = site.UserId;
                    //}
                    _DataEntities.SUC_Order.Add(dto);

                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return dto;
                    }
                }
                return null;
            }
        }

        #region 根据任务单号获取单号的详情信息
        public OrderModel GetAllDetail(String orderCode)
        {
            OrderModel model = null;
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {
                    List<OrderData> orderList = GetOrder(orderCode);
                    if (null != orderList && orderList.Count() > 0)
                    {
                        OrderData order = orderList.First(t => t.OrderCode == orderCode);
                        List<SUC_OrderDetails> Details = _DataEntities.SUC_OrderDetails.Where(o => o.OrderId == order.OrderId && o.Disabled == 0).ToList();
                        if (null == Details)
                        {
                            Details = new List<SUC_OrderDetails>();
                        }
                        List<OrderLogData> Logs = GetLogList(order.OrderId);
                        model = new OrderModel(order, Details, Logs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return model;

        }
        #endregion

        //获取任务详情
        public List<OrderData> GetOrder(String orderCode)
        {
            List<OrderData> list = new List<OrderData>();
            try
            {
                List<SqlParameter> _list = new List<SqlParameter>();
                string sql = "select row_number() over(order by so.CreateTime desc) as RowNo," +
                            "so.orderid," +
                            "so.OrderCode," +
                            "so.orderName," +
                            "CONVERT(varchar,so.FinishTime,120) FinishTime," +
                            "(select u.UserName from t_user u where u.userid = so.dealuserid) dealuserid," +
                            "(select ss.SiteName from EPS_Site ss where ss.SiteCode = so.SiteCode) SiteName," +
                            "so.OrderStatus, " +
                            "case so.OrderStatus when '0' then '下派工单' when '1' then '上报工单' when '2' then '工单审核'  when '3' then '工单完成' end as OrderStatusStr," +
                            "case so.IsNormal when '0' then '是' when '1' then '否'  end as IsNormal ," +
                            "case so.IsSupplement when '0' then '是' when '1' then '否'  end as IsSupplement, " +
                            "case so.OrderType when '1' then '无人机勘查' when '2' then '网格员现场勘查'  end as OrderType," +
                            "so.Remark " +
                            "from SUC_Order so " +
                            "where so.Disabled = 0";
                if (!string.IsNullOrEmpty(orderCode))
                {
                    sql += "and so.OrderCode=@orderCode ";
                    _list.Add(new SqlParameter("@orderCode", orderCode));
                }
                SqlParameter[] _param = _list.ToArray();
                //数据
                DataTable dt = SUC_DAL.SqlHelper.ExecuteDatatable(strCon, CommandType.Text, sql, _param);
                foreach (DataRow item in dt.Rows)
                {
                    OrderData sd = new OrderData
                    {
                        OrderId = (Guid)item["orderid"],//id
                        RowNo = Convert.ToInt32(item["RowNo"]),//序号
                        OrderCode = item["OrderCode"].ToString(),//工单号
                        SiteName = item["SiteName"].ToString(),//站点名称
                        OrderStatus = Convert.ToInt32(item["OrderStatus"]),//序号
                        OrderStatusStr = item["OrderStatusStr"].ToString(),//工单状态
                        FinishTimeStr = item["FinishTime"] != DBNull.Value ? Convert.ToDateTime(item["FinishTime"]).ToString() :"",//完成时间
                        OrderTypeStr = item["OrderType"].ToString(),//工单类型
                        OrderName = item["orderName"].ToString(),//工单标题
                        UserName = item["dealuserid"].ToString(),//处理人
                        IsSupplementStr = item["IsSupplement"].ToString(),//是否补录
                        IsNormalStr = item["IsNormal"].ToString(),//是否采样
                        Remark = item["Remark"].ToString()//工单内容
                    };
                    list.Add(sd);
                }
            }
            catch (Exception e)
            {
                LogPrintHelper.Info(e.ToString());
            }
            return list;
        }


        //获取日志详情
        public List<OrderLogData> GetLogList(Guid orderId)
        {
            List<OrderLogData> list = new List<OrderLogData>();
            try
            {
                List<SqlParameter> _list = new List<SqlParameter>();
                string sql = "select row_number() over( partition by l.orderid order by l.CreateTime desc) as RowNo , " +
                            "l.Remark," +
                            "(select u.UserName from t_user u where u.userid = l.Inspectors_id) UserName," +
                            "case l.orderlogstatus when 0 then '未超时' when 1 then '超时' end as orderlogstatus ," +
                            "CONVERT(varchar,l.ModifyTime,120) ModifyTime " +
                            "from SUC_OrderLog l where l.Disabled=0";
                if (orderId != null)
                {
                    sql += "and l.OrderId=@OrderId";
                    _list.Add(new SqlParameter("@OrderId", orderId));
                }
                else
                {
                    return null;
                }
                DataTable dt = SUC_DAL.SqlHelper.ExecuteDatatable(strCon, CommandType.Text, sql, _list.ToArray());
                foreach (DataRow item in dt.Rows)
                {
                    OrderLogData sd = new OrderLogData
                    {
                        RowNo = Convert.ToInt32(item["RowNo"]),//序号
                        Remark = item["Remark"].ToString(),//说明
                        InspectorName = item["UserName"].ToString(),//处理人姓名
                        OrderLogStatusStr = item["orderlogstatus"].ToString(),//超时状态
                        ModifyByStr = Convert.ToDateTime(item["ModifyTime"]).ToString(),//完成时间
                    };
                    list.Add(sd);
                }

            }
            catch (Exception e)
            {
                LogPrintHelper.Info("BLL_Order.GetLogList error ===>" + e.ToString());
            }
            return list;
        }

        /// <summary>
        /// 按时间获取任务,并创建excel
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public object CreateOrderExcel(DateTime start, DateTime end, string filePath)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                #region 获取数据
                var datas = (from o in _DataEntities.SUC_Order.Where(o => o.CreateTime >= start && o.CreateTime <= end && o.Disabled != 1)
                             join u in _DataEntities.t_user.Where(u => u.Disabled != 1)
                             on o.DealUserId equals u.UserId
                             //join s in _DataEntities.EPS_Site.Where(u => u.Disabled != 1)
                             //on o.SiteCode equals s.SiteCode
                             orderby o.CreateTime descending
                             select new
                             {
                                 o.OrderCode,
                                 o.OrderName,
                                 o.CreateTime,
                                 //o.FinishTime,
                                 u.UserName,
                                 //s.SiteName,
                                 o.OrderStatus,
                                 o.OrderType,
                                 //o.IsNormal,
                                 o.IsSupplement
                             }).ToList();
                //处理数据格式
                var dataList = datas.Select(s =>
                {
                    string orderStatus = "";
                    switch (s.OrderStatus)
                    {
                        case 0:
                            orderStatus = "下派工单";
                            break;
                        case 1:
                            orderStatus = "上报工单";
                            break;
                        case 2:
                            orderStatus = "工单审核";
                            break;
                        case 3:
                            orderStatus = "完成工单";
                            break;
                    }

                    string orderType = "";
                    switch (s.OrderType)
                    {
                        case 1:
                            orderType = "无人机勘查";
                            break;
                        case 2:
                            orderType = "网格员现场勘查";
                            break;
                    }

                    var createTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //var finishTime = s.FinishTime != null ? s.FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                    return new
                    {
                        s.OrderCode,
                        s.OrderName,
                        CreateTime = createTime,
                        //FinishTime = finishTime,
                        s.UserName,
                        //s.SiteName,
                        OrderStatus = orderStatus,
                        OrderType = orderType,
                        //IsNormal = s.IsNormal == 0 ? "是" : "否",
                        IsSupplement = s.IsSupplement == 0 ? "是" : "否"
                    };
                }).ToList();

                #endregion 

                //编辑excel
                IWorkbook wb = new HSSFWorkbook();
                //创建表  
                ISheet sh = wb.CreateSheet("renwu");
                #region 编辑表头
                IRow row0 = sh.CreateRow(0);
                ICell icell1top = row0.CreateCell(0);
                icell1top.SetCellValue("工单号");
                ICell icell2top = row0.CreateCell(1);
                icell2top.SetCellValue("工单标题");
                ICell icell3top = row0.CreateCell(2);
                icell3top.SetCellValue("计划完成时间");
                ICell icell4top = row0.CreateCell(3);
                icell4top.SetCellValue("创建时间");
                ICell icell5top = row0.CreateCell(4);
                icell5top.SetCellValue("处理人");
                ICell icell6top = row0.CreateCell(5);
                icell6top.SetCellValue("站点名称");
                ICell icell7top = row0.CreateCell(6);
                icell7top.SetCellValue("工单状态");
                ICell icell8top = row0.CreateCell(7);
                icell8top.SetCellValue("工单类型");
                ICell icell9top = row0.CreateCell(8);
                icell9top.SetCellValue("是否正常");
                ICell icell10top = row0.CreateCell(9);
                icell10top.SetCellValue("是否补录");
                #endregion
                #region excel主体数据
                for (int i = 0; i < dataList.Count; i++)
                {
                    var tempData = dataList[i];
                    var index = i + 1;
                    //工单号
                    IRow tempRow = sh.CreateRow(index);
                    ICell tempCell = tempRow.CreateCell(0);
                    tempCell.SetCellValue(tempData.OrderCode);
                    //工单标题
                    ICell tempCell1 = tempRow.CreateCell(1);
                    tempCell1.SetCellValue(tempData.OrderName);
                    //计划完成时间
                    ICell tempCel2 = tempRow.CreateCell(2);
                    //tempCel2.SetCellValue(tempData.FinishTime);
                    //创建时间
                    ICell tempCell3 = tempRow.CreateCell(3);
                    tempCell3.SetCellValue(tempData.CreateTime);
                    //处理人
                    ICell tempCell4 = tempRow.CreateCell(4);
                    tempCell4.SetCellValue(tempData.UserName);
                    //站点名字
                    ICell tempCell5 = tempRow.CreateCell(5);
                    //tempCell5.SetCellValue(tempData.SiteName);
                    //工单状态
                    ICell tempCell6 = tempRow.CreateCell(6);
                    //tempCell6.SetCellValue(tempData.SiteName);
                    //工单类型
                    ICell tempCell7 = tempRow.CreateCell(7);
                    tempCell7.SetCellValue(tempData.OrderType);
                    //是否正常
                    ICell tempCell8 = tempRow.CreateCell(8);
                    //tempCell8.SetCellValue(tempData.IsNormal);
                    //是否补录
                    ICell tempCell9 = tempRow.CreateCell(9);
                    tempCell9.SetCellValue(tempData.IsSupplement);
                }

                using (FileStream stm = File.OpenWrite(filePath))
                {
                    wb.Write(stm);
                    Console.WriteLine("提示：创建成功！");
                    wb.Close();
                }


                #endregion


                return datas;
            }
        }

    }

    /**
     * 查询列表返回的数据类
     */
    public class OrderData : SUC_Order
    {
        public int RowNo { get; set; }
        public string FinishTimeStr { get; set; }

        public string SiteName { get; set; }

        public string UserName { get; set; }

        public string OrderStatusStr { get; set; }
        public string IsNormalStr { get; set; }

        public string IsSupplementStr { get; set; }

        //工单类型
        public String OrderTypeStr { get; set; }
    }

    public class OrderLogData : SUC_OrderLog
    {
        public int RowNo { get; set; }

        //巡检员姓名
        public string InspectorName { get; set; }

        //任务状态 0未超时； 1超时
        public string OrderLogStatusStr { get; set; }

        public string ModifyByStr { get; set; }
    }

    /**
     * 获取order详情封装的数据类
     */
    public class OrderModel
    {
        public OrderData Order { get; set; }

        public List<SUC_OrderDetails> Details { get; set; }

        public List<OrderLogData> Logs { get; set; }

        public OrderModel(OrderData Order, List<SUC_OrderDetails> Details, List<OrderLogData> Logs)
        {
            this.Order = Order;
            this.Details = Details;
            this.Logs = Logs;
        }



    }
}
