using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApp.Api.Models.Responces
{
    public class Result<T>
    {
        public static readonly Result<T> Forbidden = new Result<T>{ErrorMessage = "Forbidden",HasError = true};

        [JsonProperty]
        public bool HasError { get; private set; }
        [JsonProperty]
        public string ErrorMessage { get; private set; }
        [JsonProperty]
        public T Value { get; private set; }


        public Result()
        {
        }

        public Result(T value)
        {
            Value = value;
        }
    }
}