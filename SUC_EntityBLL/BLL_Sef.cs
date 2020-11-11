using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_Sef
    {
        /// <summary>
        /// 根据ID筛选一级
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public List<SUC_Courseware> SelectSef()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.SUC_Courseware.Where(c => c.P_CoursewareCode == "0" && c.Disabled == 0).ToList();

            }

        }
        /// <summary>
        /// 查询是否有子级
        /// </summary>
        /// <returns></returns>
        public int SelectSefCount(Guid guid)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                List<SUC_Courseware> tRM_s = _DataEntities.SUC_Courseware.Where(c => c.P_CoursewareCode == guid.ToString() && c.Disabled == 0).ToList();
                return tRM_s.Count();
            }
        }
        /// <summary>
        /// 根据编号，Id查询子级
        /// </summary>
        /// <returns></returns>
        public List<SUC_Courseware> SelectSefId(Guid guid)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.SUC_Courseware.Where(c => c.P_CoursewareCode == guid.ToString() && c.Disabled == 0).ToList();
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="CoursewareId"></param>
        /// <param name="CoursewareCode"></param>
        /// <param name="UserCode"></param>
        /// <param name="P_CoursewareCode"></param>
        /// <param name="IsWebPub"></param>
        /// <param name="Icon"></param>
        /// <param name="SortID"></param>
        /// <param name="CoursewarePath"></param>
        /// <returns></returns>
        public int AddCourseware(Guid CoursewareId, string CoursewareCode,string CoursewareName, string P_CoursewareCode, string IsWebPub, string Icon, int SortID, string CoursewarePath,string Remark,string UserName)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var r = _DataEntities.SUC_Courseware.Where(o => o.CoursewareId == CoursewareId);
                var w= _DataEntities.SUC_Courseware.Where(o=>o.CoursewareCode==CoursewareCode).ToList();
                if (w.Count()>0)
                {
                    return 2;
                }
                if (r.Count() > 0)
                {
                    return -1;
                }
                SUC_Courseware tRM_Courseware = new SUC_Courseware();
                tRM_Courseware.CoursewareId = CoursewareId;
                tRM_Courseware.CoursewareCode = CoursewareCode;
                tRM_Courseware.CoursewareName = CoursewareName;
                tRM_Courseware.P_CoursewareCode = P_CoursewareCode;
                tRM_Courseware.IsWebPub = IsWebPub;
                tRM_Courseware.Icon = Icon;
                tRM_Courseware.SortID = SortID;
                tRM_Courseware.CoursewarePath = CoursewarePath;
                tRM_Courseware.State = 0;
                tRM_Courseware.Disabled = 0;
                tRM_Courseware.Remark = Remark;
                tRM_Courseware.Rec_CreateTime = DateTime.Now;
                tRM_Courseware.Rec_CreateBy = UserName;
                tRM_Courseware.Rec_ModifyTime = DateTime.Now;
                tRM_Courseware.Rec_ModifyBy = "";


                _DataEntities.SUC_Courseware.Add(tRM_Courseware);

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
        /// 修改数据
        /// </summary>
        /// <param name="CoursewareId"></param>
        /// <param name="CoursewareCode"></param>
        /// <param name="CoursewareName"></param>
        /// <param name="IsWebPub"></param>
        /// <param name="Icon"></param>
        /// <param name="SortID"></param>
        /// <returns></returns>
        public int UpdateCourseware(Guid CoursewareId, string CoursewareCode, string CoursewareName, string IsWebPub, string Icon, int SortID ,string UserName)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var ent = _DataEntities.SUC_Courseware.Where(c => c.CoursewareId == CoursewareId).FirstOrDefault();
                ent.CoursewareCode = CoursewareCode;
                ent.CoursewareName = CoursewareName;
                ent.IsWebPub = IsWebPub;
                ent.Icon = Icon;
                ent.SortID = SortID;
                ent.Rec_ModifyTime = DateTime.Now;
                ent.Rec_CreateBy = UserName;
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
        /// 赋值修改弹框，根据主键查询
        /// </summary>
        /// <param name="CoursewareId"></param>
        /// <returns></returns>
        public SUC_Courseware Assign(Guid CoursewareId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.SUC_Courseware.Where(c => c.CoursewareId == CoursewareId).FirstOrDefault();
            }
        }
        /// <summary>
        /// 删除数据，假删除
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool DeleteCourseware(Guid guid)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var course= _DataEntities.SUC_Courseware.Where(o=>o.CoursewareId==guid).FirstOrDefault();
                if (course==null)
                {
                    return false;
                }
                else
                {
                    course.Disabled = 1;
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
        }

        /// <summary>
        /// 查询第一级
        /// </summary>
        /// <returns></returns>
        public List<SUC_Courseware> SelectSef1()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.SUC_Courseware.Where(c => c.P_CoursewareCode == "0" && c.Disabled == 0).ToList();

            }

        }
        /// <summary>
        /// 查询是否有子级
        /// </summary>
        /// <returns></returns>
        public int SelectSefCount2(Guid guid)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                List<SUC_Courseware> tRM_s = _DataEntities.SUC_Courseware.Where(c => c.P_CoursewareCode == guid.ToString() && c.Disabled == 0).ToList();
                return tRM_s.Count();
            }
        }
        /// <summary>
        /// 根据Id查询子级
        /// </summary>
        /// <returns></returns>
        public List<SUC_Courseware> SelectSefId3(Guid guid)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.SUC_Courseware.Where(c => c.P_CoursewareCode == guid.ToString() && c.Disabled == 0).ToList();
            }
        }
    }
}
