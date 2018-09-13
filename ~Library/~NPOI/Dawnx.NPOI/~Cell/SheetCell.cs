using NPOI.SS.UserModel;
using System;
using System.Data;

namespace Dawnx.NPOI
{
    public partial class SheetCell
    {
        public ExcelSheet Sheet { get; private set; }
        public ICell MapedCell { get; private set; }
        public SheetCell(ExcelSheet sheet, ICell cell)
        {
            Sheet = sheet;
            MapedCell = cell;
        }

        public string CellName => Sheet.GetCellName((RowIndex, ColumnIndex));

        public static SheetCell operator |(SheetCell @this, bool value) => @this.Self(_ => _.SetValue(value));
        public static SheetCell operator |(SheetCell @this, double value) => @this.Self(_ => _.SetValue(value));
        public static SheetCell operator |(SheetCell @this, DateTime value) => @this.Self(_ => _.SetValue(value));
        public static SheetCell operator |(SheetCell @this, IRichTextString value) => @this.Self(_ => _.SetValue(value));
        public static SheetCell operator |(SheetCell @this, string value) => @this.Self(_ => _.SetValue(value));
        public static SheetCell operator |(SheetCell @this, object value) => @this.Self(_ => _.SetValue(value));

        public static implicit operator bool(SheetCell @this) => @this.MapedCell.BooleanCellValue;
        public static implicit operator double(SheetCell @this) => @this.MapedCell.NumericCellValue;
        public static implicit operator DateTime(SheetCell @this) => @this.MapedCell.DateCellValue;
        public static implicit operator string(SheetCell @this)
        {
            if (@this.MapedCell.CellType == CellType.Formula)
                return @this.MapedCell.StringCellValue;
            else return @this.ToString();
        }

        public void SetCellStyle(ICellStyle style) => MapedCell.CellStyle = style;
        public void SetCellStyle(BookCellStyle style) => SetCellStyle(style.CellStyle);
        public void SetBookCellStyle(Action<BookCellStyleApplier> initApplier)
            => SetCellStyle(Sheet.Book.BookCellStyle(initApplier).CellStyle);


        public void SetValue(object value)
        {
            switch (value)
            {
                case null: String = ""; return;
                case bool v: Boolean = v; return;
                case short v: Number = v; return;
                case ushort v: Number = v; return;
                case int v: Number = v; return;
                case uint v: Number = v; return;
                case long v: Number = v; return;
                case ulong v: Number = v; return;
                case float v: Number = v; return;
                case double v: Number = v; return;
                case DateTime v: DateTime = v; return;
                case IRichTextString v: RichTextString = v; return;
                case string v:
                    if (v.StartsWith("=")) Formula = v.Slice(1);
                    else String = v;
                    return;

                case SheetCell v when v.MapedCell.CellType == CellType.Blank: return;
                case SheetCell v when v.MapedCell.CellType == CellType.Error: return;
                case SheetCell v when v.MapedCell.CellType == CellType.Unknown: return;
                case SheetCell v when v.MapedCell.CellType == CellType.Boolean: Boolean = v.Boolean; return;
                case SheetCell v when v.MapedCell.CellType == CellType.Numeric: Number = v.Number; return;
                case SheetCell v when v.MapedCell.CellType == CellType.String: String = v.String; return;
                case SheetCell v when v.MapedCell.CellType == CellType.Formula: Formula = v.Formula; return;

                case CValue v:
                    SetValue(v.Value);
                    MapedCell.CellStyle = v.Style;
                    return;

                case object v: String = v.ToString(); return;
            }
        }
        public object GetValue()
        {
            switch (MapedCell.CellType)
            {
                case CellType.Boolean: return Boolean;
                case CellType.Numeric: return Number;
                case CellType.String:
                case CellType.Formula: return String;

                default: return "";
            }
        }

        public string Formula
        {
            get => MapedCell.CellFormula;
            set => MapedCell.SetCellFormula(value);
        }
        public bool Boolean
        {
            get => MapedCell.BooleanCellValue;
            set => MapedCell.SetCellValue(value);
        }
        public DateTime DateTime
        {
            get => MapedCell.DateCellValue;
            set
            {
                var book = Sheet.Book;
                MapedCell.SetCellValue(value);
                SetCellStyle(book.BookCellStyle(s => s.DataFormat = "yyyy-M-d"));
            }
        }
        public double Number
        {
            get => MapedCell.NumericCellValue;
            set => MapedCell.SetCellValue(value);
        }
        public IRichTextString RichTextString
        {
            get => MapedCell.RichStringCellValue;
            set => MapedCell.SetCellValue(value);
        }
        public string String
        {
            get => MapedCell.StringCellValue;
            set => MapedCell.SetCellValue(value);
        }

        public ICellStyle CellStyle
        {
            get => MapedCell.CellStyle;
            set => MapedCell.CellStyle = value;
        }

        public SheetRange Print(object[,] values) => Sheet.Print(values, true);
        public SheetRange Print(object[][] values) => Sheet.Print(values, true);
        public SheetRange Print(object[] values) => Sheet.Print(values, true);
        public SheetRange Print(DataTable table) => Sheet.Print(table, true);

        public void SetWidth(double width) => Sheet.SetColumnWidth(ColumnIndex, width);
        public void SetHeight(float height) => Sheet.GetRow(RowIndex).HeightInPoints = height;

    }
}
