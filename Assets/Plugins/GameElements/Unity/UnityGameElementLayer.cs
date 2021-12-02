namespace GameElements.Unity
{
     /// <inheritdoc cref="IGameElementLayer"/>
    public abstract class UnityGameElementLayer : UnityGameElement, IGameElementLayer
    {
        private readonly GameElementLayer layer;

        public UnityGameElementLayer()
        {
            this.layer = new GameElementLayer();
        }

        public bool AddElement(object element)
        {
            return this.layer.AddElement(element);
        }

        public bool RemoveElement(object element)
        {
            return this.layer.RemoveElement(element);
        }

        public T GetElement<T>()
        {
            return this.layer.GetElement<T>();
        }

        public bool TryGetElement<T>(out T element)
        {
            return this.layer.TryGetElement(out element);
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            IGameElement gameElement = this.layer;
            gameElement.Setup(this.GameSystem);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            IGameElement gameElement = this.layer;
            gameElement.Dispose();
        }
    }
}