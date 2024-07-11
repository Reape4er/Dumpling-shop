using Microsoft.AspNetCore.Mvc;
using Basket.API.Services;
using Basket.API.Models;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AddOrUpdateItem(BasketItem basketItem, int id)
        {
            await _basketService.UpdateItemAsync(basketItem, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItem(int userId)
        {
            await _basketService.ClearBasketAsync(userId);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasket(int id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            if (basket != null)
            {
                return Ok(basket);
            }
            else
            {
                // Возвращаем ошибку клиенту, если не удалось получить корзину
                return NotFound("Ошибка при получении корзины.");
            }
        }
    }
}
