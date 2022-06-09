namespace UniModules.UniGame.Core.Runtime.Interfaces
{
    using UniRx;

    public interface IReactiveValueStatus
    {
        IReadOnlyReactiveProperty<bool> HasValueSource { get; }

    }
}