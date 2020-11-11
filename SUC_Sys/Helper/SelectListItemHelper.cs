using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TRM_Sys.Helper
{
    public class SelectListItemHelper
    {
        public List<SelectListItem> GetDevMaintainState()
        {
            List<SelectListItem> lsl = new List<SelectListItem>();
            lsl.Add(new SelectListItem { Text = "请选择", Value = "" });
            SelectListItem sl1 = new SelectListItem();
            sl1.Value = "0";
            sl1.Text = "未养护(未维修)";
            lsl.Add(sl1);
            SelectListItem sl2 = new SelectListItem();
            sl2.Value = "1";
            sl2.Text = "已养护(维修)";
            lsl.Add(sl2);
            return lsl;
        }

        public List<SelectListItem> GetDevMaintainType()
        {
            List<SelectListItem> lsl = new List<SelectListItem>();
            lsl.Add(new SelectListItem { Text = "请选择", Value = "" });
            SelectListItem sl1 = new SelectListItem();
            sl1.Value = "0";
            sl1.Text = "保养";
            lsl.Add(sl1);
            SelectListItem sl2 = new SelectListItem();
            sl2.Value = "1";
            sl2.Text = "维修";
            lsl.Add(sl2);
            return lsl;
        }
       
    }
}