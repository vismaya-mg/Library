using LibraryManagement.Persistence.Context;
using LibraryManagement.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Updating IsAvailable field 
/// </summary>
public class ReturnBookCommand : IRequest<long>
{
    /// <summary>
    /// Unique identification number for each record of customer entity
    /// </summary>
    /// <example> 1 </example>
    public long CustomerId { get; set; }

    /// <summary>
    /// Unique identification number for each record of book entity
    /// </summary>
    /// <example> 1,2,3 </example>
    public List<long> BookId { get; set; }
}

/// <summary>
/// Handles command for updating book avaialbility data to the database.
/// </summary>
public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, long>
{
    private readonly IBookService _bookService;
    private readonly ILibraryDbContext _libraryDbContext;
    private readonly ILogger _logger;


    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    public ReturnBookCommandHandler(ILibraryDbContext libraryDbContext, IBookService bookService, ILogger<ReturnBookCommand> logger)
    {
        _libraryDbContext = libraryDbContext;
        _bookService = bookService;
        _logger = logger;
    }
    /// <summary>
    /// Updating IsAvailable field 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<long> Handle(ReturnBookCommand command, CancellationToken cancellation)
    {
        var fine = await _bookService.GetFine(command.BookId);

        var books = await _libraryDbContext.Books.AsTracking()
                    .Where(x => command.BookId.Contains(x.Id) && x.CustomerId == command.CustomerId)
                    .ToListAsync(cancellation);

        books.ForEach(book =>
        {
            book.IsAvailable = true;
            book.CustomerId = null;
            book.UpdatedOn = DateTime.UtcNow;
        });
        await _libraryDbContext.SaveChangesAsync(cancellation);

        if (fine == 0)
        {
            _logger.LogInformation("This customer doesn't have any fines");
        }
        return fine;
    }
}
