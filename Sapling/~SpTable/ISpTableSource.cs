namespace Sapling
{
    public interface ISpTableSource
    {
        string[] Headers { get; set; }
        string[] Types { get; set; }
        object Rows { get; set; }
    }
}
