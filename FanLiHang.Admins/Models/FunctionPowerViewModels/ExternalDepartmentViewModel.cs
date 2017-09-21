using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanLiHang.Model;
using System.ComponentModel.DataAnnotations.Schema;
using FanLiHang.Dapper.ModelData.Attribute;
using FanLiHang.Admins.Extensions;
namespace FanLiHang.Admins.Models.FunctionPowerViewModels
{
    public class ExternalDepartmentViewModel : Department
    {
        public string Text
        {
            get
            {
                return Name;
            }
        }

        public FunctionPowerType functionPowerType
        {
            get { return FunctionPowerType.Department; }
        }


        public IEnumerable<ExternalRoleViewModel> nodes
        {
            get
            {
                return Roles.MapperList<ExternalRoleViewModel, Role>();
            }
        }
    }
}
