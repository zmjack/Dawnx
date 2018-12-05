namespace Sapling
{
    public interface ISpTableSource
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int PageCount { get; set; }
        string[] Headers { get; set; }
        string[] Types { get; set; }
        object Rows { get; set; }
    }
}
