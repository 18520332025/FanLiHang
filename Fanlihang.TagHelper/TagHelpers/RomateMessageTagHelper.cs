using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FanLiHang.ValidationExpand;

namespace Fanlihang.TagHelpers.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-validation-romate-message")]
    public class RomateMessageTagHelper : TagHelper
    {

        [HtmlAttributeName("asp-validation-for")]
        public ModelExpression For
        {
            get;
            set;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For != null)
            {
                RemoteAttribute remoteAttribute = For.Metadata.ValidatorMetadata.Where(x => x is RemoteAttribute).FirstOrDefault() as RemoteAttribute;
                if (remoteAttribute != null)
                {
                    if (output.Attributes.ContainsName("data-valmsg-for-group"))
                    {
                        output.Attributes.SetAttribute("data-valmsg-for-group", remoteAttribute.RemoteName);
                    }
                    else
                    {
                        output.Attributes.Add("data-valmsg-for-group", remoteAttribute.RemoteName);
                    }
                }
                base.Process(context, output);
            }
        }
    }
}
