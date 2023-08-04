using FluentValidation;
using LibraryManagement.Constants;
using LibraryManagement.Persistence.Context;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Validator for lend a book
/// </summary>
public class LendBookCommandValidator : AbstractValidator<LendBookCommand>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Validator for defining specific rules for properties
    /// </summary>
    public LendBookCommandValidator(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
        RuleForEach(x => x.BookIds).Must(IsBookAvailable).WithMessage(ValidationMessage.BookNotAvailable)
            .Must(NotFound).WithMessage(ValidationMessage.NotFound)
            .NotEmpty().NotNull().WithMessage(ValidationMessage.Required);
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage(ValidationMessage.Required);
    }

    /// <summary>
    /// Method to find whether the book is avaible or not
    /// </summary>
    /// <param name="BookId"></param>
    /// <returns></returns>
    private bool IsBookAvailable(long BookId)
    {
        return !_libraryDbContext.Books.Any(x => x.Id == BookId && !x.IsAvailable);
    }

    /// <summary>
    /// Method to check whether the id is present in database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool NotFound(long id)
    {
        return _libraryDbContext.Books.Any(x => x.Id == id);
    }
}
