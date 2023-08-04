using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;
/// <summary>
/// Base controller for all other controllers
/// </summary>
public class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
}
