using System.Collections.Generic;

namespace Data.Models.Mapping.Requests
{
    public class BasketRequest
    {
        public List<BasketItemRequest> Items { get; set; }
    }
}
