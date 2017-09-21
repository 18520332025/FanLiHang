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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tokenJWT.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {

        // POST api/values
        [HttpPost]
        public string GetAuthToken()
        {
            var date = StreamToBytes(ControllerContext.HttpContext.Request.Body); 
            User user = JsonConvert.DeserializeObject<User>(date);
            var existUser = UserStorage.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (existUser != null)
            {
                var requestAt = DateTime.Now;
                TimeSpan expiresSpan = existUser.RememberUser ? TimeSpan.FromDays(180) : TokenAuthOption.ExpiresSpan;
                var expiresIn = requestAt + expiresSpan;
                var token = GenerateToken(existUser, expiresIn);
                return JsonConvert.SerializeObject(new UserTokenAuthResult
                {
                    stateCode = 1,
                    requertAt = requestAt,
                    expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                    accessToken = token
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

        private string GenerateToken(User user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Username, "TokenAuth"),
                new[] {
                  new Claim("ID",user.ID.ToString())
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
