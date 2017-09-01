using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace PernicekWeb.Views.TagHelpers
{
    public class link_TagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag
        }
    }
}