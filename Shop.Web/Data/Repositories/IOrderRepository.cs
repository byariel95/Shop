

namespace Shop.Web.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Models;

    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrdersAsync(string userName);

        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        Task AddItemToOrderAsync(AddItemViewModel model, string userName);

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);


    }

}
