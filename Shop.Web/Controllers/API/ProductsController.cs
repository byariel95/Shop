

namespace Shop.Web.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

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
