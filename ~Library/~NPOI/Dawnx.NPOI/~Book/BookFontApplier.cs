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
            => new BookFontApplier().Self(_ => init?.Invoke(_));

        public string FontName { get; set; } = "Calibri";
        public short FontSize { get; set; } = 11;

        public bool IsBold { get; set; } = false;
        public bool IsItalic { get; set; } = false;
        public bool IsStrikeout { get; set; } = false;

        public FontUnderlineType Underline { get; set; } = FontUnderlineType.None;
        public FontSuperScript TypeOffset { get; set; } = FontSuperScript.None;

        public RGBColor FontColor { get; set; } = RGBColor.Black;

        public void Apply(BookFont style)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var props = typeof(IBookFont).GetProperties().Where(prop => prop.CanWrite);
            foreach (var prop in props)
                prop.SetValue(style, prop.GetValue(this));
        }

    }
}
