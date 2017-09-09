using System.Collections.Specialized;


namespace refactor_me.Helper
{
    public class HttpHelpers
    {
        public static string GetQueryString(NameValueCollection collection, string queryString)
        {
            var result = string.Empty;
            foreach (string key in collection)
            {
                if (key.ToLower() == queryString)
                {
                    result = collection[key];
                    break;
                }
            }
            return result;
        }
    }
}