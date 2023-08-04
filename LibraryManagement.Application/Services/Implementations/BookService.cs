using LibraryManagement.Helpers;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LibraryManagement.Services.Implementations;
/// <summary>
/// Sevice file for all books to get the total cost
/// </summary>
public class BookService : IBookService
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Initializes a new instance of the BookService class with the provided library database context.
    /// </summary>
    /// <param name="libraryDbContext">The library database context used by the service to interact with the data store.</param>
    public BookService(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// Method to get total books cost
    /// </summary>
    /// <param name="bookIds"></param>
    /// <returns></returns>
    public async Task<long> GetTotalBookCost(List<long> bookIds)
    {
        return await _libraryDbContext.Books.Where(x => bookIds.Contains(x.Id)).SumAsync(x => x.Price);
    }
    /// <summary>
    /// Method to get fine based on the return date
    /// </summary>
    /// <param name="bookIds"></param>
    /// <returns></returns>
    public async Task<long> GetFine(List<long> bookIds)
    {

        var books = await _libraryDbContext.Books.AsTracking().Where(x => bookIds.Contains(x.Id)).ToListAsync();
        var fineTotal = 0;
        foreach (var book in books)
        {
            if (book != null)
            {
                var fine = BookHelper.GetFineAmount(book.UpdatedOn);
                fineTotal += fine;
            }
        }
        return fineTotal;
    }

}
