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

        public void Apply(BookCellStyle style)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookCellStyle).GetProperties().Where(prop => prop.CanWrite);
            foreach (var prop in props)
                prop.SetValue(style, prop.GetValue(this));

            // Font
            style.Font = style.Book.BookFont(Font);
        }

        public BookCellStyleApplier FullBorder(BorderStyle borderStyle = BorderStyle.Thin)
        {
            BorderLeft = BorderRight = BorderTop = BorderBottom = borderStyle;
            return this;
        }
        public BookCellStyleApplier LeftBorder(BorderStyle borderStyle = BorderStyle.Thin) { BorderLeft = borderStyle; return this; }
        public BookCellStyleApplier RightBorder(BorderStyle borderStyle = BorderStyle.Thin) { BorderRight = borderStyle; return this; }
        public BookCellStyleApplier TopBorder(BorderStyle borderStyle = BorderStyle.Thin) { BorderTop = borderStyle; return this; }
        public BookCellStyleApplier BottomBorder(BorderStyle borderStyle = BorderStyle.Thin) { BorderBottom = borderStyle; return this; }

        public BookCellStyleApplier CellColor(int foregroundColor) => CellColor(new RGBColor(foregroundColor));
        public BookCellStyleApplier CellColor(int foregroundColor, int backgroundColor, FillPattern pattern) => CellColor(new RGBColor(foregroundColor), new RGBColor(backgroundColor), pattern);
        public BookCellStyleApplier CellColor(RGBColor foregroundColor) { FillForegroundColor = foregroundColor; FillPattern = FillPattern.SolidForeground; return this; }
        public BookCellStyleApplier CellColor(RGBColor foregroundColor, RGBColor backgroundColor, FillPattern pattern)
        {
            FillForegroundColor = foregroundColor;
            FillBackgroundColor = backgroundColor;
            FillPattern = pattern;
            return this;
        }

        public BookCellStyleApplier Center() => HCenter().VCenter();

        public BookCellStyleApplier HLeft() { Alignment = HorizontalAlignment.Left; return this; }
        public BookCellStyleApplier HCenter() { Alignment = HorizontalAlignment.Center; return this; }
        public BookCellStyleApplier HRight() { Alignment = HorizontalAlignment.Right; return this; }

        public BookCellStyleApplier VTop() { VerticalAlignment = VerticalAlignment.Top; return this; }
        public BookCellStyleApplier VCenter() { VerticalAlignment = VerticalAlignment.Center; return this; }
        public BookCellStyleApplier VBottom() { VerticalAlignment = VerticalAlignment.Bottom; return this; }

        public BookCellStyleApplier Bold(bool value = true) { Font.IsBold = value; return this; }
        public BookCellStyleApplier Italic(bool value = true) { Font.IsItalic = value; return this; }
        public BookCellStyleApplier Strikeout(bool value = true) { Font.IsStrikeout = value; return this; }
        public BookCellStyleApplier Underline(FontUnderlineType value = FontUnderlineType.Single) { Font.Underline = value; return this; }
        public BookCellStyleApplier TypeOffset(FontSuperScript value) { Font.TypeOffset = value; return this; }

        public BookCellStyleApplier SetFont(string fontName, short size) => SetFont(fontName, size, RGBColor.Automatic);
        public BookCellStyleApplier SetFont(string fontName, short size, int rgbValue) => SetFont(fontName, size, new RGBColor(rgbValue));
        public BookCellStyleApplier SetFont(string fontName, short size, RGBColor color)
        {
            Font.FontName = fontName;
            Font.FontSize = size;
            Font.FontColor = color;
            return this;
        }
        public BookCellStyleApplier CellFormat(string dataFormat) { DataFormat = dataFormat; return this; }

        public BookCellStyleApplier WordWrap(bool value = true) { WrapText = value; return this; }
        public BookCellStyleApplier Shrink(bool value = true) { ShrinkToFit = value; return this; }

        public BookCellStyleApplier SetRotation(short value) { Rotation = value; return this; }
        public BookCellStyleApplier SetIndention(short value) { Indention = value; return this; }
    }
}
