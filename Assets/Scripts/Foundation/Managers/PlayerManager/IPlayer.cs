namespace Foundation
{
    public interface IPlayer
    {
        int Index { get; }
        ICharacterHealth Health { get; }
    }
}
