using CaoHub.Web.Areas.Facturio.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.Facturio.Controllers
{
    [Area("Facturio")]
    public class ProductsController(ProductService productService) : Controller
    {
        private readonly ProductService _productService = productService;
    }
}
