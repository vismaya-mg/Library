using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Command to lend a book
/// </summary>
public class LendBookCommand : IRequest<long>
{
    /// <summary>
    /// Unique identification number for each record of customer entity
    /// </summary>
    /// <example> 1 </example>
    public long CustomerId { get; set; }

    /// <summary>
    /// List of book id
    /// </summary>
    /// <example> 1,2,3 </example>
    public List<long> BookIds { get; set; }
}

/// <summary>
/// Handle command for buy book
/// </summary>
public class LendBookCommandHandler : IRequestHandler<LendBookCommand, long>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    /// <param name="libraryDbContext"></param>
    public LendBookCommandHandler(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// add customer id to book database 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<long> Handle(LendBookCommand command, CancellationToken cancellationToken)
    {
        var bookIds = await _libraryDbContext.Books.AsTracking()
            .Where(b => command.BookIds.Contains(b.Id))
            .ToListAsync(cancellationToken);
        bookIds.ForEach(book => { book.IsAvailable = false; book.CustomerId = command.CustomerId; book.UpdatedOn = DateTime.UtcNow; });
        await _libraryDbContext.SaveChangesAsync(cancellationToken);
        return command.CustomerId;
    }
}