using Common;
using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace SUC_EntityBLL
{
    public class BLL_Course
    {
        #region 课程表信息
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
        public PageRes GetCourses(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var courses = from c in _DataEntities.TRM_Course
                                  join u in _DataEntities.t_user
                                  on c.CreateBy equals u.UserId
                                  join c1 in _DataEntities.TRM_CourseType
                                  on c.CourseTypeCode equals c1.CourseTypeCode
                                  where /*u.Disabled != 1 &&*/ c.Disabled != 1
                                  select new
                                  {
                                      c.CourseId,
                                      c.CourseCode,
                                      c.CourseName,
                                      c.SchoolSmester,
                                      c.Remark,
                                      c.AuditOfficeCode,
                                      ModifyBy = u.UserName,
                                      CreateUser = u.UserName,
                                      CourseTypeCode = c1.CourseTypeName,
                                      c.CreateTime,
                                      c.ModifyTime
                                  };

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        courses = courses.Where(c => c.ModifyTime >= startTime && c.ModifyTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        courses = courses.Where(c => c.CourseName.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            courses = courses.OrderByDescending(c => c.CreateTime);
                        }
                        else
                        {
                            courses = courses.OrderBy(c => c.CreateTime);
                        }
                    }

                    res.total = courses.Count();
                    //分页后
                    var courses2 = courses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int i = 1;
                    List<object> resCourses = new List<object>();
                    foreach (var course in courses2)
                    {
                        string createTime = course.CreateTime != null ? course.CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        string modifyTime = course.ModifyTime != null ? course.ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        resCourses.Add(new
                        {
                            RowNo = i++,
                            course.CourseId,
                            course.CourseCode,
                            course.CourseName,
                            course.AuditOfficeCode,
                            course.CourseTypeCode,
                            course.SchoolSmester,
                            course.Remark,
                            course.ModifyBy,
                            course.CreateUser,
                            CreateTime = createTime,
                            ModifyTime = modifyTime,

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
        /// 假删除菜单
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool Delete(Guid courseId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.TRM_Course.FirstOrDefault(c => c.CourseId == courseId);
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
        public TRM_Course SubmitMenu(TRM_Course course, t_user user, string editFlag)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //修改
                if (editFlag == "Edit")
                {
                    //获取之前的数据
                    var entity = _DataEntities.TRM_Course.FirstOrDefault(c => c.CourseId == course.CourseId);
                    entity.CourseName = course.CourseName;
                    entity.CourseCode = course.CourseCode;
                    entity.CourseTypeCode = course.CourseTypeCode;
                    entity.SchoolSmester = course.SchoolSmester;
                    entity.Remark = course.Remark;
                    entity.ModifyBy = user.UserName;
                    entity.ModifyTime = DateTime.Now;
                    entity.AuditOfficeCode = course.AuditOfficeCode;
                }
                else //创建
                {

                    TRM_Course entity = new TRM_Course();
                    entity.CourseId = Guid.NewGuid();
                    entity.CourseCode = course.CourseCode;
                    entity.CourseName = course.CourseName;
                    entity.CourseTypeCode = course.CourseTypeCode;
                    entity.SchoolSmester = course.SchoolSmester;
                    entity.CreateBy = user.UserId;
                    entity.Disabled = 0;
                    entity.AuditOfficeCode = course.AuditOfficeCode;
                    entity.Remark = course.Remark;
                    entity.CreateTime = DateTime.Now;
                    entity.ModifyTime = DateTime.Now;
                    _DataEntities.TRM_Course.Add(entity);
                }
                if (_DataEntities.SaveChanges() > 0)
                {
                    return course;
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
        public object GetCourseByCode(string courseCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.TRM_Course.FirstOrDefault(m => m.CourseCode == courseCode);
                if (entity != null)
                {
                    return new
                    {
                        entity.CourseId,
                        entity.CourseCode,
                        entity.CourseName,
                        entity.CourseTypeCode,
                        entity.SchoolSmester,
                        entity.Remark,
                        entity.CreateBy,
                        CreateTime = entity.CreateTime != null ? entity.CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "",
                        entity.Disabled,
                        ModifyTime = entity.ModifyTime != null ? entity.ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : ""
                    };
                }
                else
                {
                    return null;
                }
            }
        }
        
       
        #endregion

        #region 课程类别表信息

        public PageRes GetCoursesType(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var courses = from c in _DataEntities.TRM_CourseType
                                  where c.Disabled != 1
                                  select new
                                  {
                                      c.CourseTypeId,
                                      c.CourseTypeCode,
                                      c.CourseTypeName,
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
                        courses = courses.Where(c => c.CourseTypeName.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            courses = courses.OrderByDescending(c => c.Rec_CreateTime);
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
                            course.CourseTypeId,
                            course.CourseTypeCode,
                            course.CourseTypeName,
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
        public TRM_CourseType SubmitTypeMenu(TRM_CourseType coursetype, t_user user, string editFlag)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //修改
                if (editFlag == "Edit")
                {
                    //获取之前的数据
                    var entity = _DataEntities.TRM_CourseType.FirstOrDefault(c => c.CourseTypeId == coursetype.CourseTypeId);
                    entity.CourseTypeName = coursetype.CourseTypeName;
                    entity.CourseTypeCode = coursetype.CourseTypeCode;
                    entity.Remark = coursetype.Remark;
                    entity.Rec_CreateBy = coursetype.Rec_CreateBy;
                    entity.Rec_ModifyBy = user.UserName;
                    entity.Rec_ModifyTime = DateTime.Now;

                }
                else //创建
                {

                    TRM_CourseType entity = new TRM_CourseType();
                    entity.CourseTypeId = Guid.NewGuid();
                    entity.CourseTypeCode = coursetype.CourseTypeCode;
                    entity.CourseTypeName = coursetype.CourseTypeName;
                    entity.Disabled = 0;
                    entity.Remark = coursetype.Remark;
                    entity.Rec_CreateBy = coursetype.Rec_CreateBy;
                    entity.Rec_CreateTime = DateTime.Now;
                    entity.Rec_ModifyTime = DateTime.Now;
                    _DataEntities.TRM_CourseType.Add(entity);
                }
                if (_DataEntities.SaveChanges() > 0)
                {
                    return coursetype;
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
        public object GetCourseTypeByCode(string courseTypeCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.TRM_CourseType.FirstOrDefault(m => m.CourseTypeCode == courseTypeCode);
                if (entity != null)
                {
                    return new
                    {
                        entity.CourseTypeId,
                        entity.CourseTypeCode,
                        entity.CourseTypeName,
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
        /// 假删除类别菜单
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool DeleteType(Guid courseTypeId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.TRM_CourseType.FirstOrDefault(c => c.CourseTypeId == courseTypeId);
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
        /// 获取课程类别编号
        /// </summary>
        /// <param name="classname">班级名称</param>
        /// <returns></returns>

        public TRM_CourseType GetCourseTypeCode(String typeName)
        {
            TRM_CourseType model = null;
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    model = _DataEntities.TRM_CourseType.Where(o => o.CourseTypeName == typeName && o.Disabled == 0).ToList().FirstOrDefault();


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return model;

        }

        #endregion

        #region 班级教师课程表信息

        /// <summary>
        /// 班级教师课程分页
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <returns></returns>
        public PageRes GetCoursesClaCouUser(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var courses = from a in _DataEntities.TRM_ClaTeaCurAssociative
                                  join b in _DataEntities.t_user
                                  on a.UserCode equals b.UserCode
                                  join c in _DataEntities.TRM_ClassInfo
                                  on a.ClassCode equals c.ClassCode
                                  join d in _DataEntities.TRM_Course
                                  on a.CourseCode equals d.CourseCode
                                  where a.Disabled != 1
                                  select new
                                  {
                                      a.ClaTeaCurAssociativeId,
                                      a.ClaTeaCurAssociativeCode,
                                      ClassCode = c.ClassName,
                                      CourseCode = d.CourseName,
                                      UserCode=b.UserName,
                                      a.Remark,
                                      Rec_ModifyBy = b.UserName,
                                      Rec_CreateBy = b.UserName,
                                      a.Rec_CreateTime,
                                      a.Rec_ModifyTime
                                  };

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        courses = courses.Where(c => c.Rec_ModifyTime >= startTime && c.Rec_ModifyTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        courses = courses.Where(c => c.ClassCode.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            courses = courses.OrderByDescending(c => c.Rec_CreateTime);
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
                            course.ClaTeaCurAssociativeId,
                            course.ClaTeaCurAssociativeCode,
                            course.ClassCode,
                            course.CourseCode,
                            course.UserCode,
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
        /// 关联信息修改/添加
        /// </summary>
        /// <param name="coursetype"></param>
        /// <param name="user"></param>
        /// <param name="editFlag"></param>
        /// <returns></returns>
        public TRM_ClaTeaCurAssociative SubmitClaCouUser(TRM_ClaTeaCurAssociative clateacurassociative, t_user user, string editFlag)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //修改
                if (editFlag == "Edit")
                {
                    //获取之前的数据
                    var entity = _DataEntities.TRM_ClaTeaCurAssociative.FirstOrDefault(c => c.ClaTeaCurAssociativeId == clateacurassociative.ClaTeaCurAssociativeId);
                    entity.ClaTeaCurAssociativeCode = clateacurassociative.ClaTeaCurAssociativeCode;
                    entity.CourseCode = clateacurassociative.CourseCode;
                    entity.ClassCode = clateacurassociative.ClassCode;
                    entity.UserCode = clateacurassociative.UserCode;
                    entity.Remark = clateacurassociative.Remark;
                    entity.Rec_CreateBy = clateacurassociative.Rec_CreateBy;
                    entity.Rec_ModifyBy = user.UserName;
                    entity.Rec_ModifyTime = DateTime.Now;

                }
                else //创建
                {

                    TRM_ClaTeaCurAssociative entity = new TRM_ClaTeaCurAssociative();
                    entity.ClaTeaCurAssociativeId = Guid.NewGuid();

                    entity.ClaTeaCurAssociativeCode = clateacurassociative.ClaTeaCurAssociativeCode;
                    entity.CourseCode = clateacurassociative.CourseCode;
                    entity.ClassCode = clateacurassociative.ClassCode;
                    entity.UserCode = clateacurassociative.UserCode;
                    entity.Disabled = 0;
                    entity.Remark = clateacurassociative.Remark;
                    entity.Rec_CreateBy = clateacurassociative.Rec_CreateBy;
                    entity.Rec_CreateTime = DateTime.Now;
                    entity.Rec_ModifyTime = DateTime.Now;
                    _DataEntities.TRM_ClaTeaCurAssociative.Add(entity);
                }
                if (_DataEntities.SaveChanges() > 0)
                {
                    return clateacurassociative;
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
        public object GetCourseByClaCouUser(string claTeaCurAssociativeCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.TRM_ClaTeaCurAssociative.FirstOrDefault(m => m.ClaTeaCurAssociativeCode == claTeaCurAssociativeCode);
                if (entity != null)
                {
                    return new
                    {
                        entity.ClaTeaCurAssociativeId,
                        entity.ClaTeaCurAssociativeCode,
                        entity.ClassCode,
                        entity.CourseCode,
                        entity.UserCode,
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
        /// 假删除类别菜单
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool DeleteClaTeaClaCouUser(Guid claTeaCurAssociativeId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.TRM_ClaTeaCurAssociative.FirstOrDefault(c => c.ClaTeaCurAssociativeId == claTeaCurAssociativeId);
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


        #endregion
        /// <summary>
        /// 创建excel
        /// </summary>

        /// <returns></returns>
        public void CreateExcel(string filePath)
        {


            //编辑excel
            IWorkbook wb = new XSSFWorkbook();
            //创建表  
            ISheet sh = wb.CreateSheet("课程信息模板");
            #region 编辑表头
            IRow row0 = sh.CreateRow(0);
            ICell icell1top = row0.CreateCell(0);
            icell1top.SetCellValue("学年学期");
            ICell icell2top = row0.CreateCell(1);
            icell2top.SetCellValue("课程代码");
            ICell icell3top = row0.CreateCell(2);
            icell3top.SetCellValue("课程名称");
            ICell icell4top = row0.CreateCell(3);
            icell4top.SetCellValue("任课教师");
            ICell icell5top = row0.CreateCell(4);
            icell5top.SetCellValue("授课班级");
            ICell icell6top = row0.CreateCell(5);
            icell6top.SetCellValue("审核教研室");
            ICell icell7top = row0.CreateCell(6);
            icell7top.SetCellValue("课程类别");

            #endregion

            using (FileStream stm = File.OpenWrite(filePath))
            {
                wb.Write(stm);
                Console.WriteLine("提示：创建成功！");
                wb.Close();
            }


        }

    }
}
