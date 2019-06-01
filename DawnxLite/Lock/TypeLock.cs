using System;
using System.Linq;

namespace Dawnx.Lock
{
    /// <summary>
    /// Type lock
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class TypeLock<TType>
    {
        public string[] Flags { get; }

        protected TypeLock(params string[] flags)
        {
            Flags = flags;
        }

        public static TypeLock<TType> Get(params string[] flags) => new TypeLock<TType>(flags);

        public virtual string InternString
        {
            get
            {
                return string.Intern(
                   $"{typeof(TType).FullName} " +
                   $"{Flags.Select(x => x.ToString().UrlEncode()).Join(" ")}");
            }
        }

        public Lock Begin(TimeSpan timeout) => Lock.Begin(InternString, timeout);
        public Lock Begin(int millisecondsTimeout) => Lock.Begin(InternString, millisecondsTimeout);
    }
}
