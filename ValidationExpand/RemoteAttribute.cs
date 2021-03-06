﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FanLiHang.ValidationExpand
{
    public class RemoteAttribute : ValidationAttribute
    {
        public string Url
        {
            get;
            set;
        }

        public string[] DataKey
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string RemoteName
        {
            get; set;
        }


        public override bool IsValid(object value)
        {
            
            if (value is int intValue)
                if (intValue == 0)
                    return false;
            return true;
        }
    }
}
