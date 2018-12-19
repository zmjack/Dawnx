using Dawnx.Ranges;
using System.Linq;

namespace Dawnx.AspNetCore.Algorithms
{
    public static class Pagination
    {
        public static int[] GetRange(int pageNumber, int pageSize, int navCount, bool navAlignRight = false)
        {
            int start, end;
            if (pageSize <= navCount)
            {
                start = 1;
                end = pageSize;
            }
            else
            {
                var isNavCountEven = navCount % 2 == 0;
                int relativeLeft, relativeRight;

                if (isNavCountEven && !navAlignRight)
                {
                    relativeLeft = navCount / 2 - 1;
                    relativeRight = navCount / 2;
                }
                else if (isNavCountEven && navAlignRight)
                {
                    relativeLeft = navCount / 2;
                    relativeRight = navCount / 2 - 1;
                }
                else relativeLeft = relativeRight = (navCount - 1) / 2;

                start = pageNumber - relativeLeft;
                end = pageNumber + relativeRight;

                //Revises `start` and `end` if any of them overflow
                if (start < 1)
                {
                    end += 1 - start;
                    start = 1;
                }
                else if (end > pageSize)
                {
                    start -= end - pageSize;
                    end = pageSize;
                }
            }
            return IntegerRange.Create(start, end + 1).ToArray();
        }
    }
}
