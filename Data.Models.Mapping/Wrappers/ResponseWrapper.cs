using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Data.Models.Mapping.Wrappers
{
    public class Response
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ApiResponseStatus Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string StackTrace { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrorCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Error> Errors { get; set; }

        public override string ToString()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(this, jsonSerializerSettings);
        }

        public static Response Create(Exception exception)
        {
            return new Response()
            {
                Status = ApiResponseStatus.Error,
                ErrorCode = exception.HResult,
                Message = exception.Message,
            };
        }

        public static Response Create(string message)
        {
            return new Response()
            {
                Status = ApiResponseStatus.Ok,
                Message = message
            };
        }

        public static Response Create()
        {
            return new Response()
            {
                Status = ApiResponseStatus.Ok
            };
        }
    }

    public class Response<T> : Response
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public static Response<T> Create(T data)
        {
            return new Response<T>()
            {
                Data = data,
                Status = ApiResponseStatus.Ok
            };
        }
    }

    public enum ApiResponseStatus
    {
        Error = -1,
        Ok = 1,
    }

    public class Error
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
    }
}
