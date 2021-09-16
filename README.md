# Basket.API

Asp.Net Core 3.1 ve redis ile sepete ürün ekleme, silme, update etme gibi işlemlerin yapıldığı api.
sepet onaylanırsa createOrder endpointi üzerinden mass transit kullanarak event publish etmektedir.
