using LibraryManagement.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Requests.Commands;

/// <summary>
/// Updates the customer
/// </summary>
public class UpdateCustomerCommand : IRequest<long>
{
    /// <summary>
    /// Id of the customer record to be updated
    /// </summary>
    /// <example> 1 </example>
    public long Id { get; set; }

    /// <summary>
    /// Name Attribute for Customer Entity to be updated
    /// </summary>
    /// <example> "jain" </example>
    public string CustomerName { get; set; }

    /// <summary>
    /// Phone Number Attribute for customer Entity
    /// </summary>
    /// <example> "9090876756" </example>
    public string PhoneNumber { get; set; }
}

/// <summary>
/// Handles command for updating customer data to the database.
/// </summary>

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, long>
{
    private readonly ILibraryDbContext _libraryDbContext;

    /// <summary>
    /// Injects context class from constructor to Di
    /// </summary> 
    public UpdateCustomerCommandHandler(ILibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    /// <summary>
    /// Updates customer to the database.
    /// </summary>
    public async Task<long> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _libraryDbContext.Customers.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);

        customer.Name = command.CustomerName;
        customer.PhoneNumber = command.PhoneNumber;
        customer.UpdatedOn = DateTime.UtcNow;
        await _libraryDbContext.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
