namespace Sapling
{
    public interface ISaplingTable
    {
        string[] Headers { get; set; }
        string[] Types { get; set; }
        object Rows { get; set; }
    }
}
