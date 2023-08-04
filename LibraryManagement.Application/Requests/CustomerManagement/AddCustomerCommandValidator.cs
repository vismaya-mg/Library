using FluentValidation;
using LibraryManagement.Constants;
using LibraryManagement.Persistence.Context;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Validator for Add Customer Command
/// </summary>
public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Validator for defining specific rules for properties
    /// </summary>
    public AddCustomerCommandValidator(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
        //Rules for required fields
        RuleFor(x => x.CustomerName).NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(RegexConstants.CharactersOnlyRegex).WithMessage(ValidationMessage.CharactersOnly);

        RuleFor(x => x.CustomerPhoneNumber).NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
            .Length(10).WithMessage(ValidationMessage.Length)
            .Matches(RegexConstants.DigitsOnlyRegex).WithMessage(ValidationMessage.DigitsOnly);

        RuleFor(x => x).Must(IsUnique).WithMessage(ValidationMessage.CustomerAlreadyExists);
    }

    /// <summary>
    /// Method to check wheter the duplicate record exist
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    private bool IsUnique(AddCustomerCommand command)
    {
        return !_libraryDbContext.Customers.Any(x => x.Name.ToLower() == command.CustomerName.ToLower() && x.PhoneNumber == command.CustomerPhoneNumber);
    }
}