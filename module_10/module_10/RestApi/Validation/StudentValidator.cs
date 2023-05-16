using Domain;
using FluentValidation;

namespace RestApi.Validation
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(stud => stud.Name).Length(2, 100);
            RuleFor(stud => stud.Email).EmailAddress();
            RuleFor(stud => stud.Phone).Length(5, 15);
            RuleFor(stud => stud.Age).InclusiveBetween(15, 60);
        }
    }
}