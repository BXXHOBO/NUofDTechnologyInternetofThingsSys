using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_AuditOffice
    {
        #region 教研室信息
        /// <summary>
        /// 获取课程集合
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="search">搜索字段</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="sidx">排序字段</param>
        /// <param name="sord">升降序</param>
        /// <returns></returns>
        public PageRes GetAuditOffice(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var auditoffices = from c in _DataEntities.SUC_AuditOffice
                                  where  c.Disabled != 1
                                  select new
                                  {
                                      c.AuditOfficeId,
                                      c.AuditOfficeCode,
                                      c.AuditOfficeName,
                                      //c.CourseTeacher,
                                      //c.TeachingClass,
                                      //c.AuditOffice,
                                      //c.CourseType,
                                      c.Rec_CreateBy,
                                      c.Rec_ModifyBy,
                                      c.Remark,
                                      c.Rec_CreateTime,
                                      c.Rec_ModifyTime

                                  };

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        auditoffices = auditoffices.Where(c => c.Rec_ModifyTime >= startTime && c.Rec_ModifyTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        auditoffices = auditoffices.Where(c => c.AuditOfficeName.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            auditoffices = auditoffices.OrderByDescending(c => c.Rec_CreateTime);
                        }
                        else
                        {
                            auditoffices = auditoffices.OrderBy(c => c.Rec_CreateTime);
                        }
                    }

                    res.total = auditoffices.Count();
                    //分页后
                    var auditoffices2 = auditoffices.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int i = 1;
                    List<object> resAuditOffiece = new List<object>();
                    foreach (var auditoffice in auditoffices2)
                    {
                        string createTime = auditoffice.Rec_CreateTime != null ? auditoffice.Rec_CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        string modifyTime = auditoffice.Rec_ModifyTime != null ? auditoffice.Rec_ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        resAuditOffiece.Add(new
                        {
                            RowNo = i++,
                            auditoffice.AuditOfficeId,
                            auditoffice.AuditOfficeCode,
                            auditoffice.AuditOfficeName,
                            //course.CourseTeacher,
                            //course.TeachingClass,
                            //course.AuditOffice,
                            //course.CourseType,
                            auditoffice.Rec_CreateBy,
                            auditoffice.Rec_ModifyBy,
                            auditoffice.Remark,
                            Rec_CreateTime = createTime,
                            Rec_ModifyTime = modifyTime,

                        });
                    }

                    res.rows = resAuditOffiece;
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
        /// <param name="auditofficeId"></param>
        /// <returns></returns>
        public bool Delete(Guid auditofficeId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.SUC_AuditOffice.FirstOrDefault(c => c.AuditOfficeId == auditofficeId);
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
        public SUC_AuditOffice SubmitMenu(SUC_AuditOffice auditoffice, t_user user, string editFlag)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //修改
                if (editFlag == "Edit")
                {
                    //获取之前的数据
                    var entity = _DataEntities.SUC_AuditOffice.FirstOrDefault(c => c.AuditOfficeId == auditoffice.AuditOfficeId);
                    entity.AuditOfficeName = auditoffice.AuditOfficeName;
                    entity.AuditOfficeCode = auditoffice.AuditOfficeCode;
                    entity.Remark = auditoffice.Remark;
                    entity.Rec_ModifyBy = user.UserId;
                    entity.Rec_ModifyTime = DateTime.Now;
                }
                else //创建
                {
                    
                    SUC_AuditOffice entity = new SUC_AuditOffice();
                    entity.AuditOfficeId = Guid.NewGuid();
                    entity.AuditOfficeName = auditoffice.AuditOfficeName;
                    string q = "JYS";
                    var r = DateTime.Now;
                    string y = r.Year.ToString();
                    string m = r.Month.ToString();
                    string d = r.Day.ToString();
                    string x = r.Hour.ToString();
                    string f = r.Minute.ToString();
                    string k = r.Second.ToString();
                    string l = r.Millisecond.ToString();
                    entity.AuditOfficeCode = q + y + m + d + x + f + k+l;
                    entity.Remark = auditoffice.Remark;
                    entity.Rec_CreateTime = DateTime.Now;
                    entity.Rec_CreateBy = user.UserId;
                   
                    _DataEntities.SUC_AuditOffice.Add(entity);
                }
                if (_DataEntities.SaveChanges() > 0)
                {
                    return auditoffice;
                }
                else
                {
                    return null;
                }

            }
        }

        /// <summary>
        /// 根据课程编号获取菜单
        /// </summary>
        /// <param name="menuCode">菜单编号</param>
        /// <returns></returns>
        public object GetAuditOfficeByCode(string auditofficeCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.SUC_AuditOffice.FirstOrDefault(m => m.AuditOfficeCode == auditofficeCode);
                if (entity != null)
                {
                    return new
                    {
                        entity.AuditOfficeId,
                        entity.AuditOfficeCode,
                        entity.AuditOfficeName,
                        entity.Remark,
                        entity.Rec_CreateBy,
                        Rec_CreateTime = entity.Rec_CreateTime != null ? entity.Rec_CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "",
                        entity.Disabled,
                        Rec_ModifyTime = entity.Rec_ModifyTime != null ? entity.Rec_ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : ""
                    };
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取教研室编号
        /// </summary>
        /// <param name="auditofficename">教研室名称</param>
        /// <returns></returns>

        public SUC_AuditOffice GetOfficeCode(String officeName)
        {
            SUC_AuditOffice model = null;
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    model = _DataEntities.SUC_AuditOffice.Where(o => o.AuditOfficeName == officeName && o.Disabled == 0).ToList().FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return model;

        }
        #endregion
    }
}
