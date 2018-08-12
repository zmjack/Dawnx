using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Dawnx.AspNetCore
{
    public class QueryStringContainer
    {
        private Dictionary<string, object> Source;

        public QueryStringContainer(IQueryCollection query)
        {
            Source = query.ToDictionary(x => x.Key, x => (object)x.Value.ToString());
        }

        public QueryStringContainer Set(string key, object value)
        {
            Source[key] = value;
            return this;
        }
        
        public override string ToString()
        {
            return Source.Select(pair
                => $"{WebUtility.UrlEncode(pair.Key)}={WebUtility.UrlEncode(Source[pair.Key].ToString())}").Join("&");
        }

    }
}
