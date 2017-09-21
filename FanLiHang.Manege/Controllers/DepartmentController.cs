using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mange.Auth;
using FanLiHang.Data;
namespace Mange.Controllers
{

    public class DepartmentController : BaseController
    {
        private IDepartmentDataService dataService;
        public DepartmentController(IJWTAuth auth,IDepartmentDataService dataService) : base(auth)
        {
            this.dataService = dataService;
        }
        [AuthAciton]
        public IActionResult Index()
        {
            ViewBag.datas = dataService.GetList();
            return View();
        }

        [AuthAciton]
        public IActionResult Add()
        {
            return View();
        }
    }
}