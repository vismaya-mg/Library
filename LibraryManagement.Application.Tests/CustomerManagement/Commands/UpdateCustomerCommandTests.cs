using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.BookManagement;
using LibraryManagement.Requests.Commands;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class UpdateCustomerCommandTests
{
    private readonly Mock<ILibraryDbContext> _mockLibraryDbContext;

    public UpdateCustomerCommandTests()
    {
        _mockLibraryDbContext = new Mock<ILibraryDbContext>();
        MockCustomerData();
    }

    [Fact]
    public async Task UpdateBookCommand_ShouldUpdateRecord_OnApiCall()
    {
        var command = new UpdateCustomerCommand
        {
            Id = 1,
            CustomerName = "Harry Potter",
            PhoneNumber = "1234567890",
        };
        var handler = new UpdateCustomerCommandHandler(_mockLibraryDbContext.Object);
        var response = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(1, response);
    }


    private void MockCustomerData()
    {
        _mockLibraryDbContext.Setup(x => x.Customers).Returns(new List<Customer>{
            new Customer()
            {
             Id = 1,
             Name = "Harry Potter",
             PhoneNumber ="1234567890",
            }

        }.AsQueryable().BuildMockDbSet().Object);
    }
}
