namespace Prototype
{
    public interface ICharacterManager
    {
        ICharacter GetCharacter(int id);

        bool CanUpgradeHitPoints(ICharacter character);

        void UpgradeHitPoints(ICharacter character);

        bool CanUpgradeDamage(ICharacter character);

        void UpgradeDamage(ICharacter character);

        bool CanUpgradeSpeed(ICharacter character);
        
        void UpgradeSpeed(ICharacter character);
    }
}