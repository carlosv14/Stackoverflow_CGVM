using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Mvc.Routing;
using FilterAttribute = System.Web.Http.Filters.FilterAttribute;
using IActionFilter = System.Web.Mvc.IActionFilter;

namespace WebApplication1.Controllers.CostumeDataNotation
{
    public class LoginAttribute : FilterAttribute, IActionFilter,IResultFilter,IExceptionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}