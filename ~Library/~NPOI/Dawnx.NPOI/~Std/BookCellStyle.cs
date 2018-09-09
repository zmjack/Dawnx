using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.NPOI
{
    public class BookCellStyle : IBookCellStyle
    {
        public ICellStyle CellStyle { get; private set; }
        public ExcelBook Book { get; private set; }

        public BookCellStyle() { }

        public BookCellStyle(ExcelBook book)
            : this(book, book.MapedWorkbook.CreateCellStyle())
        { }

        public BookCellStyle(ExcelBook book, ICellStyle cellStyle)
        {
            Book = book;
            CellStyle = cellStyle;
        }

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
        public ExcelColor LeftBorderColor
        {
            get => (ExcelColor)CellStyle.LeftBorderColor;
            set => CellStyle.LeftBorderColor = (short)value;
        }

        public BorderStyle BorderRight
        {
            get => CellStyle.BorderRight;
            set => CellStyle.BorderRight = value;
        }
        public ExcelColor RightBorderColor
        {
            get => (ExcelColor)CellStyle.RightBorderColor;
            set => CellStyle.RightBorderColor = (short)value;
        }

        public BorderStyle BorderTop
        {
            get => CellStyle.BorderTop;
            set => CellStyle.BorderTop = value;
        }
        public ExcelColor TopBorderColor
        {
            get => (ExcelColor)CellStyle.TopBorderColor;
            set => CellStyle.TopBorderColor = (short)value;
        }

        public BorderStyle BorderBottom
        {
            get => CellStyle.BorderBottom;
            set => CellStyle.BorderBottom = value;
        }
        public ExcelColor BottomBorderColor
        {
            get => (ExcelColor)CellStyle.BottomBorderColor;
            set => CellStyle.BottomBorderColor = (short)value;
        }

        public BorderStyle BorderDiagonalLineStyle
        {
            get => CellStyle.BorderDiagonalLineStyle;
            set => CellStyle.BorderDiagonalLineStyle = value;
        }
        public ExcelColor BorderDiagonalColor
        {
            get => (ExcelColor)CellStyle.BorderDiagonalColor;
            set => CellStyle.BorderDiagonalColor = (short)value;
        }
        public BorderDiagonal BorderDiagonal
        {
            get => CellStyle.BorderDiagonal;
            set => CellStyle.BorderDiagonal = value;
        }
        #endregion

        //public short FillForegroundColor { get; set; }
        //public short FillBackgroundColor { get; set; }
        //public FillPattern FillPattern { get; set; }
        //public IColor FillBackgroundColorColor { get; }
        //public IColor FillForegroundColorColor { get; }
        //public short Rotation { get; set; }
        //public bool WrapText { get; set; }
        //public bool IsLocked { get; set; }
        //public bool IsHidden { get; set; }
        //public short FontIndex { get; }
        //public short DataFormat { get; set; }
        //public short Index { get; }
        //public bool ShrinkToFit { get; set; }
        //public short Indention { get; set; }

        //void CloneStyleFrom(ICellStyle source);
        //string GetDataFormatString();
        //IFont GetFont(IWorkbook parentWorkbook);
        //void SetFont(IFont font);

        //public static implicit operator ICellStyle(SheetCellStyle instance)
        //{
        //    var cellStyles = Book.CellStyles;

        //    var findStyle = CellStyles.FirstOrDefault(style =>
        //    {
        //        if (props.All(prop => prop.GetValue(style).Equals(prop.GetValue(compared)))
        //            && style.FontIndex == compared.FontIndex)
        //            return true;
        //        else return false;
        //    });
        //}

        public bool InterfaceValuesEqual(IBookCellStyle obj)
        {
            var instance = obj as IBookCellStyle;

            if (instance == null) return false;

            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            return props.All(prop => prop.GetValue(this).Equals(prop.GetValue(instance)));
        }

    }
}
