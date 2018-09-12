using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.NPOI
{
    public class BookFontApplier : IBookFont
    {
        private BookFontApplier() { }

        internal static BookFontApplier Create(Action<BookFontApplier> init)
            => new BookFontApplier().Self(_ => init(_));

        public string FontName { get; set; } = "Calibri";
        public short FontSize { get; set; } = 11;

        public bool IsBold { get; set; } = false;
        public bool IsItalic { get; set; } = false;
        public bool IsStrikeout { get; set; } = false;
        public FontUnderlineType Underline { get; set; } = FontUnderlineType.None;

        public RGBColor FontColor { get; set; } = RGBColor.Black;

        public void Apply(IBookFont style)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookFont).GetProperties().Where(prop => prop.CanWrite);
            foreach (var prop in props)
                prop.SetValue(style, prop.GetValue(this));
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
