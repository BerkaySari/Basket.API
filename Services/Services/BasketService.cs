using Data.Models.Models;
using Helpers.Exceptions;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BasketService : RedisDataService<BasketItem>, IBasketService
    {
        public BasketService(IDistributedCache context) : base(context)
        {
        }


        public async Task<BasketItem> GetBasketItemAsync(string userId, string basketItemId)
        {
            string key = getKey(userId);

            var basket = await GetItemListAsync(key);

            if (basket != null && basket.Any())
            {
                var item = basket.FirstOrDefault(basketItem => basketItem.Id == basketItemId);

                if (item != null)
                {
                    return item;
                }
            }

            return new BasketItem();
        }

        public async Task<List<BasketItem>> GetBasketAsync(string userId)
        {
            string key = getKey(userId);

            return await GetItemListAsync(key);
        }

        public async Task<List<BasketItem>> AddBasketItemAsync(string userId, BasketItem model)
        {
            if(!isProductHaveInStock(model))
            {
                throw new ProductNotHaveInStockException();
            }

            string key = getKey(userId);

            var basket = await GetItemListAsync(key);

            //sepet varsa ürünü sepete ekle yoksa yeni sepet(ürün listesi) oluştur.
            if (basket != null && basket.Any())
            {
                model.LastModifiedDate = DateTimeOffset.UtcNow;
                basket.Add(model);
            }
            else
            {
                basket = new List<BasketItem> { model };
            }

            return await CreateOrUpdateItemListAsync(key, basket);
        }

        public async Task<List<BasketItem>> UpdateBasketItemAsync(string userId, string basketItemId, BasketItem model)
        {
            if (!isProductHaveInStock(model))
            {
                throw new ProductNotHaveInStockException();
            }

            string key = getKey(userId);

            var basket = await GetItemListAsync(key);

            if (basket != null && basket.Any())
            {
                basket.Where(basketItem => basketItem.Id == basketItemId).ToList().ForEach(basketItem =>
                {
                    basketItem.Email = model.Email;
                    basketItem.ProductId = model.ProductId;
                    basketItem.ProductName = model.ProductName;
                    basketItem.Supplier = model.Supplier;
                    basketItem.Quantity = model.Quantity;
                    basketItem.Price = model.Price;
                    basketItem.DeliveryDateRange = model.DeliveryDateRange;
                    basketItem.Color = model.Color;
                    basketItem.LastModifiedDate = DateTimeOffset.UtcNow;
                });
            }

            return await CreateOrUpdateItemListAsync(key, basket);
        }

        public async Task DeleteBasketItemAsync(string userId, string basketItemId)
        {
            string key = getKey(userId);

            var basket = await GetItemListAsync(key);

            if (basket != null && basket.Any())
            {
                var item = basket.FirstOrDefault(basketItem => basketItem.Id == basketItemId);

                if (item != null)
                {
                    basket.Remove(item);
                    await CreateOrUpdateItemListAsync(key, basket);
                }
            }
        }

        public async Task DeleteBasketAsync(string userId)
        {
            string key = getKey(userId);

            await DeleteItemAsync(key);
        }


        public async Task CreateOrderAsync(string userId)
        {
            //var basket = await GetBasketAsync(userId);
            //if (!basket.Any())
            //{
            //    throw new BasketIsEmptyException();
            //}

            //try
            //{
            //    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
            //    var sendEndpoint = _sendEndpointProvider.GetSendEndpoint(uri);
            //    sendEndpoint.Send<IBasketItem>(basket[0]);
            //}
            //catch (Exception ex)
            //{
            //    throw new RabbitMqCustomException(ex.Message);
            //}
        }


        #region private methods
        private string getKey(string suffix)
        {
            return "BasketItem/" + suffix;
        }

        private bool isProductHaveInStock(BasketItem model)
        {
            //mock stock control
            return model.Quantity < 5;
        }
        #endregion
    }
}
