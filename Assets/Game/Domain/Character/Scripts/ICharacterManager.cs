namespace Prototype
{
    public interface ICharacterManager
    {
        ICharacter GetCharacter(int characterId);

        ICharacterUpgrade GetHitPointsUpgrade(int characterId);

        ICharacterUpgrade GetDamageUpgrade(int characterId);
    }
}