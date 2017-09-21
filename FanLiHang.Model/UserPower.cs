using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Model
{
    [TableName("UserPower")]
    public class UserPower
    {
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }

        public int UserAuthorizationID
        {
            get;
            set;
        }

        public UserAuthorization UserAuthorization
        {
            get;
            set;
        }

        public int RoleID
        {
            get;
            set;
        }

        public List<Role> Roles
        {
            get;
            set;
        }

    }
}
