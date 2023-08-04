
namespace LibraryManagement.Dtos;

/// <summary>
/// Data transfer object for querying book table. 
/// </summary>
public class BookDto
{
    /// <summary>
    /// Book name
    /// </summary>
    /// <example> "Harry Potter"  </example>
    public string Name { get; set; }

    /// <summary>
    /// Author of the book
    /// </summary>
    /// <example> "Rowling" </example>
    public string Author { get; set; }

    /// <summary>
    /// Book price
    /// </summary>
    ///<example> 6200 </example> 
    public long Price { get; set; }

    /// <summary>
    /// Property to check whether the book is avavible o not
    /// </summary>
    ///<example> true </example> 
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Category of book
    /// </summary>
    ///<example> "Fantacy" </example>
    public String Category { get; set; }
}
