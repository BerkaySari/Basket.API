namespace Helpers.Exceptions
{
    public class RabbitMqCustomException : BaseException
    {
        public RabbitMqCustomException(string exception) : base("Rabbitmq hata: " + exception , 300)
        {

        }
    }
}
