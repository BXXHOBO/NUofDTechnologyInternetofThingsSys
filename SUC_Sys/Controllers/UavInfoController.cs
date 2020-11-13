
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
   
    public class UavInfoController : BaseHandle
    {

        //[HttpGet]
        //[HandlerAuthorize]

        public  ActionResult Index()
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
        private BLL_UavInfo bll = new BLL_UavInfo();
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
                SUC_UavInfo data = new SUC_UavInfo();
                data.UavSerialNO = "";
                return View(data);
            }
        }
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {

            List<SUC_UavInfo> q = bll.GetList(pagination, keyword);

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
            var data = new SUC_UavInfo();

            if (Guid.TryParse(keyValue, out newId))
            {
                data = bll.GetInfoById(newId);
            }
            return Content(data.ToJson());
        }

        [HttpPost]
     
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SUC_UavInfo uavinfoEntity, string keyValue)
        {
            ResultRes res = new ResultRes();

            Submit(uavinfoEntity, keyValue);
            //res.ResultValue = "操作成功！";
            //return Json(res, JsonRequestBehavior.AllowGet);
            return Success("操作成功。");
        }
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            ResultRes res = new ResultRes();
            Delete(keyValue);
            //res.ResultValue = "删除成功！";
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
                Redirect("login.html");
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
            SUC_UavInfo model = new SUC_UavInfo();
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
        public JsonResult Submit(SUC_UavInfo uavinfoInfo, string keyValue)
        {
            Common.ResultRes res = new Common.ResultRes();
            SUC_UavInfo model;
            bool isAdd = !string.IsNullOrEmpty(keyValue) ? false : true;
            string no = uavinfoInfo.UavSerialNO ?? "";
            if (isAdd)
            {
                // 添加
                model = new SUC_UavInfo();
                model.UavInfoId = Guid.NewGuid();
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
                model.Rec_ModifyTime = DateTime.Now;
                if (Session["Users"] != null)
                    model.Rec_ModifyBy = (Session["Users"] as t_user).UserCode;
                else
                    RedirectToAction("Index", "Home");
            }
            model.UavSerialNO = uavinfoInfo.UavSerialNO;
            model.OperationDate = uavinfoInfo.OperationDate;
            model.Sortie = uavinfoInfo.Sortie;
            model.VideoAddr = uavinfoInfo.VideoAddr;
            model.Longitude = uavinfoInfo.Longitude;
            model.Latitude = uavinfoInfo.Latitude;
            model.Altitude = uavinfoInfo.Altitude;
            model.Temperature = uavinfoInfo.Temperature;
            model.Humidity = uavinfoInfo.Humidity;
            model.Speed = uavinfoInfo.Speed;
            model.AtmosPressure = uavinfoInfo.AtmosPressure;
            model.Remark = uavinfoInfo.Remark ?? "";
            ViewData["isAdd"] = isAdd;
            try
            {
                if (isAdd)
                {
                    var uavinfoModle = bll.GetInfoById(model.UavInfoId);
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
       
    }
}
