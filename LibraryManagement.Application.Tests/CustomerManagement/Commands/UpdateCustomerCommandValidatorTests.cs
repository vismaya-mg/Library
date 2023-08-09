using FluentValidation.TestHelper;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class UpdateCustomerCommandValidatorTests
{
    
    private readonly Mock<ILibraryDbContext> _mockLibraryDbContext;
    private readonly UpdateCustomerCommandValidator _validator;


    public UpdateCustomerCommandValidatorTests()
    {
        _mockLibraryDbContext = new Mock<ILibraryDbContext>();
        _validator = new UpdateCustomerCommandValidator(_mockLibraryDbContext.Object);

        MockCustomerData();
    }

    [Fact]
    public async Task CustomerId_NullOrEmpty_ShouldFailValidation()
    {
        var command = new UpdateCustomerCommand
        {
            CustomerName = "Test",
            PhoneNumber = "9947003223"
        };

        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task UpdateCustomerCommandValidator_ShouldReturnHaveErrorMessage_InCorrectId()
    {
        var command = new UpdateCustomerCommand
        {
            Id = 3,
            CustomerName = "Test",
            PhoneNumber = "9947003223"
        };

        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Please enter the correct id");
    }

    [Fact]
    public async Task CustomerName_NullOrEmpty_ShouldFailValidation()
    {
        // Arrange
        var command = new UpdateCustomerCommand
        {
            Id = 2,
            CustomerName = "",
            PhoneNumber = "9947003224"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerName).WithErrorMessage("Customer Name is required");
    }
    [Fact]
    public async Task UpdateCustomerCommandValidator_DuplicateCustomerRecord_ShouldFailValidation()
    {
        var command = new UpdateCustomerCommand
        {
            Id = 1,
            CustomerName = "Hermonie",
            PhoneNumber = "9876543210"
        };

        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x).WithErrorMessage("The customer name with same phonenumber already exists");

    }

    [Fact]
    public async Task CustomerPhoneNumber_NullOrEmpty_ShouldFailValidation()
    {
        // Arrange
        var command = new UpdateCustomerCommand
        {
            Id = 2,
            CustomerName = "Hermonie",
            PhoneNumber = ""
        };

        // Act
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber).WithErrorMessage("Phone Number is required");
    }
    private void MockCustomerData()
    {
        _mockLibraryDbContext.Setup(x => x.Customers).Returns(new List<Customer>{
            new Customer()
            {
             Id = 1,
             Name = "Harry Potter",
             PhoneNumber ="1234567890",
            },
            new Customer()
            {
             Id = 2,
             Name = "Hermonie",
             PhoneNumber ="9876543210",
            },
        }.AsQueryable().BuildMockDbSet().Object);
    }
}
