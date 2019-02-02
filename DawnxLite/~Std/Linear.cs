using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx
{
    public static class Linear
    {
        public static IEnumerable<Tuple<T1, T2>> Create<T1, T2>(IEnumerable<T1> list1, IEnumerable<T2> list2)
        {
            var e1 = list1.GetEnumerator();
            var e2 = list2.GetEnumerator();

            while (true)
            {
                var move = new[] { e1.MoveNext(), e2.MoveNext() };
                if (move.All(x => x))
                    yield return Tuple.Create(e1.Current, e2.Current);
                else break;
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3>> Create<T1, T2, T3>(IEnumerable<T1> list1, IEnumerable<T2> list2, IEnumerable<T3> list3)
        {
            var e1 = list1.GetEnumerator();
            var e2 = list2.GetEnumerator();
            var e3 = list3.GetEnumerator();

            while (true)
            {
                var move = new[] { e1.MoveNext(), e2.MoveNext(), e3.MoveNext() };
                if (move.All(x => x))
                    yield return Tuple.Create(e1.Current, e2.Current, e3.Current);
                else break;
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4>> Create<T1, T2, T3, T4>(
            IEnumerable<T1> list1, IEnumerable<T2> list2, IEnumerable<T3> list3, IEnumerable<T4> list4)
        {
            var e1 = list1.GetEnumerator();
            var e2 = list2.GetEnumerator();
            var e3 = list3.GetEnumerator();
            var e4 = list4.GetEnumerator();

            while (true)
            {
                var move = new[] { e1.MoveNext(), e2.MoveNext(), e3.MoveNext(), e4.MoveNext() };
                if (move.All(x => x))
                    yield return Tuple.Create(e1.Current, e2.Current, e3.Current, e4.Current);
                else break;
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5>> Create<T1, T2, T3, T4, T5>(
            IEnumerable<T1> list1, IEnumerable<T2> list2, IEnumerable<T3> list3, IEnumerable<T4> list4,
            IEnumerable<T5> list5)
        {
            var e1 = list1.GetEnumerator();
            var e2 = list2.GetEnumerator();
            var e3 = list3.GetEnumerator();
            var e4 = list4.GetEnumerator();
            var e5 = list5.GetEnumerator();

            while (true)
            {
                var move = new[] { e1.MoveNext(), e2.MoveNext(), e3.MoveNext(), e4.MoveNext(), e5.MoveNext() };
                if (move.All(x => x))
                    yield return Tuple.Create(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current);
                else break;
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5, T6>> Create<T1, T2, T3, T4, T5, T6>(
            IEnumerable<T1> list1, IEnumerable<T2> list2, IEnumerable<T3> list3, IEnumerable<T4> list4,
            IEnumerable<T5> list5, IEnumerable<T6> list6)
        {
            var e1 = list1.GetEnumerator();
            var e2 = list2.GetEnumerator();
            var e3 = list3.GetEnumerator();
            var e4 = list4.GetEnumerator();
            var e5 = list5.GetEnumerator();
            var e6 = list6.GetEnumerator();

            while (true)
            {
                var move = new[] { e1.MoveNext(), e2.MoveNext(), e3.MoveNext(), e4.MoveNext(), e5.MoveNext(), e6.MoveNext() };
                if (move.All(x => x))
                    yield return Tuple.Create(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current);
                else break;
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Create<T1, T2, T3, T4, T5, T6, T7>(
            IEnumerable<T1> list1, IEnumerable<T2> list2, IEnumerable<T3> list3, IEnumerable<T4> list4,
            IEnumerable<T5> list5, IEnumerable<T6> list6, IEnumerable<T7> list7)
        {
            var e1 = list1.GetEnumerator();
            var e2 = list2.GetEnumerator();
            var e3 = list3.GetEnumerator();
            var e4 = list4.GetEnumerator();
            var e5 = list5.GetEnumerator();
            var e6 = list6.GetEnumerator();
            var e7 = list7.GetEnumerator();

            while (true)
            {
                var move = new[] { e1.MoveNext(), e2.MoveNext(), e3.MoveNext(), e4.MoveNext(), e5.MoveNext(), e6.MoveNext(), e7.MoveNext() };
                if (move.All(x => x))
                    yield return Tuple.Create(e1.Current, e2.Current, e3.Current, e4.Current, e5.Current, e6.Current, e7.Current);
                else break;
            }
        }

    }
}
