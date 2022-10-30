
using CustomTagHelper.Models;
using CustomTagHelper.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;

namespace CustomTagHelperTest
{
    public class CustomerDetailTaghelperUnitTest
    {
        private static string _expectedHtml = @"<dl class=""row"">
<dt class = ""col-sm-2"">Customer Id</dt>
<dd class = ""col-sm-10"">1</dd>
<dt class = ""col-sm-2"">Firstname</dt>
<dd class = ""col-sm-10"">Eric</dd>
<dt class = ""col-sm-2"">Lastname</dt>
<dd class = ""col-sm-10"">Johnson</dd>
<dt class = ""col-sm-2"">Username</dt>
<dd class = ""col-sm-10"">Eric.J</dd>
<dt class = ""col-sm-2"">Birthdate</dt>
<dd class = ""col-sm-10"">1970-01-01</dd>
<dt class = ""col-sm-2"">Email</dt>
<dd class = ""col-sm-10"">eric.johnson@test.com</dd>
</dl>
";

        [Test]
        public void test_can_render_customer_detail()
        {
            // given
            var model = new CustomerViewModel()
            {
                Id = 1,
                Firstname = "Eric",
                Lastname = "Johnson",
                Username = "Eric.J",
                BirthDate = DateTime.ParseExact("01/01/1970", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Email = "eric.johnson@test.com"
            };

            var context = new TagHelperContext (
                new TagHelperAttributeList() { 
                    new TagHelperAttribute("value")
                },
                new Dictionary<object, object>(), 
                Guid.NewGuid().ToString()
            );

            var output = new TagHelperOutput(
                "customer-info", 
                new TagHelperAttributeList(), (useCachedResult, htmlEncoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                }
            );

            // when
            var myTaghelper = new CustomerDetailTaghelper();
            myTaghelper.Value = model;
            myTaghelper.Process(context, output);

            // then 
            Assert.That(output.TagName, Is.EqualTo("div"));
            Assert.That(output.Content.GetContent(), Is.EqualTo(_expectedHtml));
        }
    }
}
