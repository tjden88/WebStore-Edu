using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore_Edu.TagHelpers;

[HtmlTargetElement(Attributes = AttributeName)]
public class IsActiveLink : TagHelper
{
    private const string AttributeName = "is-active-link";
    private const string IgnoreAction = "ignore-action";

    [ViewContext, HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll(AttributeName);

        var ignoreAction = output.Attributes.RemoveAll(IgnoreAction);

        var route = ViewContext?.RouteData.Values;

        var controllerRoute = route?["controller"];
        var actionRoute = route?["action"];

        bool needMakeActive = !(!ignoreAction
                                && context.AllAttributes["asp-action"]?.Value is { } action
                                && !Equals(action, actionRoute));

        if (context.AllAttributes["asp-controller"]?.Value is { } controller
            && !Equals(controller, controllerRoute))
            needMakeActive = false;



        if (needMakeActive)
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