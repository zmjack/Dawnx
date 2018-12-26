using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    internal class ExactEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, object> _compare;

        public ExactEqualityComparer(Func<T, object> compare)
        {
            _compare = compare;
        }

        public bool Equals(T v1, T v2) => _compare(v1).Equals(_compare(v2));
        public int GetHashCode(T obj) => 0;
    }

}