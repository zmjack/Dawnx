using Dawnx.AspNetCore;
using System;

namespace Dawnx.Tools
{
    public static class ConsoleUtility
    {
        public static bool AskYN(string hint)
        {
            Console.Write($"{hint} (Y/N): ");

            Console.CursorVisible = true;
            string ynSetup;
            do { ynSetup = Console.ReadKey().KeyChar.ToString().ToLower(); }
            while (!ynSetup.In("y", "n"));
            Console.CursorVisible = false;

            Console.WriteLine();
            return ynSetup == "y";
        }

        public static bool ConfirmUseOnlineService()
        {
            Console.WriteLine("This is a online sevice, you will upload your file to dawnx.net.");
            Console.WriteLine("  BUT, we will not collect the documents which you provided.");
            Console.WriteLine();

            return AskYN("Are you sure to use this service, and upload your file?");
        }

        public static void PrintErrorMessage(SimpleResponse resp)
        {
            Console.WriteLine($"  state: {resp.state}");
            Console.WriteLine($"  status: {resp.status}");
            Console.WriteLine($"  message: {resp.message}");
        }

    }
}
