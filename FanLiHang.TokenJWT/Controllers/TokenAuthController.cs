using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using tokenJWT.Auth;
using Newtonsoft.Json;
using FanLiHang.Auth;
using System.IO;
using FanLiHang.Data;
using FanLiHang.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tokenJWT.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {
        private IUserInfoDataService userInfoDataService;
        private IUserAuthorizationDataService userAuthorizationDataService;
        public TokenAuthController(IUserInfoDataService userInfoDataService, IUserAuthorizationDataService userAuthorizationDataService) : base()
        {
            this.userInfoDataService = userInfoDataService;
            this.userAuthorizationDataService = userAuthorizationDataService;
        }
        // POST api/values
        [HttpPost]
        public string GetAuthToken()
        {
            var date = StreamToBytes(ControllerContext.HttpContext.Request.Body);
            User user = JsonConvert.DeserializeObject<User>(date);
            var userAuthors = userAuthorizationDataService.GetListAtCache();
            var existUser = userAuthors.FirstOrDefault(u => u.LoginID == user.Username && u.Password == user.Password);
            if (existUser != null)
            {
                var requestAt = DateTime.Now;
                TimeSpan expiresSpan = user.RememberUser ? TimeSpan.FromDays(180) : TokenAuthOption.ExpiresSpan;
                var expiresIn = requestAt + expiresSpan;
                var token = GenerateToken(existUser, expiresIn, user.SystemCode);
                return JsonConvert.SerializeObject(new UserTokenAuthResult
                {
                    stateCode = 1,
                    requertAt = requestAt,
                    expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                    accessToken = token,
                    ID = existUser.ID.ToString()
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new UserTokenAuthResult { stateCode = -1, errors = "Username or password is invalid" });
            }
        }

        public string StreamToBytes(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private string GenerateToken(UserAuthorization user, DateTime expires, SystemCode SystemCode)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.LoginID, "TokenAuth"),
                new[] {
                  new Claim("ID",user.ID.ToString()),
                  new Claim("SystemCode",((int)SystemCode).ToString())
                }
            );
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }
    }



    public class User
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberUser { get; set; }
        public string[] Powers { get; set; }
        public SystemCode SystemCode { get; set; }
    }



    public static class UserStorage
    {
        public static List<User> Users { get; set; } = new List<User> {
        new User {ID=Guid.NewGuid(),Username="user1",Password = "user1psd", Powers= new string[] { "admin/index","adminEdit"} },
        new User {ID=Guid.NewGuid(),Username="user2",Password = "user2psd" },
        new User {ID=Guid.NewGuid(),Username="user3",Password = "user3psd" }
    };
    }
}
