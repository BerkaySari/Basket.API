using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.Mapping.Responses
{
    public class BasketResponse : BaseResponse
    {
        public List<BasketItemResponse> Items { get; set; }
    }
}
