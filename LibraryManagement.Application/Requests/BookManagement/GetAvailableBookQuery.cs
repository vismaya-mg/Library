using LibraryManagement.Dtos;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Requests.Queries;

/// <summary>
/// Sets Up a request with a response for querying book data
/// </summary>
public class GetAvailableBookQuery : IRequest<List<BookDto>>
{
}

/// <summary>
/// Handles query for querying book data table from the database.
/// </summary>
public class GetAvailableBookQueryHandler : IRequestHandler<GetAvailableBookQuery, List<BookDto>>
{
    private readonly ILibraryDbContext _libraryDbContext;
    private readonly ILogger<GetAvailableBookQueryHandler> _logger;

    /// <summary>
    /// Injects dbContext class to the query handler
    /// </summary>
    /// <param name="libraryDbContext"></param>
    public GetAvailableBookQueryHandler(ILibraryDbContext libraryDbContext, ILogger<GetAvailableBookQueryHandler> logger)
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
    public async Task<List<BookDto>> Handle(GetAvailableBookQuery request, CancellationToken cancellationToken)
    {
        var books = await _libraryDbContext.Books.Where(x => x.IsAvailable).Select(x => new BookDto
        {
            Name = x.Name,
            Author = x.Author,
            Category = x.Categories.ToString(),
            IsAvailable = x.IsAvailable,
            Price = x.Price
        }).ToListAsync(cancellationToken);
        _logger.LogInformation("The count of available books {booksCount}", books.Count);
        return books;
    }
}