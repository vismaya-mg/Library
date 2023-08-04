using FluentValidation;
using LibraryManagement.Constants;
using LibraryManagement.Persistence.Context;

namespace LibraryManagement.Requests.Commands;
/// <summary>
/// Validator for buy book
/// </summary>
public class BuyBookCommandValidator : AbstractValidator<BuyBookCommand>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Validator for defining specific rules for properties
    /// </summary>
    public BuyBookCommandValidator(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage(ValidationMessage.Required);
        RuleForEach(x => x.BookIds).NotEmpty().NotNull().WithMessage(ValidationMessage.Required)
               .Must(IsPresent).WithMessage(ValidationMessage.BookNotAvailable)
               .Must(InCorrectID).WithMessage(ValidationMessage.IncorrectBookId);
    }

    /// <summary>
    /// Method to find whether the book is avaible or not
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool IsPresent(long id)
    {
        return !_libraryDbContext.Books.Any(x => x.Id == id && !x.IsAvailable);
    }

    /// <summary>
    /// Method to check the id is present or not in database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool InCorrectID(long id)
    {
        return _libraryDbContext.Books.Any(x => x.Id == id);
    }

}

