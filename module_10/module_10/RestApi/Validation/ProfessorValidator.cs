using Domain;
using FluentValidation;

namespace RestApi.Validation
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        public ProfessorValidator()
        {
            RuleFor(prof => prof.Name).Length(2, 100);
            RuleFor(prof => prof.Email).EmailAddress();
        }
    }
}