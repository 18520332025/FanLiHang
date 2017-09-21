using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mange.Auth
{
    public interface IJWTAuth
    {
        /// <summary>
        /// 获取当前用户的token信息
        /// </summary>
        string Token
        {
            get;
            set;
        }

        /// <summary>
        /// 返回当前用户的权限列表信息
        /// </summary>

        string[] Powers { get; }


        /// <summary>
        /// 检验当前用户是否存在某权限
        /// </summary>
        /// <param name="power">权限名称</param>
        /// <returns></returns>
        bool CheckPower(string power);
    }
}
