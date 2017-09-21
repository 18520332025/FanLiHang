using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fanlihang.TagHelpers
{
    public class DeleteLabel : TagHelper
    {

        public int ID { get; set; } = 0;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "small";
            output.Attributes.Add("class", "label label-danger DeleteLabel");
            output.Attributes.Add("data-bind", ID);
            output.Attributes.Add("action-name", ActionName);
            output.Content.SetHtmlContent("<i class=\"fa fa-times-circle\"></i> 删除</small>");
        }

        public string ActionName { get; set; } = "./DeleteItem";
    }
}
