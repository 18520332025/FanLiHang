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
        public IActionResult Index(int ID, PagerParameter pagerParameter)
        {
            PageParameterDefault ppd = new PageParameterDefault();
            ppd.SetDefaultValue(pagerParameter);
            var pager = roleDataService.GetList(ID, pagerParameter);
            var departmentList = departmentDataService.GetList().MapperList<ExternalDepartmentViewModel, Department>(); 
            var selectDepartment = departmentDataService.Get(ID);
            ViewBag.Department = selectDepartment;
            ViewBag.DepartmentList = departmentList;
            return View(pager);
        }

        [AuthAciton]
        public IActionResult Add(int ID)
        {
            Role role = new Role();
            role.DepartmentID = ID;
            return View(role);
        }

        [AuthAciton]
        public IActionResult Edit(int ID)
        {
            Role role = roleDataService.Get(ID);
            return View("add", role);
        }
        [AuthAciton]
        public IActionResult Save(Role role)
        {
            if (role.ID == 0)
            {
                roleDataService.Add(role);
            }
            else
            {
                roleDataService.Update(role);
            }

            return Json(new APIResult<Role>(data: role));
        }
    }
}