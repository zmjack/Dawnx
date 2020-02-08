using Frontend;
using NEcho;

namespace Dawnx.Tools
{
    public static class AlertUtility
    {
        public static bool ConfirmUseOnlineService()
        {
            Echo
                .Print(
                    $"This is a online sevice, you will upload your file to dawnx.net." +
                    $"  BUT, we will NOT COLLECT the files you upload.").Line()
                .AskYN("Are you sure to use this service, and upload your file?", out var ret);

            return ret;
        }

        public static void PrintErrorMessage(JSend resp)
        {
            Echo.Print(
                $"  status:  {resp.status}" +
                $"  data:    {resp.data}" +
                $"  code:    {resp.code}" +
                $"  message: {resp.message}");
        }

    }
}
