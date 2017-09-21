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
using System.Net.Http.Headers;
using System.Text;
using FanLiHang.Model;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FanLiHang.Admins.Controllers
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
            loginUser.SystemCode = SystemCode.Power;
            HttpClient client = new HttpClient();
            try
            {
                HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "text/json");
                var responseMessage = client.PostAsync("http://localhost:33071/api/TokenAuth", httpContent).Result;
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseText = responseMessage.Content.ReadAsStringAsync().Result;
                    UserTokenAuthResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<UserTokenAuthResult>(responseText);

                    var identity = new ClaimsIdentity("Forms");
                    identity.AddClaim(new Claim("token", result.accessToken));
                    identity.AddClaim(new Claim("ID", result.ID));
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
            public string Username
            {
                get; set;
            }

            public string Password
            {
                get; set;
            }

            public bool RememberUser
            {
                get; set;
            }

            public SystemCode SystemCode
            {
                get;
                set;
            }
        }
    }
}
