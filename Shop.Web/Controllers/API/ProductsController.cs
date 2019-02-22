

namespace Shop.Web.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using Data;

    [Route("api/[Controller]")]
    public class ProductsController: Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            return this.Ok(this.productRepository.GetAllWithUsers());
        }

    }
}
