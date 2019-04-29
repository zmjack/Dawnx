using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace Dawnx.NPOI
{
    public partial class SheetCell : ICell
    {
        public int ColumnIndex => MapedCell.ColumnIndex;

        public int RowIndex => MapedCell.RowIndex;

        public IRow Row => MapedCell.Row;

        public CellType CellType => MapedCell.CellType;

        public CellType CachedFormulaResultType => MapedCell.CachedFormulaResultType;

        public string CellFormula { get => MapedCell.CellFormula; set => MapedCell.CellFormula = value; }

        public double NumericCellValue => MapedCell.NumericCellValue;

        public DateTime DateCellValue => MapedCell.DateCellValue;

        public IRichTextString RichStringCellValue => MapedCell.RichStringCellValue;

        public byte ErrorCellValue => MapedCell.ErrorCellValue;

        public string StringCellValue => MapedCell.StringCellValue;

        public bool BooleanCellValue => MapedCell.BooleanCellValue;

        public IComment CellComment { get => MapedCell.CellComment; set => MapedCell.CellComment = value; }
        public IHyperlink Hyperlink { get => MapedCell.Hyperlink; set => MapedCell.Hyperlink = value; }

        public CellRangeAddress ArrayFormulaRange => MapedCell.ArrayFormulaRange;

        public bool IsPartOfArrayFormulaGroup => MapedCell.IsPartOfArrayFormulaGroup;

        public bool IsMergedCell => MapedCell.IsMergedCell;

        ISheet ICell.Sheet => MapedCell.Sheet;

        public ICell CopyCellTo(int targetIndex)
        {
            return MapedCell.CopyCellTo(targetIndex);
        }

        public CellType GetCachedFormulaResultTypeEnum()
        {
            return MapedCell.GetCachedFormulaResultTypeEnum();
        }

        public void RemoveCellComment()
        {
            MapedCell.RemoveCellComment();
        }

        public void RemoveHyperlink()
        {
            MapedCell.RemoveHyperlink();
        }

        public void SetAsActiveCell()
        {
            MapedCell.SetAsActiveCell();
        }

        public void SetCellErrorValue(byte value)
        {
            MapedCell.SetCellErrorValue(value);
        }

        public void SetCellFormula(string formula)
        {
            MapedCell.SetCellFormula(formula);
        }

        public void SetCellType(CellType cellType)
        {
            MapedCell.SetCellType(cellType);
        }

        public void SetCellValue(double value)
        {
            MapedCell.SetCellValue(value);
        }

        public void SetCellValue(DateTime value)
        {
            MapedCell.SetCellValue(value);
        }

        public void SetCellValue(IRichTextString value)
        {
            MapedCell.SetCellValue(value);
        }

        public void SetCellValue(string value)
        {
            MapedCell.SetCellValue(value);
        }

        public void SetCellValue(bool value)
        {
            MapedCell.SetCellValue(value);
        }
    }
}
