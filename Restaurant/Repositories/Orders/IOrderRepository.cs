using Restaurant.API.Models.Entities.Orders;

namespace Restaurant.API.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task InsertOrderAsync(Order order);
        IQueryable<Order> SelectAllOrders();
        Task<Order> SelectOrderByIdAsync(Guid id);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
    }
}
