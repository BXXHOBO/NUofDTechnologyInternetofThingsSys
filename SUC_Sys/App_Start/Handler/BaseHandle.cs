using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SUC_Sys
{
    public abstract class BaseHandle : Controller
    {
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }

    }
}
