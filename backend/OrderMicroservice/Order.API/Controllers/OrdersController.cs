using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult> PostOrderAsync(DtoOrder dtoOrder)
        {
            var createdOrder = await _orderService.PostOrderAsync(dtoOrder);

            return Ok(createdOrder);
        }
    }
}
