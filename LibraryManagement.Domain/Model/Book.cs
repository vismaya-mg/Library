using LibraryManagement.Enums;
using LibraryManagement.Model.Common;

namespace LibraryManagement.Model;

/// <summary>
/// Entity model for store the details of  book
/// </summary>
public class Book : BaseDomainEntity
{
    /// <summary>
    /// Unique identifier for the book
    /// </summary>
    /// <example> 2 </example>
    public long Id { get; set; }

    /// <summary>
    /// Nmae of book
    /// </summary>
    /// <example> Eleven Minutes </example>
    public string Name { get; set; }

    /// <summary>
    /// Author of book
    /// </summary>
    /// <example> Paulo Coelho </example>
    public string Author { get; set; }

    /// <summary>
    /// The price of book
    /// </summary>
    /// <example> 1200 </example>
    public long Price { get; set; }

    /// <summary>
    /// Property to check whether the book is avavible or not
    /// </summary>
    /// <example> true </example>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Diffrent categories of book
    /// </summary>
    /// <example> 1 </example>
    public Categories Categories { get; set; }

    /// <summary>
    /// Id of customer
    /// </summary>
    /// <example> 1 </example>
    public long? CustomerId { get; set; }

    /// <summary>
    /// Navigation property for the customer who has borrowed the book.
    /// </summary>
    public Customer Customer { get; set; }
}
