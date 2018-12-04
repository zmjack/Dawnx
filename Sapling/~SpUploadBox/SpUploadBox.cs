namespace Sapling
{
    public static class SpUploadBox
    {
        public class Config : ISaplingConfig
        {
            public string StatUrl { get; set; }
            public string PreviewUrl { get; set; }
            public string SubmitUrl { get; set; }
        }
    }
}
