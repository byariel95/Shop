

namespace Shop.Web.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Data.Repositories;

    [Authorize]

    public class OrdersController: Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;

        public OrdersController(IOrderRepository orderRepository,IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await orderRepository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await this.orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return this.View(model);
        }


    }
}
