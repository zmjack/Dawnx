using Dawnx.Utilities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                if (value.Index == 0)
                {
                    if (CellStyle is XSSFCellStyle)
                        (CellStyle as XSSFCellStyle).SetLeftBorderColor(new XSSFColor(value.Bytes));
                    else CellStyle.LeftBorderColor = RGBColor.Automatic.Index;
                }
                else CellStyle.LeftBorderColor = value.Index;
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
                if (value.Index == 0)
                {
                    if (CellStyle is XSSFCellStyle)
                        (CellStyle as XSSFCellStyle).SetRightBorderColor(new XSSFColor(value.Bytes));
                    else CellStyle.RightBorderColor = RGBColor.Automatic.Index;
                }
                else CellStyle.RightBorderColor = value.Index;
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
                if (value.Index == 0)
                {
                    if (CellStyle is XSSFCellStyle)
                        (CellStyle as XSSFCellStyle).SetTopBorderColor(new XSSFColor(value.Bytes));
                    else CellStyle.TopBorderColor = RGBColor.Automatic.Index;
                }
                else CellStyle.TopBorderColor = value.Index;
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
                if (value.Index == 0)
                {
                    if (CellStyle is XSSFCellStyle)
                        (CellStyle as XSSFCellStyle).SetBottomBorderColor(new XSSFColor(value.Bytes));
                    else CellStyle.BottomBorderColor = RGBColor.Automatic.Index;
                }
                else CellStyle.BottomBorderColor = value.Index;
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
                if (value.Index == 0)
                {
                    if (CellStyle is XSSFCellStyle)
                        (CellStyle as XSSFCellStyle).SetDiagonalBorderColor(new XSSFColor(value.Bytes));
                    else CellStyle.BorderDiagonalColor = RGBColor.Automatic.Index;
                }
                else CellStyle.BorderDiagonalColor = value.Index;
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
                if (value.Index == 0)
                {
                    if (CellStyle is XSSFCellStyle)
                        (CellStyle as XSSFCellStyle).FillBackgroundXSSFColor = new XSSFColor(value.Bytes);
                    else CellStyle.FillBackgroundColor = RGBColor.Automatic.Index;
                }
                else CellStyle.FillBackgroundColor = value.Index;
            }
        }

        public RGBColor FillForegroundColor
        {
            get => CellStyle.FillForegroundColor > 0 ? RGBColor.ParseIndexed(CellStyle.FillForegroundColor)
                : (CellStyle as XSSFCellStyle)?.FillForegroundXSSFColor?.RGB?.For(_ => new RGBColor(_)) ?? RGBColor.Automatic;
            set
            {
                if (value.Index == 0)
                {
                    if (CellStyle is XSSFCellStyle)
                        (CellStyle as XSSFCellStyle).FillForegroundXSSFColor = new XSSFColor(value.Bytes);
                    else CellStyle.FillForegroundColor = RGBColor.Automatic.Index;
                }
                else CellStyle.FillForegroundColor = value.Index;
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
