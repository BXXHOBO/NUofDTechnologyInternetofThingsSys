using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_Class
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <returns></returns>
        public PageRes GetClassInfo(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var courses = from c in _DataEntities.SUC_ClassInfo
                                  where c.Disabled != 1
                                  select new
                                  {
                                      c.ClassInfoId,
                                      c.ClassCode,
                                      c.ClassName,
                                      c.Disabled,
                                      c.Remark,
                                      c.Rec_CreateTime,
                                      c.Rec_CreateBy,
                                      c.Rec_ModifyTime,
                                      c.Rec_ModifyBy,
                                  };

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        courses = courses.Where(c => c.Rec_ModifyTime >= startTime && c.Rec_ModifyTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        courses = courses.Where(c => c.ClassName.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            courses = courses.OrderByDescending(c => c.ClassName);
                        }
                        else
                        {
                            courses = courses.OrderBy(c => c.Rec_CreateTime);
                        }
                    }

                    res.total = courses.Count();
                    //分页后
                    var courses2 = courses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int i = 1;
                    List<object> resCourses = new List<object>();
                    foreach (var course in courses2)
                    {
                        string rec_createTime = course.Rec_CreateTime != null ? course.Rec_CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        string rec_modifyTime = course.Rec_ModifyTime != null ? course.Rec_ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        resCourses.Add(new
                        {
                            RowNo = i++,
                            course.ClassInfoId,
                            course.ClassCode,
                            course.ClassName,
                            course.Disabled,
                            course.Remark,
                            Rec_CreateTime = rec_createTime,
                            course.Rec_CreateBy,
                            Rec_ModifyTime = rec_modifyTime,
                            course.Rec_ModifyBy,

                        });
                    }

                    res.rows = resCourses;
                    return res;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 提交菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public SUC_ClassInfo SubmitClassMenu(SUC_ClassInfo classInfo, t_user user, string editFlag)
        {

            using (var _DataEntities = new SUC_SYSContainer())
            {
                //修改
                if (editFlag == "Edit")
                {
                    //获取之前的数据
                    var entity = _DataEntities.SUC_ClassInfo.Where(c => c.ClassInfoId == classInfo.ClassInfoId).FirstOrDefault();
                    entity.ClassCode = classInfo.ClassCode;
                    entity.ClassName = classInfo.ClassName;
                    entity.Remark = classInfo.Remark;
                    entity.Rec_CreateBy = classInfo.Rec_CreateBy;
                    entity.Rec_ModifyBy = user.UserName;
                    entity.Rec_ModifyTime = DateTime.Now;
                }

                else //创建
                {

                    SUC_ClassInfo entity = new SUC_ClassInfo();
                    entity.ClassInfoId = Guid.NewGuid();
                    entity.ClassCode = classInfo.ClassCode;
                    entity.ClassName = classInfo.ClassName;
                    entity.Disabled = 0;
                    entity.Remark = classInfo.Remark;
                    entity.Rec_CreateBy = classInfo.Rec_CreateBy;
                    entity.Rec_CreateTime = DateTime.Now;
                    entity.Rec_ModifyTime = DateTime.Now;
                    _DataEntities.SUC_ClassInfo.Add(entity);
                }
                if (_DataEntities.SaveChanges() > 0)
                {
                    return classInfo;
                }
                else
                {
                    return null;
                }

            }

        }

        /// <summary>
        /// 根据班级编号获取菜单
        /// </summary>
        /// <param name="menuCode">菜单编号</param>
        /// <returns></returns>
        public object GetClassByCode(string classCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.SUC_ClassInfo.FirstOrDefault(m => m.ClassCode == classCode);
                if (entity != null)
                {
                    return new
                    {
                        entity.ClassInfoId,
                        entity.ClassCode,
                        entity.ClassName,
                        entity.Remark,
                        entity.Rec_CreateBy,
                        entity.Rec_ModifyBy,
                        CreateTime = entity.Rec_CreateTime != null ? entity.Rec_CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "",
                        entity.Disabled,
                        ModifyTime = entity.Rec_ModifyTime != null ? entity.Rec_ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : ""
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 假删除班级菜单
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool DeleteClass(Guid classInfoId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.SUC_ClassInfo.FirstOrDefault(c => c.ClassInfoId == classInfoId);
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
        /// 获取班级编号
        /// </summary>
        /// <param name="classname">班级名称</param>
        /// <returns></returns>

        public SUC_ClassInfo GetClassInfoCode(String className)
        {
            SUC_ClassInfo model = null;
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    model = _DataEntities.SUC_ClassInfo.Where(o => o.ClassName == className && o.Disabled == 0).ToList().FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return model;

        }
    }
}
