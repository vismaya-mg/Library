using LibraryManagement.Constants;
using LibraryManagement.Dtos;
using LibraryManagement.Enums;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Requests.Queries;

/// <summary>
/// Get books based on the category query
/// </summary>
public class GetBooksByCategoryQuery : IRequest<BookDto>
{
    /// <summary>
    /// Book Category
    /// </summary>
    /// <example> 1 </example>
    public Categories Category { get; set; }
}

/// <summary>
/// Handles command for fetching book data by using the category.
/// </summary>
public class GetBooksByCategoryQueryHandler : IRequestHandler<GetBooksByCategoryQuery, BookDto>
{
    private readonly ILibraryDbContext _libraryDbContext;
    private readonly ILogger _logger;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    /// <param name="libraryDbContext"></param>
    /// <param name="logger"></param>
    public GetBooksByCategoryQueryHandler(ILibraryDbContext libraryDbContext, ILogger<GetBooksByCategoryQuery> logger)
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
    public async Task<BookDto> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Category);
        var bookDetails = await _libraryDbContext.Books
            .Where(x => x.Categories == request.Category)
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
            _logger.LogInformation("The book with this category is not present");
            throw new Exception(ValidationMessage.CategoryNotFoundMessage);

        }
        _logger.LogInformation("The book details of the given category is fetched successfully");
        return bookDetails;
    }
}
