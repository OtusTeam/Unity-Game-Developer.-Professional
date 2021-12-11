namespace Prototype.ViewModel
{
    public interface ICharactersManager
    {
        ICharacter GetCharacter(int characterId);

        ICharacterUpgrade GetHitPointsUpgrade(int characterId);

        ICharacterUpgrade GetDamageUpgrade(int characterId);
    }
}