using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUC_EntityBLL.Helper
{
    public class BLL_CodeHelper
    {
        #region  我的编号生成规则
        /// <summary>
        /// 根据表名查询编号生成规则字段，并修改Rule_two.(先加1，再赋值)
        /// </summary>
        /// <param name="Tname">表名</param>
        /// <param name="length">生成编号的长度</param>
        /// <returns>生成的编号</returns>
        public string CreateNo(string Tname, int length)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                t_code_rules mcr = _DataEntities.t_code_rules.FirstOrDefault(c => c.RuleName == Tname && c.Disabled ==0);
                if (mcr != null)
                {
                    string str = "";
                    for (int i = 0; i < length; i++)
                    {
                        str += "0";
                    }
                    mcr.RuleTwo = (Convert.ToInt32(mcr.RuleTwo) + 1).ToString(str);
                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return (mcr.RuleOne + mcr.RuleTwo).ToString();
                    }
                }
                return "";
            }
        }

        #endregion
    }
}
