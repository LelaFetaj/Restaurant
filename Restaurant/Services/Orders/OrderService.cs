using Microsoft.EntityFrameworkCore;
using Restaurant.API.Models.DTOs.Orders;
using Restaurant.API.Models.Entities.Orders;
using Restaurant.API.Repositories.Orders;

namespace Restaurant.API.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<(bool, string)> AddOrderAsync(CreateOrderDto creatOrderDto)
        {
            Order order = new Order
            {
                Food = creatOrderDto.Food,
                Price = creatOrderDto.Price,
                Qty = creatOrderDto.Qty,
                Total = creatOrderDto.Total,
                OrderDate = creatOrderDto.OrderDate,
                Status = creatOrderDto.Status,
                CustomerName = creatOrderDto.CustomerName,
                CustomerContact = creatOrderDto.CustomerContact,
                CustomerEmail = creatOrderDto.CustomerEmail,
                CustomerAddress = creatOrderDto.CustomerAddress
            };

            await orderRepository.InsertOrderAsync(order);

            return (true, "Success");
        }

        public async Task<List<Order>> RetrieveAllOrders()
        {
            List<Order> orders = await orderRepository
                .SelectAllOrders()
                .ToListAsync();

            return orders;
        }

        public async Task<Order> RetrieveOrderById(Guid id) =>
            await orderRepository.SelectOrderByIdAsync(id);

        public async Task<(bool, string)> UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            Order order = await orderRepository.SelectOrderByIdAsync(updateOrderDto.Id);

            if (order == null)
            {
                return (false, $"Couldn't find order with id: {updateOrderDto.Id}");
            }

            order.Food = updateOrderDto.Food;
            order.Price = updateOrderDto.Price;
            order.Qty = updateOrderDto.Qty;
            order.Total = updateOrderDto.Total;
            order.OrderDate = updateOrderDto.OrderDate;
            order.Status = updateOrderDto.Status;
            order.CustomerName = updateOrderDto.CustomerName;
            order.CustomerContact = updateOrderDto.CustomerContact;
            order.CustomerEmail = updateOrderDto.CustomerEmail;
            order.CustomerAddress = updateOrderDto.CustomerAddress;


            await orderRepository.UpdateOrderAsync(order);

            return (true, "Success");
        }

        public async Task<(bool, string)> DeleteOrderByIdAsync(Guid id)
        {
            Order order = await orderRepository.SelectOrderByIdAsync(id);

            if (order == null)
            {
                return (false, $"Couldn't find order with id: {id}");
            }

            await orderRepository.DeleteOrderAsync(order);

            return (true, "Success");
        }
    }
}
