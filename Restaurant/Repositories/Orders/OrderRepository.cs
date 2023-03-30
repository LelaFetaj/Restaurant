using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Orders;

namespace Restaurant.API.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public OrderRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertOrderAsync(Order order)
        {
            await _dbContext.Order.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Order> SelectAllOrders() =>
            _dbContext.Order;

        public async Task<Order> SelectOrderByIdAsync(Guid id) =>
            await _dbContext.Order.FindAsync(id);

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Order.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
