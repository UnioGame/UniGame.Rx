﻿namespace UniModules.UniGame.Core.Runtime.Interfaces.Rx
{
    using System;
    using UniRx;

    public interface IObservableUpdateNotification
    {

        IObservable<Unit> Update { get; }


    }
}
