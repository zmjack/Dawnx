using System;

namespace Dawnx.NPOI
{
    public partial class ExcelSheet : ICloneable
    {
        object ICloneable.Clone() => Book.CloneSheet(SheetName);
        public ExcelSheet Clone() => (this as ICloneable).Clone() as ExcelSheet;
        public ExcelSheet Clone(string newSheetName)
        {
            var clone = Clone();
            Book.SetSheetName(Book.GetSheetIndex(clone), newSheetName);
            return clone;
        }
    }
}