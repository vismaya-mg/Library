using FluentValidation;
using LibraryManagement.Constants;
using LibraryManagement.Persistence.Context;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Vlaidator for return the book
/// </summary>
public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
{
    private readonly ILibraryDbContext _libraryDbContext;
    /// <summary>
    /// Validator for defining specific rules for properties
    /// </summary>
    public ReturnBookCommandValidator(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
        RuleFor(x => x).Must(IsSameCustomer).WithMessage(ValidationMessage.IncorrectCustomerId);
        RuleFor(x => x.CustomerId).NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleForEach(x => x.BookId).NotNull().NotEmpty().WithMessage(ValidationMessage.Required);

    }

    /// <summary>
    /// Checks if all the books returned by the customer were originally lent to the same customer.
    /// </summary>
    /// <param name="command">The ReturnBookCommand containing the book IDs and the customer ID.</param>
    /// <returns>True if all the returned books were originally lent to the same customer; otherwise, false.</returns>
    private bool IsSameCustomer(ReturnBookCommand command)
    {
        return !_libraryDbContext.Books.Any(x => x.CustomerId != command.CustomerId && command.BookId.Contains(x.Id));
    }
}
