﻿namespace UniModules.UniCore.Runtime.Rx.Extensions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using global::UniGame.Runtime.ObjectPool;
    using Rx;
    using global::UniGame.Core.Runtime.Rx;
    using UniRx;

    public static class RxExtension
    {
        public static IObservable<bool> Any<T>(this IEnumerable<IObservable<T>> source, Predicate<T> predicate)
        {
            return source.CombineLatest().Select(x => x.Any(m => predicate(m)));
        }

        public static IObservable<bool> All<T>(this IEnumerable<IObservable<T>> source, Predicate<T> predicate)
        {
            return source.CombineLatest().Select(x => x.All(m => predicate(m)));
        }
        
        public static IObservable<TResult> CombineLatestFunc<TValue,TValue2, TResult>(
            this IObservable<TValue> source, 
            Func<IObservable<TValue>,IObservable<TValue2>> func,
            Func<TValue,TValue2,TResult> resultFunc)
        {
            var funcObservable = func(source);
            return source.CombineLatest(funcObservable, resultFunc);
        }

        public static IRecycleObserver<T> CreateRecycleObserver<T>(this object _, 
            Action<T> onNext, 
            Action onComplete = null,
            Action<Exception> onError = null)
        {
            
            var observer = ClassPool.Spawn<RecycleActionObserver<T>>();
            
            observer.Initialize(onNext,onComplete,onError);

            return observer;

        }

        public static IObservable<T> When<T>(this IObservable<T> source, Predicate<T> predicate, Action<T> action)
        {
            return source.Do(x =>
            {
                if (predicate(x))
                {
                    action(x);
                }
            });
        }

        public static IObservable<T> When<T>(this IObservable<T> source, Predicate<T> predicate, Action<T> actionIfTrue, Action<T> actionIfFalse)
        {
            return source.Do(x =>
            {
                if (predicate(x))
                {
                    actionIfTrue(x);
                }
                else
                {
                    actionIfFalse(x);
                }
            });
        }

        public static IObservable<bool> WhenTrue(this IObservable<bool> source, Action<bool> action)
        {
            return source.When(x => x, action);
        }

        public static IObservable<bool> WhenFalse(this IObservable<bool> source, Action<bool> action)
        {
            return source.When(x => !x, action);
        }
    }
}
