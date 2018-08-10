using Dawnx.Patterns;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Dawnx
{
    /// <summary>
    /// Cooperate with 'using' keyword using thread safe <see cref="Scope{T, TSelf}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSelf"></typeparam>
    public abstract class Scope<T, TSelf> : IDisposable
        where TSelf : Scope<T, TSelf>
    {
        public T Model { get; protected set; }
        
        public Scope(T model)
        {
            DoubleCheck.Do(
                lockTarget: $"{Thread.CurrentThread.ManagedThreadId} {GetType().FullName}",
                checkCondition: () => Scopes == null,
                task: () => Scopes = new Stack<Scope<T, TSelf>>());

            Model = model;
            Scopes.Push(this);
        }
        public void Dispose() { Disposing(); Scopes.Pop(); }

        public virtual void Disposing() { }

        //Use TSelf to make sure ThreadStatic working correctly.
        [ThreadStatic]
        public static Stack<Scope<T, TSelf>> Scopes;

        public static Scope<T, TSelf> Current => Scopes?.Peek();

    }
}
