using LibraryManagement.Dtos;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Requests.Queries;

/// <summary>
/// Query to get the book by id
/// </summary>
public class GetBookDetailsByIdQuery : IRequest<BookDto>
{
    /// <summary>
    /// Unique identification number for each record of book entity
    /// </summary>
    /// <example> 1 </example>
    public long BookId { get; set; }
}

/// <summary>
/// Handles command for fetching book data by using the id from the database.
/// </summary>
public class GetBookDetailsByIdQueryHandler : IRequestHandler<GetBookDetailsByIdQuery, BookDto>
{
    private readonly ILibraryDbContext _libraryDbContext;
    private readonly ILogger _logger;
    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    /// <param name="libraryDbContext"></param>
    /// <param name="logger"></param>
    public GetBookDetailsByIdQueryHandler(ILibraryDbContext libraryDbContext, ILogger<GetBookDetailsByIdQuery> logger)
    {
        _libraryDbContext = libraryDbContext;
        _logger = logger;
    }

    /// <summary>
    /// Fetches the book data from the database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>    
    public async Task<BookDto> Handle(GetBookDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var bookDetails = await _libraryDbContext.Books
            .Where(x => x.Id == request.BookId)
            .Select(y => new BookDto
            {
                Name = y.Name,
                Author = y.Author,
                Category = y.Categories.ToString(),
                IsAvailable = y.IsAvailable,
                Price = y.Price
            }).FirstOrDefaultAsync(cancellationToken);
        if (bookDetails == null)
        {
            _logger.LogInformation("The book with current id is not present");
            throw new Exception("The book with current id is not present");

        }
        _logger.LogInformation("The book details of the given id is fetched successfully");
        return bookDetails;
    }
}
