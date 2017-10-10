
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FanLiHang.ValidationExpand;

namespace Fanlihang.TagHelpers.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-validation-remote")]
    public class RemoteTagHelper : TagHelper
    {
        public RemoteTagHelper()
        {

        }
        [HtmlAttributeName("asp-validation-remote")]
        public object Model
        {
            get;
            set;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }
        public override int Order
        {
            get
            {
                return 100;
            }
        }

        /// <summary>
        /// 赋予属性
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var list = For.Metadata.ValidatorMetadata;
            foreach (var item in list)
            {
                if (item is RemoteAttribute)
                {
                    string data = "";
                    var attr = item as RemoteAttribute;
                    foreach (var key in attr.DataKey)
                    {
                        data += key + ",";
                    }
                    data = data.TrimEnd(',');

                    output.Attributes.Add("data-val", "true");
                    output.Attributes.Add("data-val-required", For.Name + "必须输入或者选择");
                    output.Attributes.Add("name", For.Name);
                    if (output.TagName == "input")
                    {
                        output.Attributes.Add("value", For.Model);
                        output.Attributes.Add("type", "text");
                    }
                    output.Attributes.Add("data-val-remote-type", attr.Type);
                    output.Attributes.Add("data-val-remote-additionalfields", data);
                    output.Attributes.Add("data-val-remote-url", attr.Url);
                    output.Attributes.Add("data-val-remote", attr.ErrorMessage);
                    output.Attributes.Add("aria-required", "true");
                    //output.Attributes.Add("aria-describedby", attr.RemoteName);
                    output.Attributes.Add("data-val-remote-group", attr.RemoteName);
                    output.Attributes.SetAttribute("class", output.Attributes["class"].Value + " valid");
                    //output.Attributes.Add("data-val-")
                }
            }
        }
    }
}
