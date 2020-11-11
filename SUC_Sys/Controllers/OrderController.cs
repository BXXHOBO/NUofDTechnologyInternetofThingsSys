using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SUC_DataEntity;
using SUC_EntityBLL;

namespace SUC_Sys.Controllers
{

    public class OrderController : Controller
    {
        BLL_Order bll_order = new BLL_Order();
        
        WxChat wxpush = new WxChat();
        private t_user GetUser()
        {
            return (t_user)Session["Users"];
        }

        #region 任务分配
        // GET: Order
        public ActionResult Add()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                //生成工单号
                SUC_Order a = new SUC_Order
                {
                    OrderCode = RandomHelper.GenerateRandomCode(11),
                    OrderStatus = 0,
                    
                    IsSupplement = 1
                };
                ViewData["siteList"] = DictList(EpsDict.ORDER_SITE_DICT);
                ViewData["isList"] = DictList(EpsDict.ORDER_IS_DICT);
                List<SelectListItem> statusList = DictList(EpsDict.ORDER_STATUS_DICT);
                foreach (SelectListItem item in statusList)
                {
                    if (item.Value == "0")
                    {
                        item.Selected = true;
                    }
                }
                ViewData["statusList"] = statusList;
                ViewData["typeList"] = DictList(EpsDict.ORDER_TYPE_DICT);
                return View(a);
            }
        }
        #endregion

        #region 获取码表集合。传入码值获取对应的码表 DictList
        public List<SelectListItem> DictList(String flag)
        {
            List<SelectListItem> lsl = new List<SelectListItem>();

            if (EpsDict.ORDER_SITE_DICT.Equals(flag))//站点列表
            {
                //List<EPS_Site> list = bll_site.GetSite();
                lsl.Add(new SelectListItem { Text = "请选择站点", Value = "" });
                //if (list != null && list.Count > 0)
                //{
                //    foreach (var item in list)
                //    {
                //        SelectListItem sl = new SelectListItem
                //        {
                //            Value = item.SiteCode.ToString(),//站点代码
                //            Text = item.SiteName.ToString()//站点名称
                //        };
                //        lsl.Add(sl);
                //    }
                //}
            }
            else if (EpsDict.ORDER_IS_DICT.Equals(flag))// 是否
            {
                lsl = new List<SelectListItem>
                {
                    new SelectListItem { Text = "请选择", Value = "" },
                    new SelectListItem { Text = "是", Value = "0" },
                    new SelectListItem { Text = "否", Value = "1" }
                };
            }
            else if (EpsDict.ORDER_STATUS_DICT.Equals(flag))// 任务状态
            {
                lsl = new List<SelectListItem>
                {
                    //new SelectListItem { Text = "任务派发", Value = "0" },
                    new SelectListItem { Text = "任务接收", Value = "1" },
                    new SelectListItem { Text = "任务分配", Value = "2" },
                    new SelectListItem { Text = "任务审核", Value = "3" },
                    new SelectListItem { Text = "任务反馈", Value = "4" },
                    new SelectListItem { Text = "任务完成", Value = "5" },
                    //new SelectListItem { Text = "处理完成", Value = "2" }
                };
            }
            else if (EpsDict.ORDER_TYPE_DICT.Equals(flag))// 工单类型
            {
                lsl = new List<SelectListItem>
                {
                    new SelectListItem { Text = "请选择类型", Value = "" },
                    new SelectListItem { Text = "无人机勘查", Value = "1" },
                    new SelectListItem { Text = "网格员现场勘查", Value = "2" }
                };
            }
            else if (EpsDict.ORDER_PROJECT_DICT.Equals(flag))//项目明细
            {
                lsl = new List<SelectListItem>
                {
                    new SelectListItem { Text = "请选择类型", Value = "" },
                    new SelectListItem { Text = "SO2", Value = "SO2" },
                    new SelectListItem { Text = "NO2", Value = "NO2" },
                    new SelectListItem { Text = "CO", Value = "CO" },
                    new SelectListItem { Text = "O3", Value = "O3" },
                    new SelectListItem { Text = "PM_25", Value = "PM_25" },
                    new SelectListItem { Text = "PM10", Value = "PM10" }
                };
            }
            else if (EpsDict.ORDER_DETAIL_STATUS_DICT.Equals(flag))//任务详情状态
            {
                lsl = new List<SelectListItem>
                {
                    new SelectListItem { Text = "请选择类型", Value = "" },
                    new SelectListItem { Text = "有效", Value = "0" },
                    new SelectListItem { Text = "无效", Value = "1" }
                };
            }

          


            return lsl;
        }
        #endregion

        #region 任务保存和更新 editFlag:save（保存） / submit(提交)
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult SubmitOrder(SUC_Order order, string editFlag)
        {
            ResultRes res = new ResultRes();
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }

            String msg = bll_order.OrderSubmit(order, user, editFlag);

            if (string.IsNullOrEmpty(msg))
            {
                res.Msg = "操作成功！";
                res.IsSuccess = true;

                string desp = "任务下派通知\r\n\r\n" + "任务编号：" + order.OrderCode.ToString() +"\r\n\r\n"+ res.Msg + "\r\n\r\n操作人：" + user.UserName.ToString() + "\r\n\r\n" + DateTime.Now;
                wxpush.ServerChanPush(0, desp);//微信通知

                //try
                //{

                //    wxpush.ServerChanPush(0, desp);//微信通知
                //}
                //catch
                //{
                //}


            }
            else
            {
                res.Msg = "操作失败！" + msg;
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 任务详情保存和更新 editFlag:save（保存） / submit(提交)
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult SubmitDetails(Guid orderId, string editFlag,string dealUserId = "",string remark = "")
        {
            ResultRes res = new ResultRes();
            t_user user = (t_user)Session["Users"];

            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }
            string json = Request["params"];

            List<SUC_OrderDetails> list = new List<SUC_OrderDetails>();
            if (json != null && json.StartsWith("[") && json.EndsWith("]"))
            {
                JArray jsonRsp = JArray.Parse(json);

                if (jsonRsp != null)
                {
                    for (int i = 0; i < jsonRsp.Count; i++)
                    {
                        JsonSerializer js = new JsonSerializer();
                        object obj = js.Deserialize(jsonRsp[i].CreateReader(), typeof(SUC_OrderDetails));
                        list.Add((SUC_OrderDetails)obj);
                    }
                }
            }

            string msg = bll_order.DetailsSubmit(list, orderId, user, editFlag, dealUserId, remark);
            if (string.IsNullOrEmpty(msg))
            {
                res.Msg = "操作成功！";
                res.IsSuccess = true;
            }
            else
            {
                res.Msg = "操作失败！" + msg;
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 任务作业 视图
        public ActionResult List()
        {
            t_user entity = (t_user)Session["Users"];

            //获取状态的下拉选
            List<SelectListItem> statusList = DictList(EpsDict.ORDER_STATUS_DICT);
            statusList.Insert(0, new SelectListItem { Text = "全部", Value = "" });
            ViewData["statusList"] = statusList;
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 发布任务视图
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishOrderList()
        {
            t_user entity = (t_user)Session["Users"];

            //获取状态的下拉选
            //List<SelectListItem> statusList = DictList(EpsDict.ORDER_STATUS_DICT);
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Insert(0, new SelectListItem { Text = "待接收", Value = "1" });
            statusList.Insert(1, new SelectListItem { Text = "办理中", Value = "98" });
            statusList.Insert(2, new SelectListItem { Text = "已办结", Value = "99" });
            ViewData["statusList"] = statusList;
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 接收任务视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AcceptOrderList()
        {
            t_user entity = (t_user)Session["Users"];

            //获取状态的下拉选
            //List<SelectListItem> statusList = DictList(EpsDict.ORDER_STATUS_DICT);
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Insert(0, new SelectListItem { Text = "未接收", Value = "1" });
            statusList.Insert(1, new SelectListItem { Text = "已接收", Value = "2" });
            ViewData["statusList"] = statusList;
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 分配任务视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AllotOrderList()
        {
            t_user entity = (t_user)Session["Users"];

            //获取状态的下拉选
            //List<SelectListItem> statusList = DictList(EpsDict.ORDER_STATUS_DICT);
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Insert(0, new SelectListItem { Text = "未分配", Value = "2" });
            statusList.Insert(1, new SelectListItem { Text = "已分配", Value = "3" });
            ViewData["statusList"] = statusList;
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 任务审核视图
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOrderList()
        {
            t_user entity = (t_user)Session["Users"];

            //获取状态的下拉选
            //List<SelectListItem> statusList = DictList(EpsDict.ORDER_STATUS_DICT);
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Insert(0, new SelectListItem { Text = "未审核", Value = "3" });
            statusList.Insert(1, new SelectListItem { Text = "已审核", Value = "4" });
            ViewData["statusList"] = statusList;
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 任务审核视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FeelBackOrderList()
        {
            t_user entity = (t_user)Session["Users"];

            //获取状态的下拉选
            //List<SelectListItem> statusList = DictList(EpsDict.ORDER_STATUS_DICT);
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Insert(0, new SelectListItem { Text = "未反馈", Value = "4" });
            statusList.Insert(1, new SelectListItem { Text = "已反馈", Value = "5" });
            ViewData["statusList"] = statusList;
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region 编辑任务获取相关信息
        /// <summary>
        /// 编辑任务获取相关信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEditInfo(string orderCode)
        {
            ResultRes res = new ResultRes();
            //List<EPS_Site> list = bll_site.GetSite();

            object OrderInfo = null;
            //获取任务信息
            var order = bll_order.GetOrderByCode(orderCode);
            //获取站点人员（暂时是全部人员，需求没有确认）
            //var users = bll_site.GetSiteUsers();
            if (order != null)
            {
                OrderInfo = order;
            }
            res.Msg = "操作成功！";
            res.IsSuccess = true;
            res.ResultValue = new
            {
                //SiteList = list,
                OrderInfo = OrderInfo,
                //Users = users
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 工单查询任务列表接口
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetOrderList()
        {
            string orderStatus = Request["orderStatus"]; //订单状态
            String search = Request["search"]; //搜索字段
            string start = Request["start"]; //开始时间
            string end = Request["end"]; //结束时间
            int pageIndex = Convert.ToInt32(Request["page"]); //页码
            int pageSize = Convert.ToInt32(Request["rows"]); //每页显示数
            string sidx = Request["sidx"]; //排序字段
            string sord = Request["sord"]; //升降序
            if (!(String.IsNullOrEmpty(search)))
            {
                search = search.Trim();
            }

            //String createBy = Request["createBy"];
            //string date = Request["txtDate"];
            //if (!(String.IsNullOrEmpty(createBy)))
            //{
            //    createBy = createBy.Trim();
            //}
            //if (!(String.IsNullOrEmpty(date)))
            //{
            //    date = date.Trim();
            //}
            PageRes res = bll_order.GetOrderList(start, end, search, orderStatus, pageIndex, pageSize, sidx, sord);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 进入任务详情，获取任务详情信息，需要获取日志
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            t_user user = (t_user)Session["Users"];
            if (user == null)
            {
                RedirectToAction("Login", "Account");
            }
            String orderCodeId = Request.QueryString["OrderCode"];
            //String orderCodeId1 = Request["OrderCode"];
            if (!(String.IsNullOrEmpty(orderCodeId)))
            {
                orderCodeId = orderCodeId.Trim();
            }
            OrderModel model = bll_order.GetAllDetail(orderCodeId);
            if (null != model)
            {
                ViewData["orderId"] = model.Order.OrderId;
                ViewData["projects"] = DictList(EpsDict.ORDER_PROJECT_DICT); ;
                ViewData["detailstatus"] = DictList(EpsDict.ORDER_DETAIL_STATUS_DICT);
            }
            return View(model);
        }
        #endregion


        #region 删除数据 根据工单号进行删除
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult DeleteOrder(string id)
        {
            string ids = Request["ids"] ?? "";
            ids = ids != "" ? ids : id;
            t_user user = null;
            if (Session["Users"] != null)
            {
                user = (Session["Users"] as t_user);
            }
            else
            {
                RedirectToAction("Login", "Account");
            }

            ResultRes res = bll_order.DeleteOrder(ids, user);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 上传图片
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpImages(string detailid)
        {
            ImagRes res = new ImagRes();
            try
            {
                if (Request.Files.Count == 0)
                    throw new Exception("请选择文件");
                System.Web.HttpPostedFileBase fileData = Request.Files[0];
                if (fileData == null)
                    throw new Exception("请选择文件");

                string sExtensionName = Path.GetExtension(fileData.FileName);

                if (sExtensionName.ToLower() != ".gif" && sExtensionName.ToLower() != ".jpg" && sExtensionName.ToLower() != ".jpeg" &&
                       sExtensionName.ToLower() != ".png")
                {
                    res.error = 1;
                    res.message = "上传文件不为图片!";
                }
                else
                {
                    var folderpath = "/uploadimgs/" + DateTime.Now.ToString("yyyy-MM") + "/";
                    var sSaveFile = Server.MapPath(folderpath);

                    //创建文件夹
                    if (!System.IO.Directory.Exists(sSaveFile))
                        System.IO.Directory.CreateDirectory(sSaveFile);

                    Random rd = new Random();

                    string sFileName = Guid.NewGuid().ToString("N") + rd.Next(10000, 99999).ToString() + sExtensionName;
                    string sSaveName = sSaveFile + sFileName;

                    string sResultPath = folderpath + sFileName;
                    fileData.SaveAs(sSaveName);

                    res.error = 0;
                    res.url = sResultPath;
                    res.tit = "暂无描述";
                }
            }
            catch (Exception message)
            {
                res.error = 1;
                res.message = message.Message;
            }
            return Json(res, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        #endregion

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetTaskNum()
        {
            int num = bll_order.GetTaskNum();
            return Json(num, JsonRequestBehavior.AllowGet);
        }

        #region 导出excel

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExcel(DateTime start, DateTime end)
        {
            try
            {
                t_user user = (t_user)Session["Users"];
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                //DateTime start = new DateTime(2020, 05, 01);
                //DateTime end = new DateTime(2020, 06, 01);
                //string dirPath = Server.MapPath("~/DownFile/");//路径
                string dirPath = @"E:/DownFile/";//路径
                if (!Directory.Exists(dirPath))//如果不存在就创建 dir 文件夹  
                    Directory.CreateDirectory(dirPath);

                //var time = (DateTime.Now.Ticks / 10000).ToString();
                var userId = user.UserId;
                string filePath = dirPath + userId + ".xls";
                //删除之前临时文件
                System.IO.File.Delete(filePath);

                //创建excel下载文件
                var datas = bll_order.CreateOrderExcel(start, end, filePath);

                return File(filePath, "text/plain", "renwu.xls"); //renwu.xls是客户端保存的名字

            }
            catch (Exception e)
            {
                var content = string.Format("Message:{0} \r\n StackTrace:{1} \r\n {2}", e.Message, e.StackTrace, DateTime.Now.ToString("yyyy-MM-dd"));
                LogPrintHelper.Info(content);
                throw;
            }

        }

        #endregion

    }

    public class ImagRes
    {
        public int error { get; set; }

        public string message { get; set; }

        public string url { get; set; }

        public string tit { get; set; }

    }





}

