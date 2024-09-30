using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.APIs;

[ApiController()]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
