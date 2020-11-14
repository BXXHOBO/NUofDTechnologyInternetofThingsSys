using Common;
using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUC_EntityBLL
{
    public class BLL_Machine
    {
        public List<SUC_Machine> GetMachineList()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_Machine.Where(p => p.Disabled == 0).OrderBy(u => u.UavSerialNO).ToList();
                return Data;
            }
        }
    }
}
