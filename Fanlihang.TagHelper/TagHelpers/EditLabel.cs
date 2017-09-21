using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fanlihang.TagHelpers
{
    public class EditLabel : TagHelper
    {
        public int ID { get; set; } = 0;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "small";
            output.Attributes.Add("class", "label label-primary EditLabel");
            output.Attributes.Add("data-bind", ID);
            output.Attributes.Add("action-name", ActionName);
            output.Attributes.Add("model-height", ModelHeight);
            output.Content.SetHtmlContent("<i class=\"fa fa-edit\"></i> " + Text + "</small>");
        }
        public string Text { get; set; } = "编辑";
        public int ModelHeight { get; set; } = 450;

        public string ActionName { get; set; } = "./Edit";
    }
}