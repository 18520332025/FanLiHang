using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanLiHang.Admins.Models.FunctionPowerViewModels
{
    public class AuthorizationQueryModel
    {
        public int AppInfoID
        {
            get; set;
        }

        public int NodeID
        {
            get; set;
        }

        public FunctionPowerType? Type
        {
            get; set;
        }
    }

    public enum FunctionPowerType
    {
        Department = 0,
        Role = 1
    }
}
