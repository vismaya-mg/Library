using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Commands;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Commands;

public class AddCustomerCommandTests
{

    [Fact]
    public async Task AddCustomerCommand_ShouldAdd_RecordToDatabase()
    {
        var mockDbContext = new Mock<ILibraryDbContext>();

        var command = new AddCustomerCommand
        {
            CustomerName = "test",
            CustomerPhoneNumber = "9947003224"
        };

       mockDbContext.Setup(x => x.Customers.Add(It.IsAny<Customer>()));

        var _handler = new AddCustomerCommandHandler(mockDbContext.Object);
        var response = await _handler.Handle(command, CancellationToken.None);

        mockDbContext.Verify(x => x.Customers.Add(It.IsAny<Customer>()), Times.Once);
        mockDbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        Assert.NotNull(response);
    }
}
