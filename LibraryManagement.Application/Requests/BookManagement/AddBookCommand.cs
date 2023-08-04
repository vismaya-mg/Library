using LibraryManagement.Enums;
using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Requests.BookManagement;

/// <summary>
/// Command for add book into database
/// </summary>
public class AddBookCommand : IRequest<long>
{
    /// <summary>
    /// Book Name
    /// </summary>
    /// <example> "Harry Potter" </example>
    public string Name { get; set; }

    /// <summary>
    /// Book Author Name
    /// </summary>
    /// <example> "Rowling" </example>
    public string Author { get; set; }

    /// <summary>
    /// Book price
    /// </summary>
    /// <example> 2000 </example>
    public int Price { get; set; }

    /// <summary>
    /// Book Category
    /// </summary>
    /// <example> 1 </example>
    public Categories Category { get; set; }
}

/// <summary>
/// Handler for executing AddBookCommand
/// </summary>
public class AddBooksCommandHandler : IRequestHandler<AddBookCommand, long>
{
    private readonly ILibraryDbContext _libraryDbContext;
    private readonly ILogger _logger;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    /// <param name="libraryDbContext"></param>
    /// <param name="logger"></param>
    public AddBooksCommandHandler(ILibraryDbContext libraryDbContext, ILogger<AddBookCommand> logger)
    {
        _libraryDbContext = libraryDbContext;
        _logger = logger;
    }

    /// <summary>
    /// add book to database
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<long> Handle(AddBookCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Command to add a new book to the database");
        var book = new Book
        {
            Name = command.Name,
            Price = command.Price,
            Author = command.Author,
            Categories = command.Category,
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow
        };
        _libraryDbContext.Books.Add(book);
        await _libraryDbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Added a new book");

        return book.Id;
    }
}
