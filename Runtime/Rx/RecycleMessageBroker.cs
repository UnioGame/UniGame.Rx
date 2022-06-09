﻿namespace UniModules.UniCore.Runtime.Rx
{
	using System;
	using System.Collections.Generic;
	using UniGame.Core.Runtime.Interfaces.Rx;
	using UniRx;

	public class RecycleMessageBrocker : IRecycleMessageBrocker
	{
		readonly Dictionary<Type, object> notifiers = new Dictionary<Type, object>();

		public void Publish<T>(T message)
		{
		    if (!notifiers.TryGetValue(typeof(T), out var notifier))
			{
				return;
			}
			((ISubject<T>)notifier).OnNext(message);
		}

		public IObservable<T> Receive<T>()
		{
		    if (!notifiers.TryGetValue(typeof(T), out var notifier))
			{
				var n = new Subject<T>().Synchronize();
				notifier = n;
				notifiers.Add(typeof(T), notifier);
			}

			return ((IObservable<T>)notifier).AsObservable();
		}

		public void Release()
		{
			
			notifiers.Clear();

        }
	}
}