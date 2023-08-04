using FluentValidation;
using LibraryManagement.Constants;
using LibraryManagement.Persistence.Context;

namespace LibraryManagement.Requests.BookManagement;

/// <summary>
/// Validator for AddBookCommand
/// </summary>
public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Validator for defining specific validation rules for properties
    /// </summary>
    public AddBookCommandValidator(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;

        RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x => x.Category).NotNull().WithMessage(ValidationMessage.Required);

        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(ValidationMessage.Required)
            .Matches(RegexConstants.CharactersOnlyRegex).WithMessage(ValidationMessage.CharactersOnly);

        RuleFor(x => x.Author).NotEmpty().NotNull().WithMessage(ValidationMessage.Required)
            .Matches(RegexConstants.CharactersOnlyRegex).WithMessage(ValidationMessage.CharactersOnly);
        RuleFor(x => x).Must(IsUnique).WithMessage(ValidationMessage.BookAlreadyExists);
    }

    /// <summary>
    /// Method to check wheter the duplicate record exist
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    private bool IsUnique(AddBookCommand command)
    {
        return !_libraryDbContext.Books.Any(x => x.Name.ToLower() == command.Name.ToLower().Trim() && x.Author.ToLower() == command.Author.ToLower().Trim());
    }
}
