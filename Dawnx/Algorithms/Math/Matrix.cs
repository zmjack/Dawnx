using System;
using System.Linq;

namespace Dawnx.Algorithms.Math
{
    public class Matrix
    {
        public double[,] Values { get; private set; }
        public int DimensionLength0 { get; private set; }
        public int DimensionLength1 { get; private set; }
        public bool IsAugmentedMatrix { get; private set; }

        public Matrix(double[,] values)
        {
            if (values.GetLength(0) == values.GetLength(1))
            {
                Values = values;
                DimensionLength0 = values.GetLength(0);
                DimensionLength1 = values.GetLength(1);
            }
            else if (values.GetLength(1) == values.GetLength(1))
            {
                Values = values;
                DimensionLength0 = values.GetLength(0);
                DimensionLength1 = values.GetLength(1);
                IsAugmentedMatrix = true;
            }
            else throw new FormatException("Can not convert the specified values to be a matrix.");
        }

        public double[] FindEquationSolution()
        {
            var ret = new double[DimensionLength0];
            if (IsAugmentedMatrix)
            {
                var divisor = SelectDeterminant(Range.Create(DimensionLength0));

                Range.Create(DimensionLength0).Each(i =>
                {
                    var dividend = SelectDeterminant(Range.Create(DimensionLength0).Self(_ => _[i] = DimensionLength1 - 1).ToArray());
                    ret[i] = dividend.Value / divisor.Value;
                });

                return ret;
            }
            else throw new InvalidOperationException("Only augmented matrix can find a equation solution.");
        }

        public Determinant SelectDeterminant(int[] cols)
        {
            var ret = new double[DimensionLength0, cols.Length];
            for (var j = 0; j < cols.Length; j++)
                for (int i = 0; i < DimensionLength0; i++)
                    ret[i, j] = Values[i, cols[j]];
            return new Determinant(ret);
        }

    }
}
