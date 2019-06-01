using System;
using System.Collections.Generic;

namespace Dawnx.NPOI
{
    public class SheetRangeColumnSelector
    {
        private SheetRange Range;

        public SheetRangeColumnSelector(SheetRange range)
        {
            Range = range;
        }

        public SheetRange this[int? startOffset, int? endOffset]
        {
            get
            {
                if (startOffset is null) startOffset = 0;
                if (endOffset is null) endOffset = Range.ColumnLengh - 1;

                return new SheetRange(Range.Sheet, (Range.Start.row, Range.Start.col + startOffset.Value), (Range.End.row, Range.Start.col + endOffset.Value));
            }
        }

        public SheetRangeGroup Select(Func<int, bool> indexSelector)
        {
            IEnumerable<SheetRange> select()
            {
                for (int index = Range.Start.col; index <= Range.End.col; index++)
                {
                    if (indexSelector(index))
                        yield return new SheetRange(Range.Sheet, (Range.Start.row, Range.Start.col + index), (Range.End.row, Range.Start.col + index));
                }
            }
            return new SheetRangeGroup(select());
        }

    }
}
