using LibraryManagement.Model;
using LibraryManagement.Persistence.Context;
using MediatR;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Command for addinf customer to databse
/// </summary>
public class AddCustomerCommand : IRequest<long>
{
    /// <summary>
    /// Customer Name
    /// </summary>
    /// <example> "Jain" </example>
    public string CustomerName { get; set; }

    /// <summary>
    /// Customer phone number
    /// </summary>
    /// <example> 9098778987 </example>
    public string CustomerPhoneNumber { get; set; }
}

/// <summary>
///  Handler for executing AddCustomerCommand
/// </summary>
public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, long>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary>
    /// <param name="libraryDbContext"></param>
    public AddCustomerCommandHandler(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// Add customer to database
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<long> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Name = command.CustomerName,
            PhoneNumber = command.CustomerPhoneNumber,
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow,
        };
        _libraryDbContext.Customers.Add(customer);
        await _libraryDbContext.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
