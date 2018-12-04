using System;

namespace Dawnx.Tools
{
    public static class AlertUtility
    {
        public static bool ConfirmUseOnlineService()
        {
            Console.WriteLine("This is a online sevice, you will upload your file to dawnx.net.");
            Console.WriteLine("  BUT, we will NOT COLLECT the files you upload.");
            Console.WriteLine();

            return AskYN("Are you sure to use this service, and upload your file?");
        }

        public static void PrintErrorMessage(JSend resp)
        {
            Console.WriteLine($"  status:  {resp.status}");
            Console.WriteLine($"  data:    {resp.data}");
            Console.WriteLine($"  code:    {resp.code}");
            Console.WriteLine($"  message: {resp.message}");
        }

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

    }
}
