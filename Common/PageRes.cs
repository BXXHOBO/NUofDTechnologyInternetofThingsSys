using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PageRes
    {
        private int _page;
        private int _pageSize;
        public int page { get { if (_page == 0) return 1; return _page; } set { _page = value; } }
        public int pageSize { get { if (_pageSize == 0) return 10; return _pageSize; } set { _pageSize = value; } }
        //总记录数
        public int total { get; set; }
        //共几页
        public int pageCount
        {
            get
            {
                return total / pageSize + ((total % pageSize) > 0 ? 1 : 0);
                //if (total % 10 == 0)
                //    return total / 10;
                //return total / 10 + 1;
            }
        }
        public Object rows { set; get; }
    }
}
