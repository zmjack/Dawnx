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
        public ExcelBook Book { get; private set; }
        public IFont Font { get; private set; }

        internal BookFont(ExcelBook book)
            : this(book, book.MapedWorkbook.CreateFont())
        { }

        internal BookFont(ExcelBook book, IFont cellStyle)
        {
            Book = book;
            Font = cellStyle;
        }

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

        public RGBColor FontColor
        {
            get
            {
                switch (Book.Version)
                {
                    case ExcelVersion.Excel2003:
                        return (Font as HSSFFont).GetHSSFColor(Book.MapedWorkbook as HSSFWorkbook)?.For(_ => new RGBColor(_.RGB));
                    case ExcelVersion.Excel2007:
                        return (Font as XSSFFont).GetXSSFColor()?.For(_ => new RGBColor(_.RGB));
                    default: throw new NotSupportedException();
                }
            }
            set
            {
                switch (Book.Version)
                {
                    case ExcelVersion.Excel2003:
                        Font.Color = value.Index;
                        return;
                    case ExcelVersion.Excel2007:
                        (Font as XSSFFont).SetColor(value?.For(_ => new XSSFColor(_.Bytes)));
                        return;
                    default: throw new NotSupportedException();
                }
            }
        }

        internal bool InterfaceValuesEqual(BookFontApplier obj)
        {
            var instance = obj as IBookFont;
            if (instance is null) return false;

            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookFont).GetProperties().Where(prop => prop.CanWrite);
            return props.All(prop => prop.GetValue(this).Equals(prop.GetValue(instance)));
        }

        //double FontHeight { get; set; }
        //short FontHeightInPoints { get; set; }
        //bool IsItalic { get; set; }
        //bool IsStrikeout { get; set; }
        //short Color { get; set; }
        //FontSuperScript TypeOffset { get; set; }
        //FontUnderlineType Underline { get; set; }
        //short Charset { get; set; }

        //short Index { get; }

        //short Boldweight { get; set; }
        //bool IsBold { get; set; }
    }

}
