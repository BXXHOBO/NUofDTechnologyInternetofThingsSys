using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DtreeDataModel
    {

        public string id { get; set; }
        public string title { get; set; }
        public bool last { get; set; }
        public string IsWebPub { get; set; }

        public BoolChecked checkArr = new BoolChecked();
        public List<DtreeDataModel> children;
        public string parentId { get; set; }
    }
}
