namespace LibraryManagement.Services.Interfaces;

/// <summary>
/// Interface for a book service that provides methods to calculate the total cost of a list of books and fine.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Gets the total cost of a list of books based on their unique IDs.
    /// </summary>
    Task<long> GetTotalBookCost(List<long> bookIds);

    /// <summary>
    /// Calculates the fine amount for a list of books based on their unique IDs.
    /// </summary>
    Task<long> GetFine(List<long> bookIds);
}
