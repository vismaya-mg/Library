using System.Net;
using LibraryManagement.Dtos;
using LibraryManagement.Enums;
using LibraryManagement.Requests.BookManagement;
using LibraryManagement.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

/// <summary>
/// Api Controller For Book
/// </summary>
[ApiController]
public class BooksController : BaseController
{
    /// <summary>
    /// Adding Book data to the database
    /// </summary>
    /// <returns> Id of inserted record </returns>
    [HttpPost("books")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
    {
        // 1.Send the AddBookCommand using the Mediator to handle adding a new book
        // 2.Return the book id
        return Ok(await Mediator.Send(command));
    }

    /// <summary>    
    /// Updating book data to the database    
    /// </summary>    
    /// <returns> Id of updated record </returns>  
    [HttpPut("books")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBookSetails([FromBody] UpdateBookCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Endpoint to fetch details of available books.
    /// </summary>
    /// <returns> It returns book details  along with http status code : OK</returns>
    [HttpGet("books/available")]
    [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAvailableBook()
    {
        // 1.Send Request to book data query using medaitor
        // 2.Return the book Dto
        return Ok(await Mediator.Send(new GetAvailableBookQuery { }));
    }

    /// <summary>
    /// Endpoint to fetch details of all books.
    /// </summary>
    /// <returns>It returns book details  along with http status code : OK</returns>
    [HttpGet("books")]
    [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBook()
    {
        // 1.Send Request to book data query using medaitor
        // 2.Return the book Dto where all the books
        return Ok(await Mediator.Send(new GetAllBooksQuery { }));
    }

    /// <summary>
    /// Endpoint to fetch details of book with given id.
    /// </summary>
    /// <param name="id"> id of the book</param>
    /// <returns></returns>
    [HttpGet("books/{id}")]
    [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBookById([FromRoute] long id)
    {
        return Ok(await Mediator.Send(new GetBookDetailsByIdQuery { BookId = id }));
    }

    /// <summary>
    /// Endpoint to fetch details of  books with the category name.
    /// </summary>
    /// <returns>It returns book details  along with http status code : OK</returns>
    [HttpGet("books/categories/{categories}")]
    [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategoryBook([FromRoute]Categories categories)
    {
        // 1.Send Request to book data query using medaitor
        // 2.Return the book Dto where all the books that contain the category
        var boooks = await Mediator.Send(new GetBooksByCategoryQuery { Category = categories });
        return Ok(boooks);
    }

}
