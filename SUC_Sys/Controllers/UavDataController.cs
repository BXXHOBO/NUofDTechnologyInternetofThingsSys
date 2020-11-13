using Common;
using SUC_DataEntity;
using SUC_EntityBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SUC_Sys.Controllers
{
    //无人机设备信息
    public class UavDataController : BaseHandle
    {
        //
        // GET: /SYManage/UavData/

       
        private BLL_UavData bll = new BLL_UavData();
        private BLL_UavSortieData sortiebll = new BLL_UavSortieData();
        //WechatPushHandler push = new WechatPushHandler();
        public ActionResult Index()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public  ActionResult Form()
        {
            t_user entity = (t_user)Session["Users"];
            if (entity == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                SUC_UavData data = new SUC_UavData();
                data.UavSerialNO = "";
                return View(data);
            }
        }
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            List<SUC_UavData> q = bll.GetList(pagination, keyword);

            var data = new
            {
                rows = q,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }



        [HttpGet]
        
        public ActionResult GetFormJson(string keyValue)
        {
            Guid newId;
            var data = new SUC_UavData();

            if (Guid.TryParse(keyValue, out newId))
            {
                data = bll.GetInfoById(newId);
            }
            return Content(data.ToJson());
        }

        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SUC_UavData uavinfoEntity, string keyValue)
        {
            ResultRes res = new ResultRes();

            Submit(uavinfoEntity, keyValue);
            res.ResultValue = "操作成功！";
            // return Json(res, JsonRequestBehavior.AllowGet);
            return Success("操作成功。");
            //string message = "操作成功";
            //return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
      
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult DeleteForm(string keyValue)
        {
            ResultRes res = new ResultRes();
            try
            {
                Delete(keyValue);
                res.IsSuccess = true;
                res.ResultValue = "删除成功！";
            }
            catch(Exception e)
            {
                res.Msg = e.ToString();
            }

            //return Json(res, JsonRequestBehavior.AllowGet);
            return Success("删除成功。");
          
        }
        //删除数据
        public string Delete(string id)
        {
           

            string ids = Request.QueryString["ids"] ?? "";
            string Nos = "";
            string _User = string.Empty;
            if (ids != "")
                Nos = ids;
            else
                Nos = id;
            if (Session["Users"] != null)
                _User = (Session["Users"] as t_user).UserCode;
            else
                RedirectToAction("Index", "Home");
            if (bll.Del(Nos, _User))
            {
                return "True";
            }
            return "False";
        }
        public ActionResult Edit(string id)
        {
            string editFlag = Request.QueryString["isEdit"] ?? "";
            ViewData["editFlag"] = editFlag;
            SUC_UavData model = new SUC_UavData();
            ViewData["NewNo"] = "";
            if (id != null)
            {
                Guid newId = new Guid(id);
                model = bll.GetInfoById(newId);

            }
            return View(model);
        }
        /// <summary>
        /// 提交编辑信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult Submit(SUC_UavData uavdataInfo, string keyValue)
        {
            Common.ResultRes res = new Common.ResultRes();
            SUC_UavData model;
            bool isAdd = !string.IsNullOrEmpty(keyValue) ? false : true;
            string no = uavdataInfo.UavSerialNO ?? "";
            string workstate = "";
            if (isAdd)
            {
                // 添加
                model = new SUC_UavData();
                model.UavDataId = Guid.NewGuid();
                model.Rec_CreateTime = DateTime.Now;
                model.Disabled = 0;
                isAdd = true;
                if (Session["Users"] != null)
                    model.Rec_CreateBy = (Session["Users"] as t_user).UserCode;
                else
                    RedirectToAction("Index", "Home");
            }
            else
            {  // 编辑
                Guid newId = new Guid(keyValue);
                model = bll.GetInfoById(newId);
                //model = bll.GetIndexphotoByNo(no);
                model.Rec_ModifyTime = DateTime.Now;
                if (Session["Users"] != null)
                    model.Rec_ModifyBy = (Session["Users"] as t_user).UserCode;
                else
                    RedirectToAction("Index", "Home");


            }
            model.UavSerialNO = uavdataInfo.UavSerialNO;
            model.WorkState = uavdataInfo.WorkState;
            model.ImgPath = uavdataInfo.ImgPath;
            model.ProductDate = uavdataInfo.ProductDate;    
            model.Remark = uavdataInfo.Remark ?? "";
            ViewData["isAdd"] = isAdd;

            
           
            try
            {
                if (isAdd)
                {
                    var uavinfoModle = bll.GetInfoById(model.UavDataId);
                    if (uavinfoModle != null)
                    {
                        res.Msg = "保存失败,该视频已经存在！";
                        res.IsSuccess = false;
                    }
                    else
                    {
                        if (bll.Add(model))
                        {
                            res.Msg = "新增成功！";
                            res.IsSuccess = true;
                        }
                        else
                        {
                            res.Msg = "新增失败！";
                            res.IsSuccess = false;
                        }
                    }
                }
                else
                {
                    if (bll.Update(model))
                    {
                        res.Msg = "修改成功！";
                        res.IsSuccess = true;

                        if (model.WorkState == "0")
                        {
                            workstate = "正在巡检";
                        }
                        if (model.WorkState == "1")
                        {
                            workstate = "空闲";
                        }
                        if (model.WorkState == "2")
                        {
                            workstate = "故障";
                        }
                        string desp = "设备：" + model.UavSerialNO + "\r\n\r\n" +
                       "工作状态：" + workstate + "\r\n\r\n" +
                       "修改时间：" + model.Rec_ModifyTime + "\r\n\r\n" +
                       "修改人：" + model.Rec_ModifyBy + "\r\n\r\n" +
                       "备注：" + model.Remark + "\r\n\r\n";
                        try
                        {
                           // push.ServerChanPush(2, "设备信息更新通知", desp);
                            
                        }
                        catch
                        {

                        }
                      
                    }
                    else
                    {
                        res.Msg = "修改失败！";
                        res.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Msg = ex.Message;
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        //获取工作的无人机
        public JsonResult GetFlightUav()
        {
            Common.ResultRes res = new Common.ResultRes();
            BLL_UavData bll = new BLL_UavData();

            List<SUC_UavData> dt = bll.GetUavListByWorkState();
           


            if (bll != null)
            {
                res.IsSuccess = true;
                res.ResultValue = dt;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                res = new Common.ResultRes();
                res.IsSuccess = true;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        //获取工作的无人机
        public JsonResult GetFlightUavbyUavSerialNO(string uavSerialNo)
        {
            Common.ResultRes res = new Common.ResultRes();
            BLL_UavData bll = new BLL_UavData();

            List<SUC_UavSortieData> dt = sortiebll.GetFlightUavbyUavSerialNO(uavSerialNo);



            if (bll != null)
            {
                res.IsSuccess = true;
                res.ResultValue = dt;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                res = new Common.ResultRes();
                res.IsSuccess = true;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetNumOfUavWorkInfo()//获取表的巡检飞机的数量
        {
            Common.ResultRes res = new Common.ResultRes();
            List<SUC_UavData> list = new List<SUC_UavData>();
            var wbList = bll.GetUavListByUavWork().Distinct().ToList();
            int nums = wbList.Count;
            res.ResultValue = nums;
            res.IsSuccess = true;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetAddrOfUavWorkInfo()//获取表的巡检飞机的地址
        {
            Common.ResultRes res = new Common.ResultRes();
           
            List<SUC_UavData> wbList = bll.GetUavListByUavWork();

            if (wbList != null)
            {
                res.IsSuccess = true;
                res.ResultValue = wbList;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                res = new Common.ResultRes();
                res.IsSuccess = true;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult Upload()
        {
            Common.ResultRes res = new Common.ResultRes();
            try
            {
                HttpPostedFileBase file = Request.Files["postFile"];
                if (file.FileName == "")
                {
                    res.IsSuccess = false;
                    res.Msg = "请选择导入文件";
                }
                else if (file.ContentLength > 0)
                {
                    #region
                    string extensionName = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();
                    if (!extensionName.Equals(".jpg") && !extensionName.Equals(".gif") && !extensionName.Equals(".png") && !extensionName.Equals(".bmp"))
                    {
                        res.IsSuccess = false;
                        res.Msg = "导入图片格式不正确";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                    string file_name = Guid.NewGuid().ToString() + extensionName;
                    string fileNamePath = "~/upload/Icon/" + System.IO.Path.GetFileName(file_name);
                    file.SaveAs(Server.MapPath(fileNamePath));
                    res.IsSuccess = true;
                    res.ResultValue = fileNamePath;
                    #endregion
                }
            }
            catch (Exception e)
            {
                if (e.InnerException == null)
                {
                    res.Msg = e.Message;
                }
                else
                {
                    res.Msg = e.InnerException.Message;
                }
                res.IsSuccess = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
