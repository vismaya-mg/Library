using System.Reflection.Metadata;
using LibraryManagement.Dtos;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Requests.Queries;

/// <summary>
/// Sets Up a request with a response for querying customer's data
/// </summary>
public class GetCustomerByIdQuery : IRequest<CustomerDto>
{
    /// <summary>
    /// Unique identification number for each record of customer entity
    /// </summary>
    /// <example> 1 </example>
    public long CustomerId { get; set; }
}

/// <summary>
/// Handles command for fetching customer data by using the id from the database.
/// </summary>
public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    /// <param name="libraryDbContext"></param>
    public GetCustomerByIdQueryHandler(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// Fetches the customer data from the database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>    
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customerDetails = await _libraryDbContext.Customers
            .Where(x => x.Id == request.CustomerId)
            .Select(y => new CustomerDto
            {
                Name = y.Name,
                PhoneNumber = y.PhoneNumber,
                Books = y.Books.Select(y => new BookBasicDetails
                {
                    Id = y.Id,
                    Name = y.Name,
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
        if (customerDetails == null)
        {
            throw new Exception("Customer with given id is not found");
        }

        return customerDetails;
    }
}
