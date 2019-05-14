using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.NPOI
{
    public class SheetRangeRowSelector
    {
        private SheetRange Range;

        public SheetRangeRowSelector(SheetRange range)
        {
            Range = range;
        }

        public SheetRange this[int? startOffset, int? endOffset]
        {
            get
            {
                if (startOffset is null) startOffset = 0;
                if (endOffset is null) endOffset = Range.RowLength - 1;

                return new SheetRange(Range.Sheet, (Range.Start.row + startOffset.Value, Range.Start.col), (Range.Start.row + endOffset.Value, Range.End.col));
            }
        }

        public SheetRangeGroup Select(Func<int, bool> indexSelector)
        {
            IEnumerable<SheetRange> select()
            {
                for (int index = Range.Start.row; index <= Range.End.row; index++)
                {
                    if (indexSelector(index))
                        yield return new SheetRange(Range.Sheet, (Range.Start.row + index, Range.Start.col), (Range.Start.row + index, Range.End.col));
                }
            }
            return new SheetRangeGroup(select());
        }

    }
}
