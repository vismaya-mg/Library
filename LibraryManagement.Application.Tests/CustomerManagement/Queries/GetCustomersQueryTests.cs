﻿using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Requests.Queries;
using MockQueryable.Moq;
using Moq;

namespace LibraryManagement.Application.Tests.CustomerManagement.Queries;

public class GetCustomersQueryTests
{

    /// <summary>
    /// Readonly Mock object of type ILibraryDbContext interface
    /// </summary>
    private readonly Mock<ILibraryDbContext> _mockLibraryDbContext;

    /// <summary>
    /// Constructor initializes mock database with data
    /// </summary>
    public GetCustomersQueryTests()
    {
        _mockLibraryDbContext = new Mock<ILibraryDbContext>();
        MockCustomerdata();
    }

    [Fact]
    public async Task GetCustomerQuery_ShouldReturn_ValidData()
    {
        var handler = new GetCustomersQueryHandler(_mockLibraryDbContext.Object);
        var result = await handler.Handle(new GetCustomersQuery(), CancellationToken.None);
        var customer = result.First();
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal("Test", customer.Name);
        Assert.Equal("1234567890", customer.PhoneNumber);
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
