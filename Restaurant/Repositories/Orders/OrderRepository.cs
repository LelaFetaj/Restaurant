using Restaurant.API.Data.Contexts;
using Restaurant.API.Models.Entities.Orders;

namespace Restaurant.API.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantDbContext dbContext;

        public OrderRepository(RestaurantDbContext dbContext)
        {
            dbContext = dbContext;
        }

        public async Task InsertOrderAsync(Order order)
        {
            await dbContext.Order.AddAsync(order);
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<Order> SelectAllOrders() =>
            dbContext.Order;

        public async Task<Order> SelectOrderByIdAsync(Guid id) =>
            await dbContext.Order.FindAsync(id);

        public async Task UpdateOrderAsync(Order order)
        {
            dbContext.Order.Update(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            dbContext.Remove(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
