using FluentValidation;
using LibraryManagement.Constants;
using LibraryManagement.Persistence.Context;

namespace LibraryManagement.Requests.BookManagement;

/// <summary>
/// Validator for update a book
/// </summary>
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{

    private readonly ILibraryDbContext _librarydbcontext;
    /// <summary>
    /// Validator for defining specific validation rules for properties
    /// </summary>
    public UpdateBookCommandValidator(ILibraryDbContext libraryDbContext)
    {
        _librarydbcontext = libraryDbContext;

        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(ValidationMessage.Required)
                .Matches("[A-Za-z]").WithMessage(ValidationMessage.CharactersOnly);

        RuleFor(x => x.Author).NotEmpty().NotNull().WithMessage(ValidationMessage.Required)
            .Matches("[A-Za-z]").WithMessage(ValidationMessage.CharactersOnly);

        RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x => x.Category).NotNull().WithMessage(ValidationMessage.Required);
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage(ValidationMessage.Required);
        RuleFor(x => x.Id).Must(NotFound).WithMessage(ValidationMessage.NotFound);
    }

    /// <summary>
    /// Method to check whether the id is present in database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool NotFound(long id)
    {
        return _librarydbcontext.Books.Any(x => x.Id == id);
    }

}
