namespace Helpers.Exceptions
{
    public class BasketIsEmptyException : BaseException
    {
        public BasketIsEmptyException() : base("Sepet boş.", 200)
        {

        }
    }
}
