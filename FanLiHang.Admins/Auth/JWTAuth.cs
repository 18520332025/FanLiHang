using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

namespace FanLiHang.Admins.Auth
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
        private int id;
        public int ID
        {
            set
            {
                if (id == 0)
                {
                    id = value;
                }
            }
            get
            {
                return id;
            }
        }

        /// <summary>
        /// 访问认证服务器获取某用户的权限组信息
        /// </summary>
        /// <param name="token">token</param>
        private string[] GetServicePowers(string token)
        {
            HttpClient client = new HttpClient();
            //HttpWebRequest request = new HttpWebRequest();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string response = client.GetAsync("http://localhost:33071/api/GetPowers").Result.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<AuthReponse<string[]>>(response);
            return obj.data;
        }

        public class AuthReponse<T>
        {
            public bool scuess { get; set; }
            public int stateCode { get; set; }
            public string[] data { get; set; }
            public string errors { get; set; }
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
