using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Vertical_Slice_Architecture.Shared;

[Route("api/[action]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public CustomBaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [NonAction]
    public IActionResult CreateActionResult<T>(AppResponse<T> response)
    {
        if (response.StatusCode == 204)
            return new ObjectResult(null) { StatusCode = response.StatusCode };

        return new ObjectResult(response) { StatusCode = response.StatusCode };
    }
}
