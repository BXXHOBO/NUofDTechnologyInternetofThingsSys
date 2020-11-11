using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_t_role_menuZzc
    {
        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public t_role_menu SelectId(string Code)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_role_menu.Where(t=>t.RoleCode==Code).FirstOrDefault();
            }
        }
        /// <summary>
        /// 根据编号修改课件权限
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        //public int UpdateCoursewareCode(string Code, string CodeId)
        //{
        //    using (var _DataEntities = new SUC_SYSContainer())
        //    {
        //       var role= _DataEntities.t_role_menu.Where(t => t.RoleCode == Code).FirstOrDefault();
        //        role.CoursewareCode = CodeId;
        //        return _DataEntities.SaveChanges();
        //    }
        //}
    }
}
