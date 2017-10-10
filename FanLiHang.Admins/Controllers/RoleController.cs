using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanLiHang.Admins.Auth;
using FanLiHang.Data;
using FanLiHang.Model;
using FanLiHang.Dapper.Helper;
using FanLiHang.Auth;
using FanLiHang.Admins.Models.FunctionPowerViewModels;
using FanLiHang.Admins.Extensions;
namespace FanLiHang.Admins.Controllers
{
    public class RoleController : BaseController
    {
        private IRoleDataService roleDataService;
        private IDepartmentDataService departmentDataService;
        public RoleController(IJWTAuth auth, IRoleDataService roleDataService, IDepartmentDataService departmentDataService) : base(auth)
        {
            this.roleDataService = roleDataService;
            this.departmentDataService = departmentDataService;
        }

        [AuthAciton]
        public IActionResult Index(int? ID, PagerParameter pagerParameter)
        {
            var departmentList = departmentDataService.GetList().MapperList<ExternalDepartmentViewModel, Department>();

            PageParameterDefault ppd = new PageParameterDefault();
            ppd.SetDefaultValue(pagerParameter);
            Department department = new Department();
            if (!ID.HasValue)
            {
                department = departmentList.First();
            }
            else
            {
                department = departmentDataService.Get(ID.Value);
            }
            if (department.ID != 0)
            {
                var pager = roleDataService.GetList(department.ID, pagerParameter);
                ViewBag.Department = department;
                ViewBag.DepartmentList = departmentList;
                return View(pager);
            }
            else
            {
                return View();
            }
        }

        [AuthAciton]
        [HttpGet]
        public IActionResult Add(int ID)
        {
            Role role = new Role();
            role.DepartmentID = ID;
            return View(role);
        }

        [AuthAciton]
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            Role role = roleDataService.Get(ID);
            return View("add", role);
        }

        [AuthAciton]
        [HttpPost]
        public IActionResult Add(Role role)
        {
            roleDataService.Add(role);
            return Json(new APIResult<Role>(data: role));
        }

        [AuthAciton]
        [HttpPost]
        public IActionResult Edit(Role role)
        {
            roleDataService.Update(role);
            return Json(new APIResult<Role>(data: role));

        }

        [AuthAciton("none")]
        [HttpPost]
        public IActionResult Save(Role role)
        {
            if (role.ID == 0)
            {
                return RedirectToAction("Add");
            }
            else
            {
                return RedirectToAction("Edit");
            }

        }
    }
}