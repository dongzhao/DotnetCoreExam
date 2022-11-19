using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomTagHelper.TagHelpers
{
    [HtmlTargetElement("ajax-button")]
    public class AjaxButtonTagHelper : TagHelper
    {
        [HtmlAttributeName("ajax-controller")]
        public string ControllerName { get;set; } = string.Empty;
        [HtmlAttributeName("ajax-method")]
        public string MethodName { get; set; } = string.Empty;
        [HtmlAttributeName("ajax-action")]
        public string ActionName { get; set; } = string.Empty;
        [HtmlAttributeName("ajax-target-id")]
        public string TargetId { get;set;}  = string.Empty;
        [HtmlAttributeName("ajax-call-success")]
        public string SuccessFunction { get; set; } = string.Empty;
        [HtmlAttributeName("ajax-call-failure")]
        public string FailureFunction { get; set; } = string.Empty;
        [HtmlAttributeName("class")]
        public string ClassProperty { get; set; } = string.Empty;
        [HtmlAttributeName("style")]
        public string StyleProperty { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;

            var hasAttr = context.AllAttributes.TryGetAttribute("class", out TagHelperAttribute classAttr);
            if (hasAttr)
            {
                output.Attributes.SetAttribute("class", "my-ajax " + classAttr.Value.ToString());
            }
            else
            {
                output.Attributes.SetAttribute("class", "my-ajax");
            }

            hasAttr = context.AllAttributes.TryGetAttribute("style", out TagHelperAttribute styleAttr);
            if (hasAttr)
            {
                output.Attributes.SetAttribute("style", styleAttr.Value.ToString());
            }

            hasAttr = context.AllAttributes.TryGetAttribute("ajax-method", out TagHelperAttribute methodAttr);
            if (hasAttr)
            {
                output.Attributes.SetAttribute("data-method", styleAttr.Value.ToString());
            }

            hasAttr = context.AllAttributes.TryGetAttribute("ajax-controller", out TagHelperAttribute controllerAttr); 
            if (!hasAttr)
            {
                throw new Exception("attribute ajax-controller should not be empty.");
            }
            hasAttr = context.AllAttributes.TryGetAttribute("ajax-action", out TagHelperAttribute actionAttr);
            if (!hasAttr)
            {
                throw new Exception("attribute ajax-action should not be empty.");
            }
            var ajaxUrl = @"\" + controllerAttr.Value.ToString() + @"\" + actionAttr.Value.ToString();
            output.Attributes.SetAttribute("data-url", ajaxUrl);

            hasAttr = context.AllAttributes.TryGetAttribute("ajax-target-id", out TagHelperAttribute targetIdAttr);
            if (hasAttr)
            {
                output.Attributes.SetAttribute("data-target-id", targetIdAttr.Value.ToString());
            }
            hasAttr = context.AllAttributes.TryGetAttribute("ajax-call-success", out TagHelperAttribute successCallAttr);
            if (hasAttr)
            {
                output.Attributes.SetAttribute("data-call-success", successCallAttr.Value.ToString());
            }
            hasAttr = context.AllAttributes.TryGetAttribute("ajax-call-failure", out TagHelperAttribute failureCallAttr);
            if (hasAttr)
            {
                output.Attributes.SetAttribute("data-call-failure", failureCallAttr.Value.ToString());
            }
        }
    }
}
