using Common;
using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUC_EntityBLL
{
    public class BLL_User
    {
        /// <summary>
        /// 获取用户集合
        /// </summary>
        /// <param name="search">搜索字段</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="sidx">排序字段</param>
        /// <param name="sord">升降序</param>
        /// <returns></returns>
        public PageRes GetUsers(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var users = from r in _DataEntities.t_roles
                                  join u in _DataEntities.t_user
                                  on r.CreateBy equals u.UserId
                                  where u.Disabled != 1 && r.Disabled != 1
                                  select new
                                  {
                                      u.UserId,
                                      u.UserCode,
                                      u.UserName,
                                      r.RoleName,
                                      u.HireDate,
                                      u.Telephone,
                                      u.Email,
                                      u.Addr,
                                      //CreateUser = u.UserName,
                                      //u.CreateTime,
                                      //u.ModifyTime

                                  };

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        //users = users.Where(c => c.ModifyTime >= startTime && c.ModifyTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        users = users.Where(c => c.UserName.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        //if ("desc".Equals(sord))
                        //{
                        //    users = users.OrderByDescending(c => c.CreateTime);
                        //}
                        //else
                        //{
                        //    users = users.OrderBy(c => c.CreateTime);
                        //}
                    }

                    res.total = users.Count();
                    //分页后
                    var users2 = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int i = 1;
                    List<object> resusers = new List<object>();
                    foreach (var user in users2)
                    {
                        //string createTime = user.CreateTime != null ? user.CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        //string modifyTime = user.ModifyTime != null ? user.ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        resusers.Add(new
                        {
                            RowNo = i++,
                            user.UserId,
                            user.UserCode,
                            user.UserName,
                            //course.CourseTeacher,
                            //course.TeachingClass,
                            //course.AuditOffice,
                            //course.CourseType,
                            user.HireDate,
                            user.Telephone,
                            user.Email,
                            user.Addr,
                            //CreateTime = createTime,
                            //ModifyTime = modifyTime,

                        });
                    }

                    res.rows = resusers;
                    return res;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="username">用户名称</param>
        /// <returns></returns>

        public t_user GetUserCode(String userName)
        {
            t_user model = null;
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    model = _DataEntities.t_user.Where(o => o.UserName == userName && o.Disabled == 0).ToList().FirstOrDefault();


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return model;

        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public t_user SelectId(string Code)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_user.Where(t => t.UserCode == Code).FirstOrDefault();
            }
        }

        /// <summary>
        /// 根据编号修改课件权限
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public int UpdateCoursewareCode(string Code, string CodeId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var role = _DataEntities.t_user.Where(t => t.UserCode == Code).FirstOrDefault();
                role.CoursewareCode = CodeId;
                return _DataEntities.SaveChanges();
            }
        }
    }
}
