using AutoMapper;
using Data.Models.Mapping.Requests;
using Data.Models.Mapping.Responses;
using Data.Models.Mapping.Wrappers;
using Data.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("basket")]
    //[ApiAuthorize(Roles = "admin, member")]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IMapper mp, IBasketService basketService) : base(mp)
        {
            _basketService = basketService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="basketItemId"></param>
        /// <returns></returns>
        [Route("~/users/{UserId}/basket/{BasketItemId}")]
        [HttpGet]
        public async Task<Response<BasketItemResponse>> GetBasketItemAsync(string userId, string basketItemId)
        {
            var basketItem = await _basketService.GetBasketItemAsync(userId, basketItemId);

            return MapResponse<BasketItemResponse>(basketItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/users/{UserId}/basket")]
        [HttpGet]
        public async Task<Response<BasketResponse>> GetBasketAsync(string userId)
        {
            var basket = await _basketService.GetBasketAsync(userId);
            
            return MapResponse<BasketResponse>(basket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("~/users/{UserId}/basket")]
        [HttpPost]
        public async Task<Response<BasketResponse>> CreateBasketItemAsync(string userId, BasketItemRequest request)
        {
            BasketItem basketItem = Map<BasketItem>(request);
            List<BasketItem> basket = await _basketService.AddBasketItemAsync(userId, basketItem);

            return MapResponse<BasketResponse>(basket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="basketItemId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("~/users/{UserId}/basket/{BasketItemId}")]
        [HttpPut]
        public async Task<Response<BasketResponse>> UpdateBasketItemAsync(string userId, string basketItemId, BasketItemRequest request)
        {
            BasketItem basketItem = Map<BasketItem>(request);
            List<BasketItem> basket = await _basketService.UpdateBasketItemAsync(userId, basketItemId, basketItem);

            return MapResponse<BasketResponse>(basket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="basketItemId"></param>
        /// <returns></returns>
        [Route("~/users/{UserId}/basket/{BasketItemId}")]
        [HttpDelete]
        public async Task<Response> DeleteBasketItemAsync(string userId, string basketItemId)
        {
            await _basketService.DeleteBasketItemAsync(userId, basketItemId);
            return EmptyResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/users/{UserId}/basket")]
        [HttpDelete]
        public async Task<Response> DeleteBasketAsync(string userId)
        {
            await _basketService.DeleteBasketAsync(userId);
            return EmptyResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("~/users/{UserId}/basket/createOrder")]
        [HttpGet]
        public async Task<Response> CreateOrderAsync(string userId)
        {
            await _basketService.CreateOrderAsync(userId);
            return EmptyResponse();
        }
    }
}
