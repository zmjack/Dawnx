namespace Sapling
{
    public interface ISpTable
    {
        string[] Headers { get; set; }
        string[] Types { get; set; }
        object Rows { get; set; }
    }
}
