namespace Sapling
{
    public static class SpDialog
    {
        public enum DialogType
        {
            normal, blank, large, overflow, lightbox
        }

        public class Config : ISaplingConfig
        {
            public DialogType dialogType { get; set; }
        }

    }
}
