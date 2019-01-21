namespace Dawnx.Net.Web
{
    public class FtpStateContainer
    {
        public string Encoding = DefaultEncoding;
        public string UserName = DefaultUserName;
        public string Password = DefaultPassword;
        public bool UseBinary = UseBinaryByDefault;
        public bool UsePassive = UsePassiveByDefault;
        public bool UseProxy = UseProxyByDefault;
        public string ProxyAddress = DefaultProxy.Address;
        public string ProxyUsername = DefaultProxy.Username;
        public string ProxyPassword = DefaultProxy.Password;

        public static string DefaultUserName { get; set; } = "anonymous";
        public static string DefaultPassword { get; set; } = "";
        public static bool UseBinaryByDefault { get; set; } = true;
        public static bool UsePassiveByDefault { get; set; } = true;
        public static bool UseProxyByDefault { get; set; } = false;
        public static ProxyInfo DefaultProxy { get; set; } = new ProxyInfo();
        public static string DefaultEncoding { get; set; } = "utf-8";

    }
}
