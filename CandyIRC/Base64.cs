using System;

namespace CandyIRC
{
    public static class Base64
    {
        public static string Decode(string encodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(encodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}