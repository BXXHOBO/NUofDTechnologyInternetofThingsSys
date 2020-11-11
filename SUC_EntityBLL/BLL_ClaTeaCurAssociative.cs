using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_ClaTeaCurAssociative
    {
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        public List<SUC_ClaTeaCurAssociative> SelectAll()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.SUC_ClaTeaCurAssociative.ToList();
            }
        }
    }
}
