using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using FanLiHang.Auth;

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

    /// <summary>
    /// 权限路由，注意权限名不分大小写
    /// </summary>
    public class AuthAciton : ActionFilterAttribute
    {
        public readonly static string NoAuth = "none";

        private string power;
        /// <summary>
        /// 设定页面权限
        /// </summary>
        /// <param name="power"></param>
        public AuthAciton(string power)
        {
            this.power = power;
        }
        /// <summary>
        /// 设定页面权限,默认以[Controller]_[Action]为权限
        /// </summary>
        public AuthAciton()
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

                try
                {
                    if (string.IsNullOrEmpty(power))
                    {
                        power = (context.RouteData.Values["Controller"].ToString() + "_" + context.RouteData.Values["Action"].ToString());
                    }

                    power = power.ToLower();
                    if (power.Equals(NoAuth)
                        || _BaseController._auth.CheckPower(power))
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
                        if (context.HttpContext.Request.Headers.Keys.Count(x => x.Equals("X-Requested-With")) > 0
                            && context.HttpContext.Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
                        {
                            context.Result = _BaseController.Json(new APIResult<string>());
                        }
                        else
                        {
                            context.Result = new RedirectResult("/login/index?action=" + context.HttpContext.Request.Path);
                            return;
                        }                        
                    }
                }
                catch (AccessViolationException ex)
                {
                    throw new Exception("<a href='/login/index'" + context.HttpContext.Request.Path + ">你没有该页面的操作权限，点击返回登录页</a>");
                }

            }
        }

    }
}
