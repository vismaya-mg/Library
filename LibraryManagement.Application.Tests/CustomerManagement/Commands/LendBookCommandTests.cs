using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class LendBookCommandTests
{
    [Fact]
    public async Task Handle_ValidCommand_Success()
    {
        // Arrange
        var libraryDbContextMock = new Mock<ILibraryDbContext>();
        var command = new LendBookCommand
        {
            CustomerId = 1,
            BookIds = new List<long> { 1, 2}
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

        libraryDbContextMock.Setup(x => x.Books).Returns(booksData.AsQueryable().BuildMockDbSet().Object);
        libraryDbContextMock.Setup(x => x.Customers).Returns(customer.AsQueryable().BuildMockDbSet().Object);

        var handler = new LendBookCommandHandler(libraryDbContextMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(command.CustomerId, result);
        libraryDbContextMock.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
