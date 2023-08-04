using LibraryManagement.Dtos;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Requests.Queries;

/// <summary>
/// Sets Up a request with a response for querying book data
/// </summary>
public class GetAllBooksQuery : IRequest<List<BookDto>>
{
}

/// <summary>
/// Handles query for querying book data table from the database.
/// </summary>
public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects dbContext class to the query handler
    /// </summary>
    /// <param name="libraryDbContext"></param>
    public GetAllBooksQueryHandler(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// Fetches the book data from the database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return await _libraryDbContext.Books.Select(x => new BookDto
        {
            Name = x.Name,
            Author = x.Author,
            Category = x.Categories.ToString(),
            IsAvailable = x.IsAvailable,
            Price = x.Price
        }).ToListAsync(cancellationToken);
    }
}