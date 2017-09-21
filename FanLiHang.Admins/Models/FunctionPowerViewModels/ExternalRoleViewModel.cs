using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanLiHang.Model;
namespace FanLiHang.Admins.Models.FunctionPowerViewModels
{
    public class ExternalRoleViewModel : Role
    {
        public string text
        {
            get
            {
                return RoleName;
            }
        }


        public FunctionPowerType functionPowerType
        {
            get
            {
                return FunctionPowerType.Role;
            }
        }
    }
}
