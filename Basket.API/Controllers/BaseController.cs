using AutoMapper;
using Data.Models.Mapping.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/Base")]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;

        public BaseController(IMapper mp)
        {
            _mapper = mp;
        }


        protected Response<T> MapResponse<T>(object value)
        {
            return Response<T>.Create(_mapper.Map<T>(value));
        }

        protected Response<T> CreateResponse<T>(T value)
        {
            return Response<T>.Create(value);
        }

        protected TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }

        protected TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        protected Response EmptyResponse()
        {
            return Data.Models.Mapping.Wrappers.Response.Create();
        }

        protected Response MessageResponse(string message)
        {
            return Data.Models.Mapping.Wrappers.Response.Create(message);
        }
    }
}
