using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwageerTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>用户列表信息</returns>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new List<User>
            {
                new User { Phone = "18520332025", UserName = "范范" }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        ///<summary>
        ///上传用户信息
        ///</summary>
        /// <remarks>
        ///  Post /User
        ///  {
        ///     "UserName" :"用户名",
        ///     "Phone":"电话"
        ///  }      
        /// </remarks>
        [HttpPost]
        public void Post([FromBody]User user)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get; set;
        }
        /// <summary>
        /// 用户电话
        /// </summary>
        public string Phone
        {
            get; set;
        }
    }
}
