﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WIFIcgq.Extends
{
    public class AdminBase:Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var result = HttpContext.Session.GetString("sUserName");
            if (result == null)
            {
                filterContext.Result = new RedirectResult("/User/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
