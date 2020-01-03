using NStandard;
using System;
using System.Collections.Generic;

namespace Dawnx.Generators
{
    public class IdGenerator<T> : IGenerator<T>
    {
        private readonly Func<T> _Method;
        private T _PrevGeneratedCode;

        public IdGenerator(Func<T> method)
        {
            _Method = method;
        }

        public T[] Take(int count)
        {
            var ret = new List<T>();
            lock (this)
            {
                foreach (var i in new int[count].Let(i => i))
                {
                    T code;
                    do { code = _Method(); }
                    while (code.Equals(_PrevGeneratedCode));

                    _PrevGeneratedCode = code;
                    ret.Add(code);
                }
            }
            return ret.ToArray();
        }

        public T TakeOne()
        {
            lock (this)
            {
                T code;
                do { code = _Method(); }
                while (code.Equals(_PrevGeneratedCode));

                _PrevGeneratedCode = code;
                return code;
            }
        }

    }
}
