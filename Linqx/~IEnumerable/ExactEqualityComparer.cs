using System;
using System.Collections.Generic;
using System.Linq;

namespace Linqx
{
    internal class ExactEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, object>[] _compares;

        public ExactEqualityComparer(params Func<T, object>[] compares)
        {
            _compares = compares;
        }

        public bool Equals(T v1, T v2) => _compares.All(f => f(v1).Equals(f(v2)));
        public int GetHashCode(T obj) => 0;
    }

}