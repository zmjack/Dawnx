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

        public static Tuple<T, T, T, T>[,,,] CreateMultiArray<T>(T[] a0, T[] a1, T[] a2, T[] a3)
        {
            var ret = new Tuple<T, T, T, T>[a0.Length, a1.Length, a2.Length, a3.Length];
            ret.Each((v, i0, i1, i2, i3) => ret[i0, i1, i2, i3] = Tuple.Create(a0[i0], a1[i1], a2[i2], a3[i3]));
            return ret;
        }

        public static Tuple<T, T, T, T, T>[,,,,] CreateMultiArray<T>(T[] a0, T[] a1, T[] a2, T[] a3, T[] a4)
        {
            var ret = new Tuple<T, T, T, T, T>[a0.Length, a1.Length, a2.Length, a3.Length, a4.Length];
            ret.Each((v, i0, i1, i2, i3, i4) => ret[i0, i1, i2, i3, i4] = Tuple.Create(a0[i0], a1[i1], a2[i2], a3[i3], a4[i4]));
            return ret;
        }

        public static Tuple<T, T, T, T, T, T>[,,,,,] CreateMultiArray<T>(T[] a0, T[] a1, T[] a2, T[] a3, T[] a4, T[] a5)
        {
            var ret = new Tuple<T, T, T, T, T, T>[a0.Length, a1.Length, a2.Length, a3.Length, a4.Length, a5.Length];
            ret.Each((v, i0, i1, i2, i3, i4, i5) => ret[i0, i1, i2, i3, i4, i5] = Tuple.Create(a0[i0], a1[i1], a2[i2], a3[i3], a4[i4], a5[i5]));
            return ret;
        }

        public static Tuple<T, T, T, T, T, T, T>[,,,,,,] CreateMultiArray<T>(T[] a0, T[] a1, T[] a2, T[] a3, T[] a4, T[] a5, T[] a6)
        {
            var ret = new Tuple<T, T, T, T, T, T, T>[a0.Length, a1.Length, a2.Length, a3.Length, a4.Length, a5.Length, a6.Length];
            ret.Each((v, i0, i1, i2, i3, i4, i5, i6) => ret[i0, i1, i2, i3, i4, i5, i6] = Tuple.Create(a0[i0], a1[i1], a2[i2], a3[i3], a4[i4], a5[i5], a6[i6]));
            return ret;
        }

    }
}
