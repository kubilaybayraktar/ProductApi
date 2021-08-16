using Newtonsoft.Json;
using System;

namespace Product.Api
{
    public class UserFriendlyError : Exception
    {
        public int StatusCode { get; set; } = 400;
        public string Text { get; set; }
        public UserFriendlyError(string message, int statusCode = 400)
        {
            StatusCode = statusCode;
            Text = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
