using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using LibraryManagement.Services.Interfaces;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class BuyBookCommandTests
{
    [Fact]
    public async Task BuyBookCommand_ShouldAddRecordToDatabase_ReturnTheId()
    {
        // Arrange
        var _libraryDbContextMock = new Mock<ILibraryDbContext>();
        var _service = new Mock<IBookService>();
        var command = new BuyBookCommand
        {
            CustomerId = 1,
            BookIds = new List<long> { 1, 2 }
        };

        var booksData = new List<Book>
        {
            new Book { Id = 1, IsAvailable = true },
            new Book { Id = 2, IsAvailable = true },
            new Book { Id = 3, IsAvailable = true }
        };

        var customer = new List<Customer>
        {
            new Customer{
            Id = 1,
            Name = "Test",
            PhoneNumber = "1234567890",
            Books = new List<Book>
            {
                new Book { Id = 1, Name = "Book 1" },
                new Book { Id = 2, Name = "Book 2" }
            }
            }
        };

        _libraryDbContextMock.Setup(x => x.Books).Returns(booksData.AsQueryable().BuildMockDbSet().Object);
        _libraryDbContextMock.Setup(x => x.Customers).Returns(customer.AsQueryable().BuildMockDbSet().Object);

        var handler = new BuyBookCommandHandler(_libraryDbContextMock.Object, _service.Object);
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        _service.Verify(service => service.GetTotalBookCost(command.BookIds), Times.Once);
    }
}
