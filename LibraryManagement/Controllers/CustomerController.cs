using System.Net;
using LibraryManagement.Dtos;
using LibraryManagement.Requests.Commands;
using LibraryManagement.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

/// <summary>
/// Api Controller For Customer
/// </summary>
[ApiController]
public class CustomerController : BaseController
{
    /// <summary>
    /// Endpoint to fetch details of all customer.
    /// </summary>
    /// <returns>It returns customer details  along with http status code : OK</returns>
    [HttpGet("customers")]
    [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCustomers()
    {
        // 1.Send Request to customer data query using medaitor
        // 2.Return the Customer Dto
        return Ok(await Mediator.Send(new GetCustomersQuery { }));
    }

    /// <summary>
    /// Adding customer data to the database
    /// </summary>
    /// <returns> Id of inserted record </returns>
    [HttpPost("customers")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerCommand command)
    {
        // 1.Send the AddCustomerCommand using the Mediator to handle adding a new customer
        // 2.Return the customer id
        return Ok(await Mediator.Send(command));
    }

    /// <summary>
    /// Endpoint to fetch details of customer with given id.
    /// </summary>
    /// <param name="id"> id of the customer id</param>
    /// <returns></returns>
    [HttpGet("customers/{id}")]
    [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCustomerById([FromRoute] long id)
    {
        return Ok(await Mediator.Send(new GetCustomerByIdQuery { CustomerId = id }));
    }

    /// <summary>    
    /// Updating customer data to the database    
    /// </summary>    
    /// <returns> Id of updated record </returns>  
    [HttpPut("customers")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCustomerDetails([FromBody] UpdateCustomerCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>    
    /// Updating isavailable field  to the database  when return the book   
    /// </summary>    
    /// <returns> Id of updated record </returns>  
    [HttpPut("customers/return-books")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> BookReturn([FromBody] ReturnBookCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Update the boooks table when buy a book 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>

    [HttpPut("customers/buy-books")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> BuyBook([FromBody] BuyBookCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Update the boooks table when customer lends a book 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("customers/lend-books")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> LendBook([FromBody] LendBookCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}