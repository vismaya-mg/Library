namespace LibraryManagement.Dtos;

/// <summary>
/// Data transfer object for customer table
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Customer Name
    /// </summary>
    /// <example> "Roy" </example>
    public string Name { get; set; }

    /// <summary>
    /// Customer phone number
    /// </summary>
    /// <example> 9207841941 </example>   
    public string PhoneNumber { get; set; }

    /// <summary>
    /// List of books
    /// </summary>
    /// <example> "Harry Potter" </example>
    public List<BookBasicDetails> Books { get; set; }
}

/// <summary>
/// Data transfer object for showing the name and of the book
/// </summary>
public class BookBasicDetails
{
    /// <summary>
    /// Unique Id of book
    /// </summary>
    /// <example> 1 </example>
    public long Id { get; set; }

    /// <summary>
    /// Name of the book
    /// </summary>
    /// <example> "Harry potter chmaber of secrets" </example>
    public string Name { get; set; }
}
