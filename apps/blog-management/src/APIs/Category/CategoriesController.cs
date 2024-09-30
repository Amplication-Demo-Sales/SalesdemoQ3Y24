using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.APIs;

[ApiController()]
public class CategoriesController : CategoriesControllerBase
{
    public CategoriesController(ICategoriesService service)
        : base(service) { }
}
