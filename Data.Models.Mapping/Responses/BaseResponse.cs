using System;

namespace Data.Models.Mapping.Responses
{
    public interface IBaseResponse<TKey>
    {
        TKey Id { get; set; }
        DateTimeOffset CreateDate { get; set; }
    }

    public abstract class BaseResponse<TKey> : IBaseResponse<TKey>
    {
        public TKey Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }

    public class BaseResponse : BaseResponse<string>
    {
        public BaseResponse()
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTimeOffset.UtcNow;
        }
    }
}
