﻿namespace UniModules.UniCore.Runtime.Rx.Extensions
{
    using System;
    using ObjectPool.Runtime;
    using UniGame.Core.Runtime.Common;
    using UniModules.UniGame.Core.Runtime.DataFlow.Interfaces;

    public static class RxLifetimeExtension 
    {

        public static T AddTo<T>(this T disposable, ILifeTime lifeTime)
            where T : IDisposable
        {
            if (disposable != null)
                lifeTime.AddDispose(disposable);
            return disposable;
        }
        
        public static IDisposableLifetime AddTo(this ILifeTime lifeTime, Action cleanupAction)
        {
            var disposableAction = ClassPool.Spawn<DisposableLifetime>();
            disposableAction.AddCleanUpAction(cleanupAction);
            lifeTime.AddDispose(disposableAction);
            return disposableAction;
        }
        
    }
}
