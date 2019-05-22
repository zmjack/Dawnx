using Newtonsoft.Json;

namespace Dawnx.AspNetCore
{
    public static partial class DawnString
    {
        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TRet JsonX<TRet>(this string @this) => JsonConvert.DeserializeObject<TRet>(@this);

        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static object JsonX(this string @this) => JsonConvert.DeserializeObject(@this);

    }
}
