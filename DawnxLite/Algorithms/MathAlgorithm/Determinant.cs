using Dawnx.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Algorithms.MathAlgorithm
{
    public class Determinant
    {
        public double[,] Matrix { get; private set; }
        public int DimensionLength { get; private set; }

        public Determinant(double[,] matrix)
        {
            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                Matrix = matrix;
                DimensionLength = matrix.GetLength(0);
            }
            else throw new FormatException("The specified matrix must be a square matrix.");
        }

        public int GetInversionNumber(int[] colNumbers)
        {
            int ret = 0;

            for (int i = 0; i < DimensionLength; i++)
                for (int j = i + 1; j < DimensionLength; j++)
                    if (colNumbers[i] > colNumbers[j]) ret++;
            return ret;
        }

        public double Value
        {
            get
            {
                double ret = 0;
                var colGroups = new HashSet<int[]>()
                    .Self(_ => CalcGroups(_, 0, IntegerRange.Create(DimensionLength), new int[0] { }));

                foreach (var colGroup in colGroups)
                {
                    double value = 1;
                    for (int i = 0; i < DimensionLength; i++)
                        value *= Matrix[i, colGroup[i]];

                    if (GetInversionNumber(colGroup).IsOdd())
                        value = -value;

                    ret += value;
                }

                return ret;
            }
        }


        private void CalcGroups(
            HashSet<int[]> colGroups,
            int row,
            IEnumerable<int> candidateCols,
            IEnumerable<int> colNumbers)
        {
            if (row < DimensionLength - 1)
            {
                foreach (var col in candidateCols)
                {
                    CalcGroups(colGroups, row + 1,
                        candidateCols.Where(x => x != col),
                        colNumbers.Concat(new[] { col }));
                }
            }
            else
            {
                var col = candidateCols.First();       //Only one
                colGroups.Add(colNumbers.Concat(new[] { col }).ToArray());
            }
        }

    }
}
