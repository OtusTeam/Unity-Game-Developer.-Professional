namespace Prototype
{
    public interface ICharacterUpgrade
    {
        int Price { get; }

        bool CanUpgrade();

        void Upgrade();
    }
}