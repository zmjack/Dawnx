using Dawnx.Security.AesSecurity;
using DotNetCli;
using Ink;
using System;

namespace Dawnx.Tools
{
    public class AesCommand : ICommand
    {
        public void PrintUsage()
        {
            throw new NotImplementedException();
        }

        public void Run(string[] args)
        {
            var conArgs = new ConArgs(args, "-");
            var aesKey = conArgs[1] == "hex" ? AesKey.HexString : AesKey.Base64String;

            var aes = new AesProvider();
            Console.WriteLine($"New {aesKey.ToString()}:\t{aes.ExportKey(aesKey)}");
        }

    }

}
