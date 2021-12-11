namespace Prototype.GameEngine
{
    public sealed class CharacterHitPointsUpgrade : ICharacterUpgrade
    {
        private MoneyStorageComponent moneyStorageComponent;
        
        
        
        public CharacterHitPointsUpgrade(IEntity entity)
        {
        }

        public int Price { get; }

        public bool CanUpgrade()
        {
            return this.moneyStorageComponent. 
        }

        public void Upgrade()
        {
        }
    }
}