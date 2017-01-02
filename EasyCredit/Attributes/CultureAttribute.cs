﻿using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EasyCredit.DAL.Attributes
{
    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cultureName = null;
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "ru";

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
