using Dawnx.Security.AesSecurity;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        public static void Aes(AesKey aesKey)
        {
            var aes = new AesProvider();
            Con.Print($"New {aesKey.ToString()}:\t{aes.ExportKey(aesKey)}").Line();
        }

    }
}
