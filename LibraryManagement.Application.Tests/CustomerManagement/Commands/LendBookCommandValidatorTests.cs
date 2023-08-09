using FluentValidation.TestHelper;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class LendBookCommandValidatorTests
{

    private readonly Mock<ILibraryDbContext> _mockDbContext;
    private readonly LendBookCommandValidator _validator;


    public LendBookCommandValidatorTests()
    {
        _mockDbContext = new Mock<ILibraryDbContext>();
        _validator = new LendBookCommandValidator(_mockDbContext.Object);
        MockCustomerdata();
        MockBookData();
    }

    [Fact]
    public async Task LendBookCommandValidator_BookUnavailable_ErrorMessage()
    {
        var command = new LendBookCommand
        {
            CustomerId = 1,
            BookIds = new List<long> { 1 ,2}
        };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.BookIds).WithErrorMessage("The book with id 2 not available");
    }

    [Fact]
    public async Task LendBookCommandValidator_BookNotFound_ErrorMessage()
    {
        var command = new LendBookCommand
        {
            CustomerId = 1,
            BookIds = new List<long> { 1,3}
        };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.BookIds).WithErrorMessage("Please enter the correct id");
    }

    [Fact]
    public async Task LendBookCommandValidator_EmptyBookIds_ErrorMessage()
    {
        var command = new LendBookCommand
        {
            BookIds = new List<long>()
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }

    //[Fact]
    //public async Task LendBookCommandValidator_BookIdsNullOrEmpty_ErrorMessage()
    //{
    //    var command = new LendBookCommand
    //    {
    //        CustomerId = 1,
    //        BookIds = null
    //    };

    //    var result = await _validator.TestValidateAsync(command);

    //    result.ShouldHaveValidationErrorFor(x => x.BookIds)
    //        .WithErrorMessage(ValidationMessage.Required);
    //}
    [Fact]
    public async Task CustomerId_NotEmpty_ShouldFailValidation()
    {
        // Arrange
        var command = new LendBookCommand
        {

            BookIds = new List<long>()
        };
        // Act
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerId).WithErrorMessage("'Customer Id' must not be empty.");
    }

    private void MockCustomerdata()
    {
        _mockDbContext.Setup(x => x.Customers).Returns(new List<Customer>{new Customer()
            {
               Id = 1,
               Name = "Test",
               PhoneNumber = "1234567890",
               Books = new List<Book>
               {
                   new Book { Id = 2, Name = "Book 2" }
               }
            }
        }.AsQueryable().BuildMockDbSet().Object);
    }


    private void MockBookData()

    {
        _mockDbContext.Setup(x => x.Books).Returns(new List<Book>
        {
            new Book() {
                Id= 1,
                IsAvailable= true
            },
             new Book() {
                Id= 2,
                IsAvailable= false
            },

        }.AsQueryable().BuildMockDbSet().Object);

    }

}
