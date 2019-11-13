using Dawnx.Data;
using Dawnx.Security.AesSecurity;
using System;

namespace Dawnx.Tools
{
    public class AesCommand : ICommand
    {
        public void PrintUsage()
        {
            throw new NotImplementedException();
        }

        public void Run(ConsoleArgs cargs)
        {
            var aesKey = cargs[1] == "hex" ? AesKey.HexString : AesKey.Base64String;

            var aes = new AesProvider();
            Console.WriteLine($"New {aesKey.ToString()}:\t{aes.ExportKey(aesKey)}");
        }

    }

}
