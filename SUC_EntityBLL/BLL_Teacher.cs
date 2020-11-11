using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_Teacher
    {
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
        public PageRes GetTeachar(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var teachers = from a in _DataEntities.t_user
                                       join b in _DataEntities.SUC_AuditOffice
                                       on a.AuditOfficeCode equals b.AuditOfficeCode
                                       join c in _DataEntities.SUC_Courseware
                                       on a.CreateBy equals c.Rec_CreateBy
                                       where a.Disabled != 1 &&b.Disabled!=1&&c.Disabled!=1
                                       select new
                                       {
                                           a.UserName,
                                           a.UserCode,
                                           b.AuditOfficeName,
                                           c.CoursewareCode,
                                           c.State,
                                           a.CreateTime,
                                           a.ModifyTime,
                                       };

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        teachers = teachers.Where(c => c.ModifyTime >= startTime && c.ModifyTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        teachers = teachers.Where(c => c.AuditOfficeName.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            teachers = teachers.OrderByDescending(c => c.CreateTime);
                        }
                        else
                        {
                            teachers = teachers.OrderBy(c => c.CreateTime);
                        }
                    }

                    res.total = teachers.Count();
                    //分页后
                    var teachers2 = teachers.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int i = 1;
                    List<object> resAuditOffiece = new List<object>();
                    foreach (var teacher in teachers2)
                    {
                        string createTime = teacher.CreateTime != null ? teacher.CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        string modifyTime = teacher.ModifyTime != null ? teacher.ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        resAuditOffiece.Add(new
                        {
                            RowNo = i++,
                            teacher.UserName,
                            teacher.UserCode,
                            teacher.AuditOfficeName,
                            CreateTime = createTime,
                            ModifyTime = modifyTime,

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
    }
}
