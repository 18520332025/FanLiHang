using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System;

namespace Fanlihang.TagHelpers
{
    public abstract class BaseDropDownListTagHelper<T> : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        Func<T, int> getVal;
        Func<T, string> getText;
        public BaseDropDownListTagHelper(Func<T, int> getVal, Func<T, string> getText)
        {
            this.getVal = getVal;
            this.getText = getText;
        }

        public abstract IEnumerable<T> GetData();

        public int Selected { get; set; } = 0;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var dataList = GetData();
            output.TagName = "select";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var i in dataList)
            {
                string text = getText(i);
                int val = getVal(i);
                sb.AppendLine("<option " + (val == Selected ? "selected='selected'" : "") + " value='" + val + "'>");
                sb.AppendLine(text);
                sb.AppendLine("</option>");
            }
            output.Content.SetHtmlContent(sb.ToString());

        }
    }
}
