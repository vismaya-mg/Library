namespace LibraryManagement.Constants;

/// <summary>
/// Constatnts for validation messages
/// </summary>
public class ValidationMessage
{
    /// <summary>
    /// Message indicating that a certain property is required for validation.
    /// </summary>
    public const string Required = "{PropertyName} is required";

    /// <summary>
    /// Message indicating that a property exceeds the maximum length allowed.
    /// </summary>
    public const string MaxLength = "{PropertyName} exceeds the maximum length";

    /// <summary>
    /// Message indicating that a property must contain characters only (no digits or special characters).
    /// </summary>
    public const string CharactersOnly = "{PropertyName} must contain characters only";

    /// <summary>
    /// Message indicating that a property should be of a specific length (in this case, 10 characters).
    /// </summary>
    public const string Length = "{PropertyName} should be of length 10";

    /// <summary>
    /// Message indicating that a property count must be greater than zero.
    /// </summary>
    public const string GreaterThanZero = "{PropertyName} count must be greater than zero";

    /// <summary>
    /// Message indicating that a property must contain digits only (no characters or special characters).
    /// </summary>
    public const string DigitsOnly = "{PropertyName} must contain digits only";

    /// <summary>
    /// Message indicating that a book with a specific ID is not available.
    /// </summary>
    public const string BookNotAvailable = "The book with id {PropertyValue} not available";

    /// <summary>
    /// Message indicating that the entered customer ID is incorrect.
    /// </summary>
    public const string IncorrectCustomerId = "Please enter the correct customer id";

    /// <summary>
    /// Message indicating that a book with the same name and author already exists.
    /// </summary>
    public const string BookAlreadyExists = "The book name with same author already exists";

    /// <summary>
    /// Message indicating that a customer with the same phone number already exists.
    /// </summary>
    public const string CustomerAlreadyExists = "The customer name with same phonenumber already exists";

    /// <summary>
    /// Message indicating that the entered ID is not found.
    /// </summary>
    public const string NotFound = "Please enter the correct id";

    /// <summary>
    /// Message indicating that the user has already taken a specific book.
    /// </summary>
    public const string AlreadyTaken = "You have already taken the book {PropertyValue}";

    /// <summary>
    /// Message indicating that the entered book ID is incorrect.
    /// </summary>
    public const string IncorrectBookId = "Please enter the correct book id";

    /// <summary>
    /// Exception message when the book with this category is not present.
    /// </summary>
    public const string CategoryNotFoundMessage = "The book with this category is not present";
}

/// <summary>
/// Constants for due days 
/// </summary>
public class DueDaysConstants
{
    /// <summary>
    /// The number of days considered for moderate fines.
    /// </summary>
    public const int ModerateFineDays = 10;

    /// <summary>
    /// The number of days considered for high fines.
    /// </summary>
    public const int HighFineDays = 20;

    /// <summary>
    /// The maximum number of days after which fines reach their maximum value.
    /// </summary>
    public const int MaximumFineDays = 30;
}

/// <summary>
/// Constants for regular expressions
/// </summary>
public class RegexConstants
{
    /// <summary>
    /// A regular expression pattern to match strings containing only alphabetical characters (uppercase or lowercase).
    /// </summary>
    public const string CharactersOnlyRegex = "[A-Za-z]";

    /// <summary>
    /// A regular expression pattern to match strings containing only digits (0-9).
    /// </summary>
    public const string DigitsOnlyRegex = "^[0-9]+$";
}

/// <summary>
/// Class to hold fine amount constants based on the number of days overdue.
/// </summary>
public class FineAmountConstants
{
    /// <summary>
    /// The fine amount for days greater than MaximumFineDays.
    /// </summary>
    public const int MaximumFineAmount = 500;

    /// <summary>
    /// The fine amount for days greater than HighFineDays but less than or equal to MaximumFineDays.
    /// </summary>
    public const int HighFineAmount = 200;

    /// <summary>
    /// The fine amount for days greater than ModerateFineDays but less than or equal to HighFineDays.
    /// </summary>
    public const int ModerateFineAmount = 100;

    /// <summary>
    /// The default fine amount for days less than or equal to ModerateFineDays.
    /// </summary>
    public const int DefaultFineAmount = 0;
}
