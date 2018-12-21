using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Lock
{
    public static class TypeLock<TType>
    {
        public static readonly string InternString = string.Intern(typeof(TType).FullName);
        
        public static Lock Begin(TimeSpan timeout) => Lock.Get(InternString, timeout);
        public static Lock Begin(int millisecondsTimeout) => Lock.Get(InternString, millisecondsTimeout);
    }
}
