using FluentValidation;

namespace NZWalks.API.Validators
{
    public class AddRegionRequestValidator : AbstractValidator<Models.DTO.AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            RuleFor(x => x.code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
