using FluentValidation.TestHelper;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class ReturnBookCommandValidatorTests
{

    private readonly Mock<ILibraryDbContext> _mockDbContext;
    private readonly ReturnBookCommandValidator _validator;


    public ReturnBookCommandValidatorTests()
    {
        _mockDbContext = new Mock<ILibraryDbContext>();
        _validator = new ReturnBookCommandValidator(_mockDbContext.Object);
        MockCustomerdata();
        MockBookData();
    }

    [Fact]
    public async Task ReturnBookCommandValidator_BookUnavailable_ErrorMessage()
    {
        var command = new ReturnBookCommand
        {
            CustomerId = 11,
            BookId = new List<long> { 2 }
        };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x).WithErrorMessage("Please enter the correct customer id");
    }


    [Fact]
    public async Task ReturnBookCommandValidator_CustomerIdEmpty_ErrorMessage()
    {
        var command = new ReturnBookCommand
        {
            BookId = new List<long> { 2 }
        };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
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
            },

            new Customer(){
               Id = 11,
               Name = "Hermoine",
               PhoneNumber = "9034567897",
               Books = new List<Book>
               {
                   new Book { Id = 1, Name = " Potter" }
               }
            },
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
                CustomerId = 10,
                IsAvailable= false
            },

        }.AsQueryable().BuildMockDbSet().Object);

    }

}
