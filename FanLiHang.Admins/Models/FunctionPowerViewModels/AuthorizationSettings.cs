using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanLiHang.Admins.Models.FunctionPowerViewModels
{
    public class AuthorizationSetting : AuthorizationQueryModel
    {
        public int[] FunctionPowerIDList
        {
            get; set;
        }
    }
}
