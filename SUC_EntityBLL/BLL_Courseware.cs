using Common;
using NPOI.DDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_Courseware
    {
        /// <summary>
        /// 显示课件列表
        /// </summary>
        /// <param name="start">开始条数</param>
        /// <param name="end">结束条数</param>
        /// <param name="search">查询关键字</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <returns></returns>
        public PageRes GetCourseware(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    //var menus = from m in _DataEntities.SUC_Courseware
                    //            join u in _DataEntities.SUC_ClaTeaCurAssociative
                    //            on m.UserCode equals u.UserCode
                    //            where u.Disabled != 1 && m.Disabled != 1
                    //            select new
                    //            {
                    //                m.CoursewareId,
                    //                UserCode = u.ClassCode,
                    //                m.CoursewareCode,
                    //                m.P_CoursewareCode,
                    //                m.IsWebPub,
                    //                m.Icon,
                    //                m.SortID,
                    //                m.CoursewarePath,
                    //                m.State,
                    //                m.Remark,
                    //                m.Rec_CreateTime,
                    //                m.Rec_ModifyTime
                    //            };



                    var menus = _DataEntities.SUC_Courseware.Where(o=>o.CoursewareCode==o.CoursewareCode&&o.Disabled==0);

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        menus = menus.Where(m => m.Rec_CreateTime >= startTime && m.Rec_CreateTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        menus = menus.Where(m => m.CoursewareCode.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            menus = menus.OrderByDescending(m => m.Rec_CreateTime);
                        }
                        else
                        {
                            menus = menus.OrderBy(m => m.Rec_CreateTime);
                        }
                    }

                    res.total = menus.Count();
                    //分页后
                    var menus2 = menus.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int i = 1;
                    List<object> resMenus = new List<object>();
                    foreach (var menu in menus2)
                    {
                        string createTime = menu.Rec_CreateTime != null ? menu.Rec_CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        string modifyTime = menu.Rec_ModifyTime != null ? menu.Rec_ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        resMenus.Add(new
                        {
                            RowNo = i++,
                            menu.CoursewareId,
                            menu.CoursewareCode,
                            menu.P_CoursewareCode,
                            menu.IsWebPub,
                            menu.Icon,
                            menu.SortID,
                            menu.CoursewarePath,
                            menu.State,
                            menu.Remark,
                            menu.Rec_CreateBy,
                            menu.Rec_ModifyBy,
                            // CreateTime = createTime,
                            //  ModifyTime = modifyTime
                        });
                    }

                    res.rows = resMenus;
                    return res;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 假删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool Delete(Guid menuId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.SUC_Courseware.FirstOrDefault(m => m.CoursewareId == menuId);
                if (entity == null)
                {
                    return false;
                }
                entity.Disabled = 1;
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 提交菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int SubmitMenu(SUC_Courseware menu, t_user user)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //修改
               
                    //获取之前的数据
                    var entity = _DataEntities.SUC_Courseware.FirstOrDefault(m => m.CoursewareCode == menu.CoursewareCode);
                    entity.P_CoursewareCode = menu.P_CoursewareCode;
                    entity.Icon = menu.Icon;
                    entity.Remark = menu.Remark;
                    entity.SortID = menu.SortID;
                    entity.Rec_ModifyTime = DateTime.Now;
                    entity.Rec_ModifyBy = user.UserId;
                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
               
            }
        }

        /// <summary>
        /// 根据菜单编号获取菜单
        /// </summary>
        /// <param name="menuCode">菜单编号</param>
        /// <returns></returns>
        public object GetCoursewareByCode(Guid UserCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.SUC_Courseware.FirstOrDefault(m => m.CoursewareId == UserCode);
                if (entity != null)
                {
                    return new
                    {
                        entity.CoursewareCode,
                        entity.IsWebPub,
                        entity.Icon,
                        entity.Remark,
                        CreateTime = entity.Rec_CreateTime != null ? entity.Rec_CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "",
                        entity.P_CoursewareCode,
                        ModifyTime = entity.Rec_ModifyTime != null ? entity.Rec_ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : ""
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public int AddCourseware(Guid CoursewareId, string CoursewareCode, string UserCode, string P_CoursewareCode, string IsWebPub, string Icon, int SortID, string CoursewarePath)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var r= _DataEntities.SUC_Courseware.Where(o => o.CoursewareId == CoursewareId);
               
                if (r.Count()>0)
                {
                    return -1;
                }
                SUC_Courseware tRM_Courseware = new SUC_Courseware();
                tRM_Courseware.CoursewareId = CoursewareId;
                tRM_Courseware.CoursewareCode = CoursewareCode;
                tRM_Courseware.P_CoursewareCode = P_CoursewareCode;
                tRM_Courseware.IsWebPub = IsWebPub;
                tRM_Courseware.Icon = Icon;
                tRM_Courseware.SortID = SortID;
                tRM_Courseware.CoursewarePath = CoursewarePath;
                tRM_Courseware.State = 0;
                tRM_Courseware.Disabled = 0;
                tRM_Courseware.Remark = "";
                tRM_Courseware.Rec_CreateTime = DateTime.Now;
                tRM_Courseware.Rec_CreateBy ="";
                tRM_Courseware.Rec_ModifyTime = DateTime.Now;
                tRM_Courseware.Rec_ModifyBy = "";


                _DataEntities.SUC_Courseware.Add(tRM_Courseware);

                if (_DataEntities.SaveChanges()>0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 全部改为0
        /// </summary>
        /// <returns></returns>
        public int UpdateAll()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var w = _DataEntities.SUC_Courseware.Where(c => c.Checked == c.Checked).ToList();

                for (int i = 0; i < w.Count; i++)
                {
                    if (w[i].Checked != 0)
                    {
                        w[i].Checked = 0;
                       
                    }

                }
                return _DataEntities.SaveChanges();
            }
        }

        /// <summary>
        /// 修改选中状态
        /// </summary>
        /// <param name="Coed">编号</param>
        /// <returns></returns>
        public bool UpdateChecked(string Code)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
              
                if (Code == "")
                {
                    var u = _DataEntities.SUC_Courseware.Where(c => c.Checked != 0).FirstOrDefault();
                    u.Checked = 0;
                }
                else
                {

                    var q = _DataEntities.SUC_Courseware.Where(c => c.CoursewareCode == Code).FirstOrDefault();
                    if (q.Checked==1)
                    {
                        return true;
                    }
                    else
                    {
                        q.Checked = 1;
                    }
                   
                   
                }

                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 根据Id修改数据
        /// </summary>
        /// <param name="CoursewareId"></param>
        /// <returns></returns>
        public bool UpdateId(Guid CoursewareId ,string UserRoles)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var t= _DataEntities.SUC_Courseware.Where(c=>c.CoursewareId==CoursewareId).FirstOrDefault();
                switch (UserRoles)
                {
                    case "R00034":
                        t.State = 1;
                        break;
                    case "R00024":
                        t.State = 2;
                        break;
                }
                if (_DataEntities.SaveChanges()>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// 根据ID修改，不通过审核
        /// </summary>
        /// <param name="CoursewareId"></param>
        /// <param name="UserRoles"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public bool OustUpdateId(Guid CoursewareId, string UserRoles,string Remark)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var t = _DataEntities.SUC_Courseware.Where(c => c.CoursewareId == CoursewareId).FirstOrDefault();
                switch (UserRoles)
                {
                    case "R00034":
                        t.State = 3;
                        break;
                    case "R00024":
                        t.State = 4;
                        break;
                }
                if (Remark!="")
                {
                    t.Remark = Remark;
                }
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public SUC_Courseware SelectId(Guid guid)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.SUC_Courseware.Where(c=>c.CoursewareId==guid).FirstOrDefault();
            }
        }

    }
}


