using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.AuthorName).MinimumLength(2).When(x => x.Model.AuthorName.Trim() != string.Empty);
        }
    }
}