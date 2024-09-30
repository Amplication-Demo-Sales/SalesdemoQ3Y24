using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[ApiController()]
public class CommentsController : CommentsControllerBase
{
    public CommentsController(ICommentsService service)
        : base(service) { }
}
