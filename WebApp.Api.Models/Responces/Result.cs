using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApp.Api.Models.Responces
{
    public class Result<T>
    {
        public static readonly Result<T> Forbidden = new Result<T> { ErrorMessage = "Forbidden", HasError = true };
        public static readonly Result<T> Unauthorized = new Result<T> { ErrorMessage = "Unauthorized", HasError = true };

        [JsonProperty]
        public bool HasError { get; set; }
        [JsonProperty]
        public string ErrorMessage { get; set; }
        [JsonProperty]
        public T Value { get; set; }


        public Result()
        {
        }

        public Result(T value)
        {
            Value = value;
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }
    }
}