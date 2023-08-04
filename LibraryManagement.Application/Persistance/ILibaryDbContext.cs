using LibraryManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Persistence.Context;

/// <summary>
/// Represents a database context interface for the library application, providing access to collections of books and customers.
/// </summary>
public interface ILibraryDbContext
{
    /// <summary>
    /// Gets or sets a database set representing the collection of books.
    /// </summary>
    DbSet<Book> Books { get; set; }

    /// <summary>
    /// Gets or sets a database set representing the collection of customers.
    /// </summary>
    DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">Optional. A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}