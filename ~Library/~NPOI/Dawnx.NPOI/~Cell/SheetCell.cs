using NPOI.SS.UserModel;
using System;
using System.Data;
using System.Drawing;

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
        public void SetCStyle(CStyle style) => SetCellStyle(style.CellStyle);
        public void SetCStyle(Action<CStyleApplier> initApplier) => SetCellStyle(Sheet.Book.CStyle(initApplier).CellStyle);
        public CStyle GetCStyle() => new CStyle(Sheet.Book, CellStyle);

        public void SetValue(object value)
        {
            switch (value)
            {
                case null: String = ""; break;
                case bool v: Boolean = v; break;
                case short v: Number = v; break;
                case ushort v: Number = v; break;
                case int v: Number = v; break;
                case uint v: Number = v; break;
                case long v: Number = v; break;
                case ulong v: Number = v; break;
                case float v: Number = v; break;
                case double v: Number = v; break;
                case DateTime v: DateTime = v; break;
                case IRichTextString v: RichTextString = v; break;
                case string v:
                    if (v.StartsWith("=")) Formula = v.Slice(1);
                    else String = v;
                    break;

                case SheetCell v when v.MapedCell.CellType == CellType.Blank: break;
                case SheetCell v when v.MapedCell.CellType == CellType.Error: break;
                case SheetCell v when v.MapedCell.CellType == CellType.Unknown: break;
                case SheetCell v when v.MapedCell.CellType == CellType.Boolean: Boolean = v.Boolean; break;
                case SheetCell v when v.MapedCell.CellType == CellType.Numeric: Number = v.Number; break;
                case SheetCell v when v.MapedCell.CellType == CellType.String: String = v.String; break;
                case SheetCell v when v.MapedCell.CellType == CellType.Formula: Formula = v.Formula; break;

                case CValue v:
                    SetValue(v.Value);
                    SetCStyle(v.Style);
                    break;

                case object v: String = v.ToString(); break;
            }

            if (Sheet.AutoSizeColumns)
            {
                //自动列宽
                var graphics = Graphics.FromImage(new Bitmap(100, 100));
                var sizeF = graphics.MeasureString("地地", new Font(GetCStyle().Font.FontName, GetCStyle().Font.FontSize));
                Sheet.GetColumnWidth(ColumnIndex);
                Sheet.SetColumnWidth();
                var w = sizeF.Width;
                GetValue().ToString()
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
                SetCStyle(book.CStyle(s => s.DataFormat = "yyyy-M-d"));
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

        public void SetWidth(double width) => Sheet.SetColumnExcelWidth(ColumnIndex, width);
        public void SetHeight(float height) => Sheet.GetRow(RowIndex).HeightInPoints = height;

    }
}
