namespace Otus
{
    public sealed class PlayerProperties
    {
        private IPropertyProvider provider;
        
        public PlayerProperties(IPropertyProvider provider)
        {
            this.provider = provider;
        }
    }
}