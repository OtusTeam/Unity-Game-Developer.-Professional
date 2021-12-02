namespace GameElements.Unity
{
    public abstract class UnityGameElementSet : UnityGameElement, IGameElementSet
    {
        private readonly GameElementSet set;

        public UnityGameElementSet()
        {
            this.set = new GameElementSet();
        }

        public bool AddElement(object element)
        {
            return this.set.AddElement(element);
        }

        public bool RemoveElement(object element)
        {
            return this.set.RemoveElement(element);
        }

        public bool ContainsElement(object element)
        {
            return this.set.ContainsElement(element);
        }
        
        protected override void OnSetup()
        {
            base.OnSetup();
            IGameElement gameElement = this.set;
            gameElement.Setup(this.GameSystem);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            IGameElement gameElement = this.set;
            gameElement.Dispose();
        }
    }
}