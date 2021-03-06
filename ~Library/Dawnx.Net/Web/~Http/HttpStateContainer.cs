﻿using System.Collections.Generic;
using System.Net;

namespace Dawnx.Net.Web
{
    public class HttpStateContainer
    {
        public string Encoding = DefaultEncoding;
        public string UserAgent = DefaultUserAgent;
        public bool UseProxy = ProxyEnabledByDefault;
        public string ProxyAddress = DefaultProxy.Address;
        public string ProxyUsername = DefaultProxy.Username;
        public string ProxyPassword = DefaultProxy.Password;
        public bool SystemLogin = SystemLoginByDefault;
        public CookieContainer Cookies = new CookieContainer();
        public Dictionary<string, string> Headers;

        public static bool ProxyEnabledByDefault { get; set; } = false;
        public static ProxyInfo DefaultProxy { get; set; } = new ProxyInfo();
        public static string DefaultUserAgent { get; set; } = "";
        public static string DefaultEncoding { get; set; } = "utf-8";
        public static bool SystemLoginByDefault { get; set; } = false;

    }
}
