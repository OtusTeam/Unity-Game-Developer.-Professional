namespace Prototype
{
    public interface ICastleManager
    {
        ICastle GetCastle(int id);

        void UpgradeLevel(ICastle castle);
    }
}