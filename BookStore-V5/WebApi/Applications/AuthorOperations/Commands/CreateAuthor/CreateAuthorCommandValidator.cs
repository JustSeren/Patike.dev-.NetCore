using System;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model).NotNull().DependentRules(() =>
            {
                Transform(command => command.Model.Name, v => v?.Trim()).NotNull().NotEmpty().MinimumLength(2);
                Transform(command => command.Model.Surname, v => v?.Trim()).NotNull().NotEmpty().MinimumLength(2);
                RuleFor(command => command.Model.AouthorBD).NotEqual(new DateTime()).LessThan(DateTime.Now.Date);
            });
        }
    }
}