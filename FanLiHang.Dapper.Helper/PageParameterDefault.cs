using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class PageParameterDefault
    {
        public void SetDefaultValue(PagerParameter pageParameter)
        {
            if (pageParameter.PageIndex == 0)
                pageParameter.PageIndex = 1;
            if (pageParameter.PageSize == 0)
                pageParameter.PageSize = 15;
            if (string.IsNullOrEmpty(pageParameter.OrderBy))
                pageParameter.OrderBy = "ID";
        }
    }
}
