using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Dawnx.NPOI
{
    public partial class ExcelSheet : ISheet
    {
        public int PhysicalNumberOfRows => MapedSheet.PhysicalNumberOfRows;

        public int FirstRowNum => MapedSheet.FirstRowNum;

        public int LastRowNum => MapedSheet.LastRowNum;

        public bool ForceFormulaRecalculation { get => MapedSheet.ForceFormulaRecalculation; set => MapedSheet.ForceFormulaRecalculation = value; }
        public int DefaultColumnWidth { get => MapedSheet.DefaultColumnWidth; set => MapedSheet.DefaultColumnWidth = value; }
        public short DefaultRowHeight { get => MapedSheet.DefaultRowHeight; set => MapedSheet.DefaultRowHeight = value; }
        public float DefaultRowHeightInPoints { get => MapedSheet.DefaultRowHeightInPoints; set => MapedSheet.DefaultRowHeightInPoints = value; }
        public bool HorizontallyCenter { get => MapedSheet.HorizontallyCenter; set => MapedSheet.HorizontallyCenter = value; }
        public bool VerticallyCenter { get => MapedSheet.VerticallyCenter; set => MapedSheet.VerticallyCenter = value; }

        public int NumMergedRegions => MapedSheet.NumMergedRegions;

        public bool DisplayZeros { get => MapedSheet.DisplayZeros; set => MapedSheet.DisplayZeros = value; }
        public bool Autobreaks { get => MapedSheet.Autobreaks; set => MapedSheet.Autobreaks = value; }
        public bool DisplayGuts { get => MapedSheet.DisplayGuts; set => MapedSheet.DisplayGuts = value; }
        public bool FitToPage { get => MapedSheet.FitToPage; set => MapedSheet.FitToPage = value; }
        public bool RowSumsBelow { get => MapedSheet.RowSumsBelow; set => MapedSheet.RowSumsBelow = value; }
        public bool RowSumsRight { get => MapedSheet.RowSumsRight; set => MapedSheet.RowSumsRight = value; }
        public bool IsPrintGridlines { get => MapedSheet.IsPrintGridlines; set => MapedSheet.IsPrintGridlines = value; }

        public IPrintSetup PrintSetup => MapedSheet.PrintSetup;

        public IHeader Header => MapedSheet.Header;

        public IFooter Footer => MapedSheet.Footer;

        public bool Protect => MapedSheet.Protect;

        public bool ScenarioProtect => MapedSheet.ScenarioProtect;

        public short TabColorIndex { get => MapedSheet.TabColorIndex; set => MapedSheet.TabColorIndex = value; }

        public IDrawing DrawingPatriarch => MapedSheet.DrawingPatriarch;

        public short TopRow { get => MapedSheet.TopRow; set => MapedSheet.TopRow = value; }
        public short LeftCol { get => MapedSheet.LeftCol; set => MapedSheet.LeftCol = value; }

        public PaneInformation PaneInformation => MapedSheet.PaneInformation;

        public bool DisplayGridlines { get => MapedSheet.DisplayGridlines; set => MapedSheet.DisplayGridlines = value; }
        public bool DisplayFormulas { get => MapedSheet.DisplayFormulas; set => MapedSheet.DisplayFormulas = value; }
        public bool DisplayRowColHeadings { get => MapedSheet.DisplayRowColHeadings; set => MapedSheet.DisplayRowColHeadings = value; }
        public bool IsActive { get => MapedSheet.IsActive; set => MapedSheet.IsActive = value; }

        public int[] RowBreaks => MapedSheet.RowBreaks;

        public int[] ColumnBreaks => MapedSheet.ColumnBreaks;

        public IWorkbook Workbook => MapedSheet.Workbook;

        public string SheetName => MapedSheet.SheetName;

        public bool IsSelected { get => MapedSheet.IsSelected; set => MapedSheet.IsSelected = value; }

        public ISheetConditionalFormatting SheetConditionalFormatting => MapedSheet.SheetConditionalFormatting;

        public bool IsRightToLeft { get => MapedSheet.IsRightToLeft; set => MapedSheet.IsRightToLeft = value; }
        public CellRangeAddress RepeatingRows { get => MapedSheet.RepeatingRows; set => MapedSheet.RepeatingRows = value; }
        public CellRangeAddress RepeatingColumns { get => MapedSheet.RepeatingColumns; set => MapedSheet.RepeatingColumns = value; }

        public int AddMergedRegion(CellRangeAddress region) => MapedSheet.AddMergedRegion(region);

        public void AddValidationData(IDataValidation dataValidation) => MapedSheet.AddValidationData(dataValidation);

        public void AutoSizeColumn(int column) => MapedSheet.AutoSizeColumn(column);

        public void AutoSizeColumn(int column, bool useMergedCells) => MapedSheet.AutoSizeColumn(column, useMergedCells);

        public IRow CopyRow(int sourceIndex, int targetIndex) => MapedSheet.CopyRow(sourceIndex, targetIndex);

        public ISheet CopySheet(string Name) => MapedSheet.CopySheet(Name);

        public ISheet CopySheet(string Name, bool copyStyle) => MapedSheet.CopySheet(Name, copyStyle);

        public IDrawing CreateDrawingPatriarch() => MapedSheet.CreateDrawingPatriarch();

        public void CreateFreezePane(int colSplit, int rowSplit, int leftmostColumn, int topRow)
            => MapedSheet.CreateFreezePane(colSplit, rowSplit, leftmostColumn, topRow);

        public void CreateFreezePane(int colSplit, int rowSplit) => MapedSheet.CreateFreezePane(colSplit, rowSplit);

        public IRow CreateRow(int rownum) => MapedSheet.CreateRow(rownum);

        public void CreateSplitPane(int xSplitPos, int ySplitPos, int leftmostColumn, int topRow, PanePosition activePane)
            => MapedSheet.CreateSplitPane(xSplitPos, ySplitPos, leftmostColumn, topRow, activePane);

        public IComment GetCellComment(int row, int column) => MapedSheet.GetCellComment(row, column);

        public int GetColumnOutlineLevel(int columnIndex) => MapedSheet.GetColumnOutlineLevel(columnIndex);

        public ICellStyle GetColumnStyle(int column) => MapedSheet.GetColumnStyle(column);

        public float GetColumnWidthInPixels(int columnIndex) => MapedSheet.GetColumnWidthInPixels(columnIndex);

        public IDataValidationHelper GetDataValidationHelper() => MapedSheet.GetDataValidationHelper();

        public List<IDataValidation> GetDataValidations() => MapedSheet.GetDataValidations();

        public IEnumerator GetEnumerator() => MapedSheet.GetEnumerator();

        public double GetMargin(MarginType margin) => MapedSheet.GetMargin(margin);

        public CellRangeAddress GetMergedRegion(int index) => MapedSheet.GetMergedRegion(index);

        public IRow GetRow(int rownum) => MapedSheet.GetRow(rownum);

        public IEnumerator GetRowEnumerator() => MapedSheet.GetRowEnumerator();

        public void GroupColumn(int fromColumn, int toColumn) => MapedSheet.GroupColumn(fromColumn, toColumn);

        public void GroupRow(int fromRow, int toRow) => MapedSheet.GroupRow(fromRow, toRow);

        public bool IsColumnBroken(int column) => MapedSheet.IsColumnBroken(column);

        public bool IsColumnHidden(int columnIndex) => MapedSheet.IsColumnHidden(columnIndex);

        public bool IsMergedRegion(CellRangeAddress mergedRegion) => MapedSheet.IsMergedRegion(mergedRegion);

        public bool IsRowBroken(int row) => MapedSheet.IsRowBroken(row);

        public void ProtectSheet(string password) => MapedSheet.ProtectSheet(password);

        public ICellRange<ICell> RemoveArrayFormula(ICell cell) => MapedSheet.RemoveArrayFormula(cell);

        public void RemoveColumnBreak(int column) => MapedSheet.RemoveColumnBreak(column);

        public void RemoveMergedRegion(int index) => MapedSheet.RemoveMergedRegion(index);

        public void RemoveRow(IRow row) => MapedSheet.RemoveRow(row);

        public void RemoveRowBreak(int row) => MapedSheet.RemoveRowBreak(row);

        public void SetActive(bool value) => MapedSheet.SetActive(value);

        public void SetActiveCell(int row, int column) => MapedSheet.SetActiveCell(row, column);

        public void SetActiveCellRange(int firstRow, int lastRow, int firstColumn, int lastColumn)
            => MapedSheet.SetActiveCellRange(firstRow, lastRow, firstColumn, lastColumn);

        public void SetActiveCellRange(List<CellRangeAddress8Bit> cellranges, int activeRange, int activeRow, int activeColumn)
            => MapedSheet.SetActiveCellRange(cellranges, activeRange, activeRow, activeColumn);

        public ICellRange<ICell> SetArrayFormula(string formula, CellRangeAddress range) => MapedSheet.SetArrayFormula(formula, range);

        public IAutoFilter SetAutoFilter(CellRangeAddress range) => MapedSheet.SetAutoFilter(range);

        public void SetColumnBreak(int column) => MapedSheet.SetColumnBreak(column);

        public void SetColumnGroupCollapsed(int columnNumber, bool collapsed) => MapedSheet.SetColumnGroupCollapsed(columnNumber, collapsed);

        public void SetColumnHidden(int columnIndex, bool hidden) => MapedSheet.SetColumnHidden(columnIndex, hidden);

        void ISheet.SetColumnWidth(int columnIndex, int width) => MapedSheet.SetColumnWidth(columnIndex, width);

        int ISheet.GetColumnWidth(int columnIndex) => MapedSheet.GetColumnWidth(columnIndex);

        public void SetDefaultColumnStyle(int column, ICellStyle style) => MapedSheet.SetDefaultColumnStyle(column, style);

        public void SetMargin(MarginType margin, double size) => MapedSheet.SetMargin(margin, size);

        public void SetRowBreak(int row) => MapedSheet.SetRowBreak(row);

        public void SetRowGroupCollapsed(int row, bool collapse) => MapedSheet.SetRowGroupCollapsed(row, collapse);

        public void SetZoom(int numerator, int denominator) => MapedSheet.SetZoom(numerator, denominator);

        public void ShiftRows(int startRow, int endRow, int n) => MapedSheet.ShiftRows(startRow, endRow, n);

        public void ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight)
            => MapedSheet.ShiftRows(startRow, endRow, n, copyRowHeight, resetOriginalRowHeight);

        public void ShowInPane(int toprow, int leftcol) => MapedSheet.ShowInPane(toprow, leftcol);

        public void ShowInPane(short toprow, short leftcol) => MapedSheet.ShowInPane(toprow, leftcol);

        public void UngroupColumn(int fromColumn, int toColumn) => MapedSheet.UngroupColumn(fromColumn, toColumn);

        public void UngroupRow(int fromRow, int toRow) => MapedSheet.UngroupRow(fromRow, toRow);

    }
}