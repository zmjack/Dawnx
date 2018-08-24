using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Dawnx
{
    public static partial class DawnString
    {
        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T GetFromJson<T>(this string @this) => JsonConvert.DeserializeObject<T>(@this);

    }
}
