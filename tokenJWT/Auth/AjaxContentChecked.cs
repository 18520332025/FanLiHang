using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tokenJWT.Auth
{

    public static class AjaxContentChecked
    {
        public static string[] ajaxContentType = { "application/json"};
        public static bool Checked(string contentType)
        {
            return ajaxContentType.Contains(contentType);
        }
    }
}
