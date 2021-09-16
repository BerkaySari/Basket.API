using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.Interfaces
{
    public interface IBasketItem : IUserModel<string>, IBasketItemModel<string>
    {
    }
}
