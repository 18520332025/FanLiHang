using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Mange.Auth
{
    /// <summary>
    /// COOKIES中的用户权限信息
    /// </summary>
    public class JWTAuth : IJWTAuth
    {
       public JWTAuth()
        {

        }

        private string token;

        /// <summary>
        /// 获取当前用户的token信息
        /// </summary>
        public string Token
        {
            set
            {
                if (token == null)
                    token = value;
            }
            get
            {
                return token;
            }

        }

        /// <summary>
        /// 访问认证服务器获取某用户的权限组信息
        /// </summary>
        /// <param name="token">token</param>
        private string[] GetServicePowers(string token)
        {
            HttpClient client = new HttpClient();
            string strPars = JsonConvert.SerializeObject(new { token = token });
            HttpContent httpContent = new StringContent(strPars);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpContent.Headers.Add("Authorization", "Basic " + token);
            string response = client.PostAsync("http://localhost:33071/api/GetPowers", httpContent).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<string[]>(response);
        }

        private string[] powers;
        /// <summary>
        /// 返回当前用户的权限列表信息
        /// </summary>
        public string[] Powers
        {
            get
            {
                if (powers == null)
                {
                    powers = GetServicePowers(token);
                }
                return powers;
            }
        }


        /// <summary>
        /// 检验当前用户是否存在某权限
        /// </summary>
        /// <param name="power">权限名称</param>
        /// <returns></returns>
        public bool CheckPower(string power)
        {
            return Powers.Contains(power);
        }
    }
}
