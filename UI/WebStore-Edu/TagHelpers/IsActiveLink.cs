using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore_Edu.TagHelpers;

[HtmlTargetElement(Attributes = AttributeName)]
public class IsActiveLink : TagHelper
{
    private const string AttributeName = "is-active-link";


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll(AttributeName);
    }
}