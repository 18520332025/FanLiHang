using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanLiHang.Admins.Auth;
using FanLiHang.Dapper.Helper;
using FanLiHang.Data;
using FanLiHang.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using FanLiHang.Auth;

namespace FanLiHang.Admins.Controllers
{
    public class UserController : BaseController
    {
        IUserAuthorizationDataService authorizationDataService;
        public UserController(IJWTAuth auth, IUserInfoDataService userInfoDataService, IUserAuthorizationDataService authorizationDataService) : base(auth)
        {
            this.userInfoDataService = userInfoDataService;
            this.authorizationDataService = authorizationDataService;
        }

        private IUserInfoDataService userInfoDataService;
        [AuthAciton]
        public IActionResult Index(PagerParameter pageParameter)
        {
            PageParameterDefault ppd = new PageParameterDefault();
            ppd.SetDefaultValue(pageParameter);
            var pager = userInfoDataService.GetPager(pageParameter);
            return View(pager);
        }

        [AuthAciton]
        public IActionResult Add()
        {
            EditUser editUser = new EditUser();
            return View(editUser);
        }
        [AuthAciton]
        [HttpPost]
        public IActionResult DeleteItem(int ID)
        {
            if (!userInfoDataService.Delete(new UserInfo { ID = ID }))
            {
                return Json(new APIResult<string>(errors: "数据操作失败"));
            }
            else
            {
                return Json(new APIResult<string>(data: ""));
            }
        }

        [AuthAciton]
        [HttpGet]
        public IActionResult Edit(int ID)
        {

            var user = userInfoDataService.Get(ID);
            return View(user);
        }
        [AuthAciton]
        public IActionResult SetAuth(int ID, PagerParameter pager)
        {
            PageParameterDefault pageParameterDefault = new PageParameterDefault();
            pageParameterDefault.SetDefaultValue(pager);
            var user = authorizationDataService.GetList(ID, pager);
            ViewBag.UserID = ID;
            return View(user);
        }

        [AuthAciton]
        public IActionResult AuthEdit(int ID)
        {
            var user = authorizationDataService.Get(ID);
            return View(user);
        }

        [AuthAciton]
        public IActionResult AuthAdd(int ID)
        {
            var user = new UserAuthorization();
            user.UserID = ID;
            return View(user);
        }

        [AuthAciton]
        [HttpPost]
        public IActionResult Save([FromServices]IHostingEnvironment env, EditUser editUser)
        {
            string fileName = "\\images\\" + Guid.NewGuid().ToString();
            if (editUser.PhotoData != null)
            {
                fileName = fileName + System.IO.Path.GetExtension(editUser.PhotoData.FileName);
                using (var stream = new FileStream(env.WebRootPath + fileName, FileMode.CreateNew))
                {
                    editUser.PhotoData.CopyTo(stream);
                    stream.Flush();
                }
                editUser.PhotoUrl = fileName.Replace("\\", "/");
            }
            if (editUser.ID == 0 && !userInfoDataService.Add(editUser))
            {
                return Json(new APIResult<string>(errors: "数据操作失败"));
            }
            if (editUser.ID != 0 && !userInfoDataService.Update(editUser))
            {
                return Json(new APIResult<string>(errors: "数据操作失败"));
            }
            else
            {
                return Json(new APIResult<string>(data: ""));
            }
            //var user = userInfoDataService.Get(editUser.ID);
            //return View(user);
        }

        [AuthAciton]
        [HttpPost]
        public IActionResult AuthSave(UserAuthorization userAuthorization)
        {
            if (userAuthorization.ID == 0 && authorizationDataService.Insert(userAuthorization))
            {
                return Json(new APIResult<string>(errors: "数据操作失败"));
            }
            else if (userAuthorization.ID != 0 && authorizationDataService.Update(userAuthorization))
            {
                return Json(new APIResult<string>(errors: "数据操作失败"));
            }
            else
            {
                return Json(new APIResult<string>(data: ""));
            }
        }

        public class EditUser : UserInfo
        {
            public IFormFile PhotoData { get; set; }
        }
    }
}