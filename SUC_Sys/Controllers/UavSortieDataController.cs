
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
    public class UavSortieDataController : BaseHandle
    {
        //
        // GET: /SYManage/UavSortieData/

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
        private BLL_UavSortieData bll = new BLL_UavSortieData();
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
                SUC_UavSortieData data = new SUC_UavSortieData();
                data.UavSerialNO = "";
                return View(data);
            }
        }
       

        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            List<SUC_UavSortieData> q = bll.GetList(pagination, keyword);

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
            var data = new SUC_UavSortieData();

            if (Guid.TryParse(keyValue, out newId))
            {
                data = bll.GetInfoById(newId);
            }
            return Content(data.ToJson());
        }

        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SUC_UavSortieData uavsortiedataEntity, string keyValue)
        {
            //ResultRes res = new ResultRes();
            Submit(uavsortiedataEntity, keyValue);
            //res.ResultValue = "操作成功！";
            //return Json(res, JsonRequestBehavior.AllowGet);
            return Success("操作成功。");
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            //ResultRes res = new ResultRes();
            Delete(keyValue);
            //res.ResultValue = "删除成功！";
            //return Json(res, JsonRequestBehavior.AllowGet);
            return Success("删除成功!");
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
            SUC_UavSortieData model = new SUC_UavSortieData();
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
        public JsonResult Submit(SUC_UavSortieData uavsortiedataInfo, string keyValue)
        {
            Common.ResultRes res = new Common.ResultRes();
            SUC_UavSortieData model;
            bool isAdd = !string.IsNullOrEmpty(keyValue) ? false : true;
            string no = uavsortiedataInfo.UavSerialNO ?? "";
            if (isAdd)
            {
                // 添加
                model = new SUC_UavSortieData();
                model.UavSortieId = Guid.NewGuid();
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
            model.UavSerialNO = uavsortiedataInfo.UavSerialNO;
            model.Sortie = uavsortiedataInfo.Sortie;
            model.VideoAddr = uavsortiedataInfo.VideoAddr;
            model.UavState = uavsortiedataInfo.UavState;
            model.HistoryAddr = uavsortiedataInfo.HistoryAddr;
            model.InitialOperTime = uavsortiedataInfo.InitialOperTime;
            model.OperationEndTime = uavsortiedataInfo.OperationEndTime;
            model.OperateAddr = uavsortiedataInfo.OperateAddr;
            model.WorkContents = uavsortiedataInfo.WorkContents;
            model.Remark = uavsortiedataInfo.Remark ?? "";
            ViewData["isAdd"] = isAdd;
            try
            {
                if (isAdd)
                {
                    var uavinfoModle = bll.GetInfoById(model.UavSortieId);
                    if (uavinfoModle != null)
                    {
                        res.Msg = "保存失败,该数据已经存在！";
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

        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public JsonResult GetLineNumberInfo()//获取表的总行数值
        {  
            Common.ResultRes res = new Common.ResultRes();
            List<SUC_UavSortieData> pipelist = new List<SUC_UavSortieData>();  
                var wbList = bll.GetUavListById().Distinct().ToList();
                int nums = wbList.Count;
                res.ResultValue =nums;
                res.IsSuccess = true;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //根据序列号和架次信息获取数据
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult GetAddrInfo(string UavSerialNO,string Sortie)
        {
            Common.ResultRes res = new Common.ResultRes();
            SUC_UavSortieData model = bll.GetUavSortieByNUMandS(UavSerialNO, Sortie); ;
            try
            {

                res.ResultValue = model;

                res.IsSuccess = true;
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
