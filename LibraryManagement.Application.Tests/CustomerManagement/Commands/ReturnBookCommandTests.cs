using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using LibraryManagement.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class ReturnBookCommandTests
{
    private readonly Mock<ILibraryDbContext> _dbContext;
    private readonly Mock<ILogger<ReturnBookCommand>> _logger;
    private readonly Mock<IBookService> _service;

    public ReturnBookCommandTests()
    {
        _dbContext = new Mock<ILibraryDbContext>();
        _logger = new Mock<ILogger<ReturnBookCommand>>();
        _service = new Mock<IBookService>();
    }
    [Fact]
    public async Task ReturnBookCommand_ShouldUpdateCustomerIdToNull()
    {
        var command = new ReturnBookCommand
        {
            CustomerId = 1,
            BookId = new List<long> { 1, 2 }
        };

        var booksData = new List<Book>
        {
            new Book { Id = 1, CustomerId = 1, IsAvailable = true },
            new Book { Id = 2, CustomerId = 1, IsAvailable = true },
            new Book { Id = 3, CustomerId = null, IsAvailable = true }
        };

        var customer = new List<Customer>
        {
            new Customer{
            Id = 1,
            Name = "Test",
            PhoneNumber = "1234567890",
            Books = new List<Book>
            {
                new Book { Id = 1, Name = "Book 1" , Price = 210},
                new Book { Id = 2, Name = "Book 2" , Price = 220}
            }
            }
        };

        _dbContext.Setup(x => x.Books).Returns(booksData.AsQueryable().BuildMockDbSet().Object);
        _dbContext.Setup(x => x.Customers).Returns(customer.AsQueryable().BuildMockDbSet().Object);

        // Act
        var handler = new ReturnBookCommandHandler(_dbContext.Object, _service.Object, _logger.Object);
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        _service.Verify(service => service.GetFine(command.BookId), Times.Once);
        _dbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));

    }


}
