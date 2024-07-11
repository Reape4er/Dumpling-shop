using Basket.API.Models;
using StackExchange.Redis;

namespace Basket.API.Services
{
    public interface IBasketService
    {
        Task UpdateItemAsync(BasketItem basketItem, int userId);
        //Task RemoveItemAsync(string userId, BasketItem basketItem);
        //Task<Dictionary<string, object>> GetBasketAsync(string userId);
        Task<List<BasketItem>> GetBasketAsync(int userId);
        Task ClearBasketAsync(int userId);

    }
    public class BasketService : IBasketService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public BasketService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task UpdateItemAsync(BasketItem basketItem, int userId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var basketKey = $"basket:{userId}";
            if (basketItem.Quantity <= 0)
            {
                // Если количество товара меньше или равно удаляемому, удаляем товар из корзины
                await db.HashDeleteAsync(basketKey, basketItem.Id);
                return;
            }
            //var itemInBasket = await db.HashGetAsync(basketKey, basketItem.ItemId);

            //if (itemInBasket.HasValue)
            //{
            //    // Товар уже есть в корзине, увеличиваем количество
            //    basketItem.Quanity += (int)itemInBasket;
            //}
            // Добавляем товар или обновляем количество
            await db.HashSetAsync(basketKey, basketItem.Id, basketItem.Quantity);
        }
        //public async Task RemoveItemAsync(string userId, BasketItem basketItem)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    var basketKey = $"basket:{userId}";
        //    var itemInBasket = await db.HashGetAsync(basketKey, basketItem.Id);

        //    if (itemInBasket.HasValue)
        //    {
        //        int currentQuantity = (int)itemInBasket;
        //        // Уменьшаем количество или удаляем товар, если количество после уменьшения равно 0
        //        if (currentQuantity <= basketItem.Quanity)
        //        {
        //            // Если количество товара меньше или равно удаляемому, удаляем товар из корзины
        //            await db.HashDeleteAsync(basketKey, basketItem.Id);
        //        }
        //        else
        //        {
        //            // Уменьшаем количество товара в корзине
        //            await db.HashSetAsync(basketKey, basketItem.Id, currentQuantity - basketItem.Quanity);
        //        }
        //    }
        //}
        //public async Task<Dictionary<string, object>> GetBasketAsync(string userId)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();
        //    var basketKey = $"basket:{userId}";
        //    var basketData = await db.HashGetAllAsync(basketKey);

        //    var basket = new Dictionary<string, object>();
        //    using var httpClient = new HttpClient();

        //    foreach (var item in basketData)
        //    {
        //        string itemId = item.Name;
        //        int quantity = (int)item.Value;

        //        // Здесь предполагается, что у вас есть endpoint для получения данных о продукте
        //        string productApiUrl = $"http://127.0.0.1:5002/products/{itemId}";
        //        var productResponse = await httpClient.GetAsync(productApiUrl);
        //        if (productResponse.IsSuccessStatusCode)
        //        {
        //            var productData = await productResponse.Content.ReadFromJsonAsync<object>();
        //            basket.Add(itemId, new { Quantity = quantity, ProductDetails = productData });
        //        }
        //        else
        //        {

        //        }
        //    }

        //    return basket;
        //}
        public async Task<List<BasketItem>> GetBasketAsync(int userId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var basketKey = $"basket:{userId}";
            var basketData = await db.HashGetAllAsync(basketKey);

            var basket = new List<BasketItem>();
            using var httpClient = new HttpClient();

            foreach (var item in basketData)
            {
                int itemId = (int)item.Name;
                int quantity = (int)item.Value;
                BasketItem basketItem = new BasketItem{ Id = itemId, Quantity = quantity};
                basket.Add(basketItem);
            }

            return basket;
        }
        public async Task ClearBasketAsync(int userId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var basketKey = $"basket:{userId}";
            await db.KeyDeleteAsync(basketKey);
        }
    }
}
