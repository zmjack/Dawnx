using Dawnx.Ranges;

namespace Dawnx.Algorithms.Math
{
    public partial class Matrix
    {
        public static Matrix NewDiagonal(double[] vector, int offset_k = 0)
        {
            var length = vector.Length + System.Math.Abs(offset_k);
            var values = new double[length, length];
            var indices = new IntegerRange(vector.Length - 1);

            if (offset_k == 0)
            {
                foreach (var i in indices)
                    values[i, i] = vector[i];
            }
            else if (offset_k > 0)
            {
                foreach (var i in indices)
                    values[i, i + offset_k] = vector[i];
            }
            else
            {
                foreach (var i in indices)
                    values[i - offset_k, i] = vector[i];
            }

            return new Matrix(values);
        }
        
    }
}
