using Data.Models.Models;
using Helpers.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Tests
{
    [TestFixture]
    public class ApiTests
    {
        private IBasketService _basketService;

        private string _userId { get; set; }
        private List<BasketItem> _basket { get; set; }

        [SetUp]
        public void Setup()
        {
            #region register services
            var services = new ServiceCollection();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "redis-server:6379";
            });
            services.AddTransient<IBasketService, BasketService>();

            var serviceProvider = services.BuildServiceProvider();

            _basketService = serviceProvider.GetService<IBasketService>();
            #endregion

            #region set properties
            _userId = Guid.NewGuid().ToString();
            _basket = setTestBasket();
            #endregion
        }


        [Test]
        public void AddBasketItems_ErrorStockControl()
        {
            var basketItem = _basket[0];
            basketItem.Quantity = 10;

            var exception = Assert.ThrowsAsync<ProductNotHaveInStockException>(async () => await _basketService.AddBasketItemAsync(_userId, basketItem));
            Assert.That(exception.ErrorCode, Is.EqualTo(100));
        }

        [Test]
        public async Task AddAndDeleteBasketItems()
        {
            foreach (var basketItem in _basket)
            {
                await _basketService.AddBasketItemAsync(_userId, basketItem);
            }
            foreach (var basketItem in _basket)
            {
                await _basketService.DeleteBasketItemAsync(_userId, basketItem.Id);
            }

            Assert.Pass();
        }

        [Test]
        public async Task AddAndUpdateBasketItems()
        {
            foreach (var basketItem in _basket)
            {
                await _basketService.AddBasketItemAsync(_userId, basketItem);
            }
            foreach (var basketItem in _basket)
            {
                basketItem.Supplier = "Test Satıcı";
                await _basketService.UpdateBasketItemAsync(_userId, basketItem.Id, basketItem);
            }

            Assert.Pass();
        }

        [Test]
        public async Task AddAndGetBasket()
        {
            foreach (var basketItem in _basket)
            {
                await _basketService.AddBasketItemAsync(_userId, basketItem);
            }

            var basket = await _basketService.GetBasketAsync(_userId);

            Assert.AreEqual(basket.Count, 5);
        }

        [Test]
        public async Task AddAndGetBasketItem()
        {
            string lastItemId = "";

            foreach (var basketItem in _basket)
            {
                lastItemId = basketItem.Id;
                await _basketService.AddBasketItemAsync(_userId, basketItem);
            }

            var item = await _basketService.GetBasketItemAsync(_userId, lastItemId);

            Assert.AreEqual(lastItemId, item.Id);
        }


        #region private methods
        private List<BasketItem> setTestBasket()
        { 
            return new List<BasketItem> 
            {
                new BasketItem
                {
                    UserId = _userId,
                    Email = "test@gmail.com",
                    ProductId = "c0033652-5010-4718-9845-0b4c87783780",
                    ProductName = "Puslu Kıtalar Atlası",
                    Supplier = "A Yayınevi",
                    Quantity = 1,
                    Price = 40.00M,
                    DeliveryDateRange = "20 - 23 Eylül",
                    Color = "Tek Renk"
                },
                new BasketItem
                {
                    UserId = _userId,
                    Email = "test@gmail.com",
                    ProductId = "a671ef00-14cf-40ea-a162-ebf7fc2a8282",
                    ProductName = "Suskunlar",
                    Supplier = "B Yayınevi",
                    Quantity = 1,
                    Price = 50.00M,
                    DeliveryDateRange = "20 - 23 Eylül",
                    Color = "Tek Renk"
                },
                new BasketItem
                {
                    UserId = _userId,
                    Email = "test@gmail.com",
                    ProductId = "c9157ad8-d9e4-4d15-8414-37b1cae2feee",
                    ProductName = "Amat",
                    Supplier = "C Yayınevi",
                    Quantity = 1,
                    Price = 30.00M,
                    DeliveryDateRange = "21 - 22 Eylül",
                    Color = "Tek Renk"
                },
                new BasketItem
                {
                    UserId = _userId,
                    Email = "test@gmail.com",
                    ProductId = "4cb7c8fd-90bb-4d16-8acd-07a603b74efd",
                    ProductName = "Yedinci Gün",
                    Supplier = "D Yayınevi",
                    Quantity = 2,
                    Price = 40.00M,
                    DeliveryDateRange = "22 - 25 Eylül",
                    Color = "Tek Renk"
                },
                new BasketItem
                {
                    UserId = _userId,
                    Email = "test@gmail.com",
                    ProductId = "4c6327fc-dbb1-4568-a236-00bc8a4055f6",
                    ProductName = "Galiz Kahraman",
                    Supplier = "E Yayınevi",
                    Quantity = 3,
                    Price = 20.00M,
                    DeliveryDateRange = "25 - 27 Eylül",
                    Color = "Tek Renk"
                }
            };
        
        }
        #endregion
    }
}
