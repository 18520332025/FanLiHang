using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mange.Auth
{
    public class BaseController : Controller
    {
        public readonly IJWTAuth _auth;

        public BaseController(IJWTAuth auth)
        {
            this._auth = auth;
            //ViewBag.auth = _auth;
        }
    }

    public class AuthAciton : ActionFilterAttribute
    {
        private readonly string power;
        public AuthAciton(string power)
        {
            this.power = power;
        }
        public AuthAciton() : this("")
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is BaseController)
            {
                BaseController BaseController = context.Controller as BaseController;
                if (BaseController._auth.Token == null)
                {
                    if (context.HttpContext.Request.Cookies.ContainsKey("token"))
                    {
                        BaseController._auth.Token = context.HttpContext.Request.Cookies["token"];
                    }
                    else
                    {
                        //new Exception("你没有该页面的操作权限"); 
                        context.Result = new RedirectResult("/login/index?action=" + context.HttpContext.Request.Path);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(power) || BaseController._auth.CheckPower(power))
                {
                    try
                    {
                        BaseController.ViewBag.Auth = BaseController._auth;
                        BaseController.ViewBag.DocumentName = context.RouteData.Values["Controller"].ToString().ToLower() + "_" + context.RouteData.Values["Action"].ToString().ToLower();
                        base.OnActionExecuting(context);
                    }
                    catch
                    {
                        BaseController.RedirectToRoute("/login/index");
                    }
                }
                else
                {
                    new Exception("你没有该页面的操作权限");
                }

            }
        }

    }
}
