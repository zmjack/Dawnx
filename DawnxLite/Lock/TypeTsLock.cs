using System.Linq;
using System.Threading;

namespace Dawnx.Lock
{
    /// <summary>
    /// Type thread safe lock, inherits from <see cref="InstanceLock{TInstance}" />.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class TypeTsLock<TType> : TypeLock<TType>
    {
        protected TypeTsLock(params string[] flags) : base(flags) { }

        public override string InternString
        {
            get
            {
                return string.Intern(
                    $"<{Thread.CurrentThread.ManagedThreadId.ToString()}> " +
                    $"{typeof(TType).FullName} " +
                    $"{Flags.Select(x => x.ToString().UrlEncode()).Join(" ")}");
            }
        }

    }
}
