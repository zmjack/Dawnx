using Dawnx.Patterns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dawnx.Generators
{
    public class IdGenerator : IGenerator
    {
        private Func<string> _Method;
        private string _PrevGeneratedCode;

        public IdGenerator(Func<string> method)
        {
            _Method = method;
        }

        public string[] Take(int count)
        {
            var locker = $"{GetType().FullName} {GetHashCode()}";

            var ret = new List<string>();
            lock (string.Intern(locker))
            {
                foreach (var i in Range.Create(count))
                {
                    var code = UseSpinLock.Do(task: () =>
                    {
                        return _Method();
                    }, until: x => x != _PrevGeneratedCode);
                    _PrevGeneratedCode = code;
                    ret.Add(code);
                }
            }
            return ret.ToArray();
        }

        public string TakeOne()
        {
            var locker = string.Intern($"{GetType().FullName} {GetHashCode()}");
            lock (locker)
            {
                var code = UseSpinLock.Do(task: () =>
                {
                    return _Method();
                }, until: x => x != _PrevGeneratedCode);
                _PrevGeneratedCode = code;
                return code;
            }
        }

    }
}
