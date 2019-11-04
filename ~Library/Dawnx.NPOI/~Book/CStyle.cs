using Dawnx.Utilities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NStandard;
using System;
using System.Linq;

namespace Dawnx.NPOI
{
    public class CStyle : ICStyle
    {
        public ExcelBook Book { get; }
        public ICellStyle CellStyle { get; }

        internal CStyle(ExcelBook book)
            : this(book, book.MapedWorkbook.CreateCellStyle())
        { }

        internal CStyle(ExcelBook book, ICellStyle cellStyle)
        {
            Book = book;
            CellStyle = cellStyle;
        }

        public short Index => CellStyle.Index;

        #region Alignment
        public HorizontalAlignment Alignment
        {
            get => CellStyle.Alignment;
            set => CellStyle.Alignment = value;
        }
        public VerticalAlignment VerticalAlignment
        {
            get => CellStyle.VerticalAlignment;
            set => CellStyle.VerticalAlignment = value;
        }
        #endregion

        #region Border
        public BorderStyle BorderLeft
        {
            get => CellStyle.BorderLeft;
            set => CellStyle.BorderLeft = value;
        }
        public RGBColor LeftBorderColor
        {
            get => CellStyle.LeftBorderColor > 0 ? RGBColor.ParseIndexed(CellStyle.LeftBorderColor)
                : (CellStyle as XSSFCellStyle)?.LeftBorderXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                CellStyle.LeftBorderColor = value.Index;
                if (CellStyle is XSSFCellStyle)
                    (CellStyle as XSSFCellStyle).SetLeftBorderColor(
                        value.Index == RGBColor.AutomaticIndex ? null : new XSSFColor(value.Bytes));
            }
        }

        public BorderStyle BorderRight
        {
            get => CellStyle.BorderRight;
            set => CellStyle.BorderRight = value;
        }
        public RGBColor RightBorderColor
        {
            get => CellStyle.RightBorderColor > 0 ? RGBColor.ParseIndexed(CellStyle.RightBorderColor)
                : (CellStyle as XSSFCellStyle)?.RightBorderXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                CellStyle.RightBorderColor = value.Index;
                if (CellStyle is XSSFCellStyle)
                    (CellStyle as XSSFCellStyle).SetRightBorderColor(
                        value.Index == RGBColor.AutomaticIndex ? null : new XSSFColor(value.Bytes));
            }
        }

        public BorderStyle BorderTop
        {
            get => CellStyle.BorderTop;
            set => CellStyle.BorderTop = value;
        }
        public RGBColor TopBorderColor
        {
            get => CellStyle.TopBorderColor > 0 ? RGBColor.ParseIndexed(CellStyle.TopBorderColor)
                : (CellStyle as XSSFCellStyle)?.TopBorderXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                CellStyle.TopBorderColor = value.Index;
                if (CellStyle is XSSFCellStyle)
                    (CellStyle as XSSFCellStyle).SetTopBorderColor(
                        value.Index == RGBColor.AutomaticIndex ? null : new XSSFColor(value.Bytes));
            }
        }

        public BorderStyle BorderBottom
        {
            get => CellStyle.BorderBottom;
            set => CellStyle.BorderBottom = value;
        }
        public RGBColor BottomBorderColor
        {
            get => CellStyle.BottomBorderColor > 0 ? RGBColor.ParseIndexed(CellStyle.BottomBorderColor)
                : (CellStyle as XSSFCellStyle)?.BottomBorderXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                CellStyle.BottomBorderColor = value.Index;
                if (CellStyle is XSSFCellStyle)
                    (CellStyle as XSSFCellStyle).SetBottomBorderColor(
                        value.Index == RGBColor.AutomaticIndex ? null : new XSSFColor(value.Bytes));
            }
        }

        public BorderStyle BorderDiagonalLineStyle
        {
            get => CellStyle.BorderDiagonalLineStyle;
            set => CellStyle.BorderDiagonalLineStyle = value;
        }
        public RGBColor BorderDiagonalColor
        {
            get => CellStyle.BorderDiagonalColor > 0 ? RGBColor.ParseIndexed(CellStyle.BorderDiagonalColor)
                : (CellStyle as XSSFCellStyle)?.DiagonalBorderXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                CellStyle.BorderDiagonalColor = value.Index;
                if (CellStyle is XSSFCellStyle)
                    (CellStyle as XSSFCellStyle).SetDiagonalBorderColor(
                        value.Index == RGBColor.AutomaticIndex ? null : new XSSFColor(value.Bytes));
            }
        }
        public BorderDiagonal BorderDiagonal
        {
            get => CellStyle.BorderDiagonal;
            set => CellStyle.BorderDiagonal = value;
        }
        #endregion

        #region Fill
        public FillPattern FillPattern
        {
            get => CellStyle.FillPattern;
            set => CellStyle.FillPattern = value;
        }

        public RGBColor FillBackgroundColor
        {
            get => CellStyle.FillBackgroundColor > 0 ? RGBColor.ParseIndexed(CellStyle.FillBackgroundColor)
                : (CellStyle as XSSFCellStyle)?.FillBackgroundXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                CellStyle.FillBackgroundColor = value.Index;
                if (CellStyle is XSSFCellStyle)
                    (CellStyle as XSSFCellStyle).FillBackgroundXSSFColor =
                        value.Index == RGBColor.AutomaticIndex ? null : new XSSFColor(value.Bytes);
            }
        }

        public RGBColor FillForegroundColor
        {
            get => CellStyle.FillForegroundColor > 0 ? RGBColor.ParseIndexed(CellStyle.FillForegroundColor)
                : (CellStyle as XSSFCellStyle)?.FillForegroundXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                CellStyle.FillForegroundColor = value.Index;
                if (CellStyle is XSSFCellStyle)
                    (CellStyle as XSSFCellStyle).FillForegroundXSSFColor =
                        value.Index == RGBColor.AutomaticIndex ? null : new XSSFColor(value.Bytes);
            }
        }
        #endregion

        #region Font
        public CFont Font
        {
            get => Book.CFontAt(CellStyle.FontIndex);
            set => CellStyle.SetFont(value.Font);
        }
        #endregion

        #region DataFormat
        public string DataFormat
        {
            get => CellStyle.GetDataFormatString();
            set => CellStyle.DataFormat = Book.GetDataFormat(value);
        }
        #endregion

        #region Others
        public short Rotation
        {
            get => CellStyle.Rotation;
            set => CellStyle.Rotation = value;
        }
        public short Indention
        {
            get => CellStyle.Indention;
            set => CellStyle.Indention = value;
        }
        public bool WrapText
        {
            get => CellStyle.WrapText;
            set => CellStyle.WrapText = value;
        }
        public bool IsLocked
        {
            get => CellStyle.IsLocked;
            set => CellStyle.IsLocked = value;
        }
        public bool IsHidden
        {
            get => CellStyle.IsHidden;
            set => CellStyle.IsHidden = value;
        }
        public bool ShrinkToFit
        {
            get => CellStyle.ShrinkToFit;
            set => CellStyle.ShrinkToFit = value;
        }
        #endregion

        public CStyleApplier GetApplier()
        {
            return CStyleApplier.Create(x =>
            {
                // Alignment
                x.Alignment = Alignment;
                x.VerticalAlignment = VerticalAlignment;

                // Border
                x.BorderLeft = BorderLeft;
                x.LeftBorderColor = LeftBorderColor;
                x.BorderRight = BorderRight;
                x.RightBorderColor = RightBorderColor;
                x.BorderTop = BorderTop;
                x.TopBorderColor = TopBorderColor;
                x.BorderBottom = BorderBottom;
                x.BottomBorderColor = BottomBorderColor;
                x.BorderDiagonalLineStyle = BorderDiagonalLineStyle;
                x.BorderDiagonalColor = BorderDiagonalColor;
                x.BorderDiagonal = BorderDiagonal;

                // Fill
                x.FillPattern = FillPattern;
                x.FillBackgroundColor = FillBackgroundColor;
                x.FillForegroundColor = FillForegroundColor;

                // Font
                x.Font = CFontApplier.Create(f =>
                {
                    f.FontName = Font.FontName;
                    f.FontSize = Font.FontSize;
                    f.IsBold = Font.IsBold;
                    f.IsItalic = Font.IsItalic;
                    f.IsStrikeout = Font.IsStrikeout;
                    f.Underline = Font.Underline;
                    f.TypeOffset = Font.TypeOffset;
                    f.FontColor = Font.FontColor;
                });

                // DataFormat
                x.DataFormat = DataFormat;

                // Others
                x.Rotation = Rotation;
                x.Indention = Indention;
                x.WrapText = WrapText;
                x.IsLocked = IsLocked;
                x.IsHidden = IsHidden;
                x.ShrinkToFit = ShrinkToFit;
            });
        }

        internal bool InterfaceValuesEqual(CStyleApplier obj)
        {
            var instance = obj as ICStyle;
            if (instance is null) return false;

            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(ICStyle).GetProperties().Where(prop => prop.CanWrite);
            return props.All(prop => CompareUtility.UsingEquals(prop.GetValue(this), prop.GetValue(instance)))
                && Font.InterfaceValuesEqual(obj.Font);
        }

    }
}
