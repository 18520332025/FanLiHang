﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanLiHang.Data;
using FanLiHang.Admins.Auth;
using FanLiHang.Model;
using FanLiHang.Auth;
using FanLiHang.Admins.Extensions;
using FanLiHang.Admins.Models.FunctionPowerViewModels;
namespace FanLiHang.Admins.Controllers
{
    public class FunctionPowerController : BaseController
    {
        IFunctionPowerDataService functionPowerDataService;
        public FunctionPowerController(IJWTAuth auth, IFunctionPowerDataService functionPowerDataService) : base(auth)
        {
            this.functionPowerDataService = functionPowerDataService;
        }

        [AuthAciton]
        public IActionResult Index(int ID)
        {
            var list = ExternalFunctionPowerSort.Sort(functionPowerDataService.GetList(ID).MapperList<ExternalFunctionPowerViewModel, FunctionPower>());
            ViewBag.List = list;
            FunctionPower functionPower = new FunctionPower
            {
                AppInfoID = ID
            };
            return View(functionPower);
        }

        [AuthAciton]
        public IActionResult Save(FunctionPower functionPower)
        {
            if (functionPower.ID == 0)
            {
                functionPowerDataService.Add(functionPower);
            }
            else
            {
                functionPowerDataService.Update(functionPower);
            }
            return Json(new APIResult<FunctionPower>(data: functionPower));
        }
    }
}