using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FanLiHang.Auth;
using FanLiHang.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FanLiHang.Model;

namespace tokenJWT.Controllers
{

    [Route("api/[controller]")]
    public class GetPowersController : Controller
    {
        private IUserAuthorizationDataService userAuthorizationDataService;
        public GetPowersController(IUserAuthorizationDataService userAuthorizationDataService)
        {
            this.userAuthorizationDataService = userAuthorizationDataService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public APIResult<string[]> GetPowers()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var id = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "ID").Value;
            var systemID = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "SystemCode").Value;
            var userAuthors = userAuthorizationDataService.GetListAtCache();
            var existUser = userAuthors.FirstOrDefault(u => u.ID.ToString() == id);

            if (existUser == null)
            {
                return new APIResult<string[]>();
            }
            else
            {
                var powers = userAuthorizationDataService.GetPowers(existUser.ID,(SystemCode)int.Parse(systemID));
                return new APIResult<string[]>(powers.Select(x => x.Power).ToArray());
            }
        }
    }
}