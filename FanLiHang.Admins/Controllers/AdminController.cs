using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanLiHang.Data;
using FanLiHang.Model;
using FanLiHang.Admins.Auth;
namespace FanLiHang.Admins.Controllers
{
    public class AdminController : BaseController
    {
        public IUserInfoDataService userInfoDataService;
        public IUserAuthorizationDataService userAuthorizationDataService;
        public IDepartmentDataService departmentDataService; 
        public AdminController(IJWTAuth auth, IUserInfoDataService userInfoDataService, IUserAuthorizationDataService userAuthorizationDataService, IDepartmentDataService departmentDataService) : base(auth)
        {
            this.userInfoDataService = userInfoDataService;
            this.userAuthorizationDataService = userAuthorizationDataService;
            this.departmentDataService = departmentDataService; 
        }
        [AuthAciton("admin/index")]
        public IActionResult Index()
        {
            var userAuthorization = userAuthorizationDataService.Get(_auth.ID);
            var userInfo = userInfoDataService.Get(userAuthorization.UserID);
            Department department = departmentDataService.Get(userAuthorization.DepartmentID);
            ViewBag.LoginID = userAuthorization.LoginID;
            ViewBag.DepartmentName = department.Name;
            return View(userInfo);
        }
        [AuthAciton]
        public IActionResult Welcome()
        {
            return View();
        }

    }
}