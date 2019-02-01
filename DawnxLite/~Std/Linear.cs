using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx
{
    public class Linear
    {
        public static IEnumerable<TRet> Create<T1, T2, TRet>(IEnumerable<T1> list1, IEnumerable<T2> list2, Func<T1, T2, TRet> linear)
        {
            var enumerator1 = list1.GetEnumerator();
            var enumerator2 = list2.GetEnumerator();

            while (true)
            {
                var move = new[] { enumerator1.MoveNext(), enumerator2.MoveNext() };
                if (move.All(x => x))
                    yield return linear(enumerator1.Current, enumerator2.Current);
                else break;
            }
        }
    }
}
