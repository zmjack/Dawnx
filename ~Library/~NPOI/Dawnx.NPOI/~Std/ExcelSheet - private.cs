namespace Dawnx.NPOI
{
    public partial class ExcelSheet
    {
        public static int GetCol(string colName) => LetterSequence.GetNumber(colName);
        public static string GetColName(int col) => LetterSequence.GetLetter(col);

        private int GetRow(string rowName) => int.Parse(rowName) - 1;
        private string GetRowName(int row) => (row + 1).ToString();

    }
}