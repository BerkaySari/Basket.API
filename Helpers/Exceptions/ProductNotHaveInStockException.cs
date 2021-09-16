namespace Helpers.Exceptions
{
    public class ProductNotHaveInStockException : BaseException
    {
        public ProductNotHaveInStockException() : base("Sepete eklemek istediğiniz ürün stokta bulunmuyor.", 100)
        {
        }
    }
}
