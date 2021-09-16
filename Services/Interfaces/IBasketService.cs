using Data.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBasketService : IDataService<BasketItem>
    {
        Task<BasketItem> GetBasketItemAsync(string userId, string basketItemId);
        Task<List<BasketItem>> GetBasketAsync(string userId);
        Task<List<BasketItem>> AddBasketItemAsync(string userId, BasketItem model);
        Task<List<BasketItem>> UpdateBasketItemAsync(string userId, string basketItemId, BasketItem model);
        Task DeleteBasketItemAsync(string userId, string basketItemId);
        Task DeleteBasketAsync(string userId);
        Task CreateOrderAsync(string userId);
    }
}
