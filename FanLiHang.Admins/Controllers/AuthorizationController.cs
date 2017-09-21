using FanLiHang.Admins.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanLiHang.Data;
using FanLiHang.Model;
using Microsoft.AspNetCore.Mvc;
using FanLiHang.Admins.Extensions;
using FanLiHang.Admins.Models.FunctionPowerViewModels;

namespace FanLiHang.Admins.Controllers
{
    public class AuthorizationController : BaseController
    {
        public IDepartmentDataService departmentDataService;
        public IRoleDataService roleDataService;
        public IFunctionPowerDataService powerDataService;
        public AuthorizationController(IJWTAuth iJWTAuth, IDepartmentDataService departmentDataService, IFunctionPowerDataService powerDataService, IRoleDataService roleDataService) : base(iJWTAuth)
        {
            this.departmentDataService = departmentDataService;
            this.powerDataService = powerDataService;
            this.roleDataService = roleDataService;
        }

        [AuthAciton]
        public IActionResult Index(int ID)
        {
            var departmentList = departmentDataService.GetList(new Dapper.Helper.PagerParameter { PageIndex = 1, PageSize = 99999999, OrderBy = "ID" },
                x =>
            {
                x.HasRotes = true;
                x.HasPowers = false;
            }).Rows.MapperList<ExternalDepartmentViewModel, Department>();
            ViewBag.DepartmentList = departmentList;
            ViewBag.AppInfoID = ID;
            return View();

        }

        [AuthAciton]
        public IActionResult GetPowers(AuthorizationQueryModel queryModel)
        {
            var powers = powerDataService.GetList(queryModel.AppInfoID).MapperList<ExternalFunctionPowerViewModel, FunctionPower>();
            if (queryModel.Type.HasValue)
            {
                var departmentID = queryModel.NodeID;
                if (queryModel.Type == FunctionPowerType.Role)
                {
                    var role = roleDataService.Get(queryModel.NodeID);
                    departmentID = role.DepartmentID;
                }
                var departmentPowers = departmentDataService.GetPowers(departmentID, queryModel.AppInfoID);
                //赋予选中
                departmentPowers.ForEach(x => powers.Where(y => y.ID == x.FunctionPowerID).First().State = new ExternalFunctionPowerViewModelState { Checked = true });
                if (queryModel.Type == FunctionPowerType.Role)
                {
                    foreach (var item in powers)
                    {
                        if (item.State != null && item.State.Checked)
                        {
                            item.Requisite = true;
                            item.Color = "silver";
                            item.Icon = "vp-requisite";
                            item.State = new ExternalFunctionPowerViewModelState
                            {
                                Checked = true
                            };
                        }
                    }
                    var rolePowers = roleDataService.GetPowers(queryModel.NodeID, queryModel.AppInfoID);
                    rolePowers.ForEach(x =>
                         powers.Where(y => y.ID == x.FunctionPowerID).First().State = new ExternalFunctionPowerViewModelState { Checked = true }
                    );
                }
            }
            powers = ExternalFunctionPowerSort.Sort(powers);
            return Json(powers);
        }


    }
}
