using FluentValidation;
using FluentValidation.Validators;

namespace Operations.Bet;

public class MakeBetInputValidator : AbstractValidator<MakeBetInput>
{
    public MakeBetInputValidator()
    {
        RuleFor(e => e.PlayerId).NotNull();
        
        RuleFor(e => e.StakeAmount).NotNull();
        RuleFor(e => e.StakeAmount).SetValidator(new ScalePrecisionValidator<MakeBetInput>(2, int.MaxValue));
        RuleFor(e => e.StakeAmount).GreaterThanOrEqualTo(0.3m);
        RuleFor(e => e.StakeAmount).LessThanOrEqualTo(10000);
        
        RuleFor(e => e.Selections).NotNull();
        RuleForEach(e => e.Selections).ChildRules(e =>
        {
            e.RuleFor(e => e.Odds).SetValidator(new ScalePrecisionValidator<MakeBetInput.Selection>(3, int.MaxValue));
            e.RuleFor(e => e.Odds).GreaterThanOrEqualTo(1);
            e.RuleFor(e => e.Odds).LessThanOrEqualTo(10000);
        });
    }
}