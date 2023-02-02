
using FluentValidation;


namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookByIdCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);

        }
    }
}