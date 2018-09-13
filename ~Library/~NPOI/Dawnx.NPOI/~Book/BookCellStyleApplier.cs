using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.NPOI
{
    public class BookCellStyleApplier : IBookCellStyle
    {
        private BookCellStyleApplier() { }

        internal static BookCellStyleApplier Create(Action<BookCellStyleApplier> init)
            => new BookCellStyleApplier().Self(_ => init?.Invoke(_));

        #region Alignment
        public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.General;
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Bottom;
        #endregion

        #region Border
        public BorderStyle BorderLeft { get; set; } = BorderStyle.None;
        public RGBColor LeftBorderColor { get; set; } = RGBColor.Black;

        public BorderStyle BorderRight { get; set; } = BorderStyle.None;
        public RGBColor RightBorderColor { get; set; } = RGBColor.Black;

        public BorderStyle BorderTop { get; set; } = BorderStyle.None;
        public RGBColor TopBorderColor { get; set; } = RGBColor.Black;

        public BorderStyle BorderBottom { get; set; } = BorderStyle.None;
        public RGBColor BottomBorderColor { get; set; } = RGBColor.Black;

        public BorderStyle BorderDiagonalLineStyle { get; set; } = BorderStyle.None;
        public RGBColor BorderDiagonalColor { get; set; } = RGBColor.Black;
        public BorderDiagonal BorderDiagonal { get; set; } = BorderDiagonal.None;
        #endregion

        #region Fill
        public FillPattern FillPattern { get; set; } = FillPattern.NoFill;
        public RGBColor FillBackgroundColor { get; set; } = RGBColor.Automatic;
        public RGBColor FillForegroundColor { get; set; } = RGBColor.Automatic;
        #endregion

        #region Font
        public BookFontApplier Font { get; } = BookFontApplier.Create(null);
        #endregion

        #region DataFormat
        public string DataFormat { get; set; } = "General";
        #endregion

        #region Others
        public short Rotation { get; set; } = 0;
        public short Indention { get; set; } = 0;
        public bool WrapText { get; set; } = false;
        public bool IsLocked { get; set; } = true;
        public bool IsHidden { get; set; } = false;
        public bool ShrinkToFit { get; set; } = false;
        #endregion

        public void FullBorder()
        {
            BorderLeft = BorderStyle.Thin;
            BorderRight = BorderStyle.Thin;
            BorderTop = BorderStyle.Thin;
            BorderBottom = BorderStyle.Thin;
        }

        public void Apply(BookCellStyle style)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            foreach (var prop in props)
                prop.SetValue(style, prop.GetValue(this));

            // Font
            style.Font = style.Book.BookFont(Font);
        }

    }
}
