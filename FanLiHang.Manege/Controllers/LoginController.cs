using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using FanLiHang.Auth;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mange.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginUser loginUser)
        {

            HttpClient client = new HttpClient();
            try
            {
                var responseMessage = client.PostAsync("http://localhost:33071/api/TokenAuth", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(loginUser))).Result;
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseText = responseMessage.Content.ReadAsStringAsync().Result;
                    APIResult<UserTokenAuthResult> result = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResult<UserTokenAuthResult>>(responseText);

                    var identity = new ClaimsIdentity("Forms");
                    identity.AddClaim(new Claim("token", result.data.accessToken));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("UserAuth", principal);
                    return new JsonResult(new APIResult<string>(data: ""));
                }
                else
                {
                    return new JsonResult(new APIResult<string>(data: responseMessage.Content.ReadAsStringAsync().Result));
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new APIResult<string>(errors: ex.Message));
            }
        }

        public IActionResult LoginOut()
        {
            HttpContext.Response.Cookies.Delete("token");
            return new RedirectResult("/login/index");
        }

        public class LoginUser
        {
            public string UserName
            {
                get; set;
            }

            public string PassWord
            {
                get; set;
            }

            public string RememberUser
            {
                get; set;
            }
        }
    }
}
