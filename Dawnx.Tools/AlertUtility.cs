using System;

namespace Dawnx.Tools
{
    public static class AlertUtility
    {
        public static bool ConfirmUseOnlineService()
        {
            Con
                .PrintLine(@"This is a online sevice, you will upload your file to dawnx.net.")
                .PrintLine("  BUT, we will NOT COLLECT the files you upload.")
                .Line()
                .AskYN("Are you sure to use this service, and upload your file?", out var ret);

            return ret;
        }

        public static void PrintErrorMessage(JSend resp)
        {
            Con.PrintLine($"  status:  {resp.status}");
            Con.PrintLine($"  data:    {resp.data}");
            Con.PrintLine($"  code:    {resp.code}");
            Con.PrintLine($"  message: {resp.message}");
        }
        
    }
}
