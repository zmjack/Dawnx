using Dawnx.Data;
using Dawnx.Security.AesSecurity;
using System;

namespace Dawnx.Tools
{
    public class AesCommand : ICommand
    {
        public void Help()
        {
            throw new NotImplementedException();
        }

        public void Run(ConsoleArgs args)
        {
            var aesKey = args[1] == "hex" ? AesKey.HexString : AesKey.Base64String;

            var aes = new AesProvider();
            Console.WriteLine($"New {aesKey.ToString()}:\t{aes.ExportKey(aesKey)}");
        }

    }

}
