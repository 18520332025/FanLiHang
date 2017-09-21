using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanLiHang.Admins;
using FanLiHang.Data;
using FanLiHang.Admins.Auth;
using FanLiHang.Dapper.Helper;
using FanLiHang.Model;
using FanLiHang.Auth;

namespace FanLiHang.Admins.Controllers
{

    public class DepartmentController : BaseController
    {
        private IDepartmentDataService dataService;
        private IUserInfoDataService userInfoDataService;
        public DepartmentController(IJWTAuth auth, IDepartmentDataService dataService, IUserInfoDataService userInfoDataService) : base(auth)
        {
            this.dataService = dataService;
            this.userInfoDataService = userInfoDataService;
        }

        [AuthAciton]
        public IActionResult Index(PagerParameter pageParameter)
        {
            PageParameterDefault ppd = new PageParameterDefault();
            ppd.SetDefaultValue(pageParameter);
            var pagers = dataService.GetList(pageParameter, x => x.HasUser = true);
            return View(pagers);
        }

        [AuthAciton]
        [HttpGet]
        public IActionResult Add()
        {
            var users = this.userInfoDataService.GetListAtCache();
            this.ViewBag.Users = users;
            return View();
        }

        [AuthAciton]
        [HttpPost]
        public IActionResult Add(Department department)
        {
            department.CreateBy = 1;
            department.Deleted = false;
            department.CreateDate = DateTime.Now;
            if (dataService.Get(department.Name) != null)
            {
                return Json(new APIResult<string>(errors: "部门已存在"));
            }
            else
            {
                dataService.Add(department);
                return Json(new APIResult<Department>(data: department));
            }
        }

        [AuthAciton]
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            var users = this.userInfoDataService.GetListAtCache();
            this.ViewBag.Users = users;
            var department = this.dataService.Get(ID);
            return View(department);
        }

        [AuthAciton]
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            var originalDepartment = this.dataService.Get(department.ID);
            originalDepartment.Name = department.Name;
            originalDepartment.Manager = department.Manager;
            if (dataService.Update(originalDepartment))
            {
                return Json(new APIResult<string>(data: ""));
            }
            else
            {
                return Json(new APIResult<string>(data: "数据操作失败"));
            }
        }

        [AuthAciton]
        [HttpPost]
        public IActionResult DeleteItem(int ID)
        {
            Department department = dataService.Get(ID);
            department.Deleted = true;
            if (dataService.Update(department))
            {
                return Json(new APIResult<string>(data: ""));
            }
            else
            {
                return Json(new APIResult<string>(data: "数据操作失败"));
            }
        }
    }
}