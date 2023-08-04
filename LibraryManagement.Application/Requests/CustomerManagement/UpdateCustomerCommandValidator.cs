using FluentValidation;
using LibraryManagement.Constants;
using LibraryManagement.Persistence.Context;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Update customer validator
/// </summary>
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Validator for defining specific rules for properties
    /// </summary>
    public UpdateCustomerCommandValidator(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;

        //Rules for required fields
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage(ValidationMessage.Required)
                          .Must(NotFound).WithMessage(ValidationMessage.NotFound);
        RuleFor(x => x.CustomerName).NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x => x).Must(IsUnique).WithMessage(ValidationMessage.CustomerAlreadyExists);
    }

    /// <summary>
    /// Method to check whether the id is present in database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool NotFound(long id)
    {
        return _libraryDbContext.Customers.Any(x => x.Id == id);
    }

    /// <summary>
    /// Method to check wheter the duplicate record exist
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>

    private bool IsUnique(UpdateCustomerCommand command)
    {
        return !_libraryDbContext.Customers.Any(x => x.Name.ToLower() == command.CustomerName.ToLower() && x.PhoneNumber == command.PhoneNumber && x.Id != command.Id);
    }
}