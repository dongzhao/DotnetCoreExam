using CustomTagHelper.Models;
using FluentValidation;

namespace CustomTagHelper.Validator
{
    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(x => x.Firstname).MaximumLength(20);
            RuleFor(x => x.Lastname).MaximumLength(20);
            RuleFor(x => x.Username).NotEmpty().Length(1, 12);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.BirthDate)
                .GreaterThan(x => DateTime.Now.AddYears(-150))
                .LessThan(x => DateTime.Now);
        }
    }
}
