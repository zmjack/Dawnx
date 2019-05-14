using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public class SheetRangeGroup : IEnumerable<SheetCell>, IStylizable
    {
        private IEnumerable<SheetRange> Ranges;

        public SheetRangeGroup(IEnumerable<SheetRange> ranges)
        {
            Ranges = ranges;
        }

        public void SetCellStyle(ICellStyle style)
        {
            foreach (var range in Ranges)
                range.SetCellStyle(style);
        }

        public void SetCStyle(CStyle style)
        {
            foreach (var range in Ranges)
                range.SetCStyle(style);
        }

        public void SetCStyle(Action<CStyleApplier> initApplier)
        {
            foreach (var range in Ranges)
                range.SetCStyle(initApplier);
        }

        public IEnumerator<SheetCell> GetEnumerator()
        {
            foreach (var range in Ranges)
                foreach (var cell in range)
                    yield return cell;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
