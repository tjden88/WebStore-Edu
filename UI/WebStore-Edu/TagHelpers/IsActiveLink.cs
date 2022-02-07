using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore_Edu.TagHelpers;

[HtmlTargetElement(Attributes = AttributeName)]
public class IsActiveLink : TagHelper
{
    private const string AttributeName = "is-active-link";

    [HtmlAttributeName("asp-controller")]
    public string? Controller { get; set; }


    [HtmlAttributeName("asp-action")]
    public string? Action { get; set; }

    [ViewContext, HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll(AttributeName);

        var route = ViewContext?.RouteData.Values;

        var controllerRoute = route?["controller"];
        var actionRoute = route?["action"];

        if (Action is { } action && Equals(action, actionRoute)
                                 && Controller is { } controller && Equals(controller, controllerRoute))
        {
            var classAttribute = output.Attributes.FirstOrDefault(a => a.Name == "class");


            if (classAttribute == null)
            {
                output.Attributes.Add("class", "active");
            }
            else
            {
                output.Attributes.SetAttribute("class", 
                    $"{classAttribute.Value.ToString()?.Replace("active", "")} active");
            }
        }

    }
}