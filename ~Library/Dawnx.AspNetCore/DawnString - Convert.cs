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
        public static TRet JsonFor<TRet>(this string @this) => JsonConvert.DeserializeObject<TRet>(@this);

        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static object JsonFor(this string @this) => JsonConvert.DeserializeObject(@this);

    }
}
