﻿namespace UniGame.Core.Runtime
{
    public interface IAsyncState : IAsyncState<AsyncStatus>
    {
    }
    
    public interface IAsyncState<TValue,TResult> : 
        IAsyncCommand<TValue,TResult>, 
        IAsyncEndPoint,
        ILifeTimeContext,
        IActiveStatus
    {
    }

    public interface IAsyncState<T> : IAsyncState<T, AsyncStatus> {
        
    }
}