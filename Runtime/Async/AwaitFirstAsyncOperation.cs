using System;
using Cysharp.Threading.Tasks;
using UniModules.UniCore.Runtime.DataFlow;
using UniModules.UniCore.Runtime.Extension;
using UniGame.Runtime.ObjectPool.Extensions;
using UniGame.Core.Runtime.ObjectPool;
using UniGame.Core.Runtime;
using UniRx;

namespace UniModules.UniGame.CoreModules.UniGame.Core.Runtime.Async
{
    public class AwaitFirstAsyncOperation<TData> : IPoolable, IDisposable
    {
        private LifeTimeDefinition _lifeTime = new LifeTimeDefinition();
        private bool _valueInitialized = false;
        private TData _value;
    
        public async UniTask<TData> AwaitFirstAsync(
            IObservable<TData> observable, 
            ILifeTime observableLIfeTime,
            Func<TData,bool> predicate = null)
        {
            _valueInitialized = false;
            
            if (observable == null) 
                return default;

            observable.Subscribe(x => OnNext(x,predicate)).AddTo(observableLIfeTime);
            await this.WaitUntil(() => _lifeTime.IsTerminated || _valueInitialized)
                .AttachExternalCancellation(_lifeTime.Token);
            return _value;
        }

        public void Dispose() => this.DespawnWithRelease();
        
        public void Release()
        {
            _lifeTime.Release();
            _valueInitialized = true;
            _value = default;
        }
    
        private void OnNext(TData value,Func<TData,bool> predicate = null)
        {
            if (_valueInitialized || (predicate != null && !predicate.Invoke(value)))
                return;
        
            _valueInitialized = true;
            _value = value;
        }

    }
}
