﻿using Microsoft.Extensions.Primitives;

namespace JoggingTime.Helpers
{
    public static class HttpRequestHelper
    {
        public static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static string GetHeaderValue(string key)
        {
            try
            {
                if (Current != null)
                {
                    Current.Request.Headers.TryGetValue(key.ToLower(), out StringValues header);
                    return header.ToString();
                }
                else
                    return "";

            }
            catch (Exception)
            {
                return "";
            }
        }
        public static bool IsHeaderContainsKey(string key)
        {
            return Current.Request?.Headers?.Any(header => header.Key.ToLower() == key.ToLower() && !string.IsNullOrEmpty(header.Value)) ?? false;
        }
    }
}
