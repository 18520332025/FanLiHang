using System;
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
        IAppInfoDataService appInfoDataService;
        public FunctionPowerController(IJWTAuth auth, IFunctionPowerDataService functionPowerDataService, IAppInfoDataService appInfoDataService) : base(auth)
        {
            this.functionPowerDataService = functionPowerDataService;
            this.appInfoDataService = appInfoDataService;
        }

        [AuthAciton]
        public IActionResult Index(int? ID)
        {
            var appInfoList = appInfoDataService.GetList();
            if (!ID.HasValue)
            {
                if (appInfoList.Count() == 0)
                    throw new Exception("暂无程序信息");
                ID = appInfoList.First().ID;
            }
            var list = ExternalFunctionPowerSort.Sort(functionPowerDataService.GetList(ID.Value).MapperList<ExternalFunctionPowerViewModel, FunctionPower>());
            ViewBag.List = list;
            FunctionPower functionPower = new FunctionPower
            {
                AppInfoID = ID.Value
            };
            return View(functionPower);
        }

        [AuthAciton("FunctionPower_Edit")]
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