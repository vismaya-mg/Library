using LibraryManagement.Dtos;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Requests.Queries;

/// <summary>
/// Sets Up a request with a response for querying customer data
/// </summary>
public class GetCustomersQuery : IRequest<List<CustomerDto>>
{
}

/// <summary>
///Handles query for querying customer data table from the database.
/// </summary>
public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects dbContext class to the query handler
    /// </summary>
    /// <param name="libraryDbContext"></param>
    public GetCustomersQueryHandler(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// Fetches the customer data from the database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _libraryDbContext.Customers.Select(x => new CustomerDto
        {
            Name = x.Name,
            PhoneNumber = x.PhoneNumber,
            Books = x.Books.Select(y => new BookBasicDetails
            {
                Id = y.Id,
                Name = y.Name
            }).ToList()
        }).ToListAsync(cancellationToken);
    }
}