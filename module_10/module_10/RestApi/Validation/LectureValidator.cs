using Domain;
using FluentValidation;
using System;

namespace RestApi.Validation
{
    public class LectureValidator : AbstractValidator<Lecture>
    {
        public LectureValidator()
        {
            RuleFor(lect => lect.Name).Length(2, 100);
            RuleFor(lect => lect.Date)
            .Must(BeAValidDate).WithMessage("Date is required");
            RuleFor(lect => lect.ProfessorName).Length(2, 100);
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}