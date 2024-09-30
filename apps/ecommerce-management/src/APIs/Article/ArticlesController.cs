using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[ApiController()]
public class ArticlesController : ArticlesControllerBase
{
    public ArticlesController(IArticlesService service)
        : base(service) { }
}
