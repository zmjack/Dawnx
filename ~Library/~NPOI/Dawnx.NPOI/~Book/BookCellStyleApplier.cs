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
            => new BookCellStyleApplier().Self(_ => init(_));

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
        public short FontIndex { get; set; }
        public IBookFont Font { get; set; }
        #endregion

        public void FullBorder()
        {
            BorderLeft = BorderStyle.Thin;
            BorderRight = BorderStyle.Thin;
            BorderTop = BorderStyle.Thin;
            BorderBottom = BorderStyle.Thin;
        }

        public void Apply(IBookCellStyle style)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            foreach (var prop in props)
                prop.SetValue(style, prop.GetValue(this));
        }
    }
}
