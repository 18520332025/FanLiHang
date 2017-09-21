using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FanLiHang.Admins.Auth
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
        public AuthAciton() : this("")
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is BaseController)
            {
                BaseController _BaseController = context.Controller as BaseController;
                if (_BaseController.User == null
                    || _BaseController.User.Claims == null
                    || _BaseController.User.Claims.FirstOrDefault(x => x.Type == "token") == null)
                {
                    context.Result = new RedirectResult("/login/index?action=" + context.HttpContext.Request.Path);
                    return;
                }
                if (_BaseController._auth.Token == null)
                {
                    _BaseController._auth.Token = _BaseController.User.Claims.FirstOrDefault(x => x.Type == "token").Value;
                    _BaseController._auth.ID = int.Parse(_BaseController.User.Claims.FirstOrDefault(x => x.Type == "ID").Value);
                }

                if (string.IsNullOrEmpty(power) || _BaseController._auth.CheckPower(power))
                {
                    try
                    {
                        _BaseController.ViewBag.Auth = _BaseController._auth;
                        _BaseController.ViewBag.DocumentName = context.RouteData.Values["Controller"].ToString().ToLower() + "_" + context.RouteData.Values["Action"].ToString().ToLower();
                        base.OnActionExecuting(context);
                    }
                    catch
                    {
                        context.Result = new RedirectResult("/login/index?action=" + context.HttpContext.Request.Path);
                        return;
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
