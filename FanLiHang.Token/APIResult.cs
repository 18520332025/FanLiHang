using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Auth
{
    public class APIResult<T>
    {
        public T data { get; set; }
        public bool scuess { get; set; }
        public int stateCode { get; set; }
        public string errors { get; set; }

        public APIResult(T data)
        {
            this.data = data;
            scuess = true;
            stateCode = 200;
            errors = null;
        }

        public APIResult(string errors)
        {
            this.data = default(T);
            this.errors = errors;
            this.scuess = false;
            this.stateCode = 500;
        }

        public APIResult()
        {
            this.data = default(T);
            this.errors = "认证失败";
            this.scuess = false;
            this.stateCode = 401;
        }

    }
}
