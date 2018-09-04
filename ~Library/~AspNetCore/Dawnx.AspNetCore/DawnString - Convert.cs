﻿using Newtonsoft.Json;
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
        public static TRet GetFromJson<TRet>(this string @this) => JsonConvert.DeserializeObject<TRet>(@this);

        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static object GetFromJson(this string @this) => JsonConvert.DeserializeObject(@this);

    }
}
