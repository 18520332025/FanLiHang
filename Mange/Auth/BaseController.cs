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
        }
    }

    public class AuthAciton : ActionFilterAttribute
    {
        private readonly string power;
        public AuthAciton(string power)
        {
            this.power = power;
        }

        public  override void OnActionExecuting(ActionExecutingContext context)
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
                        context.Result = new RedirectResult("/login/index");
                        return;
                    }
                }

                if (BaseController._auth.CheckPower(power))
                {
                    try
                    {
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
