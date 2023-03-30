using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models.DTOs.Orders;
using Restaurant.API.Models.Entities.Orders;
using Restaurant.API.Services.Orders;

namespace Restaurant.API.Controllers.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                (bool result, string message) = await orderService.AddOrderAsync(createOrderDto);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            try
            {
                List<Order> order = await orderService.RetrieveAllOrders();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            try
            {
                Order order = await orderService.RetrieveOrderById(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            try
            {
                (bool result, string message) = await orderService.UpdateOrderAsync(updateOrderDto);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteOrder(Guid id)
        {
            try
            {
                (bool result, string message) = await orderService.DeleteOrderByIdAsync(id);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
