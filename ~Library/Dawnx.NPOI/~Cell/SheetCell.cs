using NPOI.SS.UserModel;
using NStandard;
using System;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;

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
        public void UpdateCStyle(Action<CStyleApplier> initApplier)
        {
            var applier = GetCStyle().GetApplier().Then(x => initApplier(x));
            SetCellStyle(Sheet.Book.CStyle(applier).CellStyle);
        }

        public void SetValue(object value)
        {
            switch (value)
            {
                case null: SetValue(null as string); break;
                case bool v: SetValue(v); break;

                case short v: SetValue(v); break;
                case ushort v: SetValue(v); break;
                case int v: SetValue(v); break;
                case uint v: SetValue(v); break;
                case long v: SetValue(v); break;
                case ulong v: SetValue(v); break;
                case float v: SetValue(v); break;
                case double v: SetValue(v); break;
                case DateTime v: SetValue(v); break;
                case IRichTextString v: SetValue(v); break;
                case string v: SetValue(v); break;
                case SheetCell v: SetValue(v); break;

                case CValue v:
                    SetValue(v.Value);
                    if (!(v.Style is null)) SetCStyle(v.Style);
                    if (!(v.DataFormat is null)) SetCStyle(x => x.DataFormat = v.DataFormat);
                    break;

                case object v: String = v.ToString(); break;
            }
        }

        public void SetValue(bool value) => MapedCell.SetCellValue(value);
        public void SetValue(bool? value)
        {
            if (value.HasValue) SetValue(value.Value);
            else MapedCell.SetCellValue((string)null);
        }
        public void SetValue(double value) => MapedCell.SetCellValue(value);
        public void SetValue(double? value)
        {
            if (value.HasValue) SetValue(value.Value);
            else MapedCell.SetCellValue((string)null);
        }
        public void SetValue(DateTime value, string dataFormat = "yyyy/M/d")
        {
            var book = Sheet.Book;
            MapedCell.SetCellValue(value);
            SetCStyle(x => x.DataFormat = dataFormat);
        }
        public void SetValue(DateTime? value, string dataFormat = "yyyy/M/d")
        {
            if (value.HasValue) SetValue(value.Value, dataFormat);
            else MapedCell.SetCellValue((string)null);
        }
        public void SetValue(IRichTextString value) => MapedCell.SetCellValue(value);
        public void SetValue(string value)
        {
            if (value is null) MapedCell.SetCellValue(value);
            else
            {
                if (value.StartsWith("=")) SetFormulaValue(value.Substring(1));
                else MapedCell.SetCellValue(value);
            }
        }
        public void SetValue(SheetCell value)
        {
            switch (value.MapedCell.CellType)
            {
                case CellType.Blank: break;
                case CellType.Error: break;
                case CellType.Unknown: break;
                case CellType.Boolean: Boolean = value.Boolean; break;
                case CellType.Numeric: Number = value.Number; break;
                case CellType.String: String = value.String; break;
                case CellType.Formula: Formula = value.Formula; break;
            }
        }
        public void SetFormulaValue(string value) => MapedCell.SetCellFormula(value);

        public object GetValue()
        {
            switch (MapedCell.CellType)
            {
                case CellType.Boolean: return Boolean;
                case CellType.Numeric: return Number;
                case CellType.String:
                case CellType.Formula: return String;

                default: return null;
            }
        }

        public string Formula
        {
            get => MapedCell.CellFormula;
            set => SetFormulaValue(value);
        }
        public bool? Boolean
        {
            get => MapedCell.BooleanCellValue;
            set => SetValue(value);
        }
        public DateTime? DateTime
        {
            get => MapedCell.DateCellValue;
            set => SetValue(value);
        }
        public double? Number
        {
            get => MapedCell.NumericCellValue;
            set => SetValue(value);
        }
        public IRichTextString RichTextString
        {
            get => MapedCell.RichStringCellValue;
            set => SetValue(value);
        }
        public string String
        {
            get => MapedCell.StringCellValue;
            set => SetValue(value);
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

        public void SetWidth(double width) => Sheet.SetWidth(ColumnIndex, width);
        public void SetHeight(float height) => Sheet.GetRow(RowIndex).HeightInPoints = height;

        public SheetRange MergedRange
        {
            get
            {
                if (IsMergedCell)
                    return Sheet.MergedRanges.FirstOrDefault(x => x.IsInRange(this));
                else return null;
            }
        }
    }
}
