using CustomTagHelper.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace CustomTagHelper.TagHelpers
{
    [HtmlTargetElement("customer-info")]
    public class CustomerDetailTaghelper : TagHelper
    {
        public CustomerViewModel Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            var html = @"<dl class=""row"">";
            sb.AppendLine(html);
            // id
            html = string.Format(@"<dt class = ""col-sm-2"">{0}</dt>", "Customer Id");
            sb.AppendLine(html);
            html = string.Format(@"<dd class = ""col-sm-10"">{0}</dd>", this.Value.Id);
            sb.AppendLine(html);
            // Firstname
            html = string.Format(@"<dt class = ""col-sm-2"">{0}</dt>", "Firstname");
            sb.AppendLine(html);
            html = string.Format(@"<dd class = ""col-sm-10"">{0}</dd>", this.Value.Firstname);
            sb.AppendLine(html);
            // lastname
            html = string.Format(@"<dt class = ""col-sm-2"">{0}</dt>", "Lastname");
            sb.AppendLine(html);
            html = string.Format(@"<dd class = ""col-sm-10"">{0}</dd>", this.Value.Lastname);
            sb.AppendLine(html);
            // username
            html = string.Format(@"<dt class = ""col-sm-2"">{0}</dt>", "Username");
            sb.AppendLine(html);
            html = string.Format(@"<dd class = ""col-sm-10"">{0}</dd>", this.Value.Username);
            sb.AppendLine(html);
            // birthdate
            html = string.Format(@"<dt class = ""col-sm-2"">{0}</dt>", "Birthdate");
            sb.AppendLine(html);
            html = string.Format(@"<dd class = ""col-sm-10"">{0}</dd>", this.Value.BirthDate.HasValue ? this.Value.BirthDate.Value.ToString("yyyy-MM-dd") : "");
            sb.AppendLine(html);
            // email
            html = string.Format(@"<dt class = ""col-sm-2"">{0}</dt>", "Email");
            sb.AppendLine(html);
            html = string.Format(@"<dd class = ""col-sm-10"">{0}</dd>", this.Value.Email);
            sb.AppendLine(html);

            sb.AppendLine(@"</dl>");

            output.Content.SetHtmlContent(sb.ToString());

        }
    }
}
