using Dawnx.Ranges;
using System;
using System.Linq;

namespace Dawnx.Algorithms.Math
{
    public partial class Matrix : ICloneable
    {
        public int RowLength { get; private set; }
        public int ColumnLength { get; private set; }
        public bool IsAugmentedMatrix { get; private set; }
        public bool IsSquareMatrix { get; private set; }
        public double[,] Values { get; private set; }

        public Matrix(double[,] values)
        {
            Values = values.Clone() as double[,];
            RowLength = values.GetLength(0);
            ColumnLength = values.GetLength(1);

            if (RowLength == ColumnLength)
                IsSquareMatrix = true;
            else if (ColumnLength == RowLength + 1)
                IsAugmentedMatrix = true;
        }

        public double this[int i, int j]
        {
            get => Values[i, j];
            set => Values[i, j] = value;
        }

        public double[] FindEquationSolution()
        {
            var ret = new double[RowLength];
            if (IsAugmentedMatrix)
            {
                var divisor = SelectDeterminant(IntegerRange.Create(RowLength).ToArray());

                IntegerRange.Create(RowLength).Each(i =>
                {
                    var dividend = SelectDeterminant(
                        IntegerRange.Create(RowLength).ToArray()
                            .Self(_ => _[i] = ColumnLength - 1)
                            .ToArray());
                    ret[i] = dividend.Value / divisor.Value;
                });

                return ret;
            }
            else throw new InvalidOperationException("Only augmented matrix can find a solution of equation.");
        }

        public Determinant SelectDeterminant(int[] cols)
        {
            var ret = new double[RowLength, cols.Length];
            for (var j = 0; j < cols.Length; j++)
                for (int i = 0; i < RowLength; i++)
                    ret[i, j] = Values[i, cols[j]];
            return new Determinant(ret);
        }

        public Matrix Pow(int n)
        {
            if (n < 1) throw new ArgumentOutOfRangeException("The parameter n must greater than 0.");

            if (IsSquareMatrix)
            {
                var ret = this.Clone();
                for (int i = 1; i < n; i++)
                    ret = ret * this;

                return ret;
            }
            else throw new InvalidOperationException("Only square matrix can find a new matrix to the NTH power");
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.ColumnLength == matrix2.RowLength)
            {
                var length = matrix1.ColumnLength;
                var retRowLength = matrix1.RowLength;
                var retColumnLenth = matrix2.ColumnLength;

                var retValues = new double[retRowLength, retColumnLenth];
                for (int i = 0; i < retRowLength; i++)
                    for (int j = 0; j < retColumnLenth; j++)
                        retValues[i, j] = IntegerRange.Create(length).Sum(x => matrix1[i, x] * matrix2[x, j]);

                return new Matrix(retValues);
            }
            else throw new InvalidOperationException($"The column length of matrix1 must be equal to the row length of matrix2.");
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.RowLength == matrix2.RowLength && matrix1.ColumnLength == matrix2.ColumnLength)
            {
                var retRowLength = matrix1.RowLength;
                var retColumnLenth = matrix1.ColumnLength;

                var retValues = new double[retRowLength, retColumnLenth];
                for (int i = 0; i < retRowLength; i++)
                    for (int j = 0; j < retColumnLenth; j++)
                        retValues[i, j] = matrix1[i, j] + matrix2[i, j];

                return new Matrix(retValues);
            }
            else throw new InvalidOperationException($"Both must have the same row length and column length");
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            //TODO: Use hashcode to optimize
            if (matrix1.RowLength == matrix2.RowLength && matrix1.ColumnLength == matrix2.ColumnLength)
            {
                var rowLength = matrix1.RowLength;
                var columnLenth = matrix1.ColumnLength;

                for (int i = 0; i < rowLength; i++)
                    for (int j = 0; j < columnLenth; j++)
                        if (matrix1[i, j] != matrix2[i, j]) return false;

                return true;
            }
            else return false;
        }
        public static bool operator !=(Matrix matrix1, Matrix matrix2) => !(matrix1 == matrix2);

        public override bool Equals(object obj) => this == obj as Matrix;
        public override int GetHashCode() => 0;

        object ICloneable.Clone() => new Matrix(Values);
        public Matrix Clone() => (this as ICloneable).Clone() as Matrix;

    }
}
