using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Fanlihang.Auth;
namespace tokenJWT.Controllers
{
    
    public class GetPowersController : Controller
    {
        [HttpGet]
        [Authorize("Bearer")]
        public APIResult<string[]> GetPowers()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var id = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "ID").Value;
            var user = UserStorage.Users.FirstOrDefault(x => x.ID.Equals(id));
            if (user == null)
            {
                return new APIResult<string[]>();
            }
            else
            {
                return new APIResult<string[]>(user.Powers); 
            }
        }
    }
}