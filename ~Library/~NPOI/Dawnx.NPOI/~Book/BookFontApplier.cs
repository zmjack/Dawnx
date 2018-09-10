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
        public string FontName { get; set; }
        public short FontSize { get; set; }

        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsStrikeout { get; set; }
        public FontUnderlineType Underline { get; set; }

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
