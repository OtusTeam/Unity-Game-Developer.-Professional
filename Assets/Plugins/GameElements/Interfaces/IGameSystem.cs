namespace GameElements
{
    /// <summary>
    ///     <para>A game context contract.</para>
    /// </summary>
    public interface IGameSystem : IGameObservable, IGameStateable, IGameElementLayer
    {
    }
}