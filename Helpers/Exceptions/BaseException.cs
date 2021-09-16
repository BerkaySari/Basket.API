using System;

namespace Helpers.Exceptions
{
    public class BaseException : Exception
    {
        public int ErrorCode { get; set; }
        public string Key { get; set; }

        public BaseException(string message, int errorCode) : base(message)
        {
            Key = GetType().Name;
            ErrorCode = errorCode;
        }
    }
}
