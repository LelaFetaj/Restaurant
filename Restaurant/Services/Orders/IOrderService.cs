using Restaurant.API.Models.DTOs.Orders;
using Restaurant.API.Models.Entities.Orders;

namespace Restaurant.API.Services.Orders
{
    public interface IOrderService
    {
        Task<(bool, string)> AddOrderAsync(CreateOrderDto createOrderDto);

        Task<List<Order>> RetrieveAllOrders();

        Task<Order> RetrieveOrderById(Guid id);

        Task<(bool, string)> UpdateOrderAsync(UpdateOrderDto updateOrderDto);

        Task<(bool, string)> DeleteOrderByIdAsync(Guid id);
    }
}
