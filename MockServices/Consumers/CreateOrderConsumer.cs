using System;
using System.Collections.Generic;
using System.Text;

namespace MockServices.Consumers
{
    // MockServices projesinin sepet onaylanınca siparişin oluşturulduğu farklı bir mikroservis olduğunu düşünelim.
    // Bu classta sepet mikroservisinden gelen datalarla bir sipariş oluşturulduğu düşünülerek geriye bir event fırlatacak ve o kullanıcı için sepetin temizlenmesini sağlayacak.
    public class CreateOrderConsumer
    {
    }
}
