using FluentValidation.TestHelper;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class BuyBookCommandValidatorTests
{
    private readonly Mock<ILibraryDbContext> _mockDbContext;
    private readonly BuyBookCommandValidator _validator;

    public BuyBookCommandValidatorTests()
    {
        _mockDbContext = new Mock<ILibraryDbContext>();
        _validator = new BuyBookCommandValidator(_mockDbContext.Object);
        MockCustomerdata();
        MockBookData();
    }

    [Fact]
    public async Task BuyBookCommandValidator_BookUnavailable_ErrorMessage()
    {
        var command = new BuyBookCommand
        {
            CustomerId = 10,
            BookIds = new List<long> { 1, 2 }
        };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.BookIds).WithErrorMessage("The book with id 2 not available");
    }

    [Fact]
    public async Task BuyBookCommandValidator_CustomerIdEmpty_ErrorMessage()
    {
        var command = new BuyBookCommand
        {
            BookIds = new List<long> { 1, 2 }
        };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }


    [Fact]
    public async Task BuyBookCommandValidator_BookNotFound_ErrorMessage()
    {
        var command = new BuyBookCommand
        {
            CustomerId = 1,
            BookIds = new List<long> { 1, 3 }
        };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.BookIds).WithErrorMessage("Please enter the correct book id");
    }

    private void MockCustomerdata()
    {
        _mockDbContext.Setup(x => x.Customers).Returns(new List<Customer>{new Customer()
            {
               Id = 10,
               Name = "Ron",
               PhoneNumber = "9034567890",
               Books = new List<Book>
               {
                   new Book { Id = 2, Name = "Harry Potter" }
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
