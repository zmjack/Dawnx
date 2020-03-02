using NStandard;
using System;

namespace Dawnx
{
    public static class Cartesian
    {
        public static Tuple<T, T>[,] CreateMultiArray<T>(T[] a0, T[] a1)
        {
            var ret = new Tuple<T, T>[a0.Length, a1.Length];
            ret.Each((v, i0, i1) => ret[i0, i1] = Tuple.Create(a0[i0], a1[i1]));
            return ret;
        }

        public static Tuple<T, T, T>[,,] CreateMultiArray<T>(T[] a0, T[] a1, T[] a2)
        {
            var ret = new Tuple<T, T, T>[a0.Length, a1.Length, a2.Length];
            ret.Each((v, i0, i1, i2) => ret[i0, i1, i2] = Tuple.Create(a0[i0], a1[i1], a2[i2]));
            return ret;
        }

    }
}
