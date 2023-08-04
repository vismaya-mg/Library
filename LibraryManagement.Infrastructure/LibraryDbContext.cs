using LibraryManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Persistence.Context;

/// <summary>
/// Context class for interacting with the database
/// </summary>
public class LibraryDbContext : DbContext, ILibraryDbContext
{
    /// <summary>
    /// Constructor to inject the database
    /// </summary>
    /// <param name="options"></param>
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Adds Books  to the context
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Adds Customer  to the context
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Saves changes to the database asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }
}
