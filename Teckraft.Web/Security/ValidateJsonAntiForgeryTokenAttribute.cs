﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;
//using System.Web.Mvc;
using System.Web.Http.Filters;

namespace Teckraft.Web.Security
{
    public class ValidateJsonAntiForgeryTokenAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            try
            {
                string cookieToken = "";
                string formToken = "";


               // if (filterContext.RequestContext.HttpContext.Request.Headers["UID"] != null)
                    if (actionContext.Request.Headers.GetValues("UID") !=null)
                {
                    IEnumerable<string> tokenHeaders = null;
                    tokenHeaders = actionContext.Request.Headers.GetValues("UID");
                    string[] tokens = tokenHeaders.First().Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
                if (HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "_formToken"].ToString().Trim().ToLower() != formToken.ToLower().Trim()) throw new UnauthorizedAccessException();
                AntiForgery.Validate(cookieToken, formToken);
            }
            catch (Exception ex)
            {
                
                throw new UnauthorizedAccessException();
                // Your error handling here
            }
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
            
        //      base.OnActionExecuted(filterContext);
        //}
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //        base.OnResultExecuted(filterContext);
        //}

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    filterContext.Controller.TempData["filter"] = "it worked!";

        //    base.OnActionExecuting(filterContext);

        //    try
        //    {
        //        string cookieToken = "";
        //        string formToken = "";


        //        if (filterContext.RequestContext.HttpContext.Request.Headers["UID"] != null)
        //        {
        //            IEnumerable<string> tokenHeaders = null;
        //            tokenHeaders = filterContext.RequestContext.HttpContext.Request.Headers.GetValues("UID");
        //            string[] tokens = tokenHeaders.First().Split(':');
        //            if (tokens.Length == 2)
        //            {
        //                cookieToken = tokens[0].Trim();
        //                formToken = tokens[1].Trim();
        //            }
        //        }
        //        if (HttpContext.Current.Cache[HttpContext.Current.User.Identity.Name + "_formToken"].ToString().Trim().ToLower() != formToken.ToLower().Trim()) throw new UnauthorizedAccessException();
        //        AntiForgery.Validate(cookieToken, formToken);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new UnauthorizedAccessException();
        //        // Your error handling here
        //    }
        //}
    }
}