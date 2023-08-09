using FluentValidation.TestHelper;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class AddCustomerCommandValidatorTests
{

    private readonly Mock<ILibraryDbContext> _mockDbContext;
    private readonly AddCustomerCommandValidator _validator;

    public AddCustomerCommandValidatorTests()
    {
        _mockDbContext = new Mock<ILibraryDbContext>();
        _validator = new AddCustomerCommandValidator(_mockDbContext.Object);
        MockCustomerdata();
    }


    [Fact]
    public async Task CustomerName_NullOrEmpty_ShouldFailValidation()
    {
        // Arrange
        var command = new AddCustomerCommand
        {
            CustomerName = "",
            CustomerPhoneNumber = "1234567890"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerName).WithErrorMessage("Customer Name is required");
    }


    [Fact]
    public async Task CustomerName_ShouldContainOnlyCharacters()
    {
        // Arrange
        var command = new AddCustomerCommand { CustomerName = "12312" };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName)
              .WithErrorMessage("Customer Name must contain characters only");
    }


    [Fact]
    public async Task CustomerPhoneNumber_NullOrEmpty_ShouldFailValidation()
    {
        // Arrange
        var command = new AddCustomerCommand
        {
            CustomerName = "john",
            CustomerPhoneNumber = ""
        };

        // Act
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerPhoneNumber).WithErrorMessage("Customer Phone Number is required");
    }

    [Fact]
    public async Task CustomerPhoneNumber_ShouldContainOnly10Digits()
    {
        // Arrange
        var command = new AddCustomerCommand
        {
            CustomerName = "john",
            CustomerPhoneNumber = "8989728747832546"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerPhoneNumber)
              .WithErrorMessage("Customer Phone Number should be of length 10");
    }

    [Fact]
    public async Task AddCustomerCommandValidator_DuplicateCustomerRecord_ShouldFailValidation()
    {
        var command = new AddCustomerCommand
        {
            CustomerName = "Jain",
            CustomerPhoneNumber = "9947003224"
        };

        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x).WithErrorMessage("The customer name with same phonenumber already exists");

    }

    #region DatabaseInitilization
    /// <summary>
    /// Initializes Mock database with mocked object
    /// </summary>
    private void MockCustomerdata()
    {
        _mockDbContext.Setup(x => x.Customers).Returns(new List<Customer>{new Customer()
            {
               Id = 1,
               Name = "Jain",
               PhoneNumber = "9947003224",
               Books = new List<Book>
               {
                   new Book { Id = 1, Name = "Book 1" },
                   new Book { Id = 2, Name = "Book 2" }
               }
            }
        }.AsQueryable().BuildMockDbSet().Object);
    }
    #endregion
}
