﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mange.Controllers
{
    public class HomeController : Controller
    {
        [Auth.AuthAciton(power: "/home/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
