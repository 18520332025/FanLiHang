using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Mange.Auth;
namespace Mange.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(IJWTAuth auth) : base(auth)
        {
        }
        [AuthAciton("admin/index")]
        public IActionResult Index()
        {
            return View();
        }
        [AuthAciton]
        public IActionResult Welcome()
        {
            return View();
        }
         
    }
}