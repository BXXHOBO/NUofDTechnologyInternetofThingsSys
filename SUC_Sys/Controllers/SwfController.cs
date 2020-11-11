using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using SUC_DataEntity;
using SUC_EntityBLL;

namespace SUC_Sys.Controllers
{
    public class SwfController : Controller
    {
        BLL_Sef bLL_Sef = new BLL_Sef();

        // GET: Swf
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
        /// <summary>
        /// 查询树形控件一级数据
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectTree()
        {

            // string q ="[{ 'id': '001', 'title': '湖南省', 'last': false, 'parentId': '0', 'children': [{ 'id': '001001', 'title': '长沙市', 'last': true, 'parentId': '001' },{ 'id': '001002', 'title': '株洲市', 'last': true, 'parentId': '001' },{ 'id': '001003', 'title': '湘潭市', 'last': true, 'parentId': '001' },{ 'id': '001004', 'title': '衡阳市', 'last': true, 'parentId': '001' },{ 'id': '001005', 'title': '郴州市', 'last': true, 'parentId': '001' }] }, {'id': '002', 'title': '湖北省', 'last': false, 'parentId': '0','children': [{ 'id': '002001', 'title': '武汉市', 'last': true, 'parentId': '002' },{ 'id': '002002', 'title': '黄冈市', 'last': true, 'parentId': '002' },{ 'id': '002003', 'title': '潜江市', 'last': true, 'parentId': '002' },{ 'id': '002004', 'title': '荆州市', 'last': true, 'parentId': '002' },{ 'id': '002005', 'title': '襄阳市', 'last': true, 'parentId': '002' }]}, {'id': '003', 'title': '广东省', 'last': false, 'parentId': '0','children': [ { 'id': '003001', 'title': '广州市', 'last': false, 'parentId': '003','children': [ { 'id': '003001001', 'title': '天河区', 'last': true, 'parentId': '003001' }, { 'id': '003001002', 'title': '花都区', 'last': true, 'parentId': '003001' }] },{ 'id': '003002', 'title': '深圳市', 'last': true, 'parentId': '003' }, { 'id': '003003', 'title': '中山市', 'last': true, 'parentId': '003' },{ 'id': '003004', 'title': '东莞市', 'last': true, 'parentId': '003' }, { 'id': '003005', 'title': '珠海市', 'last': true, 'parentId': '003' },  { 'id': '003006', 'title': '韶关市', 'last': true, 'parentId': '003' }]},  {   'id': '004', 'title': '浙江省', 'last': false, 'parentId': '0','children': [ { 'id': '004001', 'title': '杭州市', 'last': true, 'parentId': '004' }, { 'id': '004002', 'title': '温州市', 'last': true, 'parentId': '004' },{ 'id': '004003', 'title': '绍兴市', 'last': true, 'parentId': '004' },{ 'id': '004004', 'title': '金华市', 'last': true, 'parentId': '004' }, { 'id': '004005', 'title': '义乌市', 'last': true, 'parentId': '004' }]},{ 'id': '005', 'title': '福建省', 'last': false, 'parentId': '0','children': [ { 'id': '005001', 'title': '厦门市', 'last': true, 'parentId': '005' }]}]";
            t_user entity = (t_user)Session["Users"];
            BLL_t_role_menuZzc role_MenuZzc = new BLL_t_role_menuZzc();
            var zzc= role_MenuZzc.SelectId(entity.UserRoles);
            List<DtreeDataModel> dtree = new List<DtreeDataModel>();
            List<SUC_Courseware> tRM_s = bLL_Sef.SelectSef();

            for (int i = 0; i < tRM_s.Count; i++)
            {
                DtreeDataModel d1 = new DtreeDataModel();
                d1.id = tRM_s[i].CoursewareId.ToString();
                d1.title = tRM_s[i].CoursewareName;
                d1.IsWebPub = tRM_s[i].IsWebPub;
                if (bLL_Sef.SelectSefCount(tRM_s[i].CoursewareId) > 0)
                {
                    d1.last = false;
                    ForAssign(tRM_s[i].CoursewareId, d1);
                }
                else
                {
                    d1.last = true;
                }
                d1.parentId = tRM_s[i].P_CoursewareCode;
                dtree.Add(d1);
            }



            return Json(dtree);
            //return Json(q, JsonRequestBehavior.AllowGet); 
        }
        /// <summary>
        /// 查询子类，递归赋值
        /// </summary>
        /// <returns></returns>
        public List<DtreeDataModel> ForAssign(Guid guid, DtreeDataModel model)
        {
            List<DtreeDataModel> dtree = new List<DtreeDataModel>();
            t_user entity = (t_user)Session["Users"];
            BLL_t_role_menuZzc role_MenuZzc = new BLL_t_role_menuZzc();
            var zzc = role_MenuZzc.SelectId(entity.UserRoles);
            List<SUC_Courseware> tRM_s = bLL_Sef.SelectSefId(guid);
            for (int i = 0; i < tRM_s.Count; i++)
            {
                DtreeDataModel d1 = new DtreeDataModel();
                d1.id = tRM_s[i].CoursewareId.ToString(); ;
                d1.title = tRM_s[i].CoursewareName;
                d1.IsWebPub = tRM_s[i].IsWebPub;
                if (bLL_Sef.SelectSefCount(tRM_s[i].CoursewareId) > 0)
                {
                    d1.last = false;
                    ForAssign(tRM_s[i].CoursewareId, d1);
                }
                else
                {
                    d1.last = true;
                }
                d1.parentId = tRM_s[i].P_CoursewareCode;
                dtree.Add(d1);
            }
            model.children = dtree;
            return dtree;
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <returns></returns>
        public int UpdateData()
        {
            t_user entity = (t_user)Session["Users"];
            string U_CoursewareId= Request["U_CoursewareId"];
            string U_CoursewareCode = Request["U_CoursewareCode"];
            string CoursewareName = Request["U_CoursewareName"];
            string U_IsWebPub = Request["U_IsWebPub"];
            string U_Icon = Request["U_Icon"];
            string U_SortID = Request["U_SortID"];

            Guid guid = new Guid(U_CoursewareId);
            int i = bLL_Sef.UpdateCourseware(guid, U_CoursewareCode, CoursewareName, U_IsWebPub, U_Icon,Convert.ToInt32(U_SortID), entity.UserName);
            return i;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <returns></returns>
        public ActionResult Assign()
        {
            string U_CoursewareId= Request["U_CoursewareId"];
            Guid guid = new Guid(U_CoursewareId);
             var u= bLL_Sef.Assign(guid);
            return Json(u, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 假删除
        /// </summary>
        /// <returns></returns>
        public bool DeleteData(string CoursewareId)
        {
            //string CoursewareId= Request["CoursewareId"];
            Guid guid = new Guid(CoursewareId);
            return bLL_Sef.DeleteCourseware(guid);
            
        }

        /// <summary>
        /// 上传文件,添加数据
        /// </summary>
        /// <param name="filename"></param>
        [HttpPost]
        public void ProcessUploadFiles(IEnumerable<HttpPostedFileBase> filename,string F_ID, SUC_Courseware courseware)
        {
            t_user entity = (t_user)Session["Users"];
            int i = 0;
            foreach (var file in filename)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Courseware"), fileName);
                        //var path = "D:\\测试\\test\\上传文件地址";
                        file.SaveAs(path);
                        string path1 = "~/Courseware";
                        if (F_ID=="")
                        {
                            F_ID = "0";
                        }
                        string[] r = file.FileName.Split('.');
                        string vy="J";
                        DateTime dt = DateTime.Now;
                        vy+= dt.Hour.ToString()+dt.Minute.ToString()+dt.Second.ToString()+dt.Millisecond.ToString();
                       
                        int zzc = bLL_Sef.AddCourseware(Guid.NewGuid(), vy, file.FileName, F_ID, r[r.Length-1], courseware.Icon, Convert.ToInt32(courseware.SortID), path1, courseware.Remark, entity.UserName);
                        if (zzc==2)
                        {
                            i = 2;
                        }
                        else
                        {
                            i = i + 1;
                        }
                       
                    }
                }
            }
            if (i > 0 && i != 2)
            {
                HttpContext.Response.Output.Write("<script>alert('上传成功，" + i + "条');history.go(-1);</script>");
            }
            else if (i == 2)
            {
                HttpContext.Response.Output.Write("<script>alert('已存在此编号');history.go(-1);</script>");
            }
            else
            {
                HttpContext.Response.Output.Write("<script>alert('上传失败');history.go(-1);</script>");
            }

        }
        /// <summary>
        /// 删除文件以及数据
        /// </summary>
        /// <param name="iFileName"></param>
        public bool DeleteTempFiles(string CoursewareId, string CoursewareName)
        {
            // string FileUrl = @"D:\svn\TeachingResourcesSys\01 Resources\TRM_Sys\TRM_Sys\Courseware\+"+ CoursewareName + "";
            var FileUrl = Path.Combine(Server.MapPath("~/Courseware"), CoursewareName);
            FileInfo f = new FileInfo(FileUrl);
            DirectoryInfo dir = f.Directory;
            foreach (FileInfo tfile in dir.GetFiles())
            {
                try {
                    if (tfile.ToString() == CoursewareName)
                    {
                        tfile.Delete();
                    }

                }
                catch {
                
                }
            }
            return DeleteData(CoursewareId);
        }
        /// <summary>
        /// 导出课件
        /// </summary>
        /// <returns></returns>
        public FileStreamResult DownFile(string DeriveCoursewareName)
        {
            string fileName = DeriveCoursewareName;//客户端保存的文件名
            string filePath = Server.MapPath("~/Courseware/" + DeriveCoursewareName + "");//路径
            return File(new FileStream(filePath, FileMode.Open), "text/plain",
            fileName);
        }
        /// <summary>
        /// 赋值添加下拉框
        /// </summary>
        /// <returns></returns>
        public JsonResult AddSelect()
        {
            BLL_ClaTeaCurAssociative bLL_Cla = new BLL_ClaTeaCurAssociative();
            return Json(bLL_Cla.SelectAll());
        }
    }
}
