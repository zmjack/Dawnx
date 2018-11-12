﻿using Dawnx.Patterns;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Dawnx
{
    /// <summary>
    /// Cooperate with 'using' keyword using thread safe <see cref="Scope{T, TSelf}"/>.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TSelf"></typeparam>
    public abstract class Scope<TModel, TSelf> : IDisposable
        where TSelf : Scope<TModel, TSelf>
    {
        public TModel Model { get; protected set; }
        
        public Scope(TModel model)
        {
            DoubleCheck.Do(
                locker: $"{Thread.CurrentThread.ManagedThreadId} {GetType().FullName}",
                condition: () => Scopes is null,
                task: () => Scopes = new Stack<Scope<TModel, TSelf>>());

            Model = model;
            Scopes.Push(this);
        }
        public void Dispose() { Disposing(); Scopes.Pop(); }

        public virtual void Disposing() { }

        //Use TSelf to make sure ThreadStatic working correctly.
        [ThreadStatic]
        public static Stack<Scope<TModel, TSelf>> Scopes;

        public static Scope<TModel, TSelf> Current => Scopes?.Peek();

    }
}
