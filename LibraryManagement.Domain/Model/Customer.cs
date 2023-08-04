using LibraryManagement.Model.Common;

namespace LibraryManagement.Model;

/// <summary>
/// Entity model for customer
/// </summary>
public class Customer : BaseDomainEntity
{
    /// <summary>
    /// Unique identification for customer entity
    /// </summary>
    /// <example> 2 </example>
    public long Id { get; set; }

    /// <summary>
    /// Customer name attribute of customer entity
    /// </summary>
    /// <example> Albert </example>
    public string Name { get; set; }

    /// <summary>
    /// Customer phone number attribute of customer entity
    /// </summary>
    /// <example> 9063772114 </example>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// List of books that the customer taken
    /// </summary>
    /// <example> [1, 2, 3] </example>
    public List<Book> Books { get; set; }
}
