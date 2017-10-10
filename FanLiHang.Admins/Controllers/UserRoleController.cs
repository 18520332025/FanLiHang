using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanLiHang.Admins.Auth;
using FanLiHang.Data;
using FanLiHang.Model;
using FanLiHang.Auth;

namespace FanLiHang.Admins.Controllers
{
    public class UserRoleController : BaseController
    {
        IUserInfoDataService userInfoDataService;
        IUserAuthorizationDataService userAuthorizationDataService;
        IUserPowerDataService userPowerDataService;
        public UserRoleController(IJWTAuth iJWTAuth, IUserInfoDataService userInfoDataService, IUserAuthorizationDataService userAuthorizationDataService, IUserPowerDataService userPowerDataService) : base(iJWTAuth)
        {
            this.userInfoDataService = userInfoDataService;
            this.userAuthorizationDataService = userAuthorizationDataService;
            this.userPowerDataService = userPowerDataService;
        }
        [AuthAciton]
        public IActionResult Index(int? UserID, int? AccountID)
        {
            ViewBag.Users = userInfoDataService.GetList();
            if (!UserID.HasValue)
            {
                ViewBag.BindUser = ViewBag.Users[0];
            }
            else
            {
                ViewBag.BindUser = userInfoDataService.Get(UserID.Value);

            }

            ViewBag.AccountList = userAuthorizationDataService.GetList(ViewBag.BindUser.ID, new Dapper.Helper.PagerParameter { PageIndex = 1, PageSize = 9999999, OrderBy = "ID" }).Rows;
            IEnumerable<UserPower> powers = new List<UserPower>();
            if (ViewBag.AccountList.Count > 0)
            {
                if (!AccountID.HasValue)
                {
                    ViewBag.Account = ViewBag.AccountList[0];
                }
                else
                {
                    ViewBag.Account = userAuthorizationDataService.Get(AccountID.Value);
                }
                powers = userPowerDataService.GetList(ViewBag.Account.ID);
            }
            return View(powers);
        }

        [AuthAciton]
        public IActionResult Edit(int AccountID, int? UserID)
        {
            UserPower userPower = new UserPower();
            if (UserID.HasValue)
            {
                userPower = userPowerDataService.Get(UserID.Value);
            }
            userPower.UserAuthorizationID = AccountID;
            return View(userPower);
        }

        public IActionResult Save(UserPower userPower)
        {
            try
            {
                if (userPower.ID != 0)
                {
                    userPowerDataService.Update(userPower);
                }
                else
                {
                    userPowerDataService.Insert(userPower);
                }
                return Json(new APIResult<UserPower>(userPower));
            }
            catch (Exception ex)
            {
                return Json(new APIResult<UserPower>(errors: ex.Message));
            }
        }
    }
}