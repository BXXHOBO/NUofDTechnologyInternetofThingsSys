using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResultRes
    {
        public bool IsSuccess { set; get; }
        public string Msg { set; get; }
        public object ResultValue { set; get; }
        public string Html { set; get; }
        public int ResState { set; get; }
    }
}
