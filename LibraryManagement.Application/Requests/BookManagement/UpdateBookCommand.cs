using LibraryManagement.Enums;
using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Requests.BookManagement;

/// <summary>
/// Updates the customer
/// </summary>
public class UpdateBookCommand : IRequest<long>
{
    /// <summary>
    /// Id of the customer record to be updated
    /// </summary>
    /// <example> 1 </example>
    public long Id { get; set; }

    /// <summary>
    /// Name Attribute for Customer Entity to be updated
    /// </summary>
    /// <example> "Harry Potter Chamber of secrets" </example>
    public string Name { get; set; }

    /// <summary>
    /// Author Attribute for book Entity
    /// </summary>
    /// <example> "Rowling" </example>
    public string Author { get; set; }

    /// <summary>
    /// Price Attribute for book Entity
    /// </summary>
    /// <example> 12000 </example>
    public int Price { get; set; }

    /// <summary>
    /// Categories for book Entity
    /// </summary>
    /// <example> 1 </example>
    public Categories Category { get; set; }
}

/// <summary>
/// Handles command for updating book data to the database.
/// </summary>
public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, long>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary> 
    public UpdateBookCommandHandler(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// Updates book to the database.
    /// </summary>
    public async Task<long> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _libraryDbContext.Books.FirstAsync(b => b.Id == command.Id, cancellationToken);

        book.Name = command.Name;
        book.Author = command.Author;
        book.Price = command.Price;
        book.Categories = command.Category;
        book.UpdatedOn = DateTime.UtcNow;
        await _libraryDbContext.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
