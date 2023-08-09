using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Queries;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Queries;

public class GetCustomerByIdQueryTests
{

    private readonly Mock<ILibraryDbContext> _mockLibraryDbContext;
        
    public GetCustomerByIdQueryTests()
    {
        _mockLibraryDbContext = new Mock<ILibraryDbContext>();
        MockCustomerdata();
    }

    [Fact]
    public async Task GetCustomerByIdQueryTests_ShouldReturn_CorrectDataBasedOnTheId()
    {
        var handler = new GetCustomerByIdQueryHandler(_mockLibraryDbContext.Object);
        var result = await handler.Handle(new GetCustomerByIdQuery { CustomerId = 1},CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
        Assert.Equal("1234567890", result.PhoneNumber);
    }

    #region DatabaseInitilization
    /// <summary>
    /// Initializes Mock database with mocked object
    /// </summary>
    private void MockCustomerdata()
    {
        _mockLibraryDbContext.Setup(x => x.Customers).Returns(new List<Customer>{new Customer()
            {
               Id = 1,
               Name = "Test",
               PhoneNumber = "1234567890",
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
