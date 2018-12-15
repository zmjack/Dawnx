using Dawnx.Patterns;
using System;
using System.Collections.Generic;

namespace Dawnx.Generators
{
    public class IdGenerator<T> : IGenerator<T>
    {
        private Func<T> _Method;
        private T _PrevGeneratedCode;
        private string _Locker;

        public IdGenerator(Func<T> method)
        {
            _Method = method;
            _Locker = string.Intern($"{GetType().FullName} {GetHashCode()}");
        }

        public T[] Take(int count)
        {
            var ret = new List<T>();
            lock (_Locker)
            {
                foreach (var i in Range.Create(count))
                {
                    var code = UseSpinLock.Do(task: () =>
                    {
                        return _Method();
                    }, until: x => !x.Equals(_PrevGeneratedCode));
                    _PrevGeneratedCode = code;
                    ret.Add(code);
                }
            }
            return ret.ToArray();
        }

        public T TakeOne()
        {
            lock (_Locker)
            {
                var code = UseSpinLock.Do(task: () =>
                {
                    return _Method();
                }, until: x => x.Equals(_PrevGeneratedCode));
                _PrevGeneratedCode = code;
                return code;
            }
        }

    }
}
