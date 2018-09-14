using Dawnx.Utilities;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.NPOI
{
    public class BookFont : IBookFont
    {
        public ExcelBook Book { get; }
        public IFont Font { get; }

        internal BookFont(ExcelBook book)
            : this(book, book.MapedWorkbook.CreateFont())
        { }

        internal BookFont(ExcelBook book, IFont cellStyle)
        {
            Book = book;
            Font = cellStyle;
        }

        public short Index => Font.Index;

        public string FontName
        {
            get => Font.FontName;
            set => Font.FontName = value;
        }
        public short FontSize
        {
            get => Font.FontHeightInPoints;
            set => Font.FontHeightInPoints = value;
        }

        public bool IsBold
        {
            get => Font.IsBold;
            set => Font.IsBold = value;
        }
        public bool IsItalic
        {
            get => Font.IsItalic;
            set => Font.IsItalic = value;
        }
        public bool IsStrikeout
        {
            get => Font.IsStrikeout;
            set => Font.IsStrikeout = value;
        }

        public FontUnderlineType Underline
        {
            get => Font.Underline;
            set => Font.Underline = value;
        }
        public FontSuperScript TypeOffset
        {
            get => Font.TypeOffset;
            set => Font.TypeOffset = value;
        }

        public RGBColor FontColor
        {
            get => (Font as XSSFFont)?.GetXSSFColor()?.For(_ => new RGBColor(_.RGB))
                ?? (Font as HSSFFont).GetHSSFColor(Book.MapedWorkbook as HSSFWorkbook).For(_ => new RGBColor(_.RGB));
            set
            {
                if (value.Index == 0)
                {
                    if (Font is XSSFFont)
                        (Font as XSSFFont).SetColor(new XSSFColor(value.Bytes));
                    else Font.Color = RGBColor.Automatic.Index;
                }
                else Font.Color = value.Index;
            }
        }

        internal bool InterfaceValuesEqual(BookFontApplier obj)
        {
            var instance = obj as IBookFont;
            if (instance is null) return false;

            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookFont).GetProperties().Where(prop => prop.CanWrite);
            return props.All(prop => CompareUtility.UsingEquals(prop.GetValue(this), prop.GetValue(instance)));
        }

    }

}
