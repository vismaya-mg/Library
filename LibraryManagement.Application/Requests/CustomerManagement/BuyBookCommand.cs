using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Command to buy the book
/// </summary>
public class BuyBookCommand : IRequest<long>
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
public class BuyBookCommandHandler : IRequestHandler<BuyBookCommand, long>
{
    private readonly IBookService _bookService;
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    /// <param name="libraryDbContext"></param>
    /// <param name="bookService"></param>
    public BuyBookCommandHandler(ILibraryDbContext libraryDbContext, IBookService bookService)
    {
        _libraryDbContext = libraryDbContext;
        _bookService = bookService;
    }

    /// <summary>
    /// add customer id to book database 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<long> Handle(BuyBookCommand command, CancellationToken cancellationToken)
    {
        var totalCost = await _bookService.GetTotalBookCost(command.BookIds);

        var books = await _libraryDbContext.Books.Where(b => command.BookIds.Contains(b.Id)).ToListAsync();
        _libraryDbContext.Books.RemoveRange(books);

        await _libraryDbContext.SaveChangesAsync(cancellationToken);
        return totalCost;
    }
}
