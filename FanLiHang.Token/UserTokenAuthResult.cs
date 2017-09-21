using System;

namespace FanLiHang.Auth
{
    public class UserTokenAuthResult
    {
        public int stateCode
        {
            get; set;
        }

        public bool success
        {
            get { return stateCode == 1; }
        }

        public DateTime requertAt
        {
            get; set;
        }

        public double expiresIn
        {
            get; set;
        }

        public string accessToken
        {
            get; set;
        }

        public string errors
        {
            get; set;
        }

        public string ID
        {
            get;set;
        }
    }
}
